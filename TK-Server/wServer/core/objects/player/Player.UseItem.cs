﻿using CA.Extensions.Concurrent;
using common.resources;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using wServer.core.worlds;
using wServer.core.worlds.logic;
using wServer.networking;
using wServer.networking.packets;
using wServer.networking.packets.outgoing;
using wServer.utils;

namespace wServer.core.objects
{
    partial class Player
    {
        public const int MaxAbilityDist = 14;

        public static readonly ConditionEffect[] NegativeEffs = new ConditionEffect[]
        {
            new ConditionEffect() { Effect = ConditionEffectIndex.Slowed, DurationMS = 0 },
            new ConditionEffect() { Effect = ConditionEffectIndex.Paralyzed, DurationMS = 0 },
            new ConditionEffect() { Effect = ConditionEffectIndex.Weak, DurationMS = 0 },
            new ConditionEffect() { Effect = ConditionEffectIndex.Stunned, DurationMS = 0 },
            new ConditionEffect() { Effect = ConditionEffectIndex.Confused, DurationMS = 0 },
            new ConditionEffect() { Effect = ConditionEffectIndex.Blind, DurationMS = 0 },
            new ConditionEffect() { Effect = ConditionEffectIndex.Quiet, DurationMS = 0 },
            new ConditionEffect() { Effect = ConditionEffectIndex.ArmorBroken, DurationMS = 0 },
            new ConditionEffect() { Effect = ConditionEffectIndex.Bleeding, DurationMS = 0 },
            new ConditionEffect() { Effect = ConditionEffectIndex.Dazed,DurationMS = 0 },
            new ConditionEffect() { Effect = ConditionEffectIndex.Sick, DurationMS = 0 },
            new ConditionEffect() { Effect = ConditionEffectIndex.Drunk, DurationMS = 0 },
            new ConditionEffect() { Effect = ConditionEffectIndex.Hallucinating, DurationMS = 0 },
            new ConditionEffect() { Effect = ConditionEffectIndex.Hexed, DurationMS = 0 },
            new ConditionEffect() { Effect = ConditionEffectIndex.Curse, DurationMS = 0 },
            new ConditionEffect() { Effect = ConditionEffectIndex.Unstable, DurationMS = 0 }
        };

        public bool PoisonWis = false;
        private object _useLock = new object();

        public void AEItemDust(TickData time, Item item, Position target, int slot, int objId, ActivateEffect eff)
        {
            ushort[] tierSwords = { 0x221c, 0x228e, 0x258c }; //reskins
            ushort[] tierStaff = { 0x228b, 0xf13, 0x237e, }; //reskins
            ushort[] tierDagger = { 0x237d, 0x899, 0xf11, 0x232a, 0x228d };//reskins
            ushort[] tierWands = { 0x2296, 0xf10, 0x237c, 0x228c };//reskins
            ushort[] tierBows = { 0x228f, 0x258a, 0xf12 };//reskins
            ushort[] tierKatana = { 0xcee, 0x228a, 0xf14, 0x237f };//reskins
            var gameData = CoreServerManager.Resources.GameData;
            var TieredChance = _random.NextDouble();
            ushort itemValue;
            var entity = Owner.GetEntity(objId);
            var container = entity as Container;
            if (TieredChance <= 0.10)
            {
                var itemChance = _random.Next(0, 5);
                switch (itemChance)
                {
                    case 0:
                        itemValue = 0x9c6; //Dagger T13
                        if (container != null)
                            container.Inventory[slot] = gameData.Items[itemValue];
                        else
                            Inventory[slot] = gameData.Items[itemValue];
                        SendInfo($"Used a Item Dust and obtained a {gameData.Items[itemValue].DisplayName ?? gameData.Items[itemValue].ObjectId}");
                        break;

                    case 1:
                        itemValue = 0x9cc; //Bow T13
                        if (container != null)
                            container.Inventory[slot] = gameData.Items[itemValue];
                        else
                            Inventory[slot] = gameData.Items[itemValue];
                        SendInfo($"Used a Item Dust and obtained a {gameData.Items[itemValue].DisplayName ?? gameData.Items[itemValue].ObjectId}");
                        break;

                    case 2:
                        itemValue = 0x9d1; //Staff T13
                        if (container != null)
                            container.Inventory[slot] = gameData.Items[itemValue];
                        else
                            Inventory[slot] = gameData.Items[itemValue];
                        SendInfo($"Used a Item Dust and obtained a {gameData.Items[itemValue].DisplayName ?? gameData.Items[itemValue].ObjectId}");
                        break;

                    case 3:
                        itemValue = 0x9ca; //Wand T13
                        if (container != null)
                            container.Inventory[slot] = gameData.Items[itemValue];
                        else
                            Inventory[slot] = gameData.Items[itemValue];
                        SendInfo($"Used a Item Dust and obtained a {gameData.Items[itemValue].DisplayName ?? gameData.Items[itemValue].ObjectId}");
                        break;

                    case 4:
                        itemValue = 0x9c8; //Sword T13
                        if (container != null)
                            container.Inventory[slot] = gameData.Items[itemValue];
                        else
                            Inventory[slot] = gameData.Items[itemValue];
                        SendInfo($"Used a Item Dust and obtained a {gameData.Items[itemValue].DisplayName ?? gameData.Items[itemValue].ObjectId}");
                        break;

                    case 5:
                        itemValue = 0x4959; //Katana T13
                        if (container != null)
                            container.Inventory[slot] = gameData.Items[itemValue];
                        else
                            Inventory[slot] = gameData.Items[itemValue];
                        SendInfo($"Used a Item Dust and obtained a {gameData.Items[itemValue].DisplayName ?? gameData.Items[itemValue].ObjectId}");
                        break;
                }
            }
            else
            {
                var tierChance = _random.Next(0, 5);
                switch (tierChance)
                {
                    case 0:
                        itemValue = tierSwords[_random.Next(tierSwords.Length)];
                        if (container != null)
                            container.Inventory[slot] = gameData.Items[itemValue];
                        else
                            Inventory[slot] = gameData.Items[itemValue];
                        SendInfo($"Used a Item Dust and obtained a {gameData.Items[itemValue].DisplayName ?? gameData.Items[itemValue].ObjectId}");
                        break;

                    case 1:
                        itemValue = tierStaff[_random.Next(tierStaff.Length)];
                        if (container != null)
                            container.Inventory[slot] = gameData.Items[itemValue];
                        else
                            Inventory[slot] = gameData.Items[itemValue];
                        SendInfo($"Used a Item Dust and obtained a {gameData.Items[itemValue].DisplayName ?? gameData.Items[itemValue].ObjectId}");
                        break;

                    case 2:
                        itemValue = tierDagger[_random.Next(tierDagger.Length)];
                        if (container != null)
                            container.Inventory[slot] = gameData.Items[itemValue];
                        else
                            Inventory[slot] = gameData.Items[itemValue];
                        SendInfo($"Used a Item Dust and obtained a {gameData.Items[itemValue].DisplayName ?? gameData.Items[itemValue].ObjectId}");
                        break;

                    case 3:
                        itemValue = tierWands[_random.Next(tierWands.Length)];
                        if (container != null)
                            container.Inventory[slot] = gameData.Items[itemValue];
                        else
                            Inventory[slot] = gameData.Items[itemValue];
                        SendInfo($"Used a Item Dust and obtained a {gameData.Items[itemValue].DisplayName ?? gameData.Items[itemValue].ObjectId}");
                        break;

                    case 4:
                        itemValue = tierBows[_random.Next(tierBows.Length)];
                        if (container != null)
                            container.Inventory[slot] = gameData.Items[itemValue];
                        else
                            Inventory[slot] = gameData.Items[itemValue];
                        SendInfo($"Used a Item Dust and obtained a {gameData.Items[itemValue].DisplayName ?? gameData.Items[itemValue].ObjectId}");
                        break;

                    case 5:
                        itemValue = tierKatana[_random.Next(tierKatana.Length)];
                        if (container != null)
                            container.Inventory[slot] = gameData.Items[itemValue];
                        else
                            Inventory[slot] = gameData.Items[itemValue];
                        SendInfo($"Used a Item Dust and obtained a {gameData.Items[itemValue].DisplayName ?? gameData.Items[itemValue].ObjectId}");
                        break;
                }
            }
        }

        public void AEResetSkillTree(TickData time, Item item, Position target, int slot, int objId, ActivateEffect eff)
        {
            var gameData = CoreServerManager.Resources.GameData;
            var entity = Owner.GetEntity(objId);
            var container = entity as Container;
            if (/*!MaxedLife || !MaxedMana || !MaxedAtt || !MaxedDef || !MaxedSpd || !MaxedDex || !MaxedVit || !MaxedWis || */container is GiftChest)
            {
                //SendError("You can't use this!");
                if (container is GiftChest)
                {
                    container.Inventory[slot] = item;
                    SendError("You can't use this in a Gift Chest!");
                }
                else if (!(container is GiftChest) && container != null)
                {
                    container.Inventory[slot] = item;
                    SendError("You can't use this, you're not maxed!");
                }
                else
                {
                    SendError("You can't use this, you're not maxed!");
                    Inventory[slot] = item;
                }
                return;
            }
            else
            {
                if (container != null)
                    container.Inventory[slot] = null;
                else
                    Inventory[slot] = null;
            }

            SmallSkill1 = 0;
            SmallSkill2 = 0;
            SmallSkill3 = 0;
            SmallSkill4 = 0;
            SmallSkill5 = 0;
            SmallSkill6 = 0;
            SmallSkill7 = 0;
            SmallSkill8 = 0;
            SmallSkill9 = 0;
            SmallSkill10 = 0;
            SmallSkill11 = 0;
            SmallSkill12 = 0;
            Points = 0;
            BigSkill1 = false;
            BigSkill2 = false;
            BigSkill3 = false;
            BigSkill4 = false;
            BigSkill5 = false;
            BigSkill6 = false;
            BigSkill7 = false;
            BigSkill8 = false;
            BigSkill9 = false;
            BigSkill10 = false;
            BigSkill11 = false;
            BigSkill12 = false;
            Stats.ReCalculateValues();
            Stats.Base.ReCalculateValues();
            Stats.Boost.ReCalculateValues();
            var chr = Client.Character;
            chr.SmallSkill1 = SmallSkill1;
            chr.SmallSkill2 = SmallSkill2;
            chr.SmallSkill3 = SmallSkill3;
            chr.SmallSkill4 = SmallSkill4;
            chr.SmallSkill5 = SmallSkill5;
            chr.SmallSkill6 = SmallSkill6;
            chr.SmallSkill7 = SmallSkill7;
            chr.SmallSkill8 = SmallSkill8;
            chr.SmallSkill9 = SmallSkill9;
            chr.SmallSkill10 = SmallSkill10;
            chr.SmallSkill11 = SmallSkill11;
            chr.SmallSkill12 = SmallSkill12;
            chr.Points = Points;
            chr.BigSkill1 = BigSkill1;
            chr.BigSkill2 = BigSkill2;
            chr.BigSkill3 = BigSkill3;
            chr.BigSkill4 = BigSkill4;
            chr.BigSkill5 = BigSkill5;
            chr.BigSkill6 = BigSkill6;
            chr.BigSkill7 = BigSkill7;
            chr.BigSkill8 = BigSkill8;
            chr.BigSkill9 = BigSkill9;
            chr.BigSkill10 = BigSkill10;
            chr.BigSkill11 = BigSkill11;
            chr.BigSkill12 = BigSkill12;
            SendInfo("All your Skill Points have been reset.");
        }

