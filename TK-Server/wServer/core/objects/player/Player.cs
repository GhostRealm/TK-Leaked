using common;
using common.database;
using common.resources;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using wServer.core.terrain;
using wServer.core.worlds;
using wServer.core.worlds.logic;
using wServer.logic;
using wServer.networking;
using wServer.networking.packets;
using wServer.networking.packets.outgoing;
using wServer.utils;

namespace wServer.core.objects
{
    public partial class Player : Character, IContainer, IPlayer
    {
        public const int DONOR_1 = 10;
        public const int DONOR_2 = 20;
        public const int DONOR_3 = 30;
        public const int DONOR_4 = 40;
        public const int DONOR_5 = 50;
        public const int VIP = 60;

        public Client Client;
        public bool IsAlive = true;
        public StatsManager Stats;

        private SV<int> _accountId;
        private SV<int> _admin;
        private SV<int> _baseStat;
        private SV<bool> _bigSkill1;
        private SV<bool> _bigSkill10;
        private SV<bool> _bigSkill11;
        private SV<bool> _bigSkill12;
        private SV<bool> _bigSkill2;
        private SV<bool> _bigSkill3;
        private SV<bool> _bigSkill4;
        private SV<bool> _bigSkill5;
        private SV<bool> _bigSkill6;
        private SV<bool> _bigSkill7;
        private SV<bool> _bigSkill8;
        private SV<bool> _bigSkill9;
        private double _breath;
        private int _canApplyEffect0;
        private int _canApplyEffect1;
        private int _canApplyEffect2;
        private int _canApplyEffect3;
        private SV<int> _colorchat;
        private SV<int> _colornamechat;
        private SV<int> _credits;
        private SV<int> _currentFame;
        private bool _dead;
        private SV<int> _experience;
        private SV<int> _experienceGoal;
        private SV<int> _fame;
        private SV<int> _fameGoal;
        private SV<int> _glow;
        private SV<string> _guild;
        private SV<int> _guildRank;
        private SV<bool> _hasBackpack;
        private float _hpRegenCounter;
        private SV<int> _level;
        private SV<bool> _maxedAtt;
        private SV<bool> _maxedDef;
        private SV<bool> _maxedDex;
        private SV<bool> _maxedLife;
        private SV<bool> _maxedMana;
        private SV<bool> _maxedSpd;
        private SV<bool> _maxedVit;
        private SV<bool> _maxedWis;
        private SV<int> _mp;
        private float _mpRegenCounter;
        private SV<bool> _nameChosen;
        private int _originalSkin;
        private SV<int> _oxygenBar;
        private SV<int> _partyId;
        private SV<int> _points;
        private SV<int> _rank;
        private SV<int> _skin;
        private SV<int> _smallSkill1;
        private SV<int> _smallSkill10;
        private SV<int> _smallSkill11;
        private SV<int> _smallSkill12;
        private SV<int> _smallSkill2;
        private SV<int> _smallSkill3;
        private SV<int> _smallSkill4;
        private SV<int> _smallSkill5;
        private SV<int> _smallSkill6;
        private SV<int> _smallSkill7;
        private SV<int> _smallSkill8;
        private SV<int> _smallSkill9;
        private SV<int> _stars;
        private SV<int> _texture1;
        private SV<int> _texture2;
        private SV<bool> _upgradeEnabled;
        private SV<bool> _xpBoosted;

        private SV<int> _SPSLifeCount;
        private SV<int> _SPSManaCount;
        private SV<int> _SPSDefenseCount;
        private SV<int> _SPSAttackCount;
        private SV<int> _SPSDexterityCount;
        private SV<int> _SPSSpeedCount;
        private SV<int> _SPSVitalityCount;
        private SV<int> _SPSWisdomCount;

        private SV<int> _SPSLifeCountMax;
        private SV<int> _SPSManaCountMax;
        private SV<int> _SPSDefenseCountMax;
        private SV<int> _SPSAttackCountMax;
        private SV<int> _SPSDexterityCountMax;
        private SV<int> _SPSSpeedCountMax;
        private SV<int> _SPSVitalityCountMax;
        private SV<int> _SPSWisdomCountMax;

        public int SPSLifeCount { get => _SPSLifeCount.GetValue(); set => _SPSLifeCount.SetValue(value); }
        public int SPSManaCount { get => _SPSManaCount.GetValue(); set => _SPSManaCount.SetValue(value); }
        public int SPSDefenseCount { get => _SPSDefenseCount.GetValue(); set => _SPSDefenseCount.SetValue(value); }
        public int SPSAttackCount { get => _SPSAttackCount.GetValue(); set => _SPSAttackCount.SetValue(value); }
        public int SPSDexterityCount { get => _SPSDexterityCount.GetValue(); set => _SPSDexterityCount.SetValue(value); }
        public int SPSSpeedCount { get => _SPSSpeedCount.GetValue(); set => _SPSSpeedCount.SetValue(value); }
        public int SPSVitalityCount { get => _SPSVitalityCount.GetValue(); set => _SPSVitalityCount.SetValue(value); }
        public int SPSWisdomCount { get => _SPSWisdomCount.GetValue(); set => _SPSWisdomCount.SetValue(value); }
        public int SPSLifeCountMax { get => _SPSLifeCountMax.GetValue(); set => _SPSLifeCountMax.SetValue(value); }
        public int SPSManaCountMax { get => _SPSManaCountMax.GetValue(); set => _SPSManaCountMax.SetValue(value); }
        public int SPSDefenseCountMax { get => _SPSDefenseCountMax.GetValue(); set => _SPSDefenseCountMax.SetValue(value); }
        public int SPSAttackCountMax { get => _SPSAttackCountMax.GetValue(); set => _SPSAttackCountMax.SetValue(value); }
        public int SPSDexterityCountMax { get => _SPSDexterityCountMax.GetValue(); set => _SPSDexterityCountMax.SetValue(value); }
        public int SPSSpeedCountMax { get => _SPSSpeedCountMax.GetValue(); set => _SPSSpeedCountMax.SetValue(value); }
        public int SPSVitalityCountMax { get => _SPSVitalityCountMax.GetValue(); set => _SPSVitalityCountMax.SetValue(value); }
        public int SPSWisdomCountMax { get => _SPSWisdomCountMax.GetValue(); set => _SPSWisdomCountMax.SetValue(value); }


        private const float MinMoveSpeed = 0.004f;
        private const float MaxMoveSpeed = 0.0096f;

