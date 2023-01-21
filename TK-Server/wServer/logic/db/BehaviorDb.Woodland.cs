﻿using common.resources;
using wServer.logic.behaviors;
using wServer.logic.loot;
using wServer.logic.transitions;

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ Woodland = () => Behav()
        .Init("Woodland Weakness Turret",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                new Shoot(25, projectileIndex: 0, count: 8, coolDown: 3000, coolDownOffset: 4000)
                ))
        .Init("Woodland Silence Turret",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                new Shoot(25, projectileIndex: 0, count: 8, coolDown: 3000, coolDownOffset: 4000)
                ))
        .Init("Woodland Paralyze Turret",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                new Shoot(25, projectileIndex: 0, count: 8, coolDown: 3000, coolDownOffset: 4000)
                ))

        .Init("Wooland Armor Squirrel",
            new State(
                new Prioritize(
                    new Follow(0.52, 8, 2, coolDown: 500),
                    new StayBack(0.7, 4)
                    ),
                new Shoot(25, projectileIndex: 0, count: 3, shootAngle: 30, coolDown: 700, coolDownOffset: 1000)
                ))
        .Init("Woodland Ultimate Squirrel",
            new State(
                new Prioritize(
                    new Follow(0.3, 8, 1),
                    new Wander(0.3)
                    ),
                new Shoot(25, projectileIndex: 0, count: 3, shootAngle: 10, coolDown: 900, coolDownOffset: 1000), new Shoot(25, projectileIndex: 0, count: 3, shootAngle: 35, coolDown: 900, coolDownOffset: 1000),
                new Shoot(25, projectileIndex: 0, count: 1, shootAngle: 35, coolDown: 1100, coolDownOffset: 21000)
                ))
        .Init("Woodland Goblin Mage",
            new State(
                new Prioritize(
                    new Follow(0.3, 8, 2, coolDown: 500),
                    new StayBack(0.7, 4)
                    ),
                new Shoot(55, projectileIndex: 0, count: 2, shootAngle: 10, coolDown: 900, coolDownOffset: 1000)
                ))
        .Init("Woodland Goblin",
            new State(
                new Follow(0.46, 8, 1),
                new Shoot(25, projectileIndex: 0, count: 1, shootAngle: 20, coolDown: 900, coolDownOffset: 1000)
                ))

        .Init("Woodland Mini Megamoth",
            new State(
                new Prioritize(
                    new Protect(0.5, "Epic Mama Megamoth", protectionRange: 15, reprotectRange: 3),
                    new Wander(0.35)
                    ),
                new Shoot(25, projectileIndex: 0, count: 3, shootAngle: 20, coolDown: 700, coolDownOffset: 1000)
                ),
            new Threshold(0.5,
                new ItemLoot("Magic Potion", 0.1),
                new ItemLoot("Magic Potion", 0.1)
                )
            )
        .Init("Mini Larva",
            new State(
                new ScaleHP2(30),
                new Prioritize(
                    new Protect(0.5, "Murderous Megamoth", protectionRange: 15, reprotectRange: 3),
                    new Wander(0.35)
                    ),
                new Shoot(25, projectileIndex: 0, count: 6, coolDown: 3500, coolDownOffset: 1200)
                ),
            new Threshold(0.5,
                new ItemLoot("Health Potion", 0.01),
                new ItemLoot("Magic Potion", 0.01)
                )
            )
        .Init("Blood Ground Effector",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                new ApplySetpiece("Puke"),
                new Suicide()
                ))

        .Init("Epic Larva",
            new State(
                new ScaleHP2(30),
                new State(

                    new ConditionalEffect(ConditionEffectIndex.Armored),
                    new Follow(0.08, 8, 1),
                    new Shoot(8.4, count: 1, fixedAngle: 50, projectileIndex: 0, coolDown: 1750),
                    new Shoot(8.4, count: 1, fixedAngle: 140, projectileIndex: 0, coolDown: 1750),
                    new Shoot(8.4, count: 1, fixedAngle: 240, projectileIndex: 0, coolDown: 1750),
                    new Shoot(8.4, count: 1, fixedAngle: 325, projectileIndex: 0, coolDown: 1750),
                    new Shoot(8.4, count: 1, fixedAngle: 45, projectileIndex: 0, coolDown: 1750),
                    new Shoot(8.4, count: 1, fixedAngle: 135, projectileIndex: 0, coolDown: 1750),
                    new Shoot(8.4, count: 1, fixedAngle: 235, projectileIndex: 0, coolDown: 1750),
                    new Shoot(8.4, count: 1, fixedAngle: 315, projectileIndex: 0, coolDown: 1750),
                    new TossObject("Blood Ground Effector", angle: null, coolDown: 3750),
                    new DamageTakenTransition(12500, "tran")
                    ),
                new State("tran",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new Flash(0xFF0000, 2, 2),
                    new TimedTransition(8750, "home")
                    ),
                new State("home",
                    new TransformOnDeath("Epic Mama Megamoth", 1, 1),
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new Flash(0xFF0000, 2, 2),
                    new Suicide()
                    )
                ))

        .Init("Epic Mama Megamoth",
            new State(
                new ScaleHP2(30),
                new State(
                    new Spawn("Woodland Mini Megamoth", 1, 10, coolDown: 90000),
                    new Spawn("Woodland Mini Megamoth", 1, 2, coolDown: 5500),
                    new Reproduce("Woodland Mini Megamoth", 2, 4, coolDown: 3000),
                    new Prioritize(
                        new Charge(1, range: 4, coolDown: 2000),
                        new Wander(0.36)
                        ),
                    new Shoot(8.4, count: 1, fixedAngle: 60, projectileIndex: 0, coolDown: 2000),
                    new Shoot(8.4, count: 1, fixedAngle: 150, projectileIndex: 0, coolDown: 2000),
                    new Shoot(8.4, count: 1, fixedAngle: 255, projectileIndex: 0, coolDown: 2000),
                    new Shoot(8.4, count: 1, fixedAngle: 335, projectileIndex: 0, coolDown: 2000),
                    new Shoot(8.4, count: 1, fixedAngle: 50, projectileIndex: 0, coolDown: 2000),
                    new Shoot(8.4, count: 1, fixedAngle: 140, projectileIndex: 0, coolDown: 2000),
                    new Shoot(8.4, count: 1, fixedAngle: 240, projectileIndex: 0, coolDown: 2000),
                    new Shoot(8.4, count: 1, fixedAngle: 325, projectileIndex: 0, coolDown: 2000),
                    new Shoot(8.4, count: 1, fixedAngle: 45, projectileIndex: 0, coolDown: 2000),
                    new Shoot(8.4, count: 1, fixedAngle: 135, projectileIndex: 0, coolDown: 2000),
                    new Shoot(8.4, count: 1, fixedAngle: 235, projectileIndex: 0, coolDown: 2000),
                    new Shoot(8.4, count: 1, fixedAngle: 315, projectileIndex: 0, coolDown: 2000),
                    new DamageTakenTransition(14000, "tran")
                    ),
                new State("tran",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new Flash(0xFF0000, 2, 2),
                    new TimedTransition(8750, "home")
                    ),
                new State("home",
                    new TransformOnDeath("Murderous Megamoth", 1, 1),
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new Flash(0xFF0000, 2, 2),
                    new Suicide()
                    )
                ))

        .Init("Murderous Megamoth",
            new State(
                new ScaleHP(10000, 0, true, 15, 1),
                new State(
                    new RealmPortalDrop(),
                    new Spawn("Mini Larva", 1, 14, coolDown: 90000),
                    new Spawn("Mini Larva", 1, 2, coolDown: 5500),
                    new Prioritize(
                        new Charge(1.25, range: 4, coolDown: 2000),
                        new Follow(0.4, 8, 1)
                        ),
                    new Shoot(25, projectileIndex: 1, count: 2, coolDown: 700, coolDownOffset: 1000),
                    new Shoot(8.4, count: 1, fixedAngle: 45, projectileIndex: 0, coolDown: 2000, coolDownOffset: 3000),
                    new Shoot(8.4, count: 1, fixedAngle: 135, projectileIndex: 0, coolDown: 2000, coolDownOffset: 3000),
                    new Shoot(8.4, count: 1, fixedAngle: 235, projectileIndex: 0, coolDown: 2000, coolDownOffset: 3000),
                    new Shoot(8.4, count: 1, fixedAngle: 315, projectileIndex: 0, coolDown: 2000, coolDownOffset: 3000)
                    )),
            new Threshold(0.01,
                LootTemplates.DustLoot()
                ),
            new Threshold(0.03,
                new ItemLoot("Megamoth's Scepter", 0.0014)
                ),
            new Threshold(0.01,
                new TierLoot(10, ItemType.Armor, 0.2),
                new TierLoot(11, ItemType.Armor, 0.1),
                new TierLoot(12, ItemType.Armor, 0.07),
                new TierLoot(10, ItemType.Weapon, 0.2),
                new TierLoot(11, ItemType.Weapon, 0.1),
                new TierLoot(12, ItemType.Weapon, 0.07),
                new TierLoot(3, ItemType.Ability, 0.2),
                new TierLoot(4, ItemType.Ability, 0.15),
                new TierLoot(5, ItemType.Ability, 0.07),
                new TierLoot(3, ItemType.Ring, 0.2),
                new TierLoot(4, ItemType.Ring, 0.15),
                new TierLoot(5, ItemType.Ring, 0.07),
                new ItemLoot("Potion of Vitality", 1),
                new ItemLoot("Leaf Bow", 0.01),
                new ItemLoot("Woodland Labyrinth Key", 0.01, 0, 0.03)

                )
            )

        ;
    }
}
