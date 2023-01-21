﻿using common.resources;
using wServer.logic.behaviors;
using wServer.logic.loot;
using wServer.logic.transitions;

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ SnakePit = () => Behav()
        .Init("Stheno the Snake Queen",
            new State(
                new ScaleHP2(30),
                new DropPortalOnDeath("Realm Portal", 1, 0, 0, 0, 120),
                new State("Waiting Player",
                    new PlayerWithinTransition(20, "Start")
                    ),
                new State("Start",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new Taunt("Tssss..."),
                    new TimedTransition(1000, "First Phase")
                    ),
                new State("First Phase",
                    new Wander(0.3),
                    new Reproduce("Stheno Swarm", 15, 5, 1500),
                    new Grenade(3.5, 150, 11, null, 1500, ConditionEffectIndex.Confused, 1000),
                    new Shoot(2, 3, shootAngle: 15, projectileIndex: 0, coolDown: 1500),
                    new HpLessTransition(0.66, "Second Phase Start")
                    ),
                new State("Second Phase Start",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new ReturnToSpawn(0.7, 1),
                    new Flash(0x008000, 0.5, 3),
                    new TimedTransition(1500, "Second Phase")
                    ),
                new State("Second Phase",
                    new Grenade(3.5, 150, 11, null, 1000, ConditionEffectIndex.Confused, 1000),
                    new Shoot(25, 4, null, 2, 0, 15, coolDown: 250),
                    new HpLessTransition(0.33, "Third Phase Start")
                    ),
                new State("Third Phase Start",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new Flash(0xFF0000, 0.5, 5),
                    new TimedTransition(1500, "Third Phase")
                    ),
                new State("Third Phase",
                    new Shoot(30, 3, shootAngle: 15, projectileIndex: 0, coolDown: 1500),
                    new Shoot(25, 4, null, 2, 0, 15, coolDown: 500),

                    /* 0-90-180-360 */
                    new Grenade(1.5, 75, 6, _upAngle, 1500, ConditionEffectIndex.Petrify, 1000),
                    new Grenade(1.5, 75, 6, _downAngle, 1500, ConditionEffectIndex.Petrify, 1000),
                    new Grenade(1.5, 75, 6, _leftAngle, 1500, ConditionEffectIndex.Petrify, 1000),
                    new Grenade(1.5, 75, 6, _rightAngle, 1500, ConditionEffectIndex.Petrify, 1000),

                    /* 45 */
                    new Grenade(1.5, 75, 6, _upAngle - 45, 3000, ConditionEffectIndex.Petrify, 1000),
                    new Grenade(1.5, 75, 6, _downAngle + 45, 3000, ConditionEffectIndex.Petrify, 1000),
                    new Grenade(1.5, 75, 6, _leftAngle + 45, 3000, ConditionEffectIndex.Petrify, 1000),
                    new Grenade(1.5, 75, 6, _rightAngle + 45, 3000, ConditionEffectIndex.Petrify, 1000)
                    )
                ),
            new Threshold(0.03,
                new ItemLoot("Queen's Prism", 0.0014)
                ),
            new Threshold(0.01,
                new ItemLoot("Potion of Speed", 0.8, 1),
                new ItemLoot("Stheno Queen Katana", 0.005),
                new ItemLoot("Stheno Queen Armor", 0.005),
                new ItemLoot("Stheno Queen Star", 0.005),
                new ItemLoot("Stheno Queen Ring", 0.005),
                new ItemLoot("Snake Pit Key", 0.001),
                new ItemLoot("Wand of the Bulwark", 0.008),
                new ItemLoot("Snake Skin Armor", 0.1),
                new ItemLoot("Snake Skin Shield", 0.1),
                new ItemLoot("Snake Eye Ring", 0.1),
                new TierLoot(9, ItemType.Weapon, 0.2),
                new TierLoot(10, ItemType.Weapon, 0.1),
                new TierLoot(10, ItemType.Weapon, 0.07),
                new TierLoot(8, ItemType.Armor, 0.3),
                new TierLoot(9, ItemType.Armor, 0.2),
                new TierLoot(10, ItemType.Armor, 0.1),
                new TierLoot(11, ItemType.Armor, 0.07)
                )
            )
        .Init("Stheno Swarm",
            new State(
                new State("Protect",
                    new Prioritize(
                        new Protect(0.3, "Stheno the Snake Queen"),
                        new Wander(0.3)
                        ),
                    new Shoot(10, coolDown: new Cooldown(750, 250))
                    ),
                new State("Despawn",
                    new Suicide()
                    )
                )
            )
        .Init("Stheno Pet",
            new State(
                new State("Protect",
                    new Shoot(25, coolDown: 1000),
                    new State("Protect",
                        new EntityNotExistsTransition("Stheno the Snake Queen", 100, "Wander"),
                        new Orbit(7.5, 10, acquireRange: 50, target: "Undead Stheno")
                        ),
                    new State("Wander",
                        new Prioritize(
                            new Wander(1)
                            )
                        )
                    )
                )
            )
        .Init("Pit Snake",
            new State(
                new Prioritize(
                    new StayCloseToSpawn(1),
                    new Wander(1)
                    ),
                new Shoot(20, coolDown: 1000)
                )
            )
        .Init("Pit Viper",
            new State(
                new Prioritize(
                    new StayCloseToSpawn(1),
                    new Wander(1)
                    ),
                new Shoot(20, coolDown: 1000)
                )
            )
        .Init("Yellow Python",
            new State(
                new Prioritize(
                    new Follow(1, 10, 1),
                    new StayCloseToSpawn(1),
                    new Wander(1)
                    ),
                new Shoot(20, coolDown: 1000)
                ),
            new ItemLoot("Snake Oil", 0.1),
            new ItemLoot("Ring of Speed", 0.1),
            new ItemLoot("Ring of Vitality", 0.1)
            )
        .Init("Brown Python",
            new State(
                new Prioritize(
                    new StayCloseToSpawn(1),
                    new Wander(1)
                    ),
                new Shoot(20, coolDown: 1000)
                ),
            new ItemLoot("Snake Oil", 0.1),
            new ItemLoot("Leather Armor", 0.1),
            new ItemLoot("Ring of Wisdom", 0.1)
            )
        .Init("Fire Python",
            new State(
                new Prioritize(
                    new Follow(1, 10, 1, coolDown: 2000),
                    new Wander(1)
                    ),
                new Shoot(15, count: 3, shootAngle: 5, coolDown: 1000)
                ),
            new ItemLoot("Snake Oil", 0.1),
            new ItemLoot("Fire Bow", 0.1),
            new ItemLoot("Fire Nova Spell", 0.1)
            )
        .Init("Greater Pit Snake",
            new State(
                new Prioritize(
                    new Follow(1, 10, 5),
                    new Wander(1)
                    ),
                new Shoot(15, count: 3, shootAngle: 5, coolDown: 1000)
                ),
            new ItemLoot("Snake Oil", 0.1),
            new ItemLoot("Glass Sword", 0.1),
            new ItemLoot("Avenger Staff", 0.1),
            new ItemLoot("Wand of Dark Magic", 0.1)
            )
        .Init("Greater Pit Viper",
            new State(
                new Prioritize(
                    new Follow(1, 10, 5),
                    new Wander(1)
                    ),
                new Shoot(15, coolDown: 300)
                ),
            new ItemLoot("Snake Oil", 0.1),
            new Threshold(0.1,
                new ItemLoot("Ring of Greater Attack", 0.1),
                new ItemLoot("Ring of Greater Health", 0.1)
                )
            )
        .Init("Snakepit Guard",
            new State(
                new ChangeSize(100, 100),
                new Shoot(25, count: 3, shootAngle: 25, projectileIndex: 0, coolDown: new Cooldown(1000, 200)),
                new Shoot(10, count: 6, projectileIndex: 1, coolDown: 1000),
                new State("Phase 1",
                    new Prioritize(
                        new StayCloseToSpawn(0.2, 4),
                        new Wander(0.2)
                        ),
                    new HpLessTransition(0.6, "Phase 2")
                    ),
                new State("Phase 2",
                    new Prioritize(
                        new Follow(0.2, acquireRange: 10, range: 3),
                        new Wander(0.2)
                        ),
                    new Shoot(15, count: 3, projectileIndex: 2, coolDown: 2000)
                    )
                ),
            new Threshold(0.32,
                new ItemLoot("Potion of Speed", 1)
                ),
            new Threshold(0.1,
                new ItemLoot("Wand of the Bulwark", 0.005),
                new ItemLoot("Snake Skin Armor", 0.1),
                new ItemLoot("Snake Skin Shield", 0.1),
                new ItemLoot("Snake Eye Ring", 0.1),
                new ItemLoot("Wine Cellar Incantation", 0.05),
                new TierLoot(9, ItemType.Weapon, 0.2),
                new TierLoot(10, ItemType.Weapon, 0.1),
                new TierLoot(8, ItemType.Armor, 0.3),
                new TierLoot(9, ItemType.Armor, 0.2),
                new TierLoot(10, ItemType.Armor, 0.1)
                )
            )
        .Init("Snakepit Dart Thrower",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                new State("Idle"),
                new State("Protect the Guard",
                    new EntityNotExistsTransition("Snakepit Guard", 40, "Idle")
                    )
                )
            )
        .Init("Snakepit Button",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                new State("Idle",
                    new PlayerWithinTransition(0.5, "Order")
                    ),
                new State("Order",
                    new Order(15, "Snakepit Guard Spawner", "Spawn the Guard"),
                    new SetAltTexture(1),
                    new TimedTransition(0, "I am out")
                    ),
                new State("I am out")
                )
            )
        .Init("Snakepit Guard Spawner",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                new State("Idle"),
                new State("Spawn the Guard",
                    new Order(15, "Snakepit Dart Thrower", "Protect the Guard"),
                    new Spawn("Snakepit Guard", maxChildren: 1, initialSpawn: 1),
                    new TimedTransition(0, "Idle")
                    )
                )
            )
        .Init("Snake Grate",
            new State(
                new State("Idle",
                    new EntityNotExistsTransition("Pit Snake", 5, "Spawn Pit Snake"),
                    new EntityNotExistsTransition("Pit Viper", 5, "Spawn Pit Viper")
                    ),
                new State("Spawn Pit Snake",
                    new Spawn("Pit Snake", 1, 1),
                    new TimedTransition(2000, "Idle")
                    ),
                new State("Spawn Pit Viper",
                    new Spawn("Pit Viper", 1, 1),
                    new TimedTransition(2000, "Idle")
                    )
                )
            );
    }
}