        public void AESpecialDust(TickData time, Item item, Position target, int slot, int objId, ActivateEffect eff)
        {
            ushort[] nonspecialWeapons = { 0x709, 0xced, 0xb21, 0xcde, 0xc10, 0xcec, 0x164a, 0xc15, 0xc03, 0xc24, 0xcea, 0xc1d, 0xc33, 0x183b, 0xc04, 0xceb, 0xa03, 0xcdb, 0x2303, 0xcdf, 0x164b, 0x6a9, 0x716d, 0xb3f };
            ushort[] specialWeapons = { 0xc29, 0xc0a, 0x9d5, 0xc05, 0x915, 0xcdc };
            ushort[] nonspecialAbilities = { 0x9ce, 0x8a9, 0xc09, 0xc1e, 0x16de, 0x8a8, 0xa40, 0x0d43, 0x7170, 0xc16, 0x767, 0x911, 0xc1c, 0x132, 0x912, 0xc2a, 0x136, 0x183d };
            ushort[] specialAbilities = { 0xa5a, 0xc07, 0xb41, 0xc08, 0xc0f, 0xc06, 0xc6d, 0xc0b, 0x0d42, 0xc30, 0x916 };
            ushort[] nonspecialArmors = { 0xa3e, 0xc18, 0xc28, 0x7562, 0xc14, 0xc1f, 0xc32, 0x7563, 0xc61, 0x7564 };
            ushort[] specialArmors = { 0xc82, 0xc83, 0xc84, 0x7448, 0xc6e };
            ushort[] nonspecialRings = { 0x708, 0xc17, 0xa41, 0xc5f, 0xc27, 0xc20, 0xc13, 0xc31, 0xba1, 0xba2, 0xba0 };
            ushort[] specialRings = { 0x7fd2, 0x7fd3, 0x7fd4, 0xbad, 0xbac, 0xbab };
            var gameData = CoreServerManager.Resources.GameData;
            ushort itemValue;
            var entity = Owner.GetEntity(objId);
            var container = entity as Container;
            var typeitemChance = _random.Next(0, 3);
            switch (typeitemChance)
            {
                case 0: //Weapons
                    var legchancewea = _random.NextDouble();
                    if (legchancewea <= 0.20)
                    {
                        itemValue = specialWeapons[_random.Next(specialWeapons.Length)];
                        if (container != null)
                            container.Inventory[slot] = gameData.Items[itemValue];
                        else
                            Inventory[slot] = gameData.Items[itemValue];
                        if (Rank < 60)
                            CoreServerManager.ChatManager.AnnounceLoot($"[{Name}] used a Special Dust and Obtained an [{gameData.Items[itemValue].DisplayName ?? gameData.Items[itemValue].ObjectId}]!");
                    }
                    else
                    {
                        itemValue = nonspecialWeapons[_random.Next(nonspecialWeapons.Length)];
                        if (container != null)
                            container.Inventory[slot] = gameData.Items[itemValue];
                        else
                            Inventory[slot] = gameData.Items[itemValue];
                        if (Rank < 60)
                            CoreServerManager.ChatManager.AnnounceLoot($"[{Name}] used a Special Dust and Obtained an [{gameData.Items[itemValue].DisplayName ?? gameData.Items[itemValue].ObjectId}]!");
                    }
                    break;

                case 1://Abilities
                    var legchanceabi = _random.NextDouble();
                    if (legchanceabi <= 0.20)
                    {
                        itemValue = specialAbilities[_random.Next(specialAbilities.Length)];
                        if (container != null)
                            container.Inventory[slot] = gameData.Items[itemValue];
                        else
                            Inventory[slot] = gameData.Items[itemValue];
                        if (Rank < 60)
                            CoreServerManager.ChatManager.AnnounceLoot($"[{Name}] used a Special Dust and Obtained an [{gameData.Items[itemValue].DisplayName ?? gameData.Items[itemValue].ObjectId}]!");
                    }
                    else
                    {
                        itemValue = nonspecialAbilities[_random.Next(nonspecialAbilities.Length)];
                        if (container != null)
                            container.Inventory[slot] = gameData.Items[itemValue];
                        else
                            Inventory[slot] = gameData.Items[itemValue];
                        if (Rank < 60)
                            CoreServerManager.ChatManager.AnnounceLoot($"[{Name}] used a Special Dust and Obtained an [{gameData.Items[itemValue].DisplayName ?? gameData.Items[itemValue].ObjectId}]!");
                    }
                    break;

                case 2:
                    var legchancearm = _random.NextDouble();
                    if (legchancearm <= 0.20)
                    {
                        itemValue = specialArmors[_random.Next(specialArmors.Length)];
                        if (container != null)
                            container.Inventory[slot] = gameData.Items[itemValue];
                        else
                            Inventory[slot] = gameData.Items[itemValue];
                        if (Rank < 60)
                            CoreServerManager.ChatManager.AnnounceLoot($"[{Name}] used a Special Dust and Obtained an [{gameData.Items[itemValue].DisplayName ?? gameData.Items[itemValue].ObjectId}]!");
                    }
                    else
                    {
                        itemValue = nonspecialArmors[_random.Next(nonspecialArmors.Length)];
                        if (container != null)
                            container.Inventory[slot] = gameData.Items[itemValue];
                        else
                            Inventory[slot] = gameData.Items[itemValue];
                        if (Rank < 60)
                            CoreServerManager.ChatManager.AnnounceLoot($"[{Name}] used a Special Dust and Obtained an [{gameData.Items[itemValue].DisplayName ?? gameData.Items[itemValue].ObjectId}]!");
                    }
                    break;

                case 3:
                    var legchancering = _random.NextDouble();
                    if (legchancering <= 0.20)
                    {
                        itemValue = specialRings[_random.Next(specialRings.Length)];
                        if (container != null)
                            container.Inventory[slot] = gameData.Items[itemValue];
                        else
                            Inventory[slot] = gameData.Items[itemValue];
                        if (Rank < 60)
                            CoreServerManager.ChatManager.AnnounceLoot($"[{Name}] used a Special Dust and Obtained an [{gameData.Items[itemValue].DisplayName ?? gameData.Items[itemValue].ObjectId}]!");
                    }
                    else
                    {
                        itemValue = nonspecialRings[_random.Next(nonspecialRings.Length)];
                        if (container != null)
                            container.Inventory[slot] = gameData.Items[itemValue];
                        else
                            Inventory[slot] = gameData.Items[itemValue];
                        if (Rank < 60)
                            CoreServerManager.ChatManager.AnnounceLoot($"[{Name}] used a Special Dust and Obtained an [{gameData.Items[itemValue].DisplayName ?? gameData.Items[itemValue].ObjectId}]!");
                    }
                    break;
            }
        }

        public void AEUnlockChest(TickData time, Item item, Position target, int slot, int objId, ActivateEffect eff)
        {
            Player player = this;
            var db = CoreServerManager.Database;
            var acc = player.Client.Account;
            var trans = db.Conn.CreateTransaction();
            CoreServerManager.Database.CreateChest(acc, trans);
            var t2 = trans.ExecuteAsync();
            acc.Reload("vaultCount");
            acc.Reload("fame");
            acc.Reload("totalFame");
            player.CurrentFame = acc.Fame;
            //(Owner as Vault).AddChest(this);
            player.SendInfo("Your Vault has been unlocked! If u are in your Vault, go out and enter again.");
        }

        public void AEUnlockSlotChar(TickData time, Item item, Position target, int slot, int objId, ActivateEffect eff)
        {
            Player player1 = this;
            var account = player1.Client.Account;
            var transi = CoreServerManager.Database.Conn.CreateTransaction();
            transi.AddCondition(Condition.HashEqual(account.Key, "maxCharSlot", account.MaxCharSlot));
            transi.HashIncrementAsync(account.Key, "maxCharSlot");
            var tr2 = transi.ExecuteAsync();
            account.MaxCharSlot++;
            player1.SendInfo("New Character Slot Unlocked!, go to Character selector to use them!");
        }

        public void UseItem(TickData time, int objId, int slot, Position pos, int sellMaxed)
        {
            using (TimedLock.Lock(_useLock))
            {
                //Log.Debug(objId + ":" + slot);
                var entity = Owner.GetEntity(objId);
                if (entity == null)
                {
                    Client.SendPacket(new InvResult() { Result = 1 });
                    return;
                }

                if (entity is Player && objId != Id)
                {
                    Client.SendPacket(new InvResult() { Result = 1 });
                    return;
                }

                if (entity is Player && (entity as Player).Owner is Marketplace)
                {
                    Client.SendPacket(new InvResult() { Result = 1 });
                    Client.Player.SendError("<Marketplace> Using an Item is restricted in the Marketplace!");
                    return;
                }

                var container = entity as IContainer;

                // eheh no more clearing BBQ loot bags
                if (this.Dist(entity) > 3)
                {
                    Client.SendPacket(new InvResult() { Result = 1 });
                    return;
                }

                var cInv = container?.Inventory.CreateTransaction();

                // get item
                Item item = null;
                foreach (var stack in Stacks.Where(stack => stack.Slot == slot))
                {
                    item = stack.Pop();

                    if (item == null)
                        return;

                    break;
                }
                if (item == null)
                {
                    if (container == null)
                        return;

                    item = cInv[slot];
                }

                if (item == null)
                    return;

                // make sure not trading and trying to cunsume item
                if (tradeTarget != null && item.Consumable)
                    return;
                Player player = this as Player;
                if (MP < item.MpCost)
                {
                    Client.SendPacket(new InvResult() { Result = 1 });
                    return;
                }

                // use item
                var slotType = 10;
                if (slot < cInv.Length)
                {
                    slotType = container.SlotTypes[slot];

                    if (item.TypeOfConsumable)
                    {
                        var gameData = CoreServerManager.Resources.GameData;
                        var db = CoreServerManager.Database;

                        if (item.Consumable)
                        {
                            Item successor = null;
                            if (item.SuccessorId != null)
                                successor = gameData.Items[gameData.IdToObjectType[item.SuccessorId]];
                            cInv[slot] = successor;

                            var trans = db.Conn.CreateTransaction();
                            if (container is GiftChest)
                                if (successor != null)
                                    db.SwapGift(Client.Account, item.ObjectType, successor.ObjectType, trans);
                                else
                                {
                                    player.SendError("Can't use items if they are in a Gift Chest.");
                                    return;
                                }
                        }

                        if (!Inventory.Execute(cInv)) // can result in the loss of an item if inv trans fails..
                        {
                            entity.ForceUpdate(slot);
                            return;
                        }

                        if (slotType > 0)
                        {
                            FameCounter.UseAbility();
                        }
                        else
                        {
                            if (item.ActivateEffects.Any(eff => eff.Effect == ActivateEffects.Heal ||
                                                                eff.Effect == ActivateEffects.HealNova ||
                                                                eff.Effect == ActivateEffects.Magic ||
                                                                eff.Effect == ActivateEffects.MagicNova))
                            {
                                FameCounter.DrinkPot();
                            }
                        }

                        Activate(time, item, slot, pos, objId, sellMaxed);
                        return;
                    }

                    if (slotType > 0)
                    {
                        FameCounter.UseAbility();
                    }
                }
                else
                {
                    FameCounter.DrinkPot();
                }

                //Log.Debug(item.SlotType + ":" + slotType);
                if (item.InvUse)
                    Activate(time, item, slot, pos, objId, sellMaxed);

                if (item.Consumable || item.SlotType == slotType)
                    Activate(time, item, slot, pos, objId, sellMaxed);
                else
                    Client.SendPacket(new InvResult() { Result = 1 });
            }
        }

