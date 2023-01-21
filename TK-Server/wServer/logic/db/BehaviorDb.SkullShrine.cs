﻿using common.resources;
using wServer.logic.behaviors;
using wServer.logic.loot;
using wServer.logic.transitions;

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ SkullShrine = () => Behav()
        .Init("Skull Shrine",
            new State(
                new ScaleHP2(35),
                new Shoot(30, 9, 10, coolDown: 750, predictive: 1), // add prediction after fixing it...
                new Reproduce("Red Flaming Skull", 40, 20, coolDown: 500),
                new Reproduce("Blue Flaming Skull", 40, 20, coolDown: 500)
                ),
            new Threshold(0.001,
                new ItemLoot("Potion Dust", 1, 0, 0.1),
                new ItemLoot("Potion Dust", 0.02),
                new ItemLoot("Item Dust", 0.02),
                new ItemLoot("Miscellaneous Dust", 0.01),
                new ItemLoot("Special Dust", 0.005),
                new ItemLoot("Potion of Attack", 0.70),
                new ItemLoot("Potion of Dexterity", 0.8),
                new ItemLoot("Potion of Wisdom", 0.2, 1),
                new TierLoot(8, ItemType.Weapon, 0.22),
                new TierLoot(9, ItemType.Weapon, 0.17),
                new TierLoot(10, ItemType.Weapon, 0.12),
                new TierLoot(11, ItemType.Weapon, 0.09),
                new TierLoot(3, ItemType.Ring, 0.12),
                new TierLoot(4, ItemType.Ring, 0.07),
                new TierLoot(5, ItemType.Ring, 0.03),
                new TierLoot(7, ItemType.Armor, 0.27),
                new TierLoot(8, ItemType.Armor, 0.22),
                new TierLoot(9, ItemType.Armor, 0.17),
                new TierLoot(10, ItemType.Armor, 0.12),
                new TierLoot(11, ItemType.Armor, 0.09),
                new TierLoot(4, ItemType.Ability, 0.07),
                new TierLoot(5, ItemType.Ability, 0.03),
                new ItemLoot("Magic Dust", 0.5)
                ),
            new Threshold(0.03,
                new ItemLoot("Orb of Conflict", 0.0014, threshold: 0.05),
                new ItemLoot("Dagger of Flaming Fury", 0.0014, threshold: 0.03)
                )
            )
        .Init("Red Flaming Skull",
            new State(
                new State("Orbit Skull Shrine",
                    new Prioritize(
                        new Protect(.3, "Skull Shrine", 30, 15, 15),
                        new Wander(.3)
                        ),
                    new EntityNotExistsTransition("Skull Shrine", 40, "Wander")
                    ),
                new State("Wander",
                    new Wander(.3)
                    ),
                new Shoot(12, 2, 10, coolDown: 750)
                )
            )
        .Init("Blue Flaming Skull",
            new State(
                new State("Orbit Skull Shrine",
                    new Orbit(1.5, 15, 40, "Skull Shrine", .6, 10, orbitClockwise: null),
                    new EntityNotExistsTransition("Skull Shrine", 40, "Wander")
                    ),
                new State("Wander",
                    new Wander(1.5)
                    ),
                new Shoot(12, 2, 10, coolDown: 750)
                )
            );
    }
}
