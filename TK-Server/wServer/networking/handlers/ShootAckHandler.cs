﻿using wServer.networking.packets;
using wServer.networking.packets.incoming;

namespace wServer.networking.handlers
{
    internal class ShootAckHandler : PacketHandlerBase<ShootAck>
    {
        public override PacketId ID => PacketId.SHOOTACK;

        protected override void HandlePacket(Client client, ShootAck packet)
        { }
    }
}
