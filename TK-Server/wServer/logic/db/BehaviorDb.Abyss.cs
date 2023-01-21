using common.resources;
using wServer.logic.behaviors;
using wServer.logic.loot;
using wServer.logic.transitions;

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ Abyss = () => Behav()
        .Init("Archdemon Malphas",
            new State(
                new MoveTo2(0.5f, 0.5f, isMapPosition: false, instant: true),
                new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                new OnDeathBehavior(new ApplySetpiece("AbyssDeath")),
                new ScaleHP2(30),
                new RealmPortalDrop(),
                new State("Check Player",
                    new PlayerWithinTransition(10, "Start Flashing", false)
                    ),
                new State("Start Flashing",
                    new Flash(0xFF0000, 1, 3),
                    new Taunt("My minions will end with you!"),
                    new TimedTransition(3000, "Start")
                    ),
                new State("Start",
                    new TossObject("White Demon of the Abyss", 7, 45, coolDown: 99999, coolDownOffset: -1),
                    new TossObject("White Demon of the Abyss", 7, -45, coolDown: 99999, coolDownOffset: -1),
                    new TossObject("White Demon of the Abyss", 7, 135, coolDown: 99999, coolDownOffset: -1),
                    new TossObject("White Demon of the Abyss", 7, -135, coolDown: 99999, coolDownOffset: -1),
                    new TimedTransition(1500, "Start Two")
                    ),
                new State("Start Two",
                    new RingAttack(20, 4, 0, projectileIndex: 1, 0.07, 0, coolDown: 200),
                    new EntitiesNotExistsTransition(20, "Second Phase Charge", "White Demon of the Abyss")
                    ),
                new State("Second Phase Charge",
                    new Taunt("No! What have you done!"),
                    new TimedTransition(1500, "Second Phase")
                    ),
                new State("Second Phase",
                    new RemoveConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new RingAttack(20, 4, 0, projectileIndex: 1, -0.07, 0, coolDown: 200),
                    new Shoot(20, 3, shootAngle: 15, projectileIndex: 2, coolDown: 1500),
                    new Reproduce("Malphas Missile", coolDown: 1500),
                    new HpLessTransition(0.50, "Third Phase")
                    ),
                new State("Third Phase",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, false, 1500),
                    new Taunt("I will release a part of my power."),
                    new TimedTransition(1500, "Third Phase Start")
                    ),
                new State("Third Phase Start",
                    new RingAttack(20, 6, 0, projectileIndex: 1, -0.07, 0, coolDown: 200),
                    new Shoot(20, 3, shootAngle: 15, projectileIndex: 4, coolDown: 1000),
                    new HpLessTransition(0.25, "Four Phase")
                    ),
                new State("Four Phase",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, false, 1500),
                    new Flash(0xFF0000, 0.2, 5),
                    new Taunt("NOOO! IM GONNA RELEASE ALL MY POWER!!"),
                    new TimedTransition(1500, "Four Phase Start")
                    ),
                new State("Four Phase Start",
                    new RingAttack(20, 4, 0, projectileIndex: 1, 0.07, 0, coolDown: 200),
                    new Shoot(20, 3, shootAngle: 15, projectileIndex: 4, coolDown: 1000),
                    new Wander(0.3),
                    new Reproduce("Malphas Missile", coolDown: 2000)
                    )
                ),
            new Threshold(0.01,
                LootTemplates.DustLoot()
                ),
            new Threshold(0.01,
                new ItemLoot("Malphas Sword", 0.005),
                new ItemLoot("Malphas Seal", 0.005),
                new ItemLoot("Malphas Helm", 0.005),
                new ItemLoot("Malphas Armor", 0.007),
                new ItemLoot("Malphas Juice", 0.009),
                new ItemLoot("Abyss of Demons Key", 0.01, 0, 0.03),
                new ItemLoot("Demon Blade", 0.01),
                new ItemLoot("Sword of Illumination", 0.01),
                new ItemLoot("Potion of Vitality", 0.2, 1),
                new ItemLoot("Potion of Defense", 0.8, 1),
                new TierLoot(9, ItemType.Armor, 0.1),
                new TierLoot(10, ItemType.Armor, 0.08),
                new TierLoot(11, ItemType.Armor, 0.06),
                new TierLoot(9, ItemType.Weapon, 0.1),
                new TierLoot(10, ItemType.Weapon, 0.08),
                new TierLoot(11, ItemType.Weapon, 0.06),
                new TierLoot(3, ItemType.Ring, 0.1),
                new TierLoot(4, ItemType.Ring, 0.08),
                new TierLoot(5, ItemType.Ring, 0.04),
                new TierLoot(3, ItemType.Ability, 0.1),
                new TierLoot(4, ItemType.Ability, 0.08)
                )
            )
        .Init("Malphas Missile",
            new State(
                new Follow(6, 20, 0),
                new PlayerWithinTransition(0, "Explode"),
                new State("Explode",
                    new Flash(0xFFFFFF, 0.1, 5),
                    new TimedTransition(500, "Explode v2")
                    ),
                new State("Explode v2",
                    new Shoot(10, 8),
                    new Decay(0)
                    )
                //new Flash(0xFFFFFF, 0.2, 5),
                /*new State("explode",
                new Shoot(10, 8),
                new Decay(100)
                )*/
                )
            )
        .Init("Imp of the Abyss",
            new State(
                new Wander(0.2),
                new Shoot(8, 5, 10, coolDown: 3200)
                ),
            new ItemLoot("Magic Potion", 0.1),
            new ItemLoot("Health Potion", 0.1),
            new Threshold(0.5,
                new ItemLoot("Cloak of the Red Agent", 0.01),
                new ItemLoot("Felwasp Toxin", 0.01)
                ),
            new Threshold(0.01,
                new ItemLoot("Talisman of Looting", 0.02)
                )
            )
        .Init("Demon of the Abyss",
            new State(
                new Prioritize(
                    new Follow(4, 8, 5),
                    new Wander(0.25)
                    ),
                new Shoot(8, 3, shootAngle: 10, coolDown: 5000)
                ),
            new ItemLoot("Fire Bow", 0.05),
            new Threshold(0.5,
                new ItemLoot("Mithril Armor", 0.01)
                ),
            new Threshold(0.01,
                new ItemLoot("Talisman of Looting", 0.02)
                )
            )
        .Init("Demon Warrior of the Abyss",
            new State(
                new Prioritize(
                    new Follow(5, 8, 5),
                    new Wander(0.25)
                    ),
                new Shoot(8, 3, shootAngle: 10, coolDown: 3000)
                ),
            new Threshold(0.01,
                new ItemLoot("Talisman of Looting", 0.02)
                ),
            new ItemLoot("Fire Sword", 0.025),
            new ItemLoot("Steel Shield", 0.025)
            )
        .Init("Demon Mage of the Abyss",
            new State(
                new Prioritize(
                    new Follow(4, 8, 5),
                    new Wander(0.25)
                    ),
                new Shoot(8, 3, shootAngle: 10, coolDown: 3400)
                ),
            new ItemLoot("Fire Nova Spell", 0.02),
            new Threshold(0.1,
                new ItemLoot("Wand of Dark Magic", 0.01),
                new ItemLoot("Avenger Staff", 0.01),
                new ItemLoot("Robe of the Invoker", 0.01),
                new ItemLoot("Essence Tap Skull", 0.01),
                new ItemLoot("Demonhunter Trap", 0.01)
                )
            )
        .Init("Brute of the Abyss",
            new State(
                new Prioritize(
                    new Follow(7, 8, 1),
                    new Wander(0.25)
                    ),
                new Shoot(8, 3, shootAngle: 10, coolDown: 800)
                ),
            new ItemLoot("Magic Potion", 0.1),
            new Threshold(0.1,
                new ItemLoot("Obsidian Dagger", 0.02),
                new ItemLoot("Steel Helm", 0.02)
                )
            )
        .Init("Brute Warrior of the Abyss",
            new State(
                new Prioritize(
                    new Follow(4, 8, 1),
                    new Wander(0.25)
                    ),
                new Shoot(8, 3, shootAngle: 10, coolDown: 800)
                ),
            new ItemLoot("Spirit Salve Tome", 0.02),
            new Threshold(0.5,
                new ItemLoot("Glass Sword", 0.01),
                new ItemLoot("Ring of Greater Dexterity", 0.01),
                new ItemLoot("Magesteel Quiver", 0.01)
                )
            )
        .Init("White Demon of the Abyss",
            new State(
                new Prioritize(
                    new StayAbove(1, 200),
                    new Follow(1, range: 7),
                    new Wander(0.4)
                    ),
                new Shoot(10, 3, 20, predictive: 1, coolDown: 500)
                )
            )
        ;
    }
}
