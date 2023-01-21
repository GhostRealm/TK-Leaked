using common;

namespace wServer.networking.packets.incoming
{
    internal class BigSkillTree : IncomingMessage
    {
        public int skillNumber { get; set; }

        public override PacketId ID => PacketId.BIGSKILLTREE;

        public override Packet CreateInstance()
        {
            return new BigSkillTree();
        }

        protected override void Read(NReader rdr)
        {
            skillNumber = rdr.ReadInt32();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.Write(skillNumber);
        }
    }
}
