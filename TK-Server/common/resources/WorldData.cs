﻿using Newtonsoft.Json;
using NLog;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using terrain;

namespace common.resources
{
    public class WorldData
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public WorldData(string dir, XmlData gameData)
        {
            Dictionary<string, ProtoWorld> worlds;

            Data = new ReadOnlyDictionary<string, ProtoWorld>(worlds = new Dictionary<string, ProtoWorld>());

            var basePath = Path.GetFullPath(dir);
            var jwFiles = Directory.EnumerateFiles(basePath, "*.jw", SearchOption.TopDirectoryOnly).ToArray();

            for (var i = 0; i < jwFiles.Length; i++)
            {
                var jw = File.ReadAllText(jwFiles[i]);
                var world = JsonConvert.DeserializeObject<ProtoWorld>(jw);

                if (world.maps == null)
                {
                    var jm = File.ReadAllText(jwFiles[i].Substring(0, jwFiles[i].Length - 1) + "m");
                    world.wmap = new byte[1][];
                    world.wmap[0] = Json2Wmap.Convert(gameData, jm);
                    worlds.Add(world.name, world);
                    continue;
                }

                world.wmap = new byte[world.maps.Length][];

                var di = Directory.GetParent(jwFiles[i]);

                for (var j = 0; j < world.maps.Length; j++)
                {
                    var mapFile = Path.Combine(di.FullName, world.maps[j]);
                    if (world.maps[j].EndsWith(".wmap"))
                        world.wmap[j] = File.ReadAllBytes(mapFile);
                    else
                    {
                        var jm = File.ReadAllText(mapFile);
                        world.wmap[j] = Json2Wmap.Convert(gameData, jm);
                    }
                }

                worlds.Add(world.name, world);
            }
        }

        public IDictionary<string, ProtoWorld> Data { get; private set; }
        public ProtoWorld this[string name] => Data[name];
    }
}
