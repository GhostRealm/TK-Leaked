﻿using common.resources;
using Mono.Game;
using System;
using wServer.core;
using wServer.core.objects;

namespace wServer.logic.behaviors
{
    internal class MoveTo3 : CycleBehavior
    {
        private readonly float baseX;
        private readonly float baseY;
        private readonly bool instant;
        private readonly bool isMapPosition;
        private readonly float speed;

        private bool once;
        private bool returned;
        private float X;
        private float Y;

        public MoveTo3(float X, float Y, double speed = 2, bool once = false, bool isMapPosition = false, bool instant = false)
        {
            this.isMapPosition = isMapPosition;
            this.speed = (float)speed;
            this.once = once;
            this.instant = instant;

            baseX = X;
            baseY = Y;
        }

        protected override void OnStateEntry(Entity host, TickData time, ref object state)
        {
            if (!isMapPosition)
            {
                X = baseX + host.X;
                Y = baseY + host.Y;
            }
            else
            {
                X = baseX;
                Y = baseY;
            }

            if (instant)
                host.Move(X, Y);
        }

        protected override void TickCore(Entity host, TickData time, ref object state)
        {
            if (instant)
                return;

            if (!returned)
            {
                if (host.HasConditionEffect(ConditionEffects.Paralyzed))
                    return;

                var spd = host.GetSpeed(speed) * time.DeltaTime;

                if (Math.Abs(X - host.X) > 0.5 || Math.Abs(Y - host.Y) > 0.5)
                {
                    var vect = new Vector2(X, Y) - new Vector2(host.X, host.Y);
                    vect.Normalize();
                    vect *= spd;

                    host.Move(host.X + vect.X, host.Y + vect.Y);

                    if (host.X == X && host.Y == Y && once)
                    {
                        once = true;
                        returned = true;
                    }
                }
            }
        }
    }
}
