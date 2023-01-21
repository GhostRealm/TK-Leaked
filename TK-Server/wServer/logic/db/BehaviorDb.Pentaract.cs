﻿using common.resources;
using wServer.logic.behaviors;
using wServer.logic.loot;
using wServer.logic.transitions;

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ Pentaract = () => Behav()
        .Init("Pentaract Eye",
            new State(
                new Prioritize(
                    new Swirl(2, 8, 20, true),
                    new Protect(2, "Pentaract Tower", 20, 6, 4)
                    ),
                new Shoot(9, 1, coolDown: 1000)
                )
            )
        .Init("Pentaract Tower",
            new State(
                new ScaleHP2(30),
                new Spawn("Pentaract Eye", 5, coolDown: 5000, givesNoXp: false),
                new Grenade(4, 100, 8, coolDown: 5000),
                new TransformOnDeath("Pentaract Tower Corpse"),
                new TransferDamageOnDeath("Pentaract"),
                // needed to avoid crash, Oryx.cs needs player name otherwise hangs server (will patch that later)
                new TransferDamageOnDeath("Pentaract Tower Corpse")
                )
            )
        .Init("Pentaract",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                new State("Waiting",
                    new EntityNotExistsTransition("Pentaract Tower", 50, "Die")
                    ),
                new State("Die",
                    new Suicide()
                    )
                )
            )
        .Init("Pentaract Tower Corpse",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                new State("Waiting",
                    new TimedTransition(15000, "Spawn"),
                    new EntityNotExistsTransition("Pentaract Tower", 50, "Die")
                    ),
                new State("Spawn",
                    new Transform("Pentaract Tower")
                    ),
                new State("Die",
                    new Suicide()
                    )
                ),
            new Threshold(0.3,
                new ItemLoot("Potion of Defense", 1)
                ),
            new Threshold(0.2,
                new ItemLoot("Potion of Speed", 0.5),
                new ItemLoot("Potion of Wisdom", 0.5)
                ),
            new Threshold(0.03,
                new ItemLoot("Seal of Blasphemous Prayer", 0.0014, threshold: 0.03),
                new ItemLoot("Midnight Star", 0.0014, threshold: 0.03)
                ),
            new Threshold(0.001,
                new TierLoot(8, ItemType.Weapon, .03),
                new TierLoot(9, ItemType.Weapon, .02),
                new TierLoot(10, ItemType.Weapon, .014),
                new TierLoot(11, ItemType.Weapon, .01),
                new TierLoot(4, ItemType.Ability, .03),
                new TierLoot(5, ItemType.Ability, .014),
                new TierLoot(8, ItemType.Armor, .04),
                new TierLoot(9, ItemType.Armor, .03),
                new TierLoot(10, ItemType.Armor, .02),
                new TierLoot(11, ItemType.Armor, .014),
                new TierLoot(12, ItemType.Armor, .008),
                new TierLoot(3, ItemType.Ring, .03),
                new TierLoot(4, ItemType.Ring, .014),
                new TierLoot(5, ItemType.Ring, .006),
                new ItemLoot("Potion of Defense", .02),
                new ItemLoot("Potion of Attack", .02),
                new ItemLoot("Potion of Vitality", .02),
                new ItemLoot("Potion of Wisdom", .02),
                new ItemLoot("Potion of Speed", .02),
                new ItemLoot("Potion of Dexterity", .02),

                new ItemLoot("Magic Dust", 0.02)
                )
            )
        ;
    }
}
