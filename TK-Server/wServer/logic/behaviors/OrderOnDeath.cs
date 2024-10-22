﻿using System.Linq;
using wServer.core;
using wServer.core.objects;

namespace wServer.logic.behaviors
{
    internal class OrderOnDeath : Behavior
    {
        private readonly float _probability;
        private readonly double _range;
        private readonly string _stateName;
        private readonly ushort _target;

        private State _targetState;

        public OrderOnDeath(double range, string target, string state, double probability = 1)
        {
            _range = range;
            _target = GetObjType(target);
            _stateName = state;
            _probability = (float)probability;
        }

        protected internal override void Resolve(State parent) => parent.Death += (sender, e) =>
        {
            if (_targetState == null)
                _targetState = FindState(e.Host.CoreServerManager.BehaviorDb.Definitions[_target].Item1, _stateName);

            if (e.Host.CurrentState.Is(parent) && Random.NextDouble() < _probability)
                foreach (var i in e.Host.GetNearestEntities(_range, _target))
                    if (!i.CurrentState.Is(_targetState))
                        i.SwitchTo(_targetState);
        };

        protected override void TickCore(Entity host, TickData time, ref object state)
        { }

        private static State FindState(State state, string name)
        {
            if (state.Name == name)
                return state;

            return state.States.Select(i => FindState(i, name)).FirstOrDefault(s => s != null);
        }
    }
}