        public Player(Client client, bool saveInventory = true) : base(client.CoreServerManager, client.Character.ObjectType)
        {
            var settings = CoreServerManager.Resources.Settings;
            var gameData = CoreServerManager.Resources.GameData;

            Client = client;

            _accountId = new SV<int>(this, StatDataType.AccountId, client.Account.AccountId, true);
            _experience = new SV<int>(this, StatDataType.Experience, client.Character.Experience, true);
            _experienceGoal = new SV<int>(this, StatDataType.ExperienceGoal, 0, true);
            _level = new SV<int>(this, StatDataType.Level, client.Character.Level);
            _currentFame = new SV<int>(this, StatDataType.CurrentFame, client.Account.Fame, true);
            _fame = new SV<int>(this, StatDataType.Fame, client.Character.Fame, true);
            _fameGoal = new SV<int>(this, StatDataType.FameGoal, 0, true);
            _stars = new SV<int>(this, StatDataType.Stars, 0);
            _guild = new SV<string>(this, StatDataType.Guild, "");
            _guildRank = new SV<int>(this, StatDataType.GuildRank, -1);
            _rank = new SV<int>(this, StatDataType.Rank, client.Account.Rank);
            _credits = new SV<int>(this, StatDataType.Credits, client.Account.Credits, true);
            _nameChosen = new SV<bool>(this, StatDataType.NameChosen, client.Account.NameChosen, false, v => Client.Account?.NameChosen ?? v);
            _texture1 = new SV<int>(this, StatDataType.Texture1, client.Character.Tex1);
            _texture2 = new SV<int>(this, StatDataType.Texture2, client.Character.Tex2);
            _skin = new SV<int>(this, StatDataType.Skin, 0);
            _glow = new SV<int>(this, StatDataType.Glow, 0);
            _admin = new SV<int>(this, StatDataType.Admin, client.Account.Admin ? 1 : 0);
            _xpBoosted = new SV<bool>(this, StatDataType.XPBoost, client.Character.XPBoostTime != 0, true);
            _mp = new SV<int>(this, StatDataType.MP, client.Character.MP);
            _hasBackpack = new SV<bool>(this, StatDataType.HasBackpack, client.Character.HasBackpack, true);
            _oxygenBar = new SV<int>(this, StatDataType.OxygenBar, -1, true);
            _baseStat = new SV<int>(this, StatDataType.BaseStat, client.Account.SetBaseStat, true);
            _points = new SV<int>(this, StatDataType.Points, client.Character.Points, true);
            _maxedLife = new SV<bool>(this, StatDataType.MaxedLife, client.Character.MaxedLife, true);
            _maxedMana = new SV<bool>(this, StatDataType.MaxedMana, client.Character.MaxedMana, true);
            _maxedAtt = new SV<bool>(this, StatDataType.MaxedAtt, client.Character.MaxedAtt, true);
            _maxedDef = new SV<bool>(this, StatDataType.MaxedDef, client.Character.MaxedDef, true);
            _maxedSpd = new SV<bool>(this, StatDataType.MaxedSpd, client.Character.MaxedSpd, true);
            _maxedDex = new SV<bool>(this, StatDataType.MaxedDex, client.Character.MaxedDex, true);
            _maxedVit = new SV<bool>(this, StatDataType.MaxedVit, client.Character.MaxedVit, true);
            _maxedWis = new SV<bool>(this, StatDataType.MaxedWis, client.Character.MaxedWis, true);
            _smallSkill1 = new SV<int>(this, StatDataType.SmallSkill1, client.Character.SmallSkill1, true);
            _smallSkill2 = new SV<int>(this, StatDataType.SmallSkill2, client.Character.SmallSkill2, true);
            _smallSkill3 = new SV<int>(this, StatDataType.SmallSkill3, client.Character.SmallSkill3, true);
            _smallSkill4 = new SV<int>(this, StatDataType.SmallSkill4, client.Character.SmallSkill4, true);
            _smallSkill5 = new SV<int>(this, StatDataType.SmallSkill5, client.Character.SmallSkill5, true);
            _smallSkill6 = new SV<int>(this, StatDataType.SmallSkill6, client.Character.SmallSkill6, true);
            _smallSkill7 = new SV<int>(this, StatDataType.SmallSkill7, client.Character.SmallSkill7, true);
            _smallSkill8 = new SV<int>(this, StatDataType.SmallSkill8, client.Character.SmallSkill8, true);
            _smallSkill9 = new SV<int>(this, StatDataType.SmallSkill9, client.Character.SmallSkill9, true);
            _smallSkill10 = new SV<int>(this, StatDataType.SmallSkill10, client.Character.SmallSkill10, true);
            _smallSkill11 = new SV<int>(this, StatDataType.SmallSkill11, client.Character.SmallSkill11, true);
            _smallSkill12 = new SV<int>(this, StatDataType.SmallSkill12, client.Character.SmallSkill12, true);
            _bigSkill1 = new SV<bool>(this, StatDataType.BigSkill1, client.Character.BigSkill1, true);
            _bigSkill2 = new SV<bool>(this, StatDataType.BigSkill2, client.Character.BigSkill2, true);
            _bigSkill3 = new SV<bool>(this, StatDataType.BigSkill3, client.Character.BigSkill3, true);
            _bigSkill4 = new SV<bool>(this, StatDataType.BigSkill4, client.Character.BigSkill4, true);
            _bigSkill5 = new SV<bool>(this, StatDataType.BigSkill5, client.Character.BigSkill5, true);
            _bigSkill6 = new SV<bool>(this, StatDataType.BigSkill6, client.Character.BigSkill6, true);
            _bigSkill7 = new SV<bool>(this, StatDataType.BigSkill7, client.Character.BigSkill7, true);
            _bigSkill8 = new SV<bool>(this, StatDataType.BigSkill8, client.Character.BigSkill8, true);
            _bigSkill9 = new SV<bool>(this, StatDataType.BigSkill9, client.Character.BigSkill9, true);
            _bigSkill10 = new SV<bool>(this, StatDataType.BigSkill10, client.Character.BigSkill10, true);
            _bigSkill11 = new SV<bool>(this, StatDataType.BigSkill11, client.Character.BigSkill11, true);
            _bigSkill12 = new SV<bool>(this, StatDataType.BigSkill12, client.Character.BigSkill12, true);
            _colornamechat = new SV<int>(this, StatDataType.ColorNameChat, 0);
            _colorchat = new SV<int>(this, StatDataType.ColorChat, 0);
            _upgradeEnabled = new SV<bool>(this, StatDataType.UpgradeEnabled, client.Character.UpgradeEnabled, true);
            _partyId = new SV<int>(this, StatDataType.PartyId, client.Account.PartyId, true);

            var maxPotionAmount = 50 + Client.Account.Rank * 2;

            _SPSLifeCount = new SV<int>(this, StatDataType.SPS_LIFE_COUNT, client.Account.SPSLifeCount, true);
            _SPSLifeCountMax = new SV<int>(this, StatDataType.SPS_LIFE_COUNT_MAX, maxPotionAmount, true);
            _SPSManaCount = new SV<int>(this, StatDataType.SPS_MANA_COUNT, client.Account.SPSManaCount, true);
            _SPSManaCountMax = new SV<int>(this, StatDataType.SPS_MANA_COUNT_MAX, maxPotionAmount, true);
            _SPSDefenseCount = new SV<int>(this, StatDataType.SPS_DEFENSE_COUNT, client.Account.SPSDefenseCount, true);
            _SPSDefenseCountMax = new SV<int>(this, StatDataType.SPS_DEFENSE_COUNT_MAX, maxPotionAmount, true);
            _SPSAttackCount = new SV<int>(this, StatDataType.SPS_ATTACK_COUNT, client.Account.SPSAttackCount, true);
            _SPSAttackCountMax = new SV<int>(this, StatDataType.SPS_ATTACK_COUNT_MAX, maxPotionAmount, true);
            _SPSDexterityCount = new SV<int>(this, StatDataType.SPS_DEXTERITY_COUNT, client.Account.SPSDexterityCount, true);
            _SPSDexterityCountMax = new SV<int>(this, StatDataType.SPS_DEXTERITY_COUNT_MAX, maxPotionAmount, true);
            _SPSSpeedCount = new SV<int>(this, StatDataType.SPS_SPEED_COUNT, client.Account.SPSSpeedCount, true);
            _SPSSpeedCountMax = new SV<int>(this, StatDataType.SPS_SPEED_COUNT_MAX, maxPotionAmount, true);
            _SPSVitalityCount = new SV<int>(this, StatDataType.SPS_VITALITY_COUNT, client.Account.SPSVitalityCount, true);
            _SPSVitalityCountMax = new SV<int>(this, StatDataType.SPS_VITALITY_COUNT_MAX, maxPotionAmount, true);
            _SPSWisdomCount = new SV<int>(this, StatDataType.SPS_WISDOM_COUNT, client.Account.SPSWisdomCount, true);
            _SPSWisdomCountMax = new SV<int>(this, StatDataType.SPS_WISDOM_COUNT_MAX, maxPotionAmount, true);

            PendingPackets = new ConcurrentQueue<Tuple<Client, int, PacketId, byte[]>>();
            PendingActions = new ConcurrentQueue<Action<TickData>>();

            Name = client.Account.Name;
            HP = client.Character.HP;
            ConditionEffects = 0;

            XPBoostTime = client.Character.XPBoostTime;
            LDBoostTime = client.Character.LDBoostTime;

            var s = (ushort)client.Character.Skin;
            if (gameData.Skins.Keys.Contains(s))
            {
                SetDefaultSkin(s);
                SetDefaultSize(gameData.Skins[s].Size);
            }

            var guild = CoreServerManager.Database.GetGuild(client.Account.GuildId);
            if (guild?.Name != null)
            {
                Guild = guild.Name;
                GuildRank = client.Account.GuildRank;
            }

            if (Client.Account.Size > 0)
                Size = Client.Account.Size;

            PetId = client.Character.PetId;

            HealthPots = new ItemStacker(this, 254, 0x0A22, count: client.Character.HealthStackCount, settings.MaxStackablePotions);
            MagicPots = new ItemStacker(this, 255, 0x0A23, count: client.Character.MagicStackCount, settings.MaxStackablePotions);
            Stacks = new ItemStacker[] { HealthPots, MagicPots };

            if (client.Character.Datas == null)
                client.Character.Datas = new ItemData[20];

            Inventory = new Inventory(this, Utils.ResizeArray(Client.Character.Items.Select(_ => (_ == 0xffff || !gameData.Items.ContainsKey(_)) ? null : gameData.Items[_]).ToArray(), 20), Client.Character.Datas);

            Inventory.InventoryChanged += (sender, e) => Stats.ReCalculateValues();
            SlotTypes = Utils.ResizeArray(gameData.Classes[ObjectType].SlotTypes, 20);
            Stats = new StatsManager(this);

            CoreServerManager.Database.IsMuted(client.IpAddress).ContinueWith(t =>
            {
                Muted = !Client.Account.Admin && t.IsCompleted && t.Result;
            });

            CoreServerManager.Database.IsLegend(AccountId).ContinueWith(t =>
            {
                Glow = t.Result && client.Account.GlowColor == 0 ? 0xFF0000 : client.Account.GlowColor;
            });
        }

        public int AccountId { get => _accountId.GetValue(); set => _accountId.SetValue(value); }
        public int Admin { get => _admin.GetValue(); set => _admin.SetValue(value); }
        public int BaseStat { get => _baseStat.GetValue(); set => _baseStat.SetValue(value); }
        public bool BigSkill1 { get => _bigSkill1.GetValue(); set => _bigSkill1.SetValue(value); }
        public bool BigSkill10 { get => _bigSkill10.GetValue(); set => _bigSkill10.SetValue(value); }
        public bool BigSkill11 { get => _bigSkill11.GetValue(); set => _bigSkill11.SetValue(value); }
        public bool BigSkill12 { get => _bigSkill12.GetValue(); set => _bigSkill12.SetValue(value); }
        public bool BigSkill2 { get => _bigSkill2.GetValue(); set => _bigSkill2.SetValue(value); }
        public bool BigSkill3 { get => _bigSkill3.GetValue(); set => _bigSkill3.SetValue(value); }
        public bool BigSkill4 { get => _bigSkill4.GetValue(); set => _bigSkill4.SetValue(value); }
        public bool BigSkill5 { get => _bigSkill5.GetValue(); set => _bigSkill5.SetValue(value); }
        public bool BigSkill6 { get => _bigSkill6.GetValue(); set => _bigSkill6.SetValue(value); }
        public bool BigSkill7 { get => _bigSkill7.GetValue(); set => _bigSkill7.SetValue(value); }
        public bool BigSkill8 { get => _bigSkill8.GetValue(); set => _bigSkill8.SetValue(value); }
        public bool BigSkill9 { get => _bigSkill9.GetValue(); set => _bigSkill9.SetValue(value); }

