﻿using common;
using common.database;
using System.Text;
using wServer.core.objects;

namespace wServer.core.commands
{
    public abstract partial class Command
    {
        internal class GetPartyInfo : Command
        {
            public GetPartyInfo() : base("getPartyInfo", permLevel: 100, alias: "gpt")
            {
            }

            protected override bool Process(Player player, TickData time, string args)
            {
                if (string.IsNullOrEmpty(args))
                {
                    player.SendInfo("Usage: /getPartyInfo <PartyID> or /getPartyInfo all for all Party ID's.");
                    return false;
                }

                if (args.Contains("all"))
                {
                    var partiesids = player.Client.Account.Database.HashGetAll("party");
                    var sb = new StringBuilder("Parties ID's: \n");

                    foreach (var party in partiesids)
                    {
                        var partyInfo = DbPartySystem.Get(player.Client.Account.Database, party.Name.ToString().ToInt32());
                        sb.Append($"Party ID: {partyInfo.PartyId}\nParty Leader: {partyInfo.PartyLeader.Item1} \n\n");
                    }
                    player.SendInfo(sb.ToString());
                    return true;
                }

                var specificParty = DbPartySystem.Get(player.Client.Account.Database, args.ToInt32());

                if (specificParty == null)
                {
                    player.SendError("That Party doesn't exists.");
                    return false;
                }

                player.SendInfo($"Party ID: {specificParty.PartyId}\nParty Leader: {specificParty.PartyLeader.Item1}, AccID: {specificParty.PartyLeader.Item2}\nMembers: ");
                foreach (var member in specificParty.PartyMembers)
                {
                    player.SendInfo($"Member: Name: {member.name}, AccID: {member.accid}");
                }

                return true;
            }
        }
    }
}
