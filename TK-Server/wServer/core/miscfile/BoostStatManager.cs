using System;
using System.Linq;
using wServer.core.objects;
using wServer.utils;

namespace wServer.core
{
    internal class BoostStatManager
    {
        public ActivateBoost[] ActivateBoost;
        private int[] _boost;
        private SV<int>[] _boostSV;
        private StatsManager _parent;
        private Player _player;

        public BoostStatManager(StatsManager parent)
        {
            _parent = parent;
            _player = parent.Owner;
            _boost = new int[StatsManager.NumStatTypes];
            _boostSV = new SV<int>[_boost.Length];

            for (var i = 0; i < _boostSV.Length; i++)
                _boostSV[i] = new SV<int>(_player, StatsManager.GetBoostStatType(i), _boost[i], i != 0 && i != 1);

            ActivateBoost = new ActivateBoost[_boost.Length];

            for (var i = 0; i < ActivateBoost.Length; i++)
                ActivateBoost[i] = new ActivateBoost();

            ReCalculateValues();
        }

        public int this[int index] => _boost[index];

        public void CheckItems()
        {
            if (_player == null || _player.Client == null || _player.Client.Account == null)
                return;

            for (var i = 0; i < 20; i++)
            {
                if (_player.Inventory[i] == null)
                    continue;
                if (_player.Inventory[i].ObjectId == "Cerberus's Left Claw" && _player.Inventory[i] != null)
                {
                    try
                    {
                        var surrounding = _player.Owner.EnemiesCollision.HitTest(_player.X, _player.Y, 10).Count();
                        var maxBoost = 20;
                        var countBoost = 0;
                        for (int j = 0; j < surrounding; j++)
                            if (countBoost <= maxBoost)
                            {
                                IncrementBoost(StatDataType.Attack, 1);
                                countBoost++;
                            }
                    }
                    catch (NullReferenceException e)
                    {
                        SLogger.Instance.Info(e);
                        continue;
                    }
                }
                if (_player.Inventory[i].ObjectId == "Cerberus's Right Claw" && _player.Inventory[i] != null)
                {
                    var acc = _player.CoreServerManager.Database.GetAccount(_player.AccountId);
                    var enemiesKilled = acc.EnemiesKilled;
                    var countHPBoost = 0;
                    for (int k = 0; k < enemiesKilled; k++)
                    {
                        if (k % 1000 == 0 && countHPBoost<=10) // every 1500 of the enemies
                        {
                            IncrementBoost(StatDataType.MaximumHP, 50);
                            countHPBoost++;
                        }
                    }
                    
                }
                foreach (var b in _player.Inventory[i].StatsBoostOnHandle)
                    IncrementBoost((StatDataType)b.Key, b.Value);
                
            }
        }

        public void CheckItemsNoStack() //TODO
        {
            if (_player == null || _player.Client == null || _player.Client.Account == null)
                return;

            for (var i = 0; i < 20; i++)
            {
                if (_player.Inventory[i] == null)
                    continue;

                if (_player.Inventory[i].SetStatsNoStack)
                {
                    IncrementBoost((StatDataType)_player.Inventory[i].StatSetNoStack, _player.Inventory[i].AmountSetNoStack);
                    break;
                }
            }
        }

        protected internal void ReCalculateValues()
        {
            for (var i = 0; i < _boost.Length; i++)
                _boost[i] = 0;

            ApplyEquipBonus();
            ApplyActivateBonus();
            //CheckItems();
            CheckItemsNoStack();
            IncrementStatBoost();
            IncrementSkillBoosts();

            for (var i = 0; i < _boost.Length; i++)
                _boostSV[i].SetValue(_boost[i]);
        }

        private void ApplyActivateBonus()
        {
            for (var i = 0; i < ActivateBoost.Length; i++)
            {
                // set boost
                var b = ActivateBoost[i].GetBoost();
                _boost[i] += b;
            }
        }

        private void ApplyEquipBonus()
        {
            for (var i = 0; i < 4; i++)
            {
                if (_player.Inventory[i] == null)
                    continue;

                foreach (var b in _player.Inventory[i].StatsBoost)
                    IncrementBoost((StatDataType)b.Key, b.Value);
            }
        }

        private void FixedStat(StatDataType stat, int value)
        {
            var i = StatsManager.GetStatIndex(stat);
            _boost[i] = value - _parent.Base[i];
        }