        public double Breath
        {
            get => _breath;
            set
            {
                OxygenBar = (int)value;
                _breath = value;
            }
        }

        public int ColorChat { get => _colorchat.GetValue(); set => _colorchat.SetValue(value); }
        public int ColorNameChat { get => _colornamechat.GetValue(); set => _colornamechat.SetValue(value); }
        public int Credits { get => _credits.GetValue(); set => _credits.SetValue(value); }
        public int CurrentFame { get => _currentFame.GetValue(); set => _currentFame.SetValue(value); }
        public RInventory DbLink { get; private set; }
        public int Experience { get => _experience.GetValue(); set => _experience.SetValue(value); }
        public int ExperienceGoal { get => _experienceGoal.GetValue(); set => _experienceGoal.SetValue(value); }
        public int Fame { get => _fame.GetValue(); set => _fame.SetValue(value); }
        public FameCounter FameCounter { get; private set; }
        public int FameGoal { get => _fameGoal.GetValue(); set => _fameGoal.SetValue(value); }
        public int Glow { get => _glow.GetValue(); set => _glow.SetValue(value); }
        public string Guild { get => _guild.GetValue(); set => _guild?.SetValue(value); }
        public int? GuildInvite { get; set; }
        public int GuildRank { get => _guildRank.GetValue(); set => _guildRank.SetValue(value); }
        public bool HasBackpack { get => _hasBackpack.GetValue(); set => _hasBackpack.SetValue(value); }
        public ItemStacker HealthPots { get; private set; }
        public Inventory Inventory { get; private set; }
        public bool IsInvulnerable => HasConditionEffect(ConditionEffects.Paused) || HasConditionEffect(ConditionEffects.Stasis) || HasConditionEffect(ConditionEffects.Invincible) || HasConditionEffect(ConditionEffects.Invulnerable);
        public int LDBoostTime { get; set; }
        public int Level { get => _level.GetValue(); set => _level.SetValue(value); }
        public ItemStacker MagicPots { get; private set; }
        public bool MaxedAtt { get => _maxedAtt.GetValue(); set => _maxedAtt.SetValue(value); }
        public bool MaxedDef { get => _maxedDef.GetValue(); set => _maxedDef.SetValue(value); }
        public bool MaxedDex { get => _maxedDex.GetValue(); set => _maxedDex.SetValue(value); }
        public bool MaxedLife { get => _maxedLife.GetValue(); set => _maxedLife.SetValue(value); }
        public bool MaxedMana { get => _maxedMana.GetValue(); set => _maxedMana.SetValue(value); }
        public bool MaxedSpd { get => _maxedSpd.GetValue(); set => _maxedSpd.SetValue(value); }
        public bool MaxedVit { get => _maxedVit.GetValue(); set => _maxedVit.SetValue(value); }
        public bool MaxedWis { get => _maxedWis.GetValue(); set => _maxedWis.SetValue(value); }
        public int MP { get => _mp.GetValue(); set => _mp.SetValue(value); }
        public bool Muted { get; set; }
        public bool NameChosen { get => _nameChosen.GetValue(); set => _nameChosen.SetValue(value); }
        public int OxygenBar { get => _oxygenBar.GetValue(); set => _oxygenBar.SetValue(value); }
        public ConcurrentQueue<Tuple<Client, int, PacketId, byte[]>> PendingPackets { get; private set; }
        public Pet Pet { get; set; }
        public int PetId { get; set; }
        public PlayerUpdate PlayerUpdate { get; private set; }
        public int Points { get => _points.GetValue(); set => _points.SetValue(value); }
        public Position Pos => new Position() { X = X, Y = Y };
        public int Rank { get => _rank.GetValue(); set => _rank.SetValue(value); }
        public int Skin { get => _skin.GetValue(); set => _skin.SetValue(value); }
        public int[] SlotTypes { get; private set; }
        public int SmallSkill1 { get => _smallSkill1.GetValue(); set => _smallSkill1.SetValue(value); }
        public int SmallSkill10 { get => _smallSkill10.GetValue(); set => _smallSkill10.SetValue(value); }
        public int SmallSkill11 { get => _smallSkill11.GetValue(); set => _smallSkill11.SetValue(value); }
        public int SmallSkill12 { get => _smallSkill12.GetValue(); set => _smallSkill12.SetValue(value); }
        public int SmallSkill2 { get => _smallSkill2.GetValue(); set => _smallSkill2.SetValue(value); }
        public int SmallSkill3 { get => _smallSkill3.GetValue(); set => _smallSkill3.SetValue(value); }
        public int SmallSkill4 { get => _smallSkill4.GetValue(); set => _smallSkill4.SetValue(value); }
        public int SmallSkill5 { get => _smallSkill5.GetValue(); set => _smallSkill5.SetValue(value); }
        public int SmallSkill6 { get => _smallSkill6.GetValue(); set => _smallSkill6.SetValue(value); }
        public int SmallSkill7 { get => _smallSkill7.GetValue(); set => _smallSkill7.SetValue(value); }
        public int SmallSkill8 { get => _smallSkill8.GetValue(); set => _smallSkill8.SetValue(value); }
        public int SmallSkill9 { get => _smallSkill9.GetValue(); set => _smallSkill9.SetValue(value); }
        public ItemStacker[] Stacks { get; private set; }
        public int Stars { get => _stars.GetValue(); set => _stars.SetValue(value); }
        public int Texture1 { get => _texture1.GetValue(); set => _texture1.SetValue(value); }
        public int Texture2 { get => _texture2.GetValue(); set => _texture2.SetValue(value); }
        public bool UpgradeEnabled { get => _upgradeEnabled.GetValue(); set => _upgradeEnabled.SetValue(value); }
        public bool XPBoosted { get => _xpBoosted.GetValue(); set => _xpBoosted.SetValue(value); }
        public int XPBoostTime { get; set; }
        private ConcurrentQueue<Action<TickData>> PendingActions { get; set; }

        public void AddPendingAction(Action<TickData> action) => PendingActions.Enqueue(action);

        public void CleanupPlayerUpdate()
        {
            PlayerUpdate.Dispose();
            PlayerUpdate = null;
        }

        public bool ApplyEffectCooldown(int slot)
        {
            if (slot == 0)
                if (_canApplyEffect0 > 0)
                    return false;
            if (slot == 1)
                if (_canApplyEffect1 > 0)
                    return false;
            if (slot == 2)
                if (_canApplyEffect2 > 0)
                    return false;
            if (slot == 3)
                if (_canApplyEffect3 > 0)
                    return false;
            return true;
        }

        public override bool CanBeSeenBy(Player player) => Client?.Account != null && Client.Account.Hidden ? false : true;

        public void checkMaxedStats()
        {
            var classes = CoreServerManager?.Resources?.GameData?.Classes;

            if (classes == null)
                return;

            if (!classes.ContainsKey(ObjectType))
            {
                SLogger.Instance.Error($"There is no class for object type '{ObjectType}'.");
                return;
            }

            var desc = classes[ObjectType];

            if (desc == null)
            {
                SLogger.Instance.Error($"There is no player description for object type '{ObjectType}'.");
                return;
            }

            var statInfo = desc.Stats;
            var chr = Client.Character;

            if (Stats.Base[0] >= statInfo[0].MaxValue)
            {
                if (!MaxedLife)
                {
                    Points += 5;
                    MaxedLife = true;
                    chr.Points = Points;
                    chr.MaxedLife = MaxedLife;
                }
            }
            if (Stats.Base[1] >= statInfo[1].MaxValue)
            {
                if (!MaxedMana)
                {
                    Points += 5;
                    MaxedMana = true;
                    chr.Points = Points;
                    chr.MaxedMana = MaxedMana;
                }
            }
            if (Stats.Base[2] >= statInfo[2].MaxValue)
            {
                if (!MaxedAtt)
                {
                    Points += 5;
                    MaxedAtt = true;
                    chr.Points = Points;
                    chr.MaxedAtt = MaxedAtt;
                }
            }
            if (Stats.Base[3] >= statInfo[3].MaxValue)
            {
                if (!MaxedDef)
                {
                    Points += 5;
                    MaxedDef = true;
                    chr.Points = Points;
                    chr.MaxedDef = MaxedDef;
                }
            }
            if (Stats.Base[4] >= statInfo[4].MaxValue)
            {
                if (!MaxedSpd)
                {
                    Points += 5;
                    MaxedSpd = true;
                    chr.Points = Points;
                    chr.MaxedSpd = MaxedSpd;
                }
            }
            if (Stats.Base[5] >= statInfo[5].MaxValue)
            {
                if (!MaxedDex)
                {
                    Points += 5;
                    MaxedDex = true;
                    chr.Points = Points;
                    chr.MaxedDex = MaxedDex;
                }
            }
            if (Stats.Base[6] >= statInfo[6].MaxValue)
            {
                if (!MaxedVit)
                {
                    Points += 5;
                    MaxedVit = true;
                    chr.Points = Points;
                    chr.MaxedVit = MaxedVit;
                }
            }
            if (Stats.Base[7] >= statInfo[7].MaxValue)
            {
                if (!MaxedWis)
                {
                    Points += 5;
                    MaxedWis = true;
                    chr.Points = Points;
                    chr.MaxedWis = MaxedWis;
                }
            }
        }

