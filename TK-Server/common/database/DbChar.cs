using System;

namespace common.database
{
    public class DbChar : RedisObject
    {
        public DbChar(DbAccount acc, int charId, bool isAsync = false)
        {
            Account = acc;
            CharId = charId;

            Init(acc.Database, "char." + acc.AccountId + "." + charId, null, isAsync);
        }

        public DbAccount Account { get; private set; }

        public bool BigSkill1 { get => GetValue<bool>("bigSkill1"); set => SetValue("bigSkill1", value); }
        public bool BigSkill10 { get => GetValue<bool>("bigSkill10"); set => SetValue("bigSkill10", value); }
        public bool BigSkill11 { get => GetValue<bool>("bigSkill11"); set => SetValue("bigSkill11", value); }
        public bool BigSkill12 { get => GetValue<bool>("bigSkill12"); set => SetValue("bigSkill12", value); }
        public bool BigSkill2 { get => GetValue<bool>("bigSkill2"); set => SetValue("bigSkill2", value); }
        public bool BigSkill3 { get => GetValue<bool>("bigSkill3"); set => SetValue("bigSkill3", value); }
        public bool BigSkill4 { get => GetValue<bool>("bigSkill4"); set => SetValue("bigSkill4", value); }
        public bool BigSkill5 { get => GetValue<bool>("bigSkill5"); set => SetValue("bigSkill5", value); }
        public bool BigSkill6 { get => GetValue<bool>("bigSkill6"); set => SetValue("bigSkill6", value); }
        public bool BigSkill7 { get => GetValue<bool>("bigSkill7"); set => SetValue("bigSkill7", value); }
        public bool BigSkill8 { get => GetValue<bool>("bigSkill8"); set => SetValue("bigSkill8", value); }
        public bool BigSkill9 { get => GetValue<bool>("bigSkill9"); set => SetValue("bigSkill9", value); }
        public int CharId { get; private set; }
        public DateTime CreateTime { get => GetValue<DateTime>("createTime"); set => SetValue("createTime", value); }
        public bool Dead { get => GetValue<bool>("dead"); set => SetValue("dead", value); }
        public int Experience { get => GetValue<int>("exp"); set => SetValue("exp", value); }
        public int Fame { get => GetValue<int>("fame"); set => SetValue("fame", value); }
        public byte[] FameStats { get => GetValue<byte[]>("fameStats"); set => SetValue("fameStats", value); }
        public int FinalFame { get => GetValue<int>("finalFame"); set => SetValue("finalFame", value); }
        public bool HasBackpack { get => GetValue<bool>("hasBackpack"); set => SetValue("hasBackpack", value); }
        public int HealthStackCount { get => GetValue<int>("hpPotCount"); set => SetValue("hpPotCount", value); }
        public int HP { get => GetValue<int>("hp"); set => SetValue("hp", value); }
        public ushort[] Items { get => GetValue<ushort[]>("items"); set => SetValue("items", value); }
        public ItemData[] Datas { get => GetValue<ItemData[]>("datas"); set => SetValue("datas", value); }
        public DateTime LastSeen { get => GetValue<DateTime>("lastSeen"); set => SetValue("lastSeen", value); }
        public int LDBoostTime { get => GetValue<int>("ldBoost"); set => SetValue("ldBoost", value); }
        public int Level { get => GetValue<int>("level"); set => SetValue("level", value); }
        public int MagicStackCount { get => GetValue<int>("mpPotCount"); set => SetValue("mpPotCount", value); }
        public bool MaxedAtt { get => GetValue<bool>("maxedAtt"); set => SetValue("maxedAtt", value); }
        public bool MaxedDef { get => GetValue<bool>("maxedDef"); set => SetValue("maxedDef", value); }
        public bool MaxedDex { get => GetValue<bool>("maxedDex"); set => SetValue("maxedDex", value); }
        public bool MaxedLife { get => GetValue<bool>("maxedLife"); set => SetValue("maxedLife", value); }
        public bool MaxedMana { get => GetValue<bool>("maxedMana"); set => SetValue("maxedMana", value); }
        public bool MaxedSpd { get => GetValue<bool>("maxedSpd"); set => SetValue("maxedSpd", value); }
        public bool MaxedVit { get => GetValue<bool>("maxedVit"); set => SetValue("maxedVit", value); }
        public bool MaxedWis { get => GetValue<bool>("maxedWis"); set => SetValue("maxedWis", value); }
        public int MP { get => GetValue<int>("mp"); set => SetValue("mp", value); }
        public ushort ObjectType { get => GetValue<ushort>("charType"); set => SetValue("charType", value); }
        public int PetId { get => GetValue<int>("petId"); set => SetValue("petId", value); }
        public int Points { get => GetValue<int>("points"); set => SetValue("points", value); }
        public int Skin { get => GetValue<int>("skin"); set => SetValue("skin", value); }
        public int SmallSkill1 { get => GetValue<int>("smallSkill1"); set => SetValue("smallSkill1", value); }
        public int SmallSkill10 { get => GetValue<int>("smallSkill10"); set => SetValue("smallSkill10", value); }
        public int SmallSkill11 { get => GetValue<int>("smallSkill11"); set => SetValue("smallSkill11", value); }
        public int SmallSkill12 { get => GetValue<int>("smallSkill12"); set => SetValue("smallSkill12", value); }
        public int SmallSkill2 { get => GetValue<int>("smallSkill2"); set => SetValue("smallSkill2", value); }
        public int SmallSkill3 { get => GetValue<int>("smallSkill3"); set => SetValue("smallSkill3", value); }
        public int SmallSkill4 { get => GetValue<int>("smallSkill4"); set => SetValue("smallSkill4", value); }
        public int SmallSkill5 { get => GetValue<int>("smallSkill5"); set => SetValue("smallSkill5", value); }
        public int SmallSkill6 { get => GetValue<int>("smallSkill6"); set => SetValue("smallSkill6", value); }
        public int SmallSkill7 { get => GetValue<int>("smallSkill7"); set => SetValue("smallSkill7", value); }
        public int SmallSkill8 { get => GetValue<int>("smallSkill8"); set => SetValue("smallSkill8", value); }
        public int SmallSkill9 { get => GetValue<int>("smallSkill9"); set => SetValue("smallSkill9", value); }
        public int[] Stats { get => GetValue<int[]>("stats"); set => SetValue("stats", value); }
        public int Tex1 { get => GetValue<int>("tex1"); set => SetValue("tex1", value); }
        public int Tex2 { get => GetValue<int>("tex2"); set => SetValue("tex2", value); }
        public bool UpgradeEnabled { get => GetValue<bool>("upgradeEnabled"); set => SetValue("upgradeEnabled", value); }
        public int XPBoostTime { get => GetValue<int>("xpBoost"); set => SetValue("xpBoost", value); }
    }
}
