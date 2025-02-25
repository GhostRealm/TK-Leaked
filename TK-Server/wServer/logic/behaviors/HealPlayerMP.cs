﻿using common.resources;
using System;
using System.Linq;
using wServer.core;
using wServer.core.objects;
using wServer.networking;
using wServer.networking.packets.outgoing;

namespace wServer.logic.behaviors
{
    internal class HealPlayerMP : Behavior
    {
        private readonly int _healAmount;
        private readonly double _range;
        private Cooldown _coolDown;

        public HealPlayerMP(double range, Cooldown coolDown = new Cooldown(), int healAmount = 100)
        {
            _range = range;
            _coolDown = coolDown.Normalize();
            _healAmount = healAmount;
        }

        protected override void OnStateEntry(Entity host, TickData time, ref object state) => state = 0;

        protected override void TickCore(Entity host, TickData time, ref object state)
        {
            var cool = (int)state;

            if (cool <= 0)
            {
                foreach (var entity in host.GetNearestEntities(_range, null, true).OfType<Player>())
                {
                    if ((host.AttackTarget != null && host.AttackTarget != entity) || entity.HasConditionEffect(ConditionEffects.Quiet))
                        continue;

                    var maxMp = entity.Stats[1];
                    var newMp = Math.Min(entity.MP + _healAmount, maxMp);

                    if (newMp != entity.MP)
                    {
                        var n = newMp - entity.MP;

                        entity.MP = newMp;
                        entity.Owner.BroadcastIfVisible(new ShowEffect() { EffectType = EffectType.Potion, TargetObjectId = entity.Id, Color = new ARGB(0xffffffff) }, entity, PacketPriority.Low);
                        entity.Owner.BroadcastIfVisible(new ShowEffect()
                        {
                            EffectType = EffectType.Trail,
                            TargetObjectId = host.Id,
                            Pos1 = new Position { X = entity.X, Y = entity.Y },
                            Color = new ARGB(0xffffffff)
                        }, host, PacketPriority.Low);
                        entity.Owner.BroadcastIfVisible(new Notification() { ObjectId = entity.Id, Message = "+" + n, Color = new ARGB(0xff3366ff) }, entity, PacketPriority.Low);
                    }
                }

                cool = _coolDown.Next(Random);
            }
            else
                cool -= time.ElaspedMsDelta;

            state = cool;
        }
    }
}