        private static void ActivateHealHp(Player player, int amount)
        {
            if (amount <= 0)
                return;

            var maxHp = player.Stats[0];
            var newHp = Math.Min(maxHp, player.HP + amount);
            if (newHp == player.HP)
                return;

            player.Owner.BroadcastIfVisible(new ShowEffect()
            {
                EffectType = EffectType.Potion,
                TargetObjectId = player.Id,
                Color = new ARGB(0xffffffff)
            }, player, PacketPriority.Low);
            player.Owner.BroadcastIfVisible(new Notification()
            {
                Color = new ARGB(0xff00ff00),
                ObjectId = player.Id,
                Message = "+" + (newHp - player.HP)
            }, player, PacketPriority.Low);

            player.HP = newHp;
        }

        private static void ActivateHealMp(Player player, int amount)
        {
            var maxMp = player.Stats[1];
            var newMp = Math.Min(maxMp, player.MP + amount);
            if (newMp == player.MP)
                return;

            player.Owner.BroadcastIfVisible(new ShowEffect()
            {
                EffectType = EffectType.Potion,
                TargetObjectId = player.Id,
                Color = new ARGB(0xffffffff)
            }, player, PacketPriority.Low);
            player.Owner.BroadcastIfVisible(new Notification()
            {
                Color = new ARGB(0xff9000ff),
                ObjectId = player.Id,
                Message = "+" + (newMp - player.MP)
            }, player, PacketPriority.Low);

            player.MP = newMp;
        }

        private void Activate(TickData time, Item item, int slot, Position target, int objId, int sellmaxed)
        {
            var playeren = this as Player;
            MP -= item.MpCost;

            var entity1 = Owner.GetEntity(objId);

            if (entity1 is GiftChest)
            {
                playeren.SendError("You can't use items in Gift Chests");
                return;
            }

            foreach (var eff in item.ActivateEffects)
            {
                switch (eff.Effect)
                {
                    case ActivateEffects.UpgradeStat:
                        AEUpgradeStat(time, item, target, objId, slot, eff);
                        break;

                    case ActivateEffects.UpgradeActivate:
                        AEUpgradeActivate(time, item, target, objId, slot, eff);
                        break;

                    case ActivateEffects.Fame:
                        AEAddFame(time, item, target, eff);
                        break;

                    case ActivateEffects.XPBoost:
                        AEXPBoost(time, item, target, slot, objId, eff);
                        break;

                    case ActivateEffects.ResetSkillTree:
                        AEResetSkillTree(time, item, target, slot, objId, eff);
                        break;

                    case ActivateEffects.MagicDust:
                        AEMagicDust(time, item, target, slot, objId, eff);
                        break;

                    case ActivateEffects.SpecialDust:
                        AESpecialDust(time, item, target, slot, objId, eff);
                        break;

                    case ActivateEffects.MiscellaneousDust:
                        AEMiscellaneousDust(time, item, target, slot, objId, eff);
                        break;

                    case ActivateEffects.ItemDust:
                        AEItemDust(time, item, target, slot, objId, eff);
                        break;

                    case ActivateEffects.PotionDust:
                        AEPotionDust(time, item, target, slot, objId, eff);
                        break;

                    case ActivateEffects.UnlockChest:
                        AEUnlockChest(time, item, target, slot, objId, eff);
                        break;

                    case ActivateEffects.UnlockSlotChar:
                        AEUnlockSlotChar(time, item, target, slot, objId, eff);
                        break;

                    case ActivateEffects.LDBoost:
                        AELDBoost(time, item, target, eff);
                        break;

                    case ActivateEffects.GenericActivate:
                        AEGenericActivate(time, item, target, eff);
                        break;

                    case ActivateEffects.Create:
                        AECreate(time, item, target, slot, eff);
                        break;

                    case ActivateEffects.Dye:
                        AEDye(time, item, target, eff);
                        break;

                    case ActivateEffects.Shoot:
                        AEShoot(time, item, target, eff);
                        break;

                    case ActivateEffects.IncrementStat:
                        AEIncrementStat(time, item, target, eff, objId, slot, sellmaxed);
                        break;

                    case ActivateEffects.Heal:
                        AEHeal(time, item, target, eff);
                        break;

                    case ActivateEffects.Magic:
                        AEMagic(time, item, target, eff);
                        break;

                    case ActivateEffects.HealNova:
                        AEHealNova(time, item, target, eff);
                        break;

                    case ActivateEffects.StatBoostSelf:
                        AEStatBoostSelf(time, item, target, eff);
                        break;

                    case ActivateEffects.StatBoostAura:
                        AEStatBoostAura(time, item, target, eff);
                        break;

                    case ActivateEffects.BulletNova:
                        AEBulletNova(time, item, target, eff);
                        break;

                    case ActivateEffects.ConditionEffectSelf:
                        AEConditionEffectSelf(time, item, target, eff);
                        break;

                    case ActivateEffects.ConditionEffectAura:
                        AEConditionEffectAura(time, item, target, eff);
                        break;

                    case ActivateEffects.Teleport:
                        AETeleport(time, item, target, eff);
                        break;

                    case ActivateEffects.PoisonGrenade:
                        AEPoisonGrenade(time, item, target, eff);
                        break;

                    case ActivateEffects.VampireBlast:
                        AEVampireBlast(time, item, target, eff);
                        break;

                    case ActivateEffects.Trap:
                        AETrap(time, item, target, eff);
                        break;

                    case ActivateEffects.StasisBlast:
                        StasisBlast(time, item, target, eff);
                        break;

                    case ActivateEffects.Pet:

                        #region Pet

                        Entity en = Resolve(CoreServerManager, eff.ObjectId);
                        en.Move(X, Y);
                        en.SetPlayerOwner(this);
                        Owner.EnterWorld(en);
                        Owner.Timers.Add(new WorldTimer(30 * 1000, (w, t) =>
                        {
                            w.LeaveWorld(en);
                        }));

                        #endregion Pet

                        break;

                    case ActivateEffects.Decoy:
                        AEDecoy(time, item, target, eff);
                        break;

                    case ActivateEffects.Lightning:
                        AELightning(time, item, target, eff);
                        break;

                    case ActivateEffects.UnlockPortal:
                        AEUnlockPortal(time, item, target, eff);
                        break;

                    case ActivateEffects.MagicNova:
                        AEMagicNova(time, item, target, eff);
                        break;

                    case ActivateEffects.ClearConditionEffectAura:
                        AEClearConditionEffectAura(time, item, target, eff);
                        break;

                    case ActivateEffects.RemoveNegativeConditions:
                        AERemoveNegativeConditions(time, item, target, eff);
                        break;

                    case ActivateEffects.ClearConditionEffectSelf:
                        AEClearConditionEffectSelf(time, item, target, eff);
                        break;

                    case ActivateEffects.RemoveNegativeConditionsSelf:
                        AERemoveNegativeConditionSelf(time, item, target, eff);
                        break;

                    case ActivateEffects.ShurikenAbility:
                        AEShurikenAbility(time, item, target, eff);
                        break;

                    case ActivateEffects.ShurikenAbilityBerserk:
                        AEShurikenAbilityBerserk(time, item, target, eff);
                        break;

                    case ActivateEffects.DazeBlast:
                        break;

                    case ActivateEffects.PermaPet:
                        AEPermaPet(time, item, target, eff);
                        break;

                    case ActivateEffects.Backpack:
                        AEBackpack(time, item, target, slot, objId, eff);
                        break;

                    default:
                        SLogger.Instance.Warn("Activate effect {0} not implemented.", eff.Effect);
                        break;
                }
            }
        }

        private void AEAddFame(TickData time, Item item, Position target, ActivateEffect eff)
        {
            if (Owner is Test || Client.Account == null)
                return;

            var acc = Client.Account;
            acc.Reload("fame");
            acc.Reload("totalFame");
            acc.Fame += eff.Amount;
            acc.TotalFame += eff.Amount;
            acc.FlushAsync();
            acc.Reload("fame");
            acc.Reload("totalFame");
            CurrentFame = acc.Fame;
            //Manager.Database.UpdateFame(acc, eff.Amount, null);
            /*Manager.Database.UpdateCurrency(acc, eff.Amount, CurrencyType.Fame, trans)
                .ContinueWith(t =>
                {
                    CurrentFame = acc.Fame;
                });
            trans.Execute();*/
        }

        private void AEBackpack(TickData time, Item item, Position target, int slot, int objId, ActivateEffect eff)
        {
            var entity = Owner.GetEntity(objId);
            var containerItem = entity as Container;
            if (HasBackpack)
            {
                SendInfo("You already have a Backpack!");
                if (containerItem != null)
                    containerItem.Inventory[slot] = item;
                else
                    Inventory[slot] = item;
                return;
            }
            HasBackpack = true;
        }