        public void checkSkillStats()
        {
            if (SmallSkill1 > 5)
                SmallSkill1 = 5;
            if (SmallSkill2 > 5)
                SmallSkill2 = 5;
            if (SmallSkill3 > 5)
                SmallSkill3 = 5;
            if (SmallSkill4 > 5)
                SmallSkill4 = 5;
            if (SmallSkill5 > 5)
                SmallSkill5 = 5;
            if (SmallSkill6 > 5)
                SmallSkill6 = 5;
            if (SmallSkill7 > 5)
                SmallSkill7 = 5;
            if (SmallSkill8 > 5)
                SmallSkill8 = 5;
            if (SmallSkill9 > 5)
                SmallSkill9 = 5;
            if (SmallSkill10 > 5)
                SmallSkill10 = 5;
            if (SmallSkill11 > 5)
                SmallSkill11 = 5;
            if (SmallSkill12 > 5)
                SmallSkill12 = 5;
        }

        public void Damage(int dmg, Entity src)
        {
            if (IsInvulnerable)
                return;

            dmg = (int)Stats.GetDefenseDamage(dmg, false);
            if (!HasConditionEffect(ConditionEffects.Invulnerable))
                HP -= dmg;
            Owner.BroadcastIfVisibleExclude(new Damage()
            {
                TargetId = Id,
                Effects = 0,
                DamageAmount = (ushort)dmg,
                Kill = HP <= 0,
                BulletId = 0,
                ObjectId = src.Id
            }, this, this, PacketPriority.Low);

            if (HP <= 0)
                Death(src.ObjectDesc.DisplayId ??
                      src.ObjectDesc.ObjectId,
                      src);
        }

        public void Death(string killer, Entity entity = null, WmapTile tile = null, bool rekt = false)
        {
            if (Client.State == ProtocolState.Disconnected || _dead)
                return;

            _dead = true;

            if (tile != null && tile.Spawned)
                rekt = true;

            if (Rekted(rekt))
                return;

            if (NonPermaKillEnemy(entity, killer))
                return;

            if (Resurrection())
                return;

            if (TestWorld(killer))
                return;

            SaveToCharacter();
            CoreServerManager.Database.Death(CoreServerManager.Resources.GameData, Client.Account, Client.Character, FameCounter.Stats, killer);

            GenerateGravestone();
            AnnounceDeath(killer);

            Client.SendPacket(new Death()
            {
                AccountId = AccountId,
                CharId = Client.Character.CharId,
                KilledBy = killer
            }, PacketPriority.High);

            Owner.Timers.Add(new WorldTimer(200, (w, t) =>
            {
                if (Client.Player != this)
                    return;
                Client.Disconnect("Death");
            }));
        }

        public void DoUpdate(TickData time)
        {
            IsAlive = KeepAlive(time);
            if (!IsAlive)
                return;

            PlayerUpdate.SendNewTick(time.ElaspedMsDelta);
        }

        public void DropNextRandom() => Client.Random.NextInt();

        public int GetCurrency(CurrencyType currency)
        {
            switch (currency)
            {
                case CurrencyType.Gold:
                    return Credits;

                case CurrencyType.Fame:
                    return CurrentFame;

                default:
                    return 0;
            }
        }

        public int GetMaxedStats()
        {
            var playerDesc = CoreServerManager.Resources.GameData.Classes[ObjectType];
            return playerDesc.Stats.Where((t, i) => Stats.Base[i] >= t.MaxValue).Count() + (UpgradeEnabled ? playerDesc.Stats.Where((t, i) => i == 0 ? Stats.Base[i] >= t.MaxValue + 50 : i == 1 ? Stats.Base[i] >= t.MaxValue + 50 : Stats.Base[i] >= t.MaxValue + 10).Count() : 0);
        }

        public override bool HitByProjectile(Projectile projectile, TickData time)
        {
            if (projectile.ProjectileOwner is Player || IsInvulnerable)
                return false;

            #region Item Effects

            for (var i = 0; i < 4; i++)
            {
                var item = Inventory[i];
                if (item == null || !item.Legendary && !item.Revenge && !item.Eternal && !item.Mythical)
                    continue;

                /* Eternal Effects */
                if (item.MonkeyKingsWrath)
                {
                    MonkeyKingsWrath(i);
                }
                /* Revenge Effects */
                if (item.GodTouch)
                {
                    GodTouch(i);
                }

                if (item.GodBless)
                {
                    GodBless(i);
                }

                /* Legendary Effects */
                if (item.Clarification)
                {
                    Clarification(i);
                }
            }

            #endregion Item Effects

            var dmg = (int)Stats.GetDefenseDamage(projectile.Damage, projectile.ProjDesc.ArmorPiercing);
            if (!HasConditionEffect(ConditionEffects.Invulnerable))
                HP -= dmg;
            ApplyConditionEffect(projectile.ProjDesc.Effects);
            Owner.BroadcastIfVisibleExclude(new Damage()
            {
                TargetId = Id,
                Effects = HasConditionEffect(ConditionEffects.Invincible) ? 0 : projectile.ConditionEffects,
                DamageAmount = (ushort)dmg,
                Kill = HP <= 0,
                BulletId = projectile.ProjectileId,
                ObjectId = projectile.ProjectileOwner.Self.Id
            }, this, this, PacketPriority.Normal);

            if (HP <= 0)
                Death(projectile.ProjectileOwner.Self.ObjectDesc.DisplayId ?? projectile.ProjectileOwner.Self.ObjectDesc.ObjectId, projectile.ProjectileOwner.Self);

            return base.HitByProjectile(projectile, time);
        }

        public override void Init(World owner)
        {
            var x = 0;
            var y = 0;
            var spawnRegions = owner.GetSpawnPoints();
            if (spawnRegions.Any())
            {
                var rand = new System.Random();
                var sRegion = spawnRegions.ElementAt(rand.Next(0, spawnRegions.Length));
                x = sRegion.Key.X;
                y = sRegion.Key.Y;
            }
            Move(x + 0.5f, y + 0.5f);

            // spawn pet if player has one attached
            SpawnPetIfAttached(owner);

            FameCounter = new FameCounter(this);
            FameGoal = GetFameGoal(FameCounter.ClassStats[ObjectType].BestFame);
            ExperienceGoal = GetExpGoal(Client.Character.Level);
            Stars = GetStars();

            if (owner.Name.Equals("Ocean Trench"))
                Breath = 100;

            SetNewbiePeriod();

            if (CoreServerManager.HasEvents() && owner.Id == World.Nexus)
            {
                var events = CoreServerManager.GetEventMessages();

                SendHelp($"<Announcement> This server is hosting {events.Length} event{(events.Length > 1 ? "s" : "")}!");

                for (var i = 0; i < events.Length; i++)
                    SendInfo(events[i]);
            }

            base.Init(owner);

            FameCounter = new FameCounter(this);
            PlayerUpdate = new PlayerUpdate(this);
        }

        public void ProcessNetworking(TickData time)
        {
            while (PendingActions.TryDequeue(out var callback))
                try
                {
                    callback?.Invoke(time);
                }
                catch (Exception e)
                {
                    SLogger.Instance.Error(e);
                }

            while (PendingPackets.Count > 0)
            {
                if (!PendingPackets.TryDequeue(out var pending))
                    continue;

                if (pending.Item1.Id != pending.Item2 || pending.Item1.State == ProtocolState.Disconnected)
                    continue;

                try
                {
                    var packet = Packet.Packets[pending.Item3].CreateInstance();
                    //packet.Read(pending.Item1, pending.Item4, 0, pending.Item4.Length);

                    using(var rdr = new NReader(new MemoryStream(pending.Item4)))
                    {
                        packet.ReadNew(rdr);
                    }


                    pending.Item1.ProcessPacket(packet);
                }
                catch (Exception e)
                {
                    if (!(e is EndOfStreamException))
                        SLogger.Instance.Error("Error processing packet ({0}, {1}, {2})\n{3}", (pending.Item1.Account != null) ? pending.Item1.Account.Name : "", pending.Item1.IpAddress, pending.Item2, e);
                    pending.Item1.SendFailure("An error occurred while processing data from your client.", Failure.MessageWithDisconnect);
                }
            }
        }

        public void Reconnect(World world)
        {
            Client.Reconnect(new Reconnect()
            {
                Host = "",
                Port = CoreServerManager.ServerConfig.serverInfo.port,
                GameId = world.Id,
                Name = world.Name
            });
            var party = DbPartySystem.Get(Client.Account.Database, Client.Account.PartyId);
            if (party != null && party.PartyLeader.Item1 == Client.Account.Name && party.PartyLeader.Item2 == Client.Account.AccountId)
                party.WorldId = -1;
        }

