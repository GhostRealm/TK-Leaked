using wServer.networking.packets;
using wServer.networking.packets.incoming;
using wServer.networking.packets.outgoing;

namespace wServer.networking.handlers
{
    internal class SmallSkillHandler : PacketHandlerBase<SmallSkillTree>
    {
        public override PacketId ID => PacketId.SMALLSKILLTREE;

        protected override void HandlePacket(Client client, SmallSkillTree packet) => Handle(client, packet);

        private void Handle(Client client, SmallSkillTree packet)
        {
            var player = client.Player;
            var chr = client.Character;
            if (player == null || IsTest(client))
                return;

            #region Check of things

            if (player.SmallSkill1 > 5)
            {
                player.SmallSkill1 = 5;
            }
            if (player.SmallSkill2 > 5)
            {
                player.SmallSkill2 = 5;
            }
            if (player.SmallSkill3 > 5)
            {
                player.SmallSkill3 = 5;
            }
            if (player.SmallSkill4 > 5)
            {
                player.SmallSkill4 = 5;
            }
            if (player.SmallSkill5 > 5)
            {
                player.SmallSkill5 = 5;
            }
            if (player.SmallSkill6 > 5)
            {
                player.SmallSkill6 = 5;
            }
            if (player.SmallSkill7 > 5)
            {
                player.SmallSkill7 = 5;
            }
            if (player.SmallSkill8 > 5)
            {
                player.SmallSkill8 = 5;
            }
            if (player.SmallSkill9 > 5)
            {
                player.SmallSkill9 = 5;
            }
            if (player.SmallSkill10 > 5)
            {
                player.SmallSkill10 = 5;
            }
            if (player.SmallSkill11 > 5)
            {
                player.SmallSkill11 = 5;
            }
            if (player.SmallSkill12 > 5)
            {
                player.SmallSkill12 = 5;
            }
            var maxedInt = 0;
            if (player.MaxedLife)
            {
                maxedInt += 5;
            }
            if (player.MaxedMana)
            {
                maxedInt += 5;
            }
            if (player.MaxedAtt)
            {
                maxedInt += 5;
            }
            if (player.MaxedDef)
            {
                maxedInt += 5;
            }
            if (player.MaxedSpd)
            {
                maxedInt += 5;
            }
            if (player.MaxedDex)
            {
                maxedInt += 5;
            }
            if (player.MaxedVit)
            {
                maxedInt += 5;
            }
            if (player.MaxedWis)
            {
                maxedInt += 5;
            }

            if (maxedInt > player.SmallSkill1 + player.SmallSkill2 + player.SmallSkill3 + player.SmallSkill4 + player.SmallSkill5 + player.SmallSkill6 + player.SmallSkill7 + player.SmallSkill8 + player.SmallSkill9 + player.SmallSkill10 + player.SmallSkill11 + player.SmallSkill12)
            {
                player.Points = maxedInt - (player.SmallSkill1 + player.SmallSkill2 + player.SmallSkill3 + player.SmallSkill4 + player.SmallSkill5 + player.SmallSkill6 + player.SmallSkill7 + player.SmallSkill8 + player.SmallSkill9 + player.SmallSkill10 + player.SmallSkill11 + player.SmallSkill12);
            }

            #endregion Check of things

            if (packet.skillNumber == 1)
            {
                if (player.Points <= 0)
                {
                    player.Points = 0;
                    player.SendError("You don't have Points!");
                    player.Client.SendPacket(new InvResult() { Result = 1 });
                    return;
                }
                player.SmallSkill1 += 1;
                player.Points -= 1;
                player.Stats.ReCalculateValues();
                player.Stats.Base.ReCalculateValues();
                player.Stats.Boost.ReCalculateValues();
            }

            if (packet.skillNumber == 2)
            {
                if (player.Points <= 0)
                {
                    player.Points = 0;
                    player.Client.SendPacket(new InvResult() { Result = 1 });
                    player.SendError("You don't have Points!");
                    return;
                }
                player.SmallSkill2 += 1;
                player.Points -= 1;
                player.Stats.ReCalculateValues();
                player.Stats.Base.ReCalculateValues();
                player.Stats.Boost.ReCalculateValues();
            }

            if (packet.skillNumber == 3)
            {
                if (player.Points <= 0)
                {
                    player.Points = 0;
                    player.Client.SendPacket(new InvResult() { Result = 1 });
                    player.SendError("You don't have Points!");
                    return;
                }
                player.SmallSkill3 += 1;
                player.Points -= 1;
                player.Stats.ReCalculateValues();
                player.Stats.Base.ReCalculateValues();
                player.Stats.Boost.ReCalculateValues();
            }

            if (packet.skillNumber == 4)
            {
                if (player.Points <= 0)
                {
                    player.Points = 0;
                    player.Client.SendPacket(new InvResult() { Result = 1 });
                    player.SendError("You don't have Points!");
                    return;
                }
                player.SmallSkill4 += 1;
                player.Points -= 1;
                player.Stats.ReCalculateValues();
                player.Stats.Base.ReCalculateValues();
                player.Stats.Boost.ReCalculateValues();
            }

            if (packet.skillNumber == 5)
            {
                if (player.Points <= 0)
                {
                    player.Points = 0;
                    player.Client.SendPacket(new InvResult() { Result = 1 });
                    player.SendError("You don't have Points!");
                    return;
                }
                player.SmallSkill5 += 1;
                player.Points -= 1;
                player.Stats.ReCalculateValues();
                player.Stats.Base.ReCalculateValues();
                player.Stats.Boost.ReCalculateValues();
            }

            if (packet.skillNumber == 6)
            {
                if (player.Points <= 0)
                {
                    player.Points = 0;
                    player.Client.SendPacket(new InvResult() { Result = 1 });
                    player.SendError("You don't have Points!");
                    return;
                }
                player.SmallSkill6 += 1;
                player.Points -= 1;
                player.Stats.ReCalculateValues();
                player.Stats.Base.ReCalculateValues();
                player.Stats.Boost.ReCalculateValues();
            }

            if (packet.skillNumber == 7)
            {
                if (player.Points <= 0)
                {
                    player.Points = 0;
                    player.Client.SendPacket(new InvResult() { Result = 1 });
                    player.SendError("You don't have Points!");
                    return;
                }
                player.SmallSkill7 += 1;
                player.Points -= 1;
                player.Stats.ReCalculateValues();
                player.Stats.Base.ReCalculateValues();
                player.Stats.Boost.ReCalculateValues();
            }

            if (packet.skillNumber == 8)
            {
                if (player.Points <= 0)
                {
                    player.Points = 0;
                    player.Client.SendPacket(new InvResult() { Result = 1 });
                    player.SendError("You don't have Points!");
                    return;
                }
                player.SmallSkill8 += 1;
                player.Points -= 1;
                player.Stats.ReCalculateValues();
                player.Stats.Base.ReCalculateValues();
                player.Stats.Boost.ReCalculateValues();
            }

            if (packet.skillNumber == 9)
            {
                if (player.Points <= 0)
                {
                    player.Points = 0;
                    player.Client.SendPacket(new InvResult() { Result = 1 });
                    player.SendError("You don't have Points!");
                    return;
                }
                player.SmallSkill9 += 1;
                player.Points -= 1;
                player.Stats.ReCalculateValues();
                player.Stats.Base.ReCalculateValues();
                player.Stats.Boost.ReCalculateValues();
            }

            if (packet.skillNumber == 10)
            {
                if (player.Points <= 0)
                {
                    player.Points = 0;
                    player.Client.SendPacket(new InvResult() { Result = 1 });
                    player.SendError("You don't have Points!");
                    return;
                }
                player.SmallSkill10 += 1;
                player.Points -= 1;
                player.Stats.ReCalculateValues();
                player.Stats.Base.ReCalculateValues();
                player.Stats.Boost.ReCalculateValues();
            }

            if (packet.skillNumber == 11)
            {
                if (player.Points <= 0)
                {
                    player.Points = 0;
                    player.Client.SendPacket(new InvResult() { Result = 1 });
                    player.SendError("You don't have Points!");
                    return;
                }
                player.SmallSkill11 += 1;
                player.Points -= 1;
                player.Stats.ReCalculateValues();
                player.Stats.Base.ReCalculateValues();
                player.Stats.Boost.ReCalculateValues();
            }

            if (packet.skillNumber == 12)
            {
                if (player.Points <= 0)
                {
                    player.Points = 0;
                    player.Client.SendPacket(new InvResult() { Result = 1 });
                    player.SendError("You don't have Points!");
                    return;
                }
                player.SmallSkill12 += 1;
                player.Points -= 1;
            }

            if (packet.skillNumber == 20)
            {
                chr.SmallSkill1 = player.SmallSkill1;
                chr.SmallSkill2 = player.SmallSkill2;
                chr.SmallSkill3 = player.SmallSkill3;
                chr.SmallSkill4 = player.SmallSkill4;
                chr.SmallSkill5 = player.SmallSkill5;
                chr.SmallSkill6 = player.SmallSkill6;
                chr.SmallSkill7 = player.SmallSkill7;
                chr.SmallSkill8 = player.SmallSkill8;
                chr.SmallSkill9 = player.SmallSkill9;
                chr.SmallSkill10 = player.SmallSkill10;
                chr.SmallSkill11 = player.SmallSkill11;
                chr.SmallSkill12 = player.SmallSkill12;
                chr.BigSkill1 = player.BigSkill1;
                chr.BigSkill2 = player.BigSkill2;
                chr.BigSkill3 = player.BigSkill3;
                chr.BigSkill4 = player.BigSkill4;
                chr.BigSkill5 = player.BigSkill5;
                chr.BigSkill6 = player.BigSkill6;
                chr.BigSkill7 = player.BigSkill7;
                chr.BigSkill8 = player.BigSkill8;
                chr.BigSkill9 = player.BigSkill9;
                chr.BigSkill10 = player.BigSkill10;
                chr.BigSkill11 = player.BigSkill11;
                chr.BigSkill12 = player.BigSkill12;
                chr.Points = player.Points;
                player.Stats.ReCalculateValues();
                player.Stats.Base.ReCalculateValues();
                player.Stats.Boost.ReCalculateValues();
            }
        }
    }
}