        private void AEBulletNova(TickData time, Item item, Position target, ActivateEffect eff)
        {
            var shoots = item.SpellProjectiles == 0 ? 20 : item.SpellProjectiles;
            var prjs = new Projectile[shoots];
            var prjDesc = item.Projectiles[0]; //Assume only one
            var batch = new OutgoingMessage[shoots + 1];
            for (var i = 0; i < shoots; i++)
            {
                var proj = CreateProjectile(prjDesc, item.ObjectType,
                    _random.Next(prjDesc.MinDamage, prjDesc.MaxDamage),
                    time.TotalElapsedMs, target, (float)(i * (Math.PI * 2) / shoots));
                Owner.EnterWorld(proj);
                FameCounter.Shoot(proj);
                batch[i] = new ServerPlayerShoot()
                {
                    BulletId = proj.ProjectileId,
                    OwnerId = Id,
                    ContainerType = item.ObjectType,
                    StartingPos = target,
                    Angle = proj.Angle,
                    Damage = (short)proj.Damage
                };
                prjs[i] = proj;
            }
            batch[shoots] = new ShowEffect()
            {
                EffectType = EffectType.Trail,
                Pos1 = target,
                TargetObjectId = Id,
                Color = new ARGB(eff.Color != 0 ? eff.Color : 0xFFFF00AA)
            };

            var players = Owner.Players
                .ValueWhereAsParallel(_ => _.DistSqr(this) < PlayerUpdate.VISIBILITY_RADIUS_SQR);
            for (var i = 0; i < players.Length; i++)
                players[i].Client.SendPackets(batch, PacketPriority.Low);
        }

        private void AEClearConditionEffectAura(TickData time, Item item, Position target, ActivateEffect eff)
        {
            this.AOE(eff.Range, true, player =>
            {
                var condition = eff.CheckExistingEffect;
                ConditionEffects conditions = 0;
                conditions |= (ConditionEffects)(1 << (Byte)condition.Value);
                if (!condition.HasValue || player.HasConditionEffect(conditions))
                {
                    player.ApplyConditionEffect(new ConditionEffect()
                    {
                        Effect = eff.ConditionEffect.Value,
                        DurationMS = 0
                    });
                }
            });
        }

        private void AEClearConditionEffectSelf(TickData time, Item item, Position target, ActivateEffect eff)
        {
            var condition = eff.CheckExistingEffect;
            ConditionEffects conditions = 0;

            if (condition.HasValue)
                conditions |= (ConditionEffects)(1 << (Byte)condition.Value);

            if (!condition.HasValue || HasConditionEffect(conditions))
            {
                ApplyConditionEffect(new ConditionEffect()
                {
                    Effect = eff.ConditionEffect.Value,
                    DurationMS = 0
                });
            }
        }

        private void AEConditionEffectAura(TickData time, Item item, Position target, ActivateEffect eff)
        {
            var duration = eff.DurationMS;
            var range = eff.Range;
            if (eff.UseWisMod)
            {
                duration = (int)(UseWisMod(eff.DurationSec) * 1000);
                range = UseWisMod(eff.Range);
            }

            this.AOE(range, true, player =>
            {
                player.ApplyConditionEffect(new ConditionEffect()
                {
                    Effect = eff.ConditionEffect.Value,
                    DurationMS = duration
                });
            });
            var color = 0xffffffff;
            if (eff.ConditionEffect.Value == ConditionEffectIndex.Damaging)
                color = 0xffff0000;
            Owner.BroadcastIfVisible(new ShowEffect()
            {
                EffectType = EffectType.AreaBlast,
                TargetObjectId = Id,
                Color = new ARGB(color),
                Pos1 = new Position() { X = range }
            }, this, PacketPriority.Low);
        }

        private void AEConditionEffectSelf(TickData time, Item item, Position target, ActivateEffect eff)
        {
            var duration = eff.DurationMS;
            if (eff.UseWisMod)
                duration = (int)(UseWisMod(eff.DurationSec) * 1000);

            ApplyConditionEffect(new ConditionEffect()
            {
                Effect = eff.ConditionEffect.Value,
                DurationMS = duration
            });
            Owner.BroadcastIfVisible(new ShowEffect()
            {
                EffectType = EffectType.AreaBlast,
                TargetObjectId = Id,
                Color = new ARGB(0xffffffff),
                Pos1 = new Position() { X = 1 }
            }, this, PacketPriority.Low);
        }

        private void AECreate(TickData time, Item item, Position target, int slot, ActivateEffect eff)
        {
            var gameData = CoreServerManager.Resources.GameData;

            if (Rank >= 60 && !(Owner is Vault) && Rank < 110)
            {
                SendError("You can't use Keys out of the Vault");
                Inventory[slot] = item;
                return;
            }

            if (!gameData.IdToObjectType.TryGetValue(eff.Id, out ushort objType) ||
                !gameData.Portals.ContainsKey(objType))
                return; // object not found, ignore

            var entity = Resolve(CoreServerManager, objType);
            var timeoutTime = gameData.Portals[objType].Timeout;

            entity.Move(X, Y);
            Owner.EnterWorld(entity);

            Owner.Timers.Add(new WorldTimer(timeoutTime * 1000, (world, t) => world.LeaveWorld(entity)));

            var openedByMsg = gameData.Portals[objType].DungeonName + " opened by " + Name + "!";
            Owner.Broadcast(new Notification
            {
                Color = new ARGB(0xFF00FF00),
                ObjectId = Id,
                Message = openedByMsg
            }, PacketPriority.Low);
            Owner.PlayersBroadcastAsParallel(_ => _.SendInfo(openedByMsg));
        }

        private void AEDecoy(TickData time, Item item, Position target, ActivateEffect eff)
        {
            var decoy = new Decoy(this, eff.DurationMS, 4);
            decoy.Move(X, Y);
            Owner.EnterWorld(decoy);
        }

        private void AEDye(TickData time, Item item, Position target, ActivateEffect eff)
        {
            if (item.Texture1 != 0)
                Texture1 = item.Texture1;
            if (item.Texture2 != 0)
                Texture2 = item.Texture2;
        }

        private void AEGenericActivate(TickData time, Item item, Position target, ActivateEffect eff)
        {
            var targetPlayer = eff.Target.Equals("player");
            var centerPlayer = eff.Center.Equals("player");
            var duration = eff.UseWisMod ? (int)(UseWisMod(eff.DurationSec) * 1000) : eff.DurationMS;
            var range = eff.UseWisMod
                ? UseWisMod(eff.Range)
                : eff.Range;

            if (eff.ConditionEffect != null)
                Owner.AOE(eff.Center.Equals("mouse") ? target : new Position { X = X, Y = Y }, range, targetPlayer, entity =>
                {
                    if (!entity.HasConditionEffect(ConditionEffects.Stasis) && !entity.HasConditionEffect(ConditionEffects.Invincible))
                    {
                        entity.ApplyConditionEffect(new ConditionEffect()
                        {
                            Effect = eff.ConditionEffect.Value,
                            DurationMS = duration
                        });
                    }
                });

            Owner.BroadcastIfVisible(new ShowEffect()
            {
                EffectType = (EffectType)eff.VisualEffect,
                TargetObjectId = Id,
                Color = new ARGB(eff.Color),
                Pos1 = centerPlayer ? new Position() { X = range } : target,
                Pos2 = new Position() { X = target.X - range, Y = target.Y }
            }, this, PacketPriority.Low);
        }

        private void AEHeal(TickData time, Item item, Position target, ActivateEffect eff)
        {
            if (!HasConditionEffect(ConditionEffects.Sick))
            {
                var healthAmount = eff.Amount;
                if (BigSkill9)
                    healthAmount = healthAmount / 2;
                ActivateHealHp(this, healthAmount);
            }
        }

        private void AEHealNova(TickData time, Item item, Position target, ActivateEffect eff)
        {
            var amount = eff.Amount;
            var range = eff.Range;
            if (eff.UseWisMod)
            {
                amount = (int)UseWisMod(eff.Amount, 0);
                range = UseWisMod(eff.Range);
            }

            this.AOE(range, true, player =>
            {
                if (!player.HasConditionEffect(ConditionEffects.Sick))
                    ActivateHealHp(player as Player, amount);
            });

            Owner.BroadcastIfVisible(new ShowEffect()
            {
                EffectType = EffectType.AreaBlast,
                TargetObjectId = Id,
                Color = new ARGB(0xffffffff),
                Pos1 = new Position() { X = range }
            }, this, PacketPriority.Low);
        }