        public void Reconnect(object portal, World world)
        {
            ((Portal)portal).WorldInstanceSet -= Reconnect;

            if (world == null)
                SendError("Portal Not Implemented!");
            else
            {
                Client.Reconnect(new Reconnect()
                {
                    Host = "",
                    Port = CoreServerManager.ServerConfig.serverInfo.port,
                    GameId = world.Id,
                    Name = world.Name
                });
                var party = DbPartySystem.Get(Client.Account.Database, Client.Account.PartyId);
                if (party != null && party.PartyLeader.Item1 == Client.Account.Name && party.PartyLeader.Item2 == Client.Account.AccountId)
                    party.WorldId = -1;
            }
        }

        public void RestoreDefaultSkin() => Skin = _originalSkin;

        public void SaveToCharacter()
        {
            if (Client == null || Client.Character == null) return;

            var chr = Client.Character;
            chr.Level = Level;
            chr.Experience = Experience;
            chr.Fame = Fame;
            chr.HP = HP <= 0 ? 1 : HP;
            chr.MP = MP;
            chr.Stats = Stats.Base.GetStats();
            chr.Tex1 = Texture1;
            chr.Tex2 = Texture2;
            chr.Skin = _originalSkin;
            chr.FameStats = FameCounter?.Stats?.Write() ?? chr.FameStats;
            chr.LastSeen = DateTime.Now;
            chr.HealthStackCount = HealthPots.Count;
            chr.MagicStackCount = MagicPots.Count;
            chr.HasBackpack = HasBackpack;
            chr.PetId = PetId;
            chr.Items = Inventory.GetItemTypes();
            chr.XPBoostTime = XPBoostTime;
            chr.LDBoostTime = LDBoostTime;
            chr.UpgradeEnabled = UpgradeEnabled;
            chr.Datas = Inventory.Data.GetDatas();
            Client.Account.TotalFame = Client.Account.Fame;
            Stats.ReCalculateValues();
        }

        public void SetCurrency(CurrencyType currency, int amount)
        {
            switch (currency)
            {
                case CurrencyType.Gold:
                    Credits = amount; break;
                case CurrencyType.Fame:
                    CurrentFame = amount; break;
            }
        }

        public void SetDefaultSkin(int skin)
        {
            _originalSkin = skin;
            Skin = skin;
        }

        public void Teleport(TickData time, int objId, bool ignoreRestrictions = false)
        {
            var obj = Owner.GetEntity(objId);
            if (obj == null)
            {
                SendError("Target does not exist.");
                RestartTPPeriod();
                return;
            }

            if (!ignoreRestrictions)
            {
                if (Id == objId)
                {
                    SendInfo("You are already at yourself, and always will be!");
                    return;
                }

                if (!Owner.AllowTeleport && Rank < 100)
                {
                    SendError("Cannot teleport here.");
                    RestartTPPeriod();
                    return;
                }

                if (HasConditionEffect(ConditionEffects.Paused))
                {
                    SendError("Cannot teleport while paused.");
                    RestartTPPeriod();
                    return;
                }

                if (!(obj is Player))
                {
                    SendError("Can only teleport to players.");
                    RestartTPPeriod();
                    return;
                }

                if (obj.HasConditionEffect(ConditionEffects.Invisible))
                {
                    SendError("Cannot teleport to an invisible player.");
                    RestartTPPeriod();
                    return;
                }

                if (obj.HasConditionEffect(ConditionEffects.Paused))
                {
                    SendError("Cannot teleport to a paused player.");
                    RestartTPPeriod();
                    return;
                }
                if (obj.HasConditionEffect(ConditionEffects.Hidden))
                {
                    SendError("Target does not exist.");
                    RestartTPPeriod();
                    return;
                }
            }

            ApplyConditionEffect(ConditionEffectIndex.Invulnerable, 1500);
            ApplyConditionEffect(ConditionEffectIndex.Stunned, 1500);
            TeleportPosition(time, obj.X, obj.Y, ignoreRestrictions);
        }

        public void TeleportPosition(TickData time, float x, float y, bool ignoreRestrictions = false) => TeleportPosition(time, new Position() { X = x, Y = y }, ignoreRestrictions);

        public void TeleportPosition(TickData time, Position position, bool ignoreRestrictions = false)
        {
            if (!ignoreRestrictions)
            {
                if (!TPCooledDown())
                {
                    SendError("Too soon to teleport again!");
                    return;
                }

                SetTPDisabledPeriod();
                SetNewbiePeriod();
                FameCounter.Teleport();
            }

            var id = Id;
            var tpPkts = new OutgoingMessage[]
            {
                new Goto()
                {
                    ObjectId = id,
                    Pos = position
                },
                new ShowEffect()
                {
                    EffectType = EffectType.Teleport,
                    TargetObjectId = id,
                    Pos1 = position,
                    Color = new ARGB(0xFFFFFFFF)
                }
            };
            Owner.PlayersBroadcastAsParallel(_ =>
            {
                _.AwaitGotoAck(time.TotalElapsedMs);
                _._tps += 1;
                _.Client.SendPackets(tpPkts, PacketPriority.Low);
            });
        }

        public override void Tick(TickData time)
        {
            if (!IsAlive)
                return;

            if (Owner.Name.Equals("Ocean Trench"))
            {
                if (Breath > 0)
                    Breath -= 2 * time.DeltaTime * 5;
                else
                    HP -= 5;

                if (HP < 0)
                {
                    Death("Suffocation");
                    return;
                }
            }

            CheckTradeTimeout(time);
            HandleQuest(time);
            HandleSpecialEnemies(time);

            if (!HasConditionEffect(ConditionEffects.Paused))
            {
                HandleRegen(time);
                HandleEffects(time);

                GroundEffect(time);
                TickActivateEffects(time);

                /* Skill Tree */
                checkSkillStats();
                checkMaxedStats();

                /* Item Effects */
                TimeEffects(time);
                SpecialEffects();

                FameCounter.Tick(time);

                CerberusClaws(time);
                CerberusCore(time);
            }

            base.Tick(time);
        }

        internal void setCooldownTime(int time, int slot)
        {
            if (slot == 0)
                _canApplyEffect0 = time * 1000;
            if (slot == 1)
                _canApplyEffect1 = time * 1000;
            if (slot == 2)
                _canApplyEffect2 = time * 1000;
            if (slot == 3)
                _canApplyEffect3 = time * 1000;
        }

