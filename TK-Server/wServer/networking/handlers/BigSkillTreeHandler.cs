using wServer.networking.packets;
using wServer.networking.packets.incoming;
using wServer.networking.packets.outgoing;
using System.Collections.Generic;
using System.Linq;

namespace wServer.networking.handlers
{
    internal class BigSkillHandler : PacketHandlerBase<BigSkillTree>
    {
        public override PacketId ID => PacketId.BIGSKILLTREE;

        protected override void HandlePacket(Client client, BigSkillTree packet)
        {
            Handle(client, packet);
        }

        private bool checkBigSkills(Client client)
        {
            var player = client.Player;
            var playerDesc = player.Client.CoreServerManager.Resources.GameData.Classes[player.ObjectType];
            var maxed = playerDesc.Stats.Where((t, i) => player.Stats.Base[i] >= t.MaxValue).Count() + (player.UpgradeEnabled ? playerDesc.Stats.Where((t, i) => i == 0 ? player.Stats.Base[i] >= t.MaxValue + 50 : i == 1 ? player.Stats.Base[i] >= t.MaxValue + 50 : player.Stats.Base[i] >= t.MaxValue + 10).Count() : 0);

            var maxBigSkills = 0;

            if (player.BigSkill1)
                maxBigSkills += 1;

            if (player.BigSkill2)
                maxBigSkills += 1;

            if (player.BigSkill3)
                maxBigSkills += 1;

            if (player.BigSkill4)
                maxBigSkills += 1;

            if (player.BigSkill5)
                maxBigSkills += 1;

            if (player.BigSkill6)
                maxBigSkills += 1;

            if (player.BigSkill7)
                maxBigSkills += 1;

            if (player.BigSkill8)
                maxBigSkills += 1;

            if (player.BigSkill9)
                maxBigSkills += 1;

            if (player.BigSkill10)
                maxBigSkills += 1;

            if (player.BigSkill11)
                maxBigSkills += 1;

            if (player.BigSkill12)
                maxBigSkills += 1;

            var maxedBigSkills = maxBigSkills >= 4 ? true : false;

            var fullyMaxed = maxed >= 16;

            if (fullyMaxed) //check if player is maxed 16/16
                maxedBigSkills = maxBigSkills >= 5 ? true : false;

            return maxedBigSkills;
        }

