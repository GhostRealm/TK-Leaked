﻿using wServer.logic.behaviors;
using wServer.logic.transitions;

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ Bags = () => Behav()
        .Init("Eternal Loot Bag",
            new State(
                new State("Buff",
                    //new Chase()
                    new Taunt("<3"),
                    new Flash(0x98ff98, 10000, 100),
                    new ChangeSize(3, 350),
                    new SwirlingMistDeathParticles(),
                    new TimedTransition(100000, "Suicide")
                    ),
                new State("Suicide",
                    new Decay(0)
                    )
                )
            )
        .Init("Boosted Eternal Loot Bag",
            new State(
                new State("Buff",
                    //new Chase(),
                    new Taunt("<3"),
                    new Flash(0x98ff98, 10000, 100),
                    new ChangeSize(3, 350),
                    new SwirlingMistDeathParticles(),
                    new TimedTransition(100000, "Suicide")
                    ),
                new State("Suicide",
                    new Decay(0)
                    )
                )
            );
    };
}