        protected override void ExportStats(IDictionary<StatDataType, object> stats)
        {
            base.ExportStats(stats);
            stats[StatDataType.AccountId] = AccountId;
            stats[StatDataType.Experience] = Experience - GetLevelExp(Level);
            stats[StatDataType.ExperienceGoal] = ExperienceGoal;
            stats[StatDataType.Level] = Level;
            stats[StatDataType.CurrentFame] = CurrentFame;
            stats[StatDataType.Fame] = Fame;
            stats[StatDataType.FameGoal] = FameGoal;
            stats[StatDataType.Stars] = Stars;
            stats[StatDataType.Guild] = Guild;
            stats[StatDataType.GuildRank] = GuildRank;
            stats[StatDataType.Rank] = Rank;
            stats[StatDataType.Credits] = Credits;
            stats[StatDataType.NameChosen] = (Client.Account?.NameChosen ?? NameChosen) ? 1 : 0;
            stats[StatDataType.Texture1] = Texture1;
            stats[StatDataType.Texture2] = Texture2;
            stats[StatDataType.Skin] = Skin;
            stats[StatDataType.Glow] = Glow;
            stats[StatDataType.MP] = MP;
            stats[StatDataType.Inventory0] = Inventory[0]?.ObjectType ?? -1;
            stats[StatDataType.Inventory1] = Inventory[1]?.ObjectType ?? -1;
            stats[StatDataType.Inventory2] = Inventory[2]?.ObjectType ?? -1;
            stats[StatDataType.Inventory3] = Inventory[3]?.ObjectType ?? -1;
            stats[StatDataType.Inventory4] = Inventory[4]?.ObjectType ?? -1;
            stats[StatDataType.Inventory5] = Inventory[5]?.ObjectType ?? -1;
            stats[StatDataType.Inventory6] = Inventory[6]?.ObjectType ?? -1;
            stats[StatDataType.Inventory7] = Inventory[7]?.ObjectType ?? -1;
            stats[StatDataType.Inventory8] = Inventory[8]?.ObjectType ?? -1;
            stats[StatDataType.Inventory9] = Inventory[9]?.ObjectType ?? -1;
            stats[StatDataType.Inventory10] = Inventory[10]?.ObjectType ?? -1;
            stats[StatDataType.Inventory11] = Inventory[11]?.ObjectType ?? -1;
            stats[StatDataType.BackPack0] = Inventory[12]?.ObjectType ?? -1;
            stats[StatDataType.BackPack1] = Inventory[13]?.ObjectType ?? -1;
            stats[StatDataType.BackPack2] = Inventory[14]?.ObjectType ?? -1;
            stats[StatDataType.BackPack3] = Inventory[15]?.ObjectType ?? -1;
            stats[StatDataType.BackPack4] = Inventory[16]?.ObjectType ?? -1;
            stats[StatDataType.BackPack5] = Inventory[17]?.ObjectType ?? -1;
            stats[StatDataType.BackPack6] = Inventory[18]?.ObjectType ?? -1;
            stats[StatDataType.BackPack7] = Inventory[19]?.ObjectType ?? -1;
            stats[StatDataType.MaximumHP] = Stats[0];
            stats[StatDataType.MaximumMP] = Stats[1];
            stats[StatDataType.Attack] = Stats[2];
            stats[StatDataType.Defense] = Stats[3];
            stats[StatDataType.Speed] = Stats[4];
            stats[StatDataType.Dexterity] = Stats[5];
            stats[StatDataType.Vitality] = Stats[6];
            stats[StatDataType.Wisdom] = Stats[7];
            stats[StatDataType.HPBoost] = Stats.Boost[0];
            stats[StatDataType.MPBoost] = Stats.Boost[1];
            stats[StatDataType.AttackBonus] = Stats.Boost[2];
            stats[StatDataType.DefenseBonus] = Stats.Boost[3];
            stats[StatDataType.SpeedBonus] = Stats.Boost[4];
            stats[StatDataType.DexterityBonus] = Stats.Boost[5];
            stats[StatDataType.VitalityBonus] = Stats.Boost[6];
            stats[StatDataType.WisdomBonus] = Stats.Boost[7];
            stats[StatDataType.HealthStackCount] = HealthPots.Count;
            stats[StatDataType.MagicStackCount] = MagicPots.Count;
            stats[StatDataType.HasBackpack] = HasBackpack ? 1 : 0;
            stats[StatDataType.OxygenBar] = OxygenBar;
            stats[StatDataType.LDBoostTime] = LDBoostTime / 1000;
            stats[StatDataType.XPBoost] = (XPBoostTime != 0) ? 1 : 0;
            stats[StatDataType.XPBoostTime] = XPBoostTime / 1000;
            stats[StatDataType.BaseStat] = Client?.Account?.SetBaseStat ?? 0;
            stats[StatDataType.Points] = Points;
            stats[StatDataType.MaxedLife] = MaxedLife;
            stats[StatDataType.MaxedMana] = MaxedMana;
            stats[StatDataType.MaxedAtt] = MaxedAtt;
            stats[StatDataType.MaxedDef] = MaxedDef;
            stats[StatDataType.MaxedSpd] = MaxedSpd;
            stats[StatDataType.MaxedDex] = MaxedDex;
            stats[StatDataType.MaxedVit] = MaxedVit;
            stats[StatDataType.MaxedWis] = MaxedWis;
            stats[StatDataType.SmallSkill1] = SmallSkill1;
            stats[StatDataType.SmallSkill2] = SmallSkill2;
            stats[StatDataType.SmallSkill3] = SmallSkill3;
            stats[StatDataType.SmallSkill4] = SmallSkill4;
            stats[StatDataType.SmallSkill5] = SmallSkill5;
            stats[StatDataType.SmallSkill6] = SmallSkill6;
            stats[StatDataType.SmallSkill7] = SmallSkill7;
            stats[StatDataType.SmallSkill8] = SmallSkill8;
            stats[StatDataType.SmallSkill9] = SmallSkill9;
            stats[StatDataType.SmallSkill10] = SmallSkill10;
            stats[StatDataType.SmallSkill11] = SmallSkill11;
            stats[StatDataType.SmallSkill12] = SmallSkill12;
            stats[StatDataType.BigSkill1] = BigSkill1;
            stats[StatDataType.BigSkill2] = BigSkill2;
            stats[StatDataType.BigSkill3] = BigSkill3;
            stats[StatDataType.BigSkill4] = BigSkill4;
            stats[StatDataType.BigSkill5] = BigSkill5;
            stats[StatDataType.BigSkill6] = BigSkill6;
            stats[StatDataType.BigSkill7] = BigSkill7;
            stats[StatDataType.BigSkill8] = BigSkill8;
            stats[StatDataType.BigSkill9] = BigSkill9;
            stats[StatDataType.BigSkill10] = BigSkill10;
            stats[StatDataType.BigSkill11] = BigSkill11;
            stats[StatDataType.BigSkill12] = BigSkill12;
            stats[StatDataType.ColorNameChat] = ColorNameChat;
            stats[StatDataType.ColorChat] = ColorChat;
            stats[StatDataType.UpgradeEnabled] = UpgradeEnabled ? 1 : 0;
            stats[StatDataType.PartyId] = Client.Account.PartyId;
            stats[StatDataType.InventoryData0] = Inventory.Data[0]?.GetData() ?? "{}";
            stats[StatDataType.InventoryData1] = Inventory.Data[1]?.GetData() ?? "{}";
            stats[StatDataType.InventoryData2] = Inventory.Data[2]?.GetData() ?? "{}";
            stats[StatDataType.InventoryData3] = Inventory.Data[3]?.GetData() ?? "{}";
            stats[StatDataType.InventoryData4] = Inventory.Data[4]?.GetData() ?? "{}";
            stats[StatDataType.InventoryData5] = Inventory.Data[5]?.GetData() ?? "{}";
            stats[StatDataType.InventoryData6] = Inventory.Data[6]?.GetData() ?? "{}";
            stats[StatDataType.InventoryData7] = Inventory.Data[7]?.GetData() ?? "{}";
            stats[StatDataType.InventoryData8] = Inventory.Data[8]?.GetData() ?? "{}";
            stats[StatDataType.InventoryData9] = Inventory.Data[9]?.GetData() ?? "{}";
            stats[StatDataType.InventoryData10] = Inventory.Data[10]?.GetData() ?? "{}";
            stats[StatDataType.InventoryData11] = Inventory.Data[11]?.GetData() ?? "{}";
            stats[StatDataType.BackPackData0] = Inventory.Data[12]?.GetData() ?? "{}";
            stats[StatDataType.BackPackData1] = Inventory.Data[13]?.GetData() ?? "{}";
            stats[StatDataType.BackPackData2] = Inventory.Data[14]?.GetData() ?? "{}";
            stats[StatDataType.BackPackData3] = Inventory.Data[15]?.GetData() ?? "{}";
            stats[StatDataType.BackPackData4] = Inventory.Data[16]?.GetData() ?? "{}";
            stats[StatDataType.BackPackData5] = Inventory.Data[17]?.GetData() ?? "{}";
            stats[StatDataType.BackPackData6] = Inventory.Data[18]?.GetData() ?? "{}";
            stats[StatDataType.BackPackData7] = Inventory.Data[19]?.GetData() ?? "{}";

            stats[StatDataType.SPS_LIFE_COUNT] = SPSLifeCount;
            stats[StatDataType.SPS_MANA_COUNT] = SPSManaCount;
            stats[StatDataType.SPS_ATTACK_COUNT] = SPSAttackCount;
            stats[StatDataType.SPS_DEFENSE_COUNT] = SPSDefenseCount;
            stats[StatDataType.SPS_DEXTERITY_COUNT] = SPSDexterityCount;
            stats[StatDataType.SPS_WISDOM_COUNT] = SPSWisdomCount;
            stats[StatDataType.SPS_SPEED_COUNT] = SPSSpeedCount;
            stats[StatDataType.SPS_VITALITY_COUNT] = SPSVitalityCount;
            stats[StatDataType.SPS_LIFE_COUNT_MAX] = SPSLifeCountMax;
            stats[StatDataType.SPS_MANA_COUNT_MAX] = SPSManaCountMax;
            stats[StatDataType.SPS_ATTACK_COUNT_MAX] = SPSAttackCountMax;
            stats[StatDataType.SPS_DEFENSE_COUNT_MAX] = SPSDefenseCountMax;
            stats[StatDataType.SPS_DEXTERITY_COUNT_MAX] = SPSDexterityCountMax;
            stats[StatDataType.SPS_WISDOM_COUNT_MAX] = SPSWisdomCountMax;
            stats[StatDataType.SPS_SPEED_COUNT_MAX] = SPSSpeedCountMax;
            stats[StatDataType.SPS_VITALITY_COUNT_MAX] = SPSVitalityCountMax;
        }

        private void CerberusClaws(TickData time)
        {
            var elasped = time.TotalElapsedMs;
            if (elasped % 2000 == 0)
                Stats.Boost.ReCalculateValues();
        }

        private void CerberusCore(TickData time)
        {
            var elasped = time.TotalElapsedMs;
            if (elasped % 15000 == 0)
                ApplyConditionEffect(ConditionEffectIndex.Berserk, 5000);
        }

        private void AnnounceDeath(string killer)
        {
            var maxed = GetMaxedStats();
            var deathMessage = Name + " (" + maxed + (UpgradeEnabled ? "/16, " : "/8, ") + Client.Character.Fame + ") has been killed by " + killer + "!";

            if ((maxed >= 6 || Fame >= 1000) && !Client.Account.Admin)
            {
                CoreServerManager.WorldManager
                    .WorldsBroadcastAsParallel(_ =>
                        _.PlayersBroadcastAsParallel(__ =>
                            __.DeathNotif(deathMessage)
                        )
                    );
                return;
            }

            var pGuild = Client.Account.GuildId;

            // guild case, only for level 20
            if (pGuild > 0 && Level == 20)
            {
                CoreServerManager.WorldManager
                    .WorldsBroadcastAsParallel(_ =>
                        _.PlayersBroadcastAsParallel(__ =>
                        {
                            if (__.Client.Account.GuildId == pGuild)
                                __.DeathNotif(deathMessage);
                        })
                    );

                Owner.PlayersBroadcastAsParallel(_ =>
                {
                    if (_.Client.Account.GuildId != pGuild)
                        _.DeathNotif(deathMessage);
                });
            }
            else
                // guild less case
                Owner.PlayersBroadcastAsParallel(_ => _.DeathNotif(deathMessage));
        }

