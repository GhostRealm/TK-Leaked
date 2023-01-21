﻿using common.database;
using common.resources;
using Newtonsoft.Json;
using System;
using System.Linq;
using wServer.core.objects;
using wServer.core.worlds.logic;
using wServer.networking;
using wServer.networking.packets.outgoing;
using wServer.utils;

namespace wServer.core.commands
{
    public abstract partial class Command
    {
        internal class Spawn : Command
        {
            private struct JsonSpawn
            {
                public string notif;
                public SpawnProperties[] spawns;
            }

            private struct SpawnProperties
            {
                public string name;
                public int? hp;
                public int? size;
                public int? count;
                public int[] x;
                public int[] y;
                public bool? target;
                public string clasify;
            }

            private const int Delay = 0; // in seconds

            public Spawn() : base("spawn", permLevel: 100)
            {
            }

            protected override bool Process(Player player, TickData time, string args)
            {
                args = args.Trim();
                if (!(player.Owner is Vault) && player.Rank < 110)
                {
                    player.SendError("Only in Vault boi.");
                    return false;
                }
                return args.StartsWith("{") ? SpawnJson(player, args) : SpawnBasic(player, args);
            }

            private bool SpawnJson(Player player, string json)
            {
                var gameData = player.CoreServerManager.Resources.GameData;

                JsonSpawn props;
                try
                {
                    props = JsonConvert.DeserializeObject<JsonSpawn>(json);
                }
                catch (Exception)
                {
                    player.SendError("JSON not formatted correctly!");
                    return false;
                }

                if (props.spawns != null)
                    foreach (var spawn in props.spawns)
                    {
                        if (spawn.name == null)
                        {
                            player.SendError("No mob specified. Every entry needs a name property.");
                            return false;
                        }

                        var objType = GetSpawnObjectType(gameData, spawn.name);
                        if (objType == null)
                        {
                            player.SendError("Unknown entity!");
                            return false;
                        }

                        var desc = gameData.ObjectDescs[objType.Value];

                        if (player.Client.Account.Rank < 100 && desc.ObjectId.Contains("Fountain"))
                        {
                            player.SendError("Insufficient rank.");
                            return false;
                        }

                        var hp = desc.MaxHP;
                        if (spawn.hp > hp && spawn.hp < int.MaxValue)
                            hp = spawn.hp.Value;

                        var size = desc.MinSize;
                        if (spawn.size >= 25 && spawn.size <= 500)
                            size = spawn.size.Value;

                        var count = 1;
                        if (spawn.count > count && spawn.count <= 500)
                            count = spawn.count.Value;

                        int[] x = null;
                        int[] y = null;

                        if (spawn.x != null)
                            x = new int[spawn.x.Length];

                        if (spawn.y != null)
                            y = new int[spawn.y.Length];

                        if (x != null)
                        {
                            for (int i = 0; i < x.Length && i < count; i++)
                            {
                                if (spawn.x[i] > 0 && spawn.x[i] <= player.Owner.Map.Width)
                                {
                                    x[i] = spawn.x[i];
                                }
                            }
                        }

                        if (y != null)
                        {
                            for (int i = 0; i < y.Length && i < count; i++)
                            {
                                if (spawn.y[i] > 0 && spawn.y[i] <= player.Owner.Map.Height)
                                {
                                    y[i] = spawn.y[i];
                                }
                            }
                        }

                        var clasified = "normal";
                        if (spawn.clasify != null)
                            clasified = spawn.clasify;

                        bool target = false;
                        if (spawn.target != null)
                            target = spawn.target.Value;

                        QueueSpawnEvent(player, count, objType.Value, hp, size, x, y, target, clasified);
                    }

                if (props.notif != null)
                {
                    NotifySpawn(player, props.notif);
                }

                return true;
            }