        private void AEIncrementStat(TickData time, Item item, Position target, ActivateEffect eff, int objId, int slot, int sellMaxed)
        {
            var totalAllowed = 50 + (Client.Account.Rank * 2);
            var idx = StatsManager.GetStatIndex((StatDataType)eff.Stats);
            var statInfo = CoreServerManager.Resources.GameData.Classes[ObjectType].Stats;
            var statname = StatsManager.StatIndexToName(idx);
            var ent = Owner.GetEntity(objId);
            var container = ent as Container;
            var storeAmount = eff.Amount == 5 ? 1 : eff.Amount == 10 ? 2 : eff.Amount == 2 ? 2 : 1;
            if (Stats.Base[idx] < statInfo[idx].MaxValue)
            {
                Stats.Base[idx] += eff.Amount;
                if (Stats.Base[idx] >= statInfo[idx].MaxValue)
                {
                    Stats.Base[idx] = statInfo[idx].MaxValue;
                    return;
                }
            }

            if (!UpgradeEnabled && item.Maxy && Stats.Base[idx] >= statInfo[idx].MaxValue)
            {
                if (container != null)
                {
                    Stats.Base[idx] = statInfo[idx].MaxValue;
                    container.Inventory[slot] = item;
                }
                else
                {
                    Stats.Base[idx] = statInfo[idx].MaxValue;
                    Inventory[slot] = item;
                }
                SendError("You're maxed!");
                return;
            }
            else if (UpgradeEnabled && item.Maxy)
            {
                if (container != null)
                {
                    container.Inventory[slot] = item;
                }
                else
                {
                    Inventory[slot] = item;
                }
                SendError("You're maxed!");
                return;
            }

            if (statname == "MpRegen")
                statname = "Wisdom";
            else if (statname == "HpRegen")
                statname = "Vitality";
            else if (statname == "MaxHitPoints")
                statname = "Life";
            else if (statname == "MaxMagicPoints")
                statname = "Mana";


            if ((sellMaxed == 2) && UpgradeEnabled) //"Store" Selected where Supreme IS active
            {
                //Stats.Base[idx] = statInfo[idx].MaxValue + (idx == 0 ? 50 : idx == 1 ? 50 : 10); ??
                if (container != null)
                    container.Inventory[slot] = null;
                else
                    Inventory[slot] = null;
                var storedAmount = HandleTX(statname, storeAmount);
                CoreServerManager.Database.ReloadAccount(Client.Account);
                if (storedAmount == 998)
                {
                    int fameValue = idx < 2 ? 5 : 2;
                    fameValue += eff.Amount == 10 ? 5 : eff.Amount == 2 ? 2 : 0;
                    Client.Account.Reload("fame");
                    Client.Account.Reload("totalFame");
                    Client.Account.Fame += fameValue;
                    Client.Account.TotalFame += fameValue;

                    CurrentFame = Client.Account.Fame;
                    CoreServerManager.Database.ReloadAccount(Client.Account);
                    SendError($"Your {statname} is currently Full. Sold for {fameValue} fame.");
                }
                else if (storedAmount == 999)
                {
                    SendError($"An error has occured, try again later.");
                }
                else
                {
                    SendInfo($"Added {storeAmount} {statname} to your Potion Storage! [{storedAmount} / {totalAllowed}]");
                }

                return;
            }
            else if (Stats.Base[idx] >= statInfo[idx].MaxValue && (sellMaxed == 2) && !UpgradeEnabled) //"Store" Selected where Supreme NOT active
            {
                Stats.Base[idx] = statInfo[idx].MaxValue;
                if (container != null)
                    container.Inventory[slot] = null;
                else
                    Inventory[slot] = null;
                var storedAmount = HandleTX(statname, storeAmount);
                CoreServerManager.Database.ReloadAccount(Client.Account);
                if (storedAmount == 998)
                {
                    int fameValue = idx < 2 ? 5 : 2;
                    fameValue += eff.Amount == 10 ? 5 : eff.Amount == 2 ? 2 : 0;
                    Client.Account.Reload("fame");
                    Client.Account.Reload("totalFame");
                    Client.Account.Fame += fameValue;
                    Client.Account.TotalFame += fameValue;

                    CurrentFame = Client.Account.Fame;
                    CoreServerManager.Database.ReloadAccount(Client.Account);
                    SendError($"Your {statname} is currently Full. Sold for {fameValue} fame.");
                }
                else if (storedAmount == 999)
                {
                    SendError($"An error has occured, try again later.");
                }
                else
                {
                    SendInfo($"Added {storeAmount} {statname} to your Potion Storage! [{storedAmount} / {totalAllowed}]");
                }

                return;
            }

            if ((Stats.Base[idx] >= statInfo[idx].MaxValue + (idx == 0 ? 50 : idx == 1 ? 50 : 10)) && (sellMaxed == 1) && UpgradeEnabled) //"Sell" Selected where Supreme is active
            {
                Stats.Base[idx] = statInfo[idx].MaxValue + (idx == 0 ? 50 : idx == 1 ? 50 : 10);
                if (container != null)
                    container.Inventory[slot] = null;
                else
                    Inventory[slot] = null;
                int fameValue = idx < 2 ? 5 : 2;
                fameValue += eff.Amount == 10 ? 5 : eff.Amount == 2 ? 2 : 0;
                Client.Account.Reload("fame");
                Client.Account.Reload("totalFame");
                Client.Account.Fame += fameValue;
                Client.Account.TotalFame += fameValue;

                CurrentFame = Client.Account.Fame;
                CoreServerManager.Database.ReloadAccount(Client.Account);
                SendInfo($"Your {statname} got sold for {fameValue} Fame!");
                return;
            }
            else if ((Stats.Base[idx] >= statInfo[idx].MaxValue + (idx == 0 ? 50 : idx == 1 ? 50 : 10)) && (sellMaxed == 0) && UpgradeEnabled)//"Off" Selected where Supreme IS active
            {
                if (container != null)
                    container.Inventory[slot] = item;
                else
                    Inventory[slot] = item;
                Stats.Base[idx] = statInfo[idx].MaxValue + (idx == 0 ? 50 : idx == 1 ? 50 : 10);
                SendInfo("You're Maxed in this Stat!");
                return;
            }

            if (Stats.Base[idx] >= statInfo[idx].MaxValue && (sellMaxed == 1) && !UpgradeEnabled)//"Sell" Selected where Supreme NOT active
            {
                Stats.Base[idx] = statInfo[idx].MaxValue;
                if (container != null)
                    container.Inventory[slot] = null;
                else
                    Inventory[slot] = null;
                int fameValue = idx < 2 ? 5 : 2;
                fameValue += eff.Amount == 10 ? 5 : eff.Amount == 2 ? 2 : 0;

                Client.Account.Reload("fame");
                Client.Account.Reload("totalFame");
                Client.Account.Fame += fameValue;
                Client.Account.TotalFame += fameValue;
                CurrentFame = Client.Account.Fame;

                CoreServerManager.Database.ReloadAccount(Client.Account);
                SendInfo($"Your {statname} got sold for {fameValue} Fame!");
                return;
            }
            else if (Stats.Base[idx] >= statInfo[idx].MaxValue && (sellMaxed == 0) && !UpgradeEnabled)//"Off" Selected where Supreme NOT active
            {
                if (container != null)
                    container.Inventory[slot] = item;
                else
                    Inventory[slot] = item;
                Stats.Base[idx] = statInfo[idx].MaxValue;
                SendInfo("You're Maxed in this Stat!");
                return;
            }
            else
            {
                SendInfo($"Consumed Potion of {statname}.");
            }
        }

        private int HandleTX(string statname, int amount)
        {
            var maxAllowed = 50 + Client.Account.Rank * 2;
            switch (statname)
            {
                case "Wisdom":
                    if (Client.Account.SPSWisdomCount < maxAllowed)
                    {
                        Client.Account.SPSWisdomCount += amount;
                        return Client.Account.SPSWisdomCount;
                    }
                    return 998;
                case "Vitality":
                    if (Client.Account.SPSVitalityCount < maxAllowed)
                    {
                        Client.Account.SPSVitalityCount += amount;
                        return Client.Account.SPSVitalityCount;
                    }
                    return 998;
                case "Life":
                    if (Client.Account.SPSLifeCount < maxAllowed)
                    {
                        Client.Account.SPSLifeCount += amount;
                        return Client.Account.SPSLifeCount;
                    }
                    return 998;
                case "Mana":
                    if (Client.Account.SPSManaCount < maxAllowed)
                    {
                        Client.Account.SPSManaCount += amount;
                        return Client.Account.SPSManaCount;
                    }
                    return 998;
                case "Speed":
                    if (Client.Account.SPSSpeedCount < maxAllowed)
                    {
                        Client.Account.SPSSpeedCount += amount;
                        return Client.Account.SPSSpeedCount;
                    }
                    return 998;
                case "Attack":
                    if (Client.Account.SPSAttackCount < maxAllowed)
                    {
                        Client.Account.SPSAttackCount += amount;
                        return Client.Account.SPSAttackCount;
                    }
                    return 998;
                case "Defense":
                    if (Client.Account.SPSDefenseCount < maxAllowed)
                    {
                        Client.Account.SPSDefenseCount += amount;
                        return Client.Account.SPSDefenseCount;
                    }
                    return 998;
                case "Dexterity":
                    if (Client.Account.SPSDexterityCount < maxAllowed)
                    {
                        Client.Account.SPSDexterityCount += amount;
                        return Client.Account.SPSDexterityCount;
                    }
                    return 998;
            }
            return 999;
        }

        private void AELDBoost(TickData time, Item item, Position target, ActivateEffect eff)
        {
            //  if (LDBoostTime < 0 || (LDBoostTime > eff.DurationMS && eff.DurationMS >= 0))
            //      return;

            if (LDBoostTime < 0) //|| (LDBoostTime > eff.DurationMS && eff.DurationMS >= 0))
                return;

            if (LDBoostTime <= 0)
            {
                SendInfo2("Your Loot Drop Potion has been activated!");
            }
            if (LDBoostTime > 0)
            {
                SendInfo2("Your Loot Drop Potion has been stacked up!");
            }
            LDBoostTime = eff.DurationMS + LDBoostTime;
            InvokeStatChange(StatDataType.LDBoostTime, LDBoostTime / 1000, true);
        }

        private void AELightning(TickData time, Item item, Position target, ActivateEffect eff)
        {
            const double coneRange = Math.PI / 4;
            var mouseAngle = Math.Atan2(target.Y - Y, target.X - X);

            // get starting target
            var startTarget = this.GetNearestEntity(MaxAbilityDist, false, e => e is Enemy &&
                Math.Abs(mouseAngle - Math.Atan2(e.Y - Y, e.X - X)) <= coneRange);

            // no targets? bolt air animation
            if (startTarget == null)
            {
                var angles = new double[] { mouseAngle, mouseAngle - coneRange, mouseAngle + coneRange };
                for (var i = 0; i < 3; i++)
                {
                    var x = (int)(MaxAbilityDist * Math.Cos(angles[i])) + X;
                    var y = (int)(MaxAbilityDist * Math.Sin(angles[i])) + Y;
                    Owner.BroadcastIfVisible(new ShowEffect()
                    {
                        EffectType = EffectType.Trail,
                        TargetObjectId = Id,
                        Color = new ARGB(0xffff0088),
                        Pos1 = new Position()
                        {
                            X = x,
                            Y = y
                        },
                        Pos2 = new Position() { X = 350 }
                    }, target, PacketPriority.Low);
                }
                return;
            }

            var current = startTarget;
            var targets = new Entity[eff.MaxTargets];
            for (int i = 0; i < targets.Length; i++)
            {
                targets[i] = current;
                var next = current.GetNearestEntity(10, false, e =>
                {
                    if (!(e is Enemy) ||
                        e.HasConditionEffect(ConditionEffects.Invincible) ||
                        e.HasConditionEffect(ConditionEffects.Stasis) ||
                        Array.IndexOf(targets, e) != -1)
                        return false;

                    return true;
                });

                if (next == null)
                    break;

                current = next;
            }

            for (var i = 0; i < targets.Length; i++)
            {
                if (targets[i] == null)
                    break;

                var prev = i == 0 ? this : targets[i - 1];

                var damage = eff.UseWisMod ? UseWisMod(eff.TotalDamage) : eff.TotalDamage;

                (targets[i] as Enemy).Damage(this, time, (int)damage, false);

                if (eff.ConditionEffect != null)
                    targets[i].ApplyConditionEffect(new ConditionEffect()
                    {
                        Effect = eff.ConditionEffect.Value,
                        DurationMS = (int)(eff.EffectDuration * 1000)
                    });

                Owner.BroadcastIfVisible(new ShowEffect()
                {
                    EffectType = EffectType.Lightning,
                    TargetObjectId = prev.Id,
                    Color = new ARGB(0xffff0088),
                    Pos1 = new Position()
                    {
                        X = targets[i].X,
                        Y = targets[i].Y
                    },
                    Pos2 = new Position() { X = 350 }
                }, this, PacketPriority.Low);
            }
        }