        private void Handle(Client client, BigSkillTree packet)
        {
            var player = client.Player;
            var chr = client.Character;

            if (player == null || IsTest(client))
                return;

            if (packet.skillNumber == 1)
            {
                if (player.SmallSkill1 < 5 || player.SmallSkill9 < 3 || player.SmallSkill7 < 2 || checkBigSkills(client))
                {
                    player.Client.SendPacket(new InvResult() { Result = 1 });
                    return;
                }

                player.BigSkill1 = true;
                player.Stats.ReCalculateValues();
                player.Stats.Base.ReCalculateValues();
                player.Stats.Boost.ReCalculateValues();

                if (checkBigSkills(client))
                    chr.BigSkill1 = player.BigSkill1;
                else
                    return;
            }

            if (packet.skillNumber == 2)
            {
                if (player.SmallSkill2 < 5 || player.SmallSkill10 < 3 || player.SmallSkill8 < 2 || checkBigSkills(client))
                {
                    player.Client.SendPacket(new InvResult() { Result = 1 });
                    return;
                }

                player.BigSkill2 = true;
                player.Stats.ReCalculateValues();
                player.Stats.Base.ReCalculateValues();
                player.Stats.Boost.ReCalculateValues();

                if (checkBigSkills(client))
                    chr.BigSkill2 = player.BigSkill2;
                else
                    return;
            }

            if (packet.skillNumber == 3)
            {
                if (player.SmallSkill3 < 5 || player.SmallSkill6 < 3 || checkBigSkills(client))
                {
                    player.Client.SendPacket(new InvResult() { Result = 1 });
                    return;
                }

                player.BigSkill3 = true;
                player.Stats.ReCalculateValues();
                player.Stats.Base.ReCalculateValues();
                player.Stats.Boost.ReCalculateValues();

                if (checkBigSkills(client))
                    chr.BigSkill3 = player.BigSkill3;
                else
                    return;
            }

            if (packet.skillNumber == 4)
            {
                if (player.SmallSkill4 < 5 || player.SmallSkill1 < 3 || checkBigSkills(client))
                {
                    player.Client.SendPacket(new InvResult() { Result = 1 });
                    return;
                }

                player.BigSkill4 = true;
                player.Stats.ReCalculateValues();
                player.Stats.Base.ReCalculateValues();
                player.Stats.Boost.ReCalculateValues();

                if (checkBigSkills(client))
                    chr.BigSkill4 = player.BigSkill4;
                else
                    return;
            }

            if (packet.skillNumber == 5)
            {
                if (player.SmallSkill5 < 5 || player.SmallSkill4 < 3 || player.SmallSkill7 < 2 || checkBigSkills(client))
                {
                    player.Client.SendPacket(new InvResult() { Result = 1 });
                    return;
                }

                player.BigSkill5 = true;
                player.Stats.ReCalculateValues();
                player.Stats.Base.ReCalculateValues();
                player.Stats.Boost.ReCalculateValues();

                if (checkBigSkills(client))
                    chr.BigSkill5 = player.BigSkill5;
                else
                    return;
            }

            if (packet.skillNumber == 6)
            {
                if (player.SmallSkill6 < 5 || player.SmallSkill3 < 2 || player.SmallSkill11 < 1 || checkBigSkills(client))
                {
                    player.Client.SendPacket(new InvResult() { Result = 1 });
                    return;
                }

                player.BigSkill6 = true;
                player.Stats.ReCalculateValues();
                player.Stats.Base.ReCalculateValues();
                player.Stats.Boost.ReCalculateValues();

                if (checkBigSkills(client))
                    chr.BigSkill6 = player.BigSkill6;
                else
                    return;
            }

            if (packet.skillNumber == 7)
            {
                if (player.SmallSkill7 < 5 || player.SmallSkill9 < 3 || checkBigSkills(client))
                {
                    player.Client.SendPacket(new InvResult() { Result = 1 });
                    return;
                }

                player.BigSkill7 = true;
                player.Stats.ReCalculateValues();
                player.Stats.Base.ReCalculateValues();
                player.Stats.Boost.ReCalculateValues();

                if (checkBigSkills(client))
                    chr.BigSkill7 = player.BigSkill7;
                else
                    return;
            }

            if (packet.skillNumber == 8)
            {
                if (player.SmallSkill8 < 5 || player.SmallSkill10 < 3 || checkBigSkills(client))
                {
                    player.Client.SendPacket(new InvResult() { Result = 1 });
                    return;
                }

                player.BigSkill8 = true;
                player.Stats.ReCalculateValues();
                player.Stats.Base.ReCalculateValues();
                player.Stats.Boost.ReCalculateValues();

                if (checkBigSkills(client))
                    chr.BigSkill8 = player.BigSkill8;
                else
                    return;
            }

            if (packet.skillNumber == 9)
            {
                if (player.SmallSkill9 < 5 || player.SmallSkill1 < 3 || checkBigSkills(client))
                {
                    player.Client.SendPacket(new InvResult() { Result = 1 });
                    return;
                }

                player.BigSkill9 = true;
                player.Stats.ReCalculateValues();
                player.Stats.Base.ReCalculateValues();
                player.Stats.Boost.ReCalculateValues();

                if (checkBigSkills(client))
                    chr.BigSkill9 = player.BigSkill9;
                else
                    return;
            }

            if (packet.skillNumber == 10)
            {
                if (player.SmallSkill10 < 5 || player.SmallSkill2 < 3 || checkBigSkills(client))
                {
                    player.Client.SendPacket(new InvResult() { Result = 1 });
                    return;
                }

                player.BigSkill10 = true;
                player.Stats.ReCalculateValues();
                player.Stats.Base.ReCalculateValues();
                player.Stats.Boost.ReCalculateValues();

                if (checkBigSkills(client))
                    chr.BigSkill10 = player.BigSkill10;
                else
                    return;
            }

            if (packet.skillNumber == 11)
            {
                if (player.SmallSkill11 < 5 || player.SmallSkill6 < 3 || checkBigSkills(client))
                {
                    player.Client.SendPacket(new InvResult() { Result = 1 });
                    return;
                }

                player.BigSkill11 = true;
                player.Stats.ReCalculateValues();
                player.Stats.Base.ReCalculateValues();
                player.Stats.Boost.ReCalculateValues();

                if (checkBigSkills(client))
                    chr.BigSkill11 = player.BigSkill11;
                else
                    return;
            }

            if (packet.skillNumber == 12)
            {
                if (player.SmallSkill12 < 5 || player.SmallSkill11 < 3 || checkBigSkills(client))
                {
                    player.Client.SendPacket(new InvResult() { Result = 1 });
                    return;
                }

                player.BigSkill12 = true;
                player.Stats.ReCalculateValues();
                player.Stats.Base.ReCalculateValues();
                player.Stats.Boost.ReCalculateValues();

                if (checkBigSkills(client))
                    chr.BigSkill12 = player.BigSkill12;
                else
                    return;
            }
        }
    }
}