            private bool SpawnBasic(Player player, string args)
            {
                var gameData = player.CoreServerManager.Resources.GameData;

                // split argument
                var index = args.IndexOf(' ');
                var name = args;
                if (args.IndexOf(' ') > 0 && int.TryParse(args.Substring(0, args.IndexOf(' ')), out int num)) //multi
                    name = args.Substring(index + 1);
                else
                    num = 1;

                var objType = GetSpawnObjectType(gameData, name);
                if (objType == null)
                {
                    player.SendError("Unknown entity!");
                    return false;
                }

                if (num <= 0)
                {
                    player.SendInfo($"Really? {num} {name}? I'll get right on that...");
                    return false;
                }

                if(num > 10 && player.Rank <120)
                {
                    player.SendError("Only 10!");
                    return false;
                }

                var id = player.CoreServerManager.Resources.GameData.ObjectTypeToId[objType.Value];
                if (player.Client.Account.Rank < 100 && id.Contains("Fountain"))
                {
                    player.SendError("Insufficient rank.");
                    return false;
                }

                NotifySpawn(player, id, num);
                QueueSpawnEvent(player, num, objType.Value);
                return true;
            }

            private ushort? GetSpawnObjectType(XmlData gameData, string name)
            {
                if (!gameData.IdToObjectType.TryGetValue(name, out ushort objType) ||
                    !gameData.ObjectDescs.ContainsKey(objType))
                {
                    // no match found, try to get partial match
                    var mobs = gameData.IdToObjectType
                        .Where(m => m.Key.ContainsIgnoreCase(name) && gameData.ObjectDescs.ContainsKey(m.Value))
                        .Select(m => gameData.ObjectDescs[m.Value]);

                    if (!mobs.Any())
                        return null;

                    var maxHp = mobs.Max(e => e.MaxHP);
                    objType = mobs.First(e => e.MaxHP == maxHp).ObjectType;
                }

                return objType;
            }

            private void NotifySpawn(Player player, string mob, int? num = null)
            {
                var w = player.Owner;

                var notif = mob;
                if (num != null)
                    notif = "Spawning " + ((num > 1) ? num + " " : "") + mob + "...";

                w.BroadcastIfVisible(new Notification
                {
                    Color = new ARGB(0xffff0000),
                    ObjectId = player.Id,
                    Message = notif
                }, player, PacketPriority.Low);

                w.BroadcastIfVisible(new Text
                {
                    Name = $"#{player.Name}",
                    NumStars = player.Stars,
                    Admin = player.Admin,
                    BubbleTime = 0,
                    Txt = notif
                }, player, PacketPriority.Low);
            }

            private void QueueSpawnEvent(
                Player player,
                int num,
                ushort mobObjectType, int? hp = null, int? size = null,
                int[] x = null, int[] y = null,
                bool? target = false, string clasified = "normal")
            {
                var pX = player.X;
                var pY = player.Y;

                player.Owner.Timers.Add(new WorldTimer(Delay * 1000, (world, t) => // spawn mob in delay seconds
                {
                    for (var i = 0; i < num && i < 500; i++)
                    {
                        Entity entity;
                        try
                        {
                            entity = Entity.Resolve(world.Manager, mobObjectType);
                        }
                        catch (Exception e)
                        {
                            SLogger.Instance.Error(e.ToString());
                            return;
                        }

                        entity.Spawned = true;

                        if (entity is Enemy enemy)
                        {
                            if (hp != null)
                            {
                                enemy.HP = hp.Value;
                                enemy.MaximumHP = enemy.HP;
                            }

                            if (size != null)
                                enemy.SetDefaultSize(size.Value);

                            if (target == true)
                                enemy.AttackTarget = player;

                            /*enemy.ApplyConditionEffect(new ConditionEffect()
                            {
                                Effect = ConditionEffectIndex.Invisible,
                                DurationMS = -1
                            });*/
                        }

                        if (clasified != "normal")
                            (entity as Enemy).ClasifyEnemyJson(clasified);

                        var sX = (x != null && i < x.Length) ? x[i] : pX;
                        var sY = (y != null && i < y.Length) ? y[i] : pY;

                        entity.Move(sX, sY);

                        if (!world.Deleted)
                            world.EnterWorld(entity);
                    }
                }));
            }
        }
    }
}