        private void Clarification(int slot)
        {
            if (_random.NextDouble() < 0.1 && ApplyEffectCooldown(slot))
            {
                Owner.BroadcastIfVisible(new ShowEffect()
                {
                    EffectType = EffectType.AreaBlast,
                    TargetObjectId = Id,
                    Color = new ARGB(0xff00A6FF),
                    Pos1 = new Position() { X = 3 }
                }, this, PacketPriority.Low);

                Owner.BroadcastIfVisible(new Notification()
                {
                    Message = "Clarification!",
                    Color = new ARGB(0xFF00A6FF),
                    PlayerId = Id,
                    ObjectId = Id
                }, this, PacketPriority.Low);

                ActivateHealMp(this, 30 * Stats[1] / 100);
                setCooldownTime(15, slot);
            }
        }

        private void EternalEffects(Item item, int slot)
        {
            if (item.MonkeyKingsWrath)
            {
                if (_random.NextDouble() < .5 && ApplyEffectCooldown(slot))// 50 % chance
                {
                    Size = 100;
                    setCooldownTime(10, slot);
                    Owner.BroadcastIfVisible(new ShowEffect()
                    {
                        EffectType = EffectType.AreaBlast,
                        TargetObjectId = Id,
                        Color = new ARGB(0xFF98ff98),
                        Pos1 = new Position() { X = 3 }
                    }, this, PacketPriority.Low);

                    Owner.BroadcastIfVisible(new Notification()
                    {
                        Message = "Monkey King's Wrath!",
                        Color = new ARGB(0xFF98ff98),
                        PlayerId = Id,
                        ObjectId = Id
                    }, this, PacketPriority.Low);
                    //TO BE DECIDED
                    Size = 300;
                }
            }
        }

        private void GenerateGravestone(bool phantomDeath = false)
        {
            var playerDesc = CoreServerManager.Resources.GameData.Classes[ObjectType];
            //var maxed = playerDesc.Stats.Where((t, i) => Stats.Base[i] >= t.MaxValue).Count();
            var maxed = playerDesc.Stats.Where((t, i) => Stats.Base[i] >= t.MaxValue).Count() + (UpgradeEnabled ? playerDesc.Stats.Where((t, i) => i == 0 ? Stats.Base[i] >= t.MaxValue + 50 : i == 1 ? Stats.Base[i] >= t.MaxValue + 50 : Stats.Base[i] >= t.MaxValue + 10).Count() : 0);
            ushort objType;
            int time;
            switch (maxed)
            {
                case 16: objType = 0xa00e; time = 600000; break;
                case 15: objType = 0xa00d; time = 600000; break;
                case 14: objType = 0xa00c; time = 600000; break;
                case 13: objType = 0xa00b; time = 600000; break;
                case 12: objType = 0xa00a; time = 600000; break;
                case 11: objType = 0xa009; time = 600000; break;
                case 10: objType = 0xa008; time = 600000; break;
                case 9: objType = 0xa007; time = 600000; break;
                case 8: objType = 0x0735; time = 600000; break;
                case 7: objType = 0x0734; time = 600000; break;
                case 6: objType = 0x072b; time = 600000; break;
                case 5: objType = 0x072a; time = 600000; break;
                case 4: objType = 0x0729; time = 600000; break;
                case 3: objType = 0x0728; time = 600000; break;
                case 2: objType = 0x0727; time = 600000; break;
                case 1: objType = 0x0726; time = 600000; break;
                default:
                    objType = 0x0725; time = 300000;
                    if (Level < 20) { objType = 0x0724; time = 60000; }
                    if (Level <= 1) { objType = 0x0723; time = 30000; }
                    break;
            }

            var deathMessage = Name + " (" + maxed + (UpgradeEnabled ? "/16, " : "/8, ") + Client.Character.Fame + ")";
            //var deathMessage = Name + " (" + maxed + ("/8, ") + _client.Character.Fame + ")";
            var obj = new StaticObject(CoreServerManager, objType, time, true, true, false);
            obj.Move(X, Y);
            obj.Name = (!phantomDeath) ? deathMessage : $"{Name} got rekt";
            Owner.EnterWorld(obj);
        }

        private void GodBless(int slot)
        {
            if (_random.NextDouble() < 0.03 && ApplyEffectCooldown(slot))
            {
                Owner.BroadcastIfVisible(new ShowEffect()
                {
                    EffectType = EffectType.AreaBlast,
                    TargetObjectId = Id,
                    Color = new ARGB(0xffA1A1A1),
                    Pos1 = new Position() { X = 3 }
                }, this, PacketPriority.Low);
                Owner.BroadcastIfVisible(new Notification()
                {
                    Message = "God Bless!",
                    Color = new ARGB(0xFFFFFFFF),
                    PlayerId = Id,
                    ObjectId = Id
                }, this, PacketPriority.Low);

                ApplyConditionEffect(ConditionEffectIndex.Invulnerable, 3000);
                setCooldownTime(5, slot);
            }
        }

        private void GodTouch(int slot)
        {
            if (_random.NextDouble() < 0.02 && ApplyEffectCooldown(slot))
            {
                ActivateHealHp(this, 25 * Stats[0] / 100);
                Owner.BroadcastIfVisible(new ShowEffect()
                {
                    EffectType = EffectType.AreaBlast,
                    TargetObjectId = Id,
                    Color = new ARGB(0xffffffff),
                    Pos1 = new Position() { X = 3 }
                }, this, PacketPriority.Low);

                Owner.BroadcastIfVisible(new Notification()
                {
                    Message = "God Touch!",
                    Color = new ARGB(0xFFFFFFFF),
                    PlayerId = Id,
                    ObjectId = Id
                }, this, PacketPriority.Low);
                setCooldownTime(30, slot);
            }
        }

        private void HandleRegen(TickData time)
        {
            // hp regen
            if (HP == Stats[0] || !CanHpRegen())
                _hpRegenCounter = 0;
            else
            {
                _hpRegenCounter += Stats.GetHPRegen() * time.ElaspedMsDelta / 1000f;
                var regen = (int)_hpRegenCounter;
                if (regen > 0)
                {
                    HP = Math.Min(Stats[0], HP + regen);
                    _hpRegenCounter -= regen;
                }
            }

            // mp regen
            if (MP == Stats[1] || !CanMpRegen())
                _mpRegenCounter = 0;
            else
            {
                _mpRegenCounter += Stats.GetMPRegen() * time.ElaspedMsDelta / 1000f;
                var regen = (int)_mpRegenCounter;
                if (regen > 0)
                {
                    MP = Math.Min(Stats[1], MP + regen);
                    _mpRegenCounter -= regen;
                }
            }
        }

        private void LegendaryEffects(Item item, int slot)
        {
            var Slot = slot;

            if (item.OutOfOneMind)
            {
                if (_random.NextDouble() < 0.02 && ApplyEffectCooldown(Slot))
                {
                    Owner.BroadcastIfVisible(new Notification()
                    {
                        Message = "Out of One's Mind!",
                        Color = new ARGB(0xFF00D5D8),
                        PlayerId = Id,
                        ObjectId = Id
                    }, this, PacketPriority.Low);

                    ApplyConditionEffect(ConditionEffectIndex.Berserk, 3000);
                    setCooldownTime(10, Slot);
                }
            }

            if (item.SteamRoller)
            {
                if (_random.NextDouble() < 0.05 && ApplyEffectCooldown(Slot))
                {
                    Owner.BroadcastIfVisible(new Notification()
                    {
                        Message = "Steam Roller!",
                        Color = new ARGB(0xFF717171),
                        PlayerId = Id,
                        ObjectId = Id
                    }, this, PacketPriority.Low);

                    ApplyConditionEffect(ConditionEffectIndex.Armored, 5000);
                    setCooldownTime(10, Slot);
                }
            }

            if (item.Mutilate)
            {
                if (_random.NextDouble() < 0.08 && ApplyEffectCooldown(Slot))
                {
                    Owner.BroadcastIfVisible(new Notification()
                    {
                        Message = "Mutilate!",
                        Color = new ARGB(0xFFFF4600),
                        PlayerId = Id,
                        ObjectId = Id
                    }, this, PacketPriority.Low);

                    ApplyConditionEffect(ConditionEffectIndex.Damaging, 3000);
                    setCooldownTime(10, Slot);
                }
            }
        }

        private void MonkeyKingsWrath(int slot)
        {
            if (_random.NextDouble() < .5 && ApplyEffectCooldown(slot))// 50 % chance
            {
                Owner.BroadcastIfVisible(new ShowEffect()
                {
                    EffectType = EffectType.AreaBlast,
                    TargetObjectId = Id,
                    Color = new ARGB(0xffff0000),
                    Pos1 = new Position() { X = 3 }
                }, this, PacketPriority.Low);

                Owner.BroadcastIfVisible(new Notification()
                {
                    Message = "Monkey King's Wrath!",
                    Color = new ARGB(0xFFFF0000),
                    PlayerId = Id,
                    ObjectId = Id
                }, this, PacketPriority.Low);
                //TO BE DECIDED
                this.Client.SendPacket(new GlobalNotification() { Text = "monkeyKing" });
                setCooldownTime(10, slot);
            }
        }

