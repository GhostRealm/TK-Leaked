﻿using System;

namespace wServer.networking.packets
{
    [Flags]
    public enum PacketId : byte
    {
        FAILURE = 0,
        CREATE_SUCCESS = 1,
        CREATE = 2,
        PLAYERSHOOT = 3,
        MOVE = 4,
        PLAYERTEXT = 5,
        TEXT = 6,
        SERVERPLAYERSHOOT = 7,
        DAMAGE = 8,
        UPDATE = 9,
        UPDATEACK = 10,
        NOTIFICATION = 11,
        NEWTICK = 12,
        INVSWAP = 13,
        USEITEM = 14,
        SHOWEFFECT = 15,
        HELLO = 16,
        GOTO = 17,
        INVDROP = 18,
        INVRESULT = 19,
        RECONNECT = 20,
        PING = 21,
        PONG = 22,
        MAPINFO = 23,
        LOAD = 24,
        PIC = 25,
        SETCONDITION = 26,
        TELEPORT = 27,
        USEPORTAL = 28,
        DEATH = 29,
        BUY = 30,
        BUYRESULT = 31,
        AOE = 32,
        GROUNDDAMAGE = 33,
        PLAYERHIT = 34,
        ENEMYHIT = 35,
        AOEACK = 36,
        SHOOTACK = 37,
        OTHERHIT = 38,
        SQUAREHIT = 39,
        GOTOACK = 40,
        EDITACCOUNTLIST = 41,
        ACCOUNTLIST = 42,
        QUESTOBJID = 43,
        CHOOSENAME = 44,
        NAMERESULT = 45,

        //Guild
        CREATEGUILD = 46,

        GUILDRESULT = 47,
        GUILDREMOVE = 48,
        GUILDINVITE = 49,

        ALLYSHOOT = 50,
        ENEMYSHOOT = 51,

        //Trade
        REQUESTTRADE = 52,

        TRADEREQUESTED = 53,
        TRADESTART = 54,
        CHANGETRADE = 55,
        TRADECHANGED = 56,
        ACCEPTTRADE = 57,
        CANCELTRADE = 58,
        TRADEDONE = 59,
        TRADEACCEPTED = 60,

        CLIENTSTAT = 61,
        CHECKCREDITS = 62,
        ESCAPE = 63,
        FILE = 64,
        INVITEDTOGUILD = 65,
        JOINGUILD = 66,
        CHANGEGUILDRANK = 67,
        PLAYSOUND = 68,
        GLOBAL_NOTIFICATION = 69,
        RESKIN = 70,

        //Magician
        UPGRADESTAT = 71,

        //Skill Tree
        SMALLSKILLTREE = 72,

        BIGSKILLTREE = 73,

        //Forge
        FORGEFUSION = 74,

        //Market
        MARKET_SEARCH = 75,

        MARKET_SEARCH_RESULT = 76,
        MARKET_BUY = 77,
        MARKET_BUY_RESULT = 78,
        MARKET_ADD = 79,
        MARKET_ADD_RESULT = 80,
        MARKET_REMOVE = 81,
        MARKET_REMOVE_RESULT = 82,
        MARKET_MY_OFFERS = 83,
        MARKET_MY_OFFERS_RESULT = 84,

        OPTIONS = 85,

        /* Bounty */
        BOUNTYREQUEST = 86, /* Start the Bounty */
        BOUNTYMEMBERLISTREQUEST = 87, /* Client ask for Players in the Area */
        BOUNTYMEMBERLISTSEND = 88, /* Server sends all the data to the Client */

        /* Party System */
        PARTY_INVITE = 89,
        INVITED_TO_PARTY = 90,
        JOIN_PARTY = 91,
        POTION_STORAGE_INTERACTION = 92
    }
}
