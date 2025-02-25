﻿using System;
using wServer.core;
using wServer.core.objects;

namespace wServer.logic.behaviors
{
    internal class RemoveObjectOnDeath : Behavior
    {
        private readonly string _objName;
        private readonly int _range;

        public RemoveObjectOnDeath(string objName, int range)
        {
            _objName = objName;
            _range = range;
        }

        protected internal override void Resolve(State parent) => parent.Death += (sender, e) =>
        {
            var dat = e.Host.CoreServerManager.Resources.GameData;
            var objType = dat.IdToObjectType[_objName];
            var map = e.Host.Owner.Map;
            var w = map.Width;
            var h = map.Height;

            for (var y = 0; y < h; y++)
                for (var x = 0; x < w; x++)
                {
                    var tile = map[x, y];

                    if (tile.ObjType != objType)
                        continue;

                    var dx = Math.Abs(x - (int)e.Host.X);
                    var dy = Math.Abs(y - (int)e.Host.Y);

                    if (dx > _range || dy > _range)
                        continue;

                    tile.ObjType = 0;
                    tile.UpdateCount++;

                    map[x, y] = tile;
                }
        };

        protected override void TickCore(Entity host, TickData time, ref object state)
        { }
    }
}
