﻿using Newtonsoft.Json;
using StackExchange.Redis;
using System.Collections.Generic;

namespace common.database
{
    public class DbIpInfo
    {
        private readonly IDatabase _db;

        public DbIpInfo(IDatabase db, string ip)
        {
            _db = db;

            IP = ip;

            var json = (string)db.HashGet("ips", ip);
            if (json == null)
                IsNull = true;
            else
                JsonConvert.PopulateObject(json, this);
        }

        public List<int> Accounts { get; set; }
        public bool Banned { get; set; }

        [JsonIgnore] public string IP { get; private set; }
        [JsonIgnore] public bool IsNull { get; private set; }

        public string Notes { get; set; }

        public void Flush() => _db.HashSetAsync("ips", IP, JsonConvert.SerializeObject(this));
    }
}