        private void AEMagic(TickData time, Item item, Position target, ActivateEffect eff)
        {
            for (var i = 0; i < 4; i++)
            {
                var item1 = Inventory[i];

                if (item1 == null || !item1.Legendary && !item1.Revenge)
                    continue;

                if (item1.SonicBlaster)
                {
                    SonicBlaster(i);
                }
            }
            var pkts = new List<Packet>();
            var healthAmount = eff.Amount;
            if (BigSkill10)
                healthAmount = 75 * healthAmount / 100;
            ActivateHealMp(this, healthAmount);
        }

        private void AEMagicDust(TickData time, Item item, Position target, int slot, int objId, ActivateEffect eff)
        {
            //Potion dust = 0x4995, ItemDust = 0x4993, Miscellaneous dust = 0x4994, Special dust = 0x4996
            var gameData = CoreServerManager.Resources.GameData;
            var entity = Owner.GetEntity(objId);
            var containerItem = entity as Container;
            ushort itemValue;
            var itemchance = _random.NextDouble();
            if (Inventory.Data[slot] == null)
            {
                SendError("Something wrong happens with your Magic Dust!");
                return;
            }
            if (Inventory.Data[slot].Stack < Inventory.Data[slot].MaxStack)
                return;

            if (itemchance <= 0.05) //Le sumo el porcentaje que quiero que el proximo item tenga (0.05% Special Dust, 0.20% Miscellaneous Dust (0.05 + 0.15(Chance del MDust) = 0.20)
            {
                itemValue = 0x4996; //Special Dust
                if (containerItem != null)
                    containerItem.Inventory[slot] = gameData.Items[itemValue];
                else
                    Inventory[slot] = gameData.Items[itemValue];
                SendInfo($"Used a Magic Dust and obtained a {gameData.Items[itemValue].DisplayName ?? gameData.Items[itemValue].ObjectId}");
            }
            else if (itemchance <= 0.20)
            {
                itemValue = 0x4994; //Miscellaneous Dust
                if (containerItem != null)
                    containerItem.Inventory[slot] = gameData.Items[itemValue];
                else
                    Inventory[slot] = gameData.Items[itemValue];
                SendInfo($"Used a Magic Dust and obtained a {gameData.Items[itemValue].DisplayName ?? gameData.Items[itemValue].ObjectId}");
            }
            else if (itemchance <= 0.40)
            {
                itemValue = 0x4993; //Item Dust
                if (containerItem != null)
                    containerItem.Inventory[slot] = gameData.Items[itemValue];
                else
                    Inventory[slot] = gameData.Items[itemValue];
                SendInfo($"Used a Magic Dust and obtained a {gameData.Items[itemValue].DisplayName ?? gameData.Items[itemValue].ObjectId}");
            }
            else if (itemchance <= 1)
            {
                itemValue = 0x4995; //Potion Dust
                if (containerItem != null)
                    containerItem.Inventory[slot] = gameData.Items[itemValue];
                else
                    Inventory[slot] = gameData.Items[itemValue];
                SendInfo($"Used a Magic Dust and obtained a {gameData.Items[itemValue].DisplayName ?? gameData.Items[itemValue].ObjectId}");
            }
            Inventory.Data[slot] = null;
            InvokeStatChange((StatDataType)((int)StatDataType.InventoryData0 + slot), Inventory.Data[slot]?.GetData() ?? "{}");
        }

        private void AEMagicNova(TickData time, Item item, Position target, ActivateEffect eff)
        {
            this.AOE(eff.Range, true, player =>
                ActivateHealMp(player as Player, eff.Amount));

            Owner.BroadcastIfVisible(new ShowEffect()
            {
                EffectType = EffectType.AreaBlast,
                TargetObjectId = Id,
                Color = new ARGB(0xffffffff),
                Pos1 = new Position() { X = eff.Range }
            }, this, PacketPriority.Low);
        }

        private void AEMiscellaneousDust(TickData time, Item item, Position target, int slot, int objId, ActivateEffect eff)
        {
            var gameData = CoreServerManager.Resources.GameData;
            ushort itemValue;
            var entity = Owner.GetEntity(objId);
            var container = entity as Container;
            var itemchance = _random.Next(0, 4);
            switch (itemchance)
            {
                case 0:
                    itemValue = 0xc6c; //Backpack
                    if (container != null)
                        container.Inventory[slot] = gameData.Items[itemValue];
                    else
                        Inventory[slot] = gameData.Items[itemValue];
                    SendInfo($"Used a Miscellaneous Dust and obtained a {gameData.Items[itemValue].DisplayName ?? gameData.Items[itemValue].ObjectId}");
                    break;

                case 1:
                    itemValue = 0xc6b; //XP Booster 20 min
                    if (container != null)
                        container.Inventory[slot] = gameData.Items[itemValue];
                    else
                        Inventory[slot] = gameData.Items[itemValue];
                    SendInfo($"Used a Miscellaneous Dust and obtained a {gameData.Items[itemValue].DisplayName ?? gameData.Items[itemValue].ObjectId}");
                    break;

                case 2:
                    itemValue = 0xc69; //Loot Drop Potion
                    if (container != null)
                        container.Inventory[slot] = gameData.Items[itemValue];
                    else
                        Inventory[slot] = gameData.Items[itemValue];
                    SendInfo($"Used a Miscellaneous Dust and obtained a {gameData.Items[itemValue].DisplayName ?? gameData.Items[itemValue].ObjectId}");
                    break;

                case 3:
                    itemValue = 0x32a; //Char Slot Unlocker
                    if (container != null)
                        container.Inventory[slot] = gameData.Items[itemValue];
                    else
                        Inventory[slot] = gameData.Items[itemValue];
                    SendInfo($"Used a Miscellaneous Dust and obtained a {gameData.Items[itemValue].DisplayName ?? gameData.Items[itemValue].ObjectId}");
                    break;

                case 4:
                    itemValue = 0x32b; //Vault Chest Unlocker
                    if (container != null)
                        container.Inventory[slot] = gameData.Items[itemValue];
                    else
                        Inventory[slot] = gameData.Items[itemValue];
                    SendInfo($"Used a Miscellaneous Dust and obtained a {gameData.Items[itemValue].DisplayName ?? gameData.Items[itemValue].ObjectId}");
                    break;
            }
        }

        private void AEPermaPet(TickData time, Item item, Position target, ActivateEffect eff)
        {
            var type = CoreServerManager.Resources.GameData.IdToObjectType[eff.ObjectId];
            var desc = CoreServerManager.Resources.GameData.ObjectDescs[type];
            //Log.Debug(desc.ObjectType);
            PetId = desc.ObjectType;
            SpawnPetIfAttached(Owner);
            //Log.Debug("hey!");
        }

        private void AEPoisonGrenade(TickData time, Item item, Position target, ActivateEffect eff)
        {
            if (MathsUtils.DistSqr(target.X, target.Y, X, Y) > MaxAbilityDist * MaxAbilityDist) return;
            var impDamage = eff.ImpactDamage;
            if (eff.UseWisMod)
            {
                impDamage = (int)UseWisMod(eff.ImpactDamage);
                PoisonWis = true;
            }

            Owner.BroadcastIfVisible(new ShowEffect()
            {
                EffectType = EffectType.Throw,
                Color = new ARGB(eff.Color != 0 ? eff.Color : 0xffffffff),
                TargetObjectId = Id,
                Pos1 = target,
                Duration = eff.ThrowTime / 1000
            }, this, PacketPriority.Low);

            var x = new Placeholder(CoreServerManager, eff.ThrowTime * 1000);
            x.Move(target.X, target.Y);
            Owner.EnterWorld(x);
            Owner.Timers.Add(new WorldTimer(eff.ThrowTime, (world, t) =>
            {
                world.BroadcastIfVisible(new ShowEffect()
                {
                    EffectType = EffectType.AreaBlast,
                    Color = new ARGB(eff.Color != 0 ? eff.Color : 0xffffffff),
                    TargetObjectId = x.Id,
                    Pos1 = new Position() { X = eff.Radius },
                    Pos2 = new Position() { X = Id, Y = 255 }
                }, x, PacketPriority.Low);

                world.AOE(target, eff.Radius, false, entity =>
                {
                    PoisonEnemy(world, (Enemy)entity, eff);
                    ((Enemy)entity).Damage(this, time, impDamage, true);
                });
            }));
        }

        private void AEPotionDust(TickData time, Item item, Position target, int slot, int objId, ActivateEffect eff)
        {
            ushort[] _commonPotions = { 0xa4c, 0xa1f, 0xa20, 0xa35, 0xa34, 0xa21 };
            var gameData = CoreServerManager.Resources.GameData;
            var entity = Owner.GetEntity(objId);
            var container = entity as Container;
            ushort itemValue = 0x0;
            double potionRoll = _random.NextDouble();
            if (potionRoll <= 0.025)
            {
                //Potion of Life
                if (container != null)
                    container.Inventory[slot] = gameData.Items[0xae9];
                else
                    Inventory[slot] = gameData.Items[0xae9];
                SendInfo($"Used a Potion Dust and obtained a {gameData.Items[0xae9].DisplayName ?? gameData.Items[0xae9].ObjectId}");
            }
            else if (potionRoll <= 0.05)
            {
                //Potion of Mana
                if (container != null)
                    container.Inventory[slot] = gameData.Items[0xaea];
                else
                    Inventory[slot] = gameData.Items[0xaea];
                SendInfo($"Used a Potion Dust and obtained a {gameData.Items[0xaea].DisplayName ?? gameData.Items[0xaea].ObjectId}");
            }
            else
            {
                // Common potion
                var random = _random.Next(0, _commonPotions.Length);
                switch (random)
                {
                    case 0:
                        itemValue = _commonPotions[random];
                        if (container != null)
                            container.Inventory[slot] = gameData.Items[itemValue];
                        else
                            Inventory[slot] = gameData.Items[itemValue];
                        SendInfo($"Used a Potion Dust and obtained a {gameData.Items[itemValue].DisplayName ?? gameData.Items[itemValue].ObjectId}");
                        break;

                    case 1:
                        itemValue = _commonPotions[random];
                        if (container != null)
                            container.Inventory[slot] = gameData.Items[itemValue];
                        else
                            Inventory[slot] = gameData.Items[itemValue];
                        SendInfo($"Used a Potion Dust and obtained a {gameData.Items[itemValue].DisplayName ?? gameData.Items[itemValue].ObjectId}");
                        break;

                    case 2:
                        itemValue = _commonPotions[random];
                        if (container != null)
                            container.Inventory[slot] = gameData.Items[itemValue];
                        else
                            Inventory[slot] = gameData.Items[itemValue];
                        SendInfo($"Used a Potion Dust and obtained a {gameData.Items[itemValue].DisplayName ?? gameData.Items[itemValue].ObjectId}");
                        break;

                    case 3:
                        itemValue = _commonPotions[random];
                        if (container != null)
                            container.Inventory[slot] = gameData.Items[itemValue];
                        else
                            Inventory[slot] = gameData.Items[itemValue];
                        SendInfo($"Used a Potion Dust and obtained a {gameData.Items[itemValue].DisplayName ?? gameData.Items[itemValue].ObjectId}");
                        break;

                    case 4:
                        itemValue = _commonPotions[random];
                        if (container != null)
                            container.Inventory[slot] = gameData.Items[itemValue];
                        else
                            Inventory[slot] = gameData.Items[itemValue];
                        SendInfo($"Used a Potion Dust and obtained a {gameData.Items[itemValue].DisplayName ?? gameData.Items[itemValue].ObjectId}");
                        break;

                    case 5:
                        itemValue = _commonPotions[random];
                        if (container != null)
                            container.Inventory[slot] = gameData.Items[itemValue];
                        else
                            Inventory[slot] = gameData.Items[itemValue];
                        SendInfo($"Used a Potion Dust and obtained a {gameData.Items[itemValue].DisplayName ?? gameData.Items[itemValue].ObjectId}");
                        break;
                }
            }
        }

