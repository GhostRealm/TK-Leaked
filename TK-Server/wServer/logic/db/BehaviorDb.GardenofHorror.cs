using common.resources;
using wServer.logic.behaviors;
using wServer.logic.loot;
using wServer.logic.transitions;

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ GardenofHorror = () => Behav()
        .Init("Plantera",
            new State(
                new ScaleHP2(15),
                new State("idle",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                    new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                    new PlayerWithinTransition(20, "prepare")
                    ),
                new State("prepare",
                    new Taunt("Well hello there.."),
                    new Taunt("Come to settle your debt I see"),
                    new TimedTransition(5000, "start flashing")
                    ),
                new State("start flashing",
                    new Flash(0xFF0000, 1, 3),
                    new Taunt("Fluuurrrishh!"),
                    new TimedTransition(5000, "start")
                    ),
                new State("start",
                    new TossObject("Raged Piranha Plant", 3, 0, coolDown: 99999),
                    new TossObject("Raged Piranha Plant", 3, 90, coolDown: 99999),
                    new TossObject("Raged Piranha Plant", 3, 180, coolDown: 99999),
                    new TossObject("Raged Piranha Plant", 3, 270, coolDown: 99999),
                    new TossObject("Piranha Plant", 6, 45, coolDown: 99999),
                    new TossObject("Piranha Plant", 6, 135, coolDown: 99999),
                    new TossObject("Piranha Plant", 6, 225, coolDown: 99999),
                    new TossObject("Piranha Plant", 6, 315, coolDown: 99999),
                    new TimedTransition(5000, "attack1")
                    ),
                new State("attack1",
                    new RemoveConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new RemoveConditionalEffect(ConditionEffectIndex.Invincible),
                    new ChangeSize(1, 180),
                    new Shoot(15, projectileIndex: 0, count: 2, shootAngle: 0, coolDownOffset: 100, coolDown: 2000),
                    new Shoot(15, projectileIndex: 0, count: 2, shootAngle: 18, coolDownOffset: 200, coolDown: 2000),
                    new Shoot(15, projectileIndex: 0, count: 2, shootAngle: 36, coolDownOffset: 300, coolDown: 2000),
                    new Shoot(15, projectileIndex: 0, count: 2, shootAngle: 54, coolDownOffset: 400, coolDown: 2000),
                    new Shoot(15, projectileIndex: 0, count: 2, shootAngle: 72, coolDownOffset: 500, coolDown: 2000),

                    new Shoot(15, projectileIndex: 0, count: 2, shootAngle: 180, coolDownOffset: 1100, coolDown: 2000),
                    new Shoot(15, projectileIndex: 0, count: 2, shootAngle: 198, coolDownOffset: 1200, coolDown: 2000),
                    new Shoot(15, projectileIndex: 0, count: 2, shootAngle: 216, coolDownOffset: 1300, coolDown: 2000),
                    new Shoot(15, projectileIndex: 0, count: 2, shootAngle: 234, coolDownOffset: 1400, coolDown: 2000),
                    new Shoot(15, projectileIndex: 0, count: 2, shootAngle: 252, coolDownOffset: 1500, coolDown: 2000),

                    new Shoot(15, projectileIndex: 0, count: 2, shootAngle: 90, coolDownOffset: 600, coolDown: 2000),
                    new Shoot(15, projectileIndex: 0, count: 2, shootAngle: 108, coolDownOffset: 700, coolDown: 2000),
                    new Shoot(15, projectileIndex: 0, count: 2, shootAngle: 126, coolDownOffset: 800, coolDown: 2000),
                    new Shoot(15, projectileIndex: 0, count: 2, shootAngle: 144, coolDownOffset: 900, coolDown: 2000),
                    new Shoot(15, projectileIndex: 0, count: 2, shootAngle: 162, coolDownOffset: 1000, coolDown: 2000),

                    new Shoot(15, projectileIndex: 0, count: 2, shootAngle: 270, coolDownOffset: 1600, coolDown: 2000),
                    new Shoot(15, projectileIndex: 0, count: 2, shootAngle: 288, coolDownOffset: 1700, coolDown: 2000),
                    new Shoot(15, projectileIndex: 0, count: 2, shootAngle: 306, coolDownOffset: 1800, coolDown: 2000),
                    new Shoot(15, projectileIndex: 0, count: 2, shootAngle: 324, coolDownOffset: 1900, coolDown: 2000),
                    new Shoot(15, projectileIndex: 0, count: 2, shootAngle: 342, coolDownOffset: 2000, coolDown: 2000),
                    new HpLessTransition(0.8, "attack2")
                    ),
                new State("attack2",
                    new ChangeSize(1, 200),
                    new TossObject("Raged Piranha Plant", 3, 0, coolDown: 99999),
                    new TossObject("Raged Piranha Plant", 3, 90, coolDown: 99999),
                    new TossObject("Raged Piranha Plant", 3, 180, coolDown: 99999),
                    new TossObject("Raged Piranha Plant", 3, 270, coolDown: 99999),
                    new TossObject("Piranha Plant", 6, 45, coolDown: 99999),
                    new TossObject("Piranha Plant", 6, 135, coolDown: 99999),
                    new TossObject("Piranha Plant", 6, 225, coolDown: 99999),
                    new TossObject("Piranha Plant", 6, 315, coolDown: 99999),
                    new RingAttack(20, 8, 0, projectileIndex: 0, -0.07, 0, coolDown: 200),
                    new Shoot(15, 4, projectileIndex: 1, predictive: 1.2, coolDown: 400),
                    new HpLessTransition(0.6, "attack3")
                    ),
                new State("attack3",
                    new Taunt("Prepare for DEATH!"),
                    new SetAltTexture(1),
                    new ChangeSize(20, 230),
                    new Chase(1.2, 10),
                    new Shoot(8, 8, projectileIndex: 1, shootAngle: 45, coolDown: 1000),
                    new Shoot(8, 4, projectileIndex: 4, shootAngle: 5, predictive: 1, coolDown: 400),
                    new HpLessTransition(0.4, "spawn minions")
                    ),
                new State("spawn minions",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, false),
                    new Taunt("My Children... finish them!"),
                    new TossObject("Piranha Plant", 6, 0, coolDown: 99999),
                    new TossObject("Piranha Plant", 6, 90, coolDown: 99999),
                    new TossObject("Piranha Plant", 6, 180, coolDown: 99999),
                    new TossObject("Piranha Plant", 6, 270, coolDown: 99999),
                    new TossObject("Raged Piranha Plant", 8, 45, coolDown: 99999),
                    new TossObject("Raged Piranha Plant", 8, 135, coolDown: 99999),
                    new TossObject("Raged Piranha Plant", 8, 225, coolDown: 99999),
                    new TossObject("Raged Piranha Plant", 8, 315, coolDown: 99999),
                    new TimedTransition(5000, "attack4")
                    ),
                new State("attack4",
                    new RingAttack(20, 6, 0, projectileIndex: 0, -0.10, 0, coolDown: 200),
                    new Shoot(20, 1, projectileIndex: 2, predictive: 1.3, coolDown: 1000),
                    new HpLessTransition(0.2, "attack5")
                    ),
                new State("attack5",
                    new SetAltTexture(2),
                    new ChangeSize(1, 130),
                    new Wander(0.8),
                    new Shoot(20, 10, projectileIndex: 3, shootAngle: 36, coolDown: 1000),
                    new Shoot(20, 8, projectileIndex: 2, shootAngle: 45, coolDown: 600),
                    new Shoot(20, 5, projectileIndex: 0, shootAngle: 72, coolDown: 800),
                    new Shoot(20, 12, projectileIndex: 4, shootAngle: 30, coolDown: 1200)
                    )
                ),
            new Threshold(0.01,
                LootTemplates.DustLoot()
                ),
            new Threshold(0.01,
                new TierLoot(12, ItemType.Weapon, 0.2),
                new TierLoot(13, ItemType.Weapon, 0.15),
                new TierLoot(12, ItemType.Armor, 0.2),
                new TierLoot(13, ItemType.Armor, 0.15),
                new TierLoot(6, ItemType.Ring, 0.07),
                new TierLoot(6, ItemType.Ability, 0.07),
                new ItemLoot("Potion of Speed", 1),
                new ItemLoot("Potion of Life", 0.15),
                new ItemLoot("Potion of Mana", 0.15),
                new ItemLoot("Potion of Wisdom", 1)
                )
            )
        .Init("Piranha Plant",
            new State(
                new ScaleHP2(15),
                new State("attack",
                    new Orbit(2, 6, 20, "Plantera", orbitClockwise: true),
                    new Shoot(10, 2, projectileIndex: 0, predictive: .8, coolDown: 500)
                    )
                )
            )
        .Init("Raged Piranha Plant",
            new State(
                new ScaleHP2(15),
                new State("attack",
                new Taunt("FLUHH"),
                new Chase(6),
                new Shoot(12, 2, projectileIndex: 0, predictive: 1.2, coolDown: 800)
                    )
                ));
    }
}

