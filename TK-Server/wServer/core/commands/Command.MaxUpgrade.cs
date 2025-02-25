﻿using wServer.core.objects;

namespace wServer.core.commands
{
    public abstract partial class Command
    {
        internal class MaxUpgrade : Command
        {
            public MaxUpgrade() : base("maxupgrade", permLevel: 60)
            {
            }

            protected override bool Process(Player player, TickData time, string args)
            {
                if (player.UpgradeEnabled == false)
                {
                    player.UpgradeEnabled = true;
                }

                var pd = player.CoreServerManager.Resources.GameData.Classes[player.ObjectType];

                player.Stats.Base[0] = pd.Stats[0].MaxValue + 50;
                player.Stats.Base[1] = pd.Stats[1].MaxValue + 50;
                player.Stats.Base[2] = pd.Stats[2].MaxValue + 10;
                player.Stats.Base[3] = pd.Stats[3].MaxValue + 10;
                player.Stats.Base[4] = pd.Stats[4].MaxValue + 10;
                player.Stats.Base[5] = pd.Stats[5].MaxValue + 10;
                player.Stats.Base[6] = pd.Stats[6].MaxValue + 10;
                player.Stats.Base[7] = pd.Stats[7].MaxValue + 10;

                player.SendInfo("Your character Stats have been maxed.");
                return true;
            }
        }
    }
}