        private void AERemoveNegativeConditions(TickData time, Item item, Position target, ActivateEffect eff)
        {
            this.AOE(eff.Range, true, player => player.ApplyConditionEffect(NegativeEffs));
            Owner.BroadcastIfVisible(new ShowEffect()
            {
                EffectType = EffectType.AreaBlast,
                TargetObjectId = Id,
                Color = new ARGB(0xffffffff),
                Pos1 = new Position() { X = eff.Range }
            }, this, PacketPriority.Low);
        }

        private void AERemoveNegativeConditionSelf(TickData time, Item item, Position target, ActivateEffect eff)
        {
            ApplyConditionEffect(NegativeEffs);
            Owner.BroadcastIfVisible(new ShowEffect()
            {
                EffectType = EffectType.AreaBlast,
                TargetObjectId = Id,
                Color = new ARGB(0xffffffff),
                Pos1 = new Position() { X = 1 }
            }, this, PacketPriority.Low);
        }

        private void AEShoot(TickData time, Item item, Position target, ActivateEffect eff)
        {
            var arcGap = item.ArcGap * Math.PI / 180;
            var startAngle = Math.Atan2(target.Y - Y, target.X - X) - (item.NumProjectiles - 1) / 2 * arcGap;
            var prjDesc = item.Projectiles[0]; //Assume only one

            var sPkts = new OutgoingMessage[item.NumProjectiles];
            for (var i = 0; i < item.NumProjectiles; i++)
            {
                var proj = CreateProjectile(prjDesc, item.ObjectType, Stats.GetAttackDamage(prjDesc.MinDamage, prjDesc.MaxDamage, true), time.TotalElapsedMs, new Position() { X = X, Y = Y }, (float)(startAngle + arcGap * i));
                Owner.EnterWorld(proj);
                sPkts[i] = new AllyShoot()
                {
                    OwnerId = Id,
                    Angle = proj.Angle,
                    ContainerType = item.ObjectType,
                    BulletId = proj.ProjectileId
                };
                FameCounter.Shoot(proj);
            }

            for (var i = 0; i < item.NumProjectiles; i++)
                Owner.BroadcastIfVisible(sPkts[i], this, PacketPriority.Low);
        }

        private void AEShurikenAbility(TickData time, Item item, Position target, ActivateEffect eff)
        {
            if (!HasConditionEffect(ConditionEffects.NinjaSpeedy))
            {
                ApplyConditionEffect(ConditionEffectIndex.NinjaSpeedy);
                return;
            }

            if (MP >= item.MpEndCost)
            {
                MP -= item.MpEndCost;
                AEShoot(time, item, target, eff);
            }

            ApplyConditionEffect(ConditionEffectIndex.NinjaSpeedy, 0);
        }

        private void AEShurikenAbilityBerserk(TickData time, Item item, Position target, ActivateEffect eff)
        {
            if (!HasConditionEffect(ConditionEffects.Berserk))
            {
                ApplyConditionEffect(ConditionEffectIndex.Berserk);
                return;
            }

            if (MP >= item.MpEndCost)
            {
                MP -= item.MpEndCost;
                AEShoot(time, item, target, eff);
            }

            ApplyConditionEffect(ConditionEffectIndex.Berserk, 0);
        }

        private void AEShurikenAbilityDamaging(TickData time, Item item, Position target, ActivateEffect eff)
        {
            if (!HasConditionEffect(ConditionEffects.NinjaDamaging))
            {
                ApplyConditionEffect(ConditionEffectIndex.NinjaDamaging);
                return;
            }

            if (MP >= item.MpEndCost)
            {
                MP -= item.MpEndCost;
                AEShoot(time, item, target, eff);
            }

            ApplyConditionEffect(ConditionEffectIndex.NinjaDamaging, 0);
        }

        private void AEStatBoostAura(TickData time, Item item, Position target, ActivateEffect eff)
        {
            var idx = StatsManager.GetStatIndex((StatDataType)eff.Stats);
            var amount = eff.Amount;
            var duration = eff.DurationMS;
            var range = eff.Range;
            if (eff.UseWisMod)
            {
                amount = (int)UseWisMod(eff.Amount, 0);
                duration = (int)(UseWisMod(eff.DurationSec) * 1000);
                range = UseWisMod(eff.Range);
            }

            this.AOE(range, true, player =>
            {
                if (player.HasConditionEffect(ConditionEffects.HPBoost))
                {
                    return;
                }

                if (!player.HasConditionEffect(ConditionEffects.HPBoost))
                {
                    Owner.Timers.Add(new WorldTimer(0, (world, t) => player.ApplyConditionEffect(ConditionEffectIndex.HPBoost, duration)));
                }

                ((Player)player).Stats.Boost.ActivateBoost[idx].Push(amount, false);
                ((Player)player).Stats.ReCalculateValues();

                // hack job to allow instant heal of nostack boosts
                //if (eff.NoStack && amount > 0 && idx == 0)
                //{
                //    ((Player)player).HP = Math.Min(((Player)player).Stats[0], ((Player)player).HP + amount);
                //}

                Owner.Timers.Add(new WorldTimer(duration, (world, t) =>
                {
                    ((Player)player).Stats.Boost.ActivateBoost[idx].Pop(amount, false);
                    ((Player)player).Stats.ReCalculateValues();
                }));
            });

            Owner.BroadcastIfVisible(new ShowEffect()
            {
                EffectType = EffectType.AreaBlast,
                TargetObjectId = Id,
                Color = new ARGB(0xffffffff),
                Pos1 = new Position() { X = range }
            }, this, PacketPriority.Low);
        }

        private void AEStatBoostSelf(TickData time, Item item, Position target, ActivateEffect eff)
        {
            var idx = StatsManager.GetStatIndex((StatDataType)eff.Stats);
            var s = eff.Amount;
            Stats.Boost.ActivateBoost[idx].Push(s, false);
            Stats.ReCalculateValues();
            Owner.Timers.Add(new WorldTimer(eff.DurationMS, (world, t) =>
            {
                Stats.Boost.ActivateBoost[idx].Pop(s, false);
                Stats.ReCalculateValues();
            }));

            Owner.BroadcastIfVisible(new ShowEffect()
            {
                EffectType = EffectType.Potion,
                TargetObjectId = Id,
                Color = new ARGB(0xffffffff)
            }, this, PacketPriority.Low);
        }

        private void AETeleport(TickData time, Item item, Position target, ActivateEffect eff)
        {
            TeleportPosition(time, target, true);
        }

        private void AETrap(TickData time, Item item, Position target, ActivateEffect eff)
        {
            Owner.BroadcastIfVisible(new ShowEffect()
            {
                EffectType = EffectType.Throw,
                Color = new ARGB(0xff9000ff),
                TargetObjectId = Id,
                Pos1 = target
            }, target, PacketPriority.Low);

            Owner.Timers.Add(new WorldTimer(1500, (world, t) =>
            {
                var trap = new Trap(this, eff.Radius, eff.TotalDamage, eff.ConditionEffect ?? ConditionEffectIndex.Slowed, eff.EffectDuration);
                trap.Move(target.X, target.Y);
                world.EnterWorld(trap);
            }));
        }

        private void AEUnlockPortal(TickData time, Item item, Position target, ActivateEffect eff)
        {
            var gameData = CoreServerManager.Resources.GameData;

            // find locked portal
            var portals = Owner.StaticObjects
                .ValueWhereAsParallel(_ => _ is Portal
                && _.ObjectDesc.ObjectId.Equals(eff.LockedName)
                && _.DistSqr(this) <= 9d)
                .Select(_ => _ as Portal);
            if (!portals.Any())
                return;
            var portal = portals.Aggregate(
                (curmin, x) => curmin == null || x.DistSqr(this) < curmin.DistSqr(this) ? x : curmin);
            if (portal == null)
                return;

            // get proto of world
            if (!CoreServerManager.Resources.Worlds.Data.TryGetValue(eff.DungeonName, out ProtoWorld proto))
            {
                SLogger.Instance.Error("Unable to unlock portal. \"" + eff.DungeonName + "\" does not exist.");
                return;
            }

            if (proto.portals == null || proto.portals.Length < 1)
            {
                SLogger.Instance.Error("World is not associated with any portals.");
                return;
            }

            // create portal of unlocked world
            var portalType = (ushort)proto.portals[0];
            if (!(Resolve(CoreServerManager, portalType) is Portal uPortal))
            {
                SLogger.Instance.Error("Error creating portal: {0}", portalType);
                return;
            }

            var portalDesc = gameData.Portals[portal.ObjectType];
            var uPortalDesc = gameData.Portals[portalType];

            // create world
            World world;
            if (proto.id < 0)
                world = CoreServerManager.WorldManager.GetWorld(proto.id);
            else
            {
                DynamicWorld.TryGetWorld(proto, Client, out world);
                world = CoreServerManager.WorldManager.AddWorld(world ?? new World(proto));
            }
            uPortal.WorldInstance = world;

            // swap portals
            if (!portalDesc.NexusPortal || !CoreServerManager.WorldManager.PortalMonitor.RemovePortal(portal))
                Owner.LeaveWorld(portal);
            uPortal.Move(portal.X, portal.Y);
            uPortal.Name = uPortalDesc.DisplayId;
            var uPortalPos = new Position() { X = portal.X - .5f, Y = portal.Y - .5f };
            if (!uPortalDesc.NexusPortal || !CoreServerManager.WorldManager.PortalMonitor.AddPortal(world.Id, uPortal, uPortalPos))
                Owner.EnterWorld(uPortal);

            // setup timeout
            if (!uPortalDesc.NexusPortal)
            {
                var timeoutTime = gameData.Portals[portalType].Timeout;
                Owner.Timers.Add(new WorldTimer(timeoutTime * 1000, (w, t) => w.LeaveWorld(uPortal)));
            }

            // announce
            Owner.Broadcast(new Notification
            {
                Color = new ARGB(0xFF00FF00),
                ObjectId = Id,
                Message = "Unlocked by " + Name
            }, PacketPriority.Low);
            Owner.PlayersBroadcastAsParallel(_ => _.SendInfo($"{world.SBName} unlocked by {Name}!"));
        }