        private bool NonPermaKillEnemy(Entity entity, string killer)
        {
            if (entity == null)
                return false;

            if (!entity.Spawned && entity.Controller == null)
                return false;

            GenerateGravestone(true);
            ReconnectToNexus();
            return true;
        }

        private void ReconnectToNexus()
        {
            HP = 1;
            Client.Reconnect(new Reconnect()
            {
                Host = "",
                Port = CoreServerManager.ServerConfig.serverInfo.port,
                GameId = World.Nexus,
                Name = "Nexus"
            });
            var party = DbPartySystem.Get(Client.Account.Database, Client.Account.PartyId);
            if (party != null && party.PartyLeader.Item1 == Client.Account.Name && party.PartyLeader.Item2 == Client.Account.AccountId)
                party.WorldId = -1;
        }

        private bool Rekted(bool rekt)
        {
            if (!rekt)
                return false;
            GenerateGravestone(true);
            ReconnectToNexus();
            return true;
        }

        private bool Resurrection()
        {
            for (int i = 0; i < 4; i++)
            {
                var item = Inventory[i];

                if (item == null || !item.Resurrects)
                    continue;

                Inventory[i] = null;
                Owner.PlayersBroadcastAsParallel(_ => _.SendInfo($"{Name}'s {item.DisplayName} breaks and he disappears"));
                ReconnectToNexus();
                return true;
            }
            return false;
        }

        private void RevengeEffects(Item item, int slot)
        {
            if (item.Lucky)
            {
                if (_random.NextDouble() < 0.1 && ApplyEffectCooldown(slot))
                {
                    setCooldownTime(20, slot);
                    for (var j = 0; j < 8; j++)
                        Stats.Boost.ActivateBoost[j].Push(j == 0 ? 100 : j == 1 ? 100 : 15, true);
                    Stats.ReCalculateValues();

                    #region Boosted Eff

                    ApplyConditionEffect(ConditionEffectIndex.HPBoost, 5000);
                    ApplyConditionEffect(ConditionEffectIndex.MPBoost, 5000);
                    ApplyConditionEffect(ConditionEffectIndex.AttBoost, 5000);
                    ApplyConditionEffect(ConditionEffectIndex.DefBoost, 5000);
                    ApplyConditionEffect(ConditionEffectIndex.DexBoost, 5000);
                    ApplyConditionEffect(ConditionEffectIndex.SpdBoost, 5000);
                    ApplyConditionEffect(ConditionEffectIndex.WisBoost, 5000);
                    ApplyConditionEffect(ConditionEffectIndex.VitBoost, 5000);

                    #endregion Boosted Eff

                    Owner.BroadcastIfVisible(new Notification()
                    {
                        Message = "Boosted!",
                        Color = new ARGB(0xFF00FF00),
                        PlayerId = Id,
                        ObjectId = Id
                    }, this, PacketPriority.Low);

                    Owner.Timers.Add(new WorldTimer(5000, (world, t) =>
                    {
                        for (var i = 0; i < 8; i++)
                            Stats.Boost.ActivateBoost[i].Pop(i == 0 ? 100 : i == 1 ? 100 : 15, true);
                        Stats.ReCalculateValues();
                    }));
                }
            }

            if (item.Insanity)
            {
                if (_random.NextDouble() < 0.05 && ApplyEffectCooldown(slot))
                {
                    Owner.BroadcastIfVisible(new Notification()
                    {
                        Message = "Insanity!",
                        Color = new ARGB(0xFFFF0000),
                        PlayerId = Id,
                        ObjectId = Id
                    }, this, PacketPriority.Low);

                    setCooldownTime(10, slot);
                    ApplyConditionEffect(ConditionEffectIndex.Berserk, 3000);
                    ApplyConditionEffect(ConditionEffectIndex.Damaging, 3000);
                }
            }

            if (item.HolyProtection)
            {
                if (_random.NextDouble() < 0.1 && ApplyEffectCooldown(slot))
                {
                    if (!(HasConditionEffect(ConditionEffects.Quiet)
                        || HasConditionEffect(ConditionEffects.Weak)
                        || HasConditionEffect(ConditionEffects.Slowed)
                        || HasConditionEffect(ConditionEffects.Sick)
                        || HasConditionEffect(ConditionEffects.Dazed)
                        || HasConditionEffect(ConditionEffects.Stunned)
                        || HasConditionEffect(ConditionEffects.Blind)
                        || HasConditionEffect(ConditionEffects.Hallucinating)
                        || HasConditionEffect(ConditionEffects.Drunk)
                        || HasConditionEffect(ConditionEffects.Confused)
                        || HasConditionEffect(ConditionEffects.Paralyzed)
                        || HasConditionEffect(ConditionEffects.Bleeding)
                        || HasConditionEffect(ConditionEffects.Hexed)
                        || HasConditionEffect(ConditionEffects.Unstable)
                        || HasConditionEffect(ConditionEffects.Curse)
                        || HasConditionEffect(ConditionEffects.Petrify)
                        || HasConditionEffect(ConditionEffects.Darkness)))
                        return;
                    Owner.BroadcastIfVisible(new Notification()
                    {
                        Message = "Holy Protection!",
                        Color = new ARGB(0xFFFFFFFF),
                        PlayerId = Id,
                        ObjectId = Id
                    }, this, PacketPriority.Low);

                    setCooldownTime(7, slot);
                    ApplyConditionEffect(NegativeEffs);
                }
            }

            /* God Touch, God Bless in HitByProjectile */

            /* Electrify in HitByProjectile (Enemy) */
        }

        private void SonicBlaster(int slot)
        {
            if (ApplyEffectCooldown(slot))
            {
                Owner.BroadcastIfVisible(new ShowEffect()
                {
                    EffectType = EffectType.AreaBlast,
                    TargetObjectId = Id,
                    Color = new ARGB(0xff6F00C0),
                    Pos1 = new Position() { X = 3 }
                }, this, PacketPriority.Low);

                Owner.BroadcastIfVisible(new Notification()
                {
                    Message = "Sonic Blaster!",
                    Color = new ARGB(0xFF9300FF),
                    PlayerId = Id,
                    ObjectId = Id
                }, this, PacketPriority.Low);

                ApplyConditionEffect(ConditionEffectIndex.Invisible, 6000);
                ApplyConditionEffect(ConditionEffectIndex.Speedy, 6000);
                setCooldownTime(30, slot);
            }
        }

        private void SpawnPetIfAttached(World owner)
        {
            // despawn old pet if found
            Pet?.Owner?.LeaveWorld(Pet);

            if (Client.Account.Hidden)
                return;

            // create new pet
            var petId = PetId;
            if (petId != 0)
            {
                var pet = new Pet(CoreServerManager, this, (ushort)petId);
                pet.Move(X, Y);
                owner.EnterWorld(pet);
                pet.SetDefaultSize(pet.ObjectDesc.Size);
                Pet = pet;
            }
        }

        private void SpecialEffects()
        {
            for (var i = 0; i < 4; i++)
            {
                var item = Inventory[i];
                if (item == null || !item.Legendary && !item.Revenge && !item.Mythical)
                    continue;

                if (item.Mythical || item.Revenge)
                    RevengeEffects(item, i);

                if (item.Legendary)
                    LegendaryEffects(item, i);

                if (item.Eternal)
                    EternalEffects(item, i);
            }
        }

        private bool TestWorld(string killer)
        {
            if (!(Owner is Test))
                return false;

            GenerateGravestone();
            ReconnectToNexus();
            return true;
        }

        private void TickActivateEffects(TickData time)
        {
            var dt = time.ElaspedMsDelta;
            if (Owner == null)
                return;

            if (Owner is Vault || Owner is Nexus || Owner is GuildHall || Owner.Id == 10)
                return;

            if (XPBoostTime != 0)
                if (Level >= 20)
                    XPBoostTime = 0;

            if (XPBoostTime > 0)
                XPBoostTime = Math.Max(XPBoostTime - dt, 0);
            if (XPBoostTime == 0)
                XPBoosted = false;
            if (LDBoostTime > 0)
                LDBoostTime = Math.Max(LDBoostTime - dt, 0);
        }

        private void TimeEffects(TickData time)
        {
            if (_canApplyEffect0 > 0)
            {
                _canApplyEffect0 -= time.ElaspedMsDelta;
                if (_canApplyEffect0 < 0)
                    _canApplyEffect0 = 0;
            }

            if (_canApplyEffect1 > 0)
            {
                _canApplyEffect1 -= time.ElaspedMsDelta;
                if (_canApplyEffect1 < 0)
                    _canApplyEffect1 = 0;
            }

            if (_canApplyEffect2 > 0)
            {
                _canApplyEffect2 -= time.ElaspedMsDelta;
                if (_canApplyEffect2 < 0)
                    _canApplyEffect2 = 0;
            }

            if (_canApplyEffect3 > 0)
            {
                _canApplyEffect3 -= time.ElaspedMsDelta;
                if (_canApplyEffect3 < 0)
                    _canApplyEffect3 = 0;
            }
        }
    }
}