        private int IncreasePercentage(int percentageToIncrease, int stat)
        {
            int percentage = percentageToIncrease;
            var result = percentage * _parent.Base[stat] / 100;
            return result;
        }

        public void IncrementBoost(StatDataType stat, int amount)
        {
            var i = StatsManager.GetStatIndex(stat);

            if (_parent.Base[i] + amount < 1)
                amount = (i == 0) ? -_parent.Base[i] + 1 : -_parent.Base[i];

            _boost[i] += amount;
        }

        private void IncrementSkillBoosts()
        {
            if (_player == null || _player.Client == null || _player.Client.Account == null)
                return;

            var life = 0;
            var mana = 1;
            var att = 2;
            var def = 3;
            var spd = 4;
            var dex = 5;
            var vit = 6;
            var wis = 7;

            /* <StatToBoost> += <Percentage> * <StatToBoost> / 100;*/
            _boost[life] += IncreasePercentage(_player.SmallSkill1 * 2, life);
            _boost[mana] += IncreasePercentage(_player.SmallSkill2 * 2, mana);
            _boost[att] += IncreasePercentage(_player.SmallSkill3 * 2, att);
            _boost[def] += IncreasePercentage(_player.SmallSkill4 * 2, def);
            _boost[spd] += IncreasePercentage(_player.SmallSkill5 * 2, spd);
            _boost[dex] += IncreasePercentage(_player.SmallSkill6 * 2, dex);
            _boost[vit] += IncreasePercentage(_player.SmallSkill7 * 2, vit);
            _boost[wis] += IncreasePercentage(_player.SmallSkill8 * 2, wis);

            if (_player.BigSkill1) //Big Life
            {
                _boost[life] += IncreasePercentage(40, life);
                _boost[vit] += IncreasePercentage(-15, vit);
                _boost[wis] += IncreasePercentage(-30, wis);
            }

            if (_player.BigSkill2) //Big Mana
            {
                _boost[mana] += IncreasePercentage(50, mana);
                _boost[wis] += IncreasePercentage(-15, wis);
            }

            if (_player.BigSkill3) //Big Attack
            {
                _boost[att] += IncreasePercentage(15, att) + 5;
                _boost[spd] += IncreasePercentage(-10, spd);
                _boost[vit] += IncreasePercentage(-10, vit);
                _boost[life] += IncreasePercentage(-10, life);
            }

            if (_player.BigSkill4) //Big Defense
            {
                _boost[def] += IncreasePercentage(40, def);
                _boost[spd] += IncreasePercentage(-20, spd);
            }

            if (_player.BigSkill5) //Big Speed
            {
                _boost[mana] += IncreasePercentage(-10, mana);
                _boost[def] += IncreasePercentage(-5, def);
                _boost[spd] += IncreasePercentage(25, spd);
                _boost[vit] += 10;
            }

            if (_player.BigSkill6) //Big Dexterity
            {
                _boost[dex] += IncreasePercentage(15, dex) + 5;
                _boost[def] += IncreasePercentage(-15, def);
                _boost[wis] += IncreasePercentage(-10, wis);
                _boost[mana] += IncreasePercentage(-10, mana);
            }

            if (_player.BigSkill7) //Big Vitality
            {
                _boost[vit] += IncreasePercentage(20, vit);
                _boost[life] += IncreasePercentage(-5, life);
            }

            if (_player.BigSkill8) //Big Wisdom
            {
                _boost[wis] += IncreasePercentage(20, wis);
                _boost[mana] += IncreasePercentage(-10, mana);
            }

            if (_player.BigSkill9) //HP Regen
            {
                _boost[life] += IncreasePercentage(-5, life);
                _boost[vit] += IncreasePercentage(5, vit);
            }

            if (_player.BigSkill10) //MP Regen
            {
                _boost[mana] += IncreasePercentage(-10, mana);
                _boost[wis] += IncreasePercentage(10, wis);
            }
        }

        private void IncrementStatBoost()
        {
            if (_player == null || _player.Client == null || _player.Client.Account == null)
                return;

            for (var a = 0; a < 8; a++)
            {
                if (a >= 7)
                    a = 7;

                if (a >= 8)
                    return;
                if (_player.Client.Account.SetBaseStat > 0)
                    _boost[a] += a < 2 ? _player.Client.Account.SetBaseStat * 5 : _player.Client.Account.SetBaseStat;
            }
        }
    }
}