        private void AEUpgradeActivate(TickData time, Item item, Position target, int objId, int slot, ActivateEffect eff)
        {
            var playerDesc = CoreServerManager.Resources.GameData.Classes[ObjectType];
            var maxed = playerDesc.Stats.Where((t, i) => Stats.Base[i] >= t.MaxValue).Count();
            var entity = Owner.GetEntity(objId);
            var container = entity as Container;
            if (maxed < 8)
            {
                SendError("You must be 8/8 to Upgrade.");
                if (container != null)
                    container.Inventory[slot] = item;
                else
                    Inventory[slot] = item;
                return;
            }
            if (UpgradeEnabled)
            {
                SendInfo("You already have your Character Upgraded.");
                if (container != null)
                    container.Inventory[slot] = item;
                else
                    Inventory[slot] = item;
                return;
            }
            SendInfo("Your Character has been Upgraded successfully.");
            UpgradeEnabled = true;
            var chr = Client.Character;
            chr.UpgradeEnabled = UpgradeEnabled;
        }

        private void AEUpgradeStat(TickData time, Item item, Position target, int objId, int slot, ActivateEffect eff)
        {
            var entity = Owner.GetEntity(objId);
            var container = entity as Container;
            if (UpgradeEnabled)
            {
                var idx = StatsManager.GetStatIndex((StatDataType)eff.Stats);
                var statname = StatsManager.StatIndexToName(idx);
                if (statname == "MpRegen")
                    statname = "Wisdom";
                else if (statname == "HpRegen")
                    statname = "Vitality";
                else if (statname == "MaxHitPoints")
                    statname = "Life";
                else if (statname == "MaxMagicPoints")
                    statname = "Mana";

                var statInfo = CoreServerManager.Resources.GameData.Classes[ObjectType].Stats;

                Stats.Base[idx] += eff.Amount;
                if (Stats.Base[idx] > statInfo[idx].MaxValue + (idx < 2 ? 50 : 10))
                {
                    Stats.Base[idx] = statInfo[idx].MaxValue + (idx < 2 ? 50 : 10);
                    SendInfo("You're maxed.");
                    if (container != null)
                        container.Inventory[slot] = item;
                    else
                        Inventory[slot] = item;
                    return;
                }

                SendInfo($"Soul of {statname} consumed. {statInfo[idx].MaxValue + (idx < 2 ? 50 : 10) - Stats.Base[idx]} left to Max.");
            }
            else
            {
                SendInfo("A character that isn't Upgraded can't use Soul Potions.");
                if (container != null)
                    container.Inventory[slot] = item;
                else
                    Inventory[slot] = item;
                return;
            }
        }

        private void AEVampireBlast(TickData time, Item item, Position target, ActivateEffect eff)
        {
            var pkts = new List<OutgoingMessage>
            {
                new ShowEffect()
                {
                    EffectType = EffectType.Trail,
                    TargetObjectId = Id,
                    Pos1 = target,
                    Color = new ARGB(0xFFFF0000)
                },
                new ShowEffect
                {
                    EffectType = EffectType.Diffuse,
                    Color = new ARGB(0xFFFF0000),
                    TargetObjectId = Id,
                    Pos1 = target,
                    Pos2 = new Position { X = target.X + eff.Radius, Y = target.Y }
                }
            };

            Owner.BroadcastIfVisible(pkts[0], target, PacketPriority.Low);
            Owner.BroadcastIfVisible(pkts[1], target, PacketPriority.Low);

            var totalDmg = 0;
            var effDamage = eff.UseWisMod ? UseWisMod(eff.TotalDamage) : eff.TotalDamage;
            var enemies = new List<Enemy>();
            Owner.AOE(target, eff.Radius, false, enemy =>
            {
                enemies.Add(enemy as Enemy);
                totalDmg += (enemy as Enemy).Damage(this, time, (int)effDamage, false);
            });

            var players = new List<Player>();
            this.AOE(eff.Radius, true, player =>
            {
                if (!player.HasConditionEffect(ConditionEffects.Sick))
                {
                    players.Add(player as Player);
                    ActivateHealHp(player as Player, totalDmg);
                }
            });

            if (enemies.Count > 0)
            {
                var rand = new Random();
                for (var i = 0; i < 5; i++)
                {
                    var a = enemies[rand.Next(0, enemies.Count)];
                    var b = players[rand.Next(0, players.Count)];

                    Owner.BroadcastIfVisible(new ShowEffect()
                    {
                        EffectType = EffectType.Flow,
                        TargetObjectId = b.Id,
                        Pos1 = new Position() { X = a.X, Y = a.Y },
                        Color = new ARGB(0xffffffff)
                    }, target, PacketPriority.Low);
                }
            }
        }

        private void AEXPBoost(TickData time, Item item, Position target, int slot, int objId, ActivateEffect eff)
        {
            if (XPBoostTime < 0 || (XPBoostTime > eff.DurationMS && eff.DurationMS >= 0))
                return;

            var entity = Owner.GetEntity(objId);
            var containerItem = entity as Container;

            if (XPBoostTime > 0 && XPBoosted)
            {
                SendInfo("You already have a XPBooster activated!");
                if (containerItem != null)
                    containerItem.Inventory[slot] = item;
                else
                    Inventory[slot] = item;
                return;
            }
            if (Level >= 20)
            {
                SendInfo("You're level 20!");
                if (containerItem != null)
                    containerItem.Inventory[slot] = item;
                else
                    Inventory[slot] = item;
                return;
            }
            XPBoostTime = eff.DurationMS;
            XPBoosted = true;
            InvokeStatChange(StatDataType.XPBoostTime, XPBoostTime / 1000, true);
        }

        private void HealingPlayersPoison(World world, Player player, ActivateEffect eff)
        {
            var remainingHeal = eff.TotalDamage;
            var perHeal = eff.TotalDamage * 1000 / eff.DurationMS;

            WorldTimer tmr = null;
            var x = 0;

            bool healTick(World w, TickData t)
            {
                if (player.Owner == null || w == null)
                    return true;

                if (x % 4 == 0) // make sure to change this if timer delay is changed
                {
                    var thisHeal = perHeal;
                    if (remainingHeal < thisHeal)
                        thisHeal = remainingHeal;

                    ActivateHealHp(player, thisHeal);

                    remainingHeal -= thisHeal;
                    if (remainingHeal <= 0)
                        return true;
                }
                x++;

                tmr.Reset();
                return false;
            }

            tmr = new WorldTimer(250, healTick);
            world.Timers.Add(tmr);
        }

        private void PoisonEnemy(World world, Enemy enemy, ActivateEffect eff)
        {
            var remainingDmg = (int)StatsManager.GetDefenseDamage(enemy, eff.TotalDamage, enemy.ObjectDesc.Defense);
            var perDmg = remainingDmg * 1000 / eff.DurationMS;

            if (PoisonWis)
            {
                remainingDmg = (int)UseWisMod(remainingDmg);
                perDmg = (int)UseWisMod(perDmg);
            }

            WorldTimer tmr = null;
            var x = 0;

            bool poisonTick(World w, TickData t)
            {
                if (enemy.Owner == null || w == null)
                    return true;

                if (x % 4 == 0)
                {
                    var thisDmg = perDmg;
                    if (remainingDmg < thisDmg)
                        thisDmg = remainingDmg;

                    if (enemy == null)
                        return false;
                    enemy?.Damage(this, t, thisDmg, true);
                    remainingDmg -= thisDmg;
                    if (remainingDmg <= 0)
                        return true;
                }
                x++;

                tmr.Reset();
                return false;
            }

            tmr = new WorldTimer(250, poisonTick);
            world.Timers.Add(tmr);
        }

        private void StasisBlast(TickData time, Item item, Position target, ActivateEffect eff)
        {
            Owner.BroadcastIfVisible(new ShowEffect()
            {
                EffectType = EffectType.Concentrate,
                TargetObjectId = Id,
                Pos1 = target,
                Pos2 = new Position() { X = target.X + 3, Y = target.Y },
                Color = new ARGB(0xffffffff)
            }, target, PacketPriority.Low);

            Owner.AOE(target, 3, false, enemy =>
            {
                if (enemy.HasConditionEffect(ConditionEffects.StasisImmune))
                {
                    Owner.BroadcastIfVisible(new Notification()
                    {
                        ObjectId = enemy.Id,
                        Color = new ARGB(0xff00ff00),
                        Message = "Immune",
                        PlayerId = Id
                    }, enemy, PacketPriority.Low);
                }
                else if (!enemy.HasConditionEffect(ConditionEffects.Stasis) && !enemy.HasConditionEffect(ConditionEffects.StasisImmune))
                {
                    enemy.ApplyConditionEffect(ConditionEffectIndex.Stasis, eff.DurationMS);

                    Owner.Timers.Add(new WorldTimer(eff.DurationMS - 200, (world, t) => enemy.ApplyConditionEffect(ConditionEffectIndex.StasisImmune, 3200)));

                    Owner.BroadcastIfVisible(new Notification()
                    {
                        ObjectId = enemy.Id,
                        Color = new ARGB(0xffff0000),
                        Message = "Stasis",
                        PlayerId = Id
                    }, enemy, PacketPriority.Low);
                }
            });
        }

        private float UseWisMod(float value, int offset = 1)
        {
            double totalWisdom = Stats.Base[7] + Stats.Boost[7];

            if (totalWisdom < 50)
                return value;

            double m = (value < 0) ? -1 : 1;
            double n = (value * totalWisdom / 150) + (value * m);
            n = Math.Floor(n * Math.Pow(100, offset)) / Math.Pow(100, offset);
            if (n - (int)n * m >= 1 / Math.Pow(100, offset) * m)
            {
                return ((int)(n * 10)) / 10.0f;
            }

            return (int)n;
        }
    }
}
