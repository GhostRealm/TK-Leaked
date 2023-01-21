using common.resources;
using wServer.logic.behaviors;
using wServer.logic.loot;
using wServer.logic.transitions;

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ SoulBosses = () => Behav()

        .Init("Soul Death",
            new State(
                new SetNoXP(),
                new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                new State("First",
                    new TimedTransition(150, "Second")
                    ),
                new State("Second",
                    new SetAltTexture(1, 1),
                    new TimedTransition(300, "Third")
                    ),
                new State("Third",
                    new SetAltTexture(2, 2),
                    new TimedTransition(300, "Suicide")
                    ),
                new State("Suicide",
                    new Suicide()
                    )
                )
            )

        #region Soul of Life Boss (TOMB) - Done

        .Init("The Bes Nuttiest Geb Sarcophagus",
            new State(
                new OnDeathBehavior(new SwirlingMistDeathParticles()),
                new ScaleHP2(20),
                new State("Waiting Player",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                    new PlayerWithinTransition(15, "Start")
                    ),
                new State("Start",
                    new Taunt("May the strength of the gods be with you ... you will need it."),
                    new TimedTransition(2000, "Start Spawning")
                    ),
                new State("Start Spawning",
                    new Spawn("Jackal Lord", 3, 0.5, coolDown: 1000, true),
                    new EntityNotExistsTransition("Jackal Lord", 30, "Weakness")
                    ),
                new State("Weakness",
                    new TimedTransition(500, "Weakness 2")
                    ),
                new State("Weakness 2",
                    new Shoot(50, 20, projectileIndex: 0, coolDown: 3000),
                    new TimedTransition(6000, "Vulnerable")
                    ),
                new State("Vulnerable",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, false, 0),
                    new Shoot(7, 4, shootAngle: 15, projectileIndex: 2, coolDown: 1000),
                    new Shoot(30, 5, shootAngle: 25, projectileIndex: 1, coolDown: 2500),
                    new Shoot(30, 5, shootAngle: 25, projectileIndex: 1, coolDown: 2750, coolDownOffset: 2500),
                    new Shoot(30, 5, shootAngle: 25, projectileIndex: 1, coolDown: 2900, coolDownOffset: 2750),
                    new HpLessTransition(0.8, "Hard")
                    ),
                new State("Hard",
                    new Shoot(7, 4, shootAngle: 15, projectileIndex: 2, coolDown: 1000),
                    new Shoot(30, 5, shootAngle: 25, projectileIndex: 1, coolDown: 2500),
                    new Shoot(30, 5, shootAngle: 25, projectileIndex: 1, coolDown: 2750, coolDownOffset: 2500),
                    new Shoot(30, 3, shootAngle: 15, projectileIndex: 3, coolDown: 1500),
                    new HpLessTransition(0.5, "Fast Shots")
                    ),
                new State("Fast Shots",
                    new Spawn("Jackal Lord", 3, 0.5, coolDown: 1000, true),
                    new Shoot(7, 4, shootAngle: 15, projectileIndex: 2, coolDown: 1000),
                    new Shoot(30, 5, shootAngle: 25, projectileIndex: 1, coolDown: 2500),
                    new Shoot(30, 5, shootAngle: 25, projectileIndex: 1, coolDown: 2750, coolDownOffset: 2500),
                    new Shoot(30, 3, shootAngle: 35, projectileIndex: 4, coolDown: 750),
                    new TransformOnDeath("Soul of Life Mob", 1, 1, 1)
                    )
                )
            )

        .Init("Soul of Life Opener",
            new State(
                new State("Waiting",
                    new EntitiesNotExistsTransition(100, "OpenDoor", "Tomb Defender", "Tomb Attacker", "Tomb Support", "Tomb Defender Statue", "Tomb Attacker Statue", "Tomb Support Statue")
                    ),
                new State("OpenDoor",
                    new OpenGate(145, 145, 12, 14),
                    new Suicide()
                    )
                )
            )

        .Init("Soul of Life Mob",
            new State(
                new TransformOnDeath("Soul Death", 1, 1, 1),
                new ScaleHP2(10),
                new State("Poison",
                    new Grenade(3, 25, 15, coolDown: 1500, effect: ConditionEffectIndex.ArmorBroken, effectDuration: 1000),
                    new HpLessTransition(0.5, "Rage")
                    ),
                new State("Rage",
                    new SetAltTexture(1, 1),
                    new Grenade(3, 25, 15, coolDown: 1000, effect: ConditionEffectIndex.ArmorBroken, effectDuration: 1000)
                    )
                ),
            new Threshold(0.01,
                LootTemplates.DustLoot()
                ),
            new Threshold(0.007,
                new ItemLoot("Soul of Life", 1)
                ),
            new Threshold(0.03,
                new ItemLoot("Bow of the Havens", 0.0014),
                new ItemLoot("Geb's Hand", 0.0014),
                new ItemLoot("Shield of The Ancient's", 0.0014),
                new ItemLoot("Soul of Mana", 0.1),
                new ItemLoot("Pharaoh's Tome", 0.0014)
                ),
            new Threshold(0.01,
                new TierLoot(10, ItemType.Weapon, 0.25),
                new TierLoot(11, ItemType.Weapon, 0.2),
                new TierLoot(4, ItemType.Ring, 0.25),
                new TierLoot(5, ItemType.Ability, 0.2),
                new TierLoot(11, ItemType.Armor, 0.2)
                )
            )

        #endregion Soul of Life Boss (TOMB) - Done

        #region Soul of Mana Boss (OT) - Done

        .Init("Soul of Mana Opener",
            new State(
                new State("Waiting",
                    new EntityNotExistsTransition("Thessal the Mermaid Goddess", 100, "Done")
                    ),
                new State("Done",
                    new OpenGate(27, 27, 41, 43),
                    new Suicide()
                    )
                )
            )

        .Init("Undead Thessal",
            new State(
                new OnDeathBehavior(new SwirlingMistDeathParticles()),
                new ScaleHP2(30),
                new State("Waiting",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                    new PlayerWithinTransition(15, "Taunt")
                    ),
                new State("Taunt",
                    new Taunt("Do you want me to take you to the depths? Where there are things that nobody knows."),
                    new TimedTransition(2000, "Shot")
                    ),
                new State("Shot",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, false, 0),
                    new Shoot(20, 3, shootAngle: 15, projectileIndex: 0, coolDown: 500),
                    new HpLessTransition(0.8, "Chase")
                    ),
                new State("Chase",
                    new Follow(1, 15, 1),
                    new Wander(1),
                    new Shoot(20, 8, projectileIndex: 1, coolDown: 2000),
                    new Shoot(20, 3, shootAngle: 15, projectileIndex: 0, coolDown: 500),
                    new HpLessTransition(0.6, "Back")
                    ),
                new State("Back",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                    new ReturnToSpawn(1.5),
                    new TimedTransition(2000, "Back V2")
                    ),
                new State("Back V2",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, false, 0),
                    new Grenade(3, 65, range: 15, coolDown: 1000, effect: ConditionEffectIndex.Quiet, effectDuration: 1000),
                    new Shoot(15, 1, projectileIndex: 2, coolDown: 1500),
                    new Shoot(15, 2, shootAngle: 25, projectileIndex: 2, coolDownOffset: 500, coolDown: 1500),
                    new HpLessTransition(0.3, "Stay Back")
                    ),
                new State("Stay Back",
                    new StayBack(0.7, 7),
                    new Shoot(15, 1, projectileIndex: 3, coolDown: 1250),
                    new Shoot(15, 2, shootAngle: 25, projectileIndex: 3, coolDownOffset: 500, coolDown: 1250),
                    new Shoot(20, 3, shootAngle: 15, projectileIndex: 0, coolDown: 500),
                    new TransformOnDeath("Soul of Mana Mob", 1, 1, 1)
                    )
                )
            )

        .Init("Soul of Mana Mob",
            new State(
                new TransformOnDeath("Soul Death", 1, 1, 1),
                new ScaleHP2(10),
                new State("Moving",
                    new Wander(1),
                    new StayBack(1, 6),
                    new Shoot(20, 1, projectileIndex: 0, coolDown: 1500),
                    new Shoot(20, 2, shootAngle: 25, projectileIndex: 1, coolDown: 1500),
                    new HpLessTransition(0.5, "Rage")
                    ),
                new State("Rage",
                    new SetAltTexture(1, 1),
                    new Wander(1.2),
                    new StayBack(1.2, 6),
                    new Shoot(20, 1, projectileIndex: 0, coolDown: 1000),
                    new Shoot(20, 2, shootAngle: 25, projectileIndex: 1, coolDown: 1000)
                    )
                ),
            new Threshold(0.01,
                LootTemplates.DustLoot()
                ),
            new Threshold(0.005,
                new ItemLoot("Soul of Mana", 1)
                ),
            new Threshold(0.03,
                new ItemLoot("Soul of Mana", 0.1),
                new ItemLoot("Thessal's Slayer", 0.0014),
                new ItemLoot("Heart of the Sea", 0.0014)
                ),
            new Threshold(0.01,
                new TierLoot(11, ItemType.Armor, 0.2),
                new TierLoot(10, ItemType.Armor, 0.25),
                new TierLoot(4, ItemType.Ability, 0.25),
                new TierLoot(5, ItemType.Ring, 0.2),
                new TierLoot(10, ItemType.Weapon, 0.25),
                new ItemLoot("Coral Bow", 0.01),
                new ItemLoot("Coral Venom Trap", 0.01),
                new ItemLoot("Coral Silk Armor", 0.01),
                new ItemLoot("Coral Ring", 0.01)
                )
            )

        #endregion Soul of Mana Boss (OT) - Done

        #region Soul of Attack Boss (Puppet) - Done

        .Init("Soul of Attack Opener",
            new State(
                new State("Waiting",
                    new EntitiesNotExistsTransition(100, "Done", "The Puppet Master", "Puppet Loot Chest")
                    ),
                new State("Done",
                    new OpenGate(51, 51, 56, 58),
                    new Suicide()
                    )
                )
            )

        .Init("Undead Puppet Master",
            new State(
                new ScaleHP2(30),
                new OnDeathBehavior(new SwirlingMistDeathParticles()),
                new State("Waiting",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                    new PlayerWithinTransition(9, "Taunt")
                    ),
                new State("Taunt",
                    new Taunt("Do you want to see a magic trick?"),
                    new MoveTo(1, 62, 57),
                    new TimedTransition(2000, "Start")
                    ),
                new State("Start",
                    new Spawn("Puppet Knight 2", 10, 0.1, coolDown: 3000),
                    new Spawn("Puppet Priest 2", 15, 0.1, coolDown: 3000),
                    new Spawn("Puppet Wizard 2", 10, 0.1, coolDown: 3000),
                    new Shoot(20, 1, projectileIndex: 2, coolDown: 1000),
                    new RingAttack(50, 6, 0, projectileIndex: 0, 0.03, fixedAngle: 0, coolDown: 1000),
                    new EntitiesNotExistsTransition(30, "Last Phase", "Puppet Knight 2", "Puppet Priest", "Puppet Wizard 2")
                    ),
                new State("Last Phase",
                    new Wander(0.5),
                    new StayBack(0.5, 7),
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, false, 0),
                    new Shoot(20, 3, shootAngle: 15, projectileIndex: 1, coolDown: 1000),
                    new Shoot(20, 1, projectileIndex: 2, coolDown: 1000),
                    new TransformOnDeath("Soul of Attack Mob", 1, 1, 1)
                    )
                )
            )

        .Init("Puppet Wizard 2",
            new State(
                new Prioritize(
                    new Orbit(0.37, 4, 20, "Undead Puppet Master"),
                    new Wander(0.4)
                    ),
                new Shoot(8.4, count: 10, projectileIndex: 0, coolDown: 2650)
                )
            )

        .Init("Puppet Priest 2",
            new State(
                new Orbit(0.37, 4, 20, "Undead Puppet Master"),
                new HealGroup(8, "Master", coolDown: 4500, healAmount: 75)
                )
            )

        .Init("Puppet Knight 2",
            new State(
                new Prioritize(
                    new Follow(0.58, 8, 1),
                    new Wander(0.2)
                    ),
                new Shoot(8.4, count: 1, projectileIndex: 0, coolDown: 1750),
                new Shoot(8, count: 1, projectileIndex: 1, coolDown: 2000)
                )
            )

        .Init("Soul of Attack Mob",
            new State(
                new TransformOnDeath("Soul Death", 1, 1, 1),
                new ScaleHP2(10),
                new Wander(0.4),
                new StayBack(1.1, 7),
                new State("Shot",
                    new Shoot(20, 8, projectileIndex: 0, coolDown: 1000),
                    new Grenade(2, 50, 20, coolDown: 2000),
                    new HpLessTransition(0.5, "Rage")
                    ),
                new State("Rage",
                    new SetAltTexture(1, 1),
                    new Shoot(20, 8, projectileIndex: 0, coolDown: 1000),
                    new Grenade(3, 50, 20, coolDown: 2000, effect: ConditionEffectIndex.Confused, effectDuration: 1000)
                    )
                ),
            new Threshold(0.01,
                LootTemplates.DustLoot()
                ),
            new Threshold(0.005,
                new ItemLoot("Soul of Attack", 1)
                ),
            new Threshold(0.03,
                new ItemLoot("Harlequin Armor", 0.0014),
                new ItemLoot("Soul of Attack", 0.1),
                new ItemLoot("Laughing Gas", 0.0014)
                ),
            new Threshold(0.01,
                new TierLoot(10, ItemType.Weapon, 0.15),
                new TierLoot(11, ItemType.Weapon, 0.1),
                new TierLoot(4, ItemType.Ability, 0.15),
                new TierLoot(4, ItemType.Ring, 0.15),
                new TierLoot(11, ItemType.Armor, 0.1)
                )
            )

        #endregion Soul of Attack Boss (Puppet) - Done

        #region Soul of Defense Boss (TCave) - Done

        .Init("Throne",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                new State("Waiting"
                    ),
                new State("Im here",
                    new EntitiesNotExistsTransition(100, "Transform", "Golden Idol 1", "Golden Idol 2", "Golden Idol 3")
                    ),
                new State("Transform",
                    new SwirlingMistDeathParticles(),
                    new Transform("Soul of Defense Mob"),
                    new Decay(100)
                    )
                )
            )

        .Init("Soul of Defense Mob",
            new State(
                new TransformOnDeath("Soul Death", 1, 1, 1),
                new ScaleHP2(10),
                new Wander(0.4),
                new Follow(1.1, 20, 1),
                new State("First",
                    new Shoot(20, 2, projectileIndex: 0, coolDown: 1000),
                    new Shoot(20, 1, projectileIndex: 1, coolDown: 750),
                    new HpLessTransition(0.5, "Second")
                    ),
                new State("Second",
                    new SetAltTexture(1, 1),
                    new Shoot(20, 8, projectileIndex: 0, coolDown: 750),
                    new Shoot(20, 1, projectileIndex: 1, coolDown: 500)
                    )
                ),
            new Threshold(0.005,
                new ItemLoot("Soul of Defense", 1)
                ),
            new Threshold(0.01,
                LootTemplates.DustLoot()
                ),
            new Threshold(0.03,
                new ItemLoot("Soul of Defense", 0.1),
                new ItemLoot("Golden Coat", 0.0014)
                ),
            new Threshold(0.01,
                new TierLoot(10, ItemType.Armor, 0.15),
                new TierLoot(11, ItemType.Armor, 0.1),
                new TierLoot(5, ItemType.Ring, 0.1),
                new TierLoot(5, ItemType.Ability, 0.1),
                new ItemLoot("Golden Oryx Sword", 0.005),
                new ItemLoot("Golden Oryx Helm", 0.005),
                new ItemLoot("Golden Oryx Armor", 0.005),
                new ItemLoot("Golden Oryx Ring", 0.005)
                )
            )

        .Init("Soul of Defense Opener",
            new State(
                new State("Waiting",
                    new EntityNotExistsTransition("Golden Oryx Effigy", 30, "Open")
                    ),
                new State("Open",
                    new OpenGate(26, 26, 18, 20),
                    new Suicide()
                    )
                )
            )

        .Init("Golden Idol 1",
            new State(
                new TransformOnDeath("Gold Statue 1", 1, 1, 1),
                new Order(10, "Throne", "Im here"),
                new ScaleHP2(30),
                new State("Start",
                    new ConditionalEffect(ConditionEffectIndex.Armored, true),
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                    new PlayerWithinTransition(15, "Taunt")
                    ),
                new State("Taunt",
                    new Taunt("Hello Warrior, We have some gold to give you."),
                    new TimedTransition(2000, "Start Shooting")
                    ),
                new State("Start Shooting",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, false, 0),
                    new Grenade(2, 25, 15, coolDown: 1500, effect: ConditionEffectIndex.Weak, effectDuration: 1500),
                    new Shoot(20, 1, projectileIndex: 1, coolDown: 1500),
                    new Shoot(20, 4, projectileIndex: 0, coolDown: 1000),
                    new HpLessTransition(0.6, "Prepare")
                    ),
                new State("Prepare",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, false, 2000),
                    new TimedTransition(2000, "Orbit")
                    ),
                new State("Orbit",
                    new Orbit(0.4, 2, 10, "Throne"),
                    new Shoot(20, 1, projectileIndex: 1, coolDown: 1500),
                    new Shoot(20, 4, projectileIndex: 0, coolDown: 1000),
                    new HpLessTransition(0.3, "Prepare v2")
                    ),
                new State("Prepare v2",
                    new Orbit(0.6, 2, 10, "Throne"),
                    new Shoot(20, 1, projectileIndex: 1, coolDown: 1500),
                    new Shoot(20, 4, projectileIndex: 0, coolDown: 1000),
                    new Shoot(20, 3, shootAngle: 15, projectileIndex: 3, coolDown: 750),
                    new HpLessTransition(0.1, "Last")
                    ),
                new State("Last",
                    new Shoot(20, 1, projectileIndex: 1, coolDown: 1500),
                    new Shoot(20, 2, shootAngle: 25, projectileIndex: 2, coolDown: 2000),
                    new Shoot(20, 3, shootAngle: 15, projectileIndex: 3, coolDown: 750),
                    new Grenade(2, 50, 15, coolDown: 2000, effect: ConditionEffectIndex.Slowed, effectDuration: 1000)
                    )
                )
            )

        .Init("Golden Idol 2",
            new State(
                new TransformOnDeath("Gold Statue 2", 1, 1, 1),
                new ScaleHP2(30),
                new State("Start",
                    new ConditionalEffect(ConditionEffectIndex.Armored, true),
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                    new PlayerWithinTransition(15, "Taunt")
                    ),
                new State("Taunt",
                    new Taunt("Hello Warrior, We have some gold to give you."),
                    new TimedTransition(2000, "Start Shooting")
                    ),
                new State("Start Shooting",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, false, 0),
                    new Grenade(2, 25, 15, coolDown: 1500, effect: ConditionEffectIndex.Weak, effectDuration: 1500),
                    new Shoot(20, 1, projectileIndex: 1, coolDown: 1500),
                    new Shoot(20, 4, projectileIndex: 0, coolDown: 1000),
                    new HpLessTransition(0.6, "Prepare")
                    ),
                new State("Prepare",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, false, 2000),
                    new TimedTransition(2000, "Orbit")
                    ),
                new State("Orbit",
                    new Orbit(0.4, 2, 10, "Throne"),
                    new Shoot(20, 1, projectileIndex: 1, coolDown: 1500),
                    new Shoot(20, 4, projectileIndex: 0, coolDown: 1000),
                    new HpLessTransition(0.3, "Prepare v2")
                    ),
                new State("Prepare v2",
                    new Orbit(0.6, 2, 10, "Throne"),
                    new Shoot(20, 1, projectileIndex: 1, coolDown: 1500),
                    new Shoot(20, 4, projectileIndex: 0, coolDown: 1000),
                    new Shoot(20, 3, shootAngle: 15, projectileIndex: 3, coolDown: 750),
                    new HpLessTransition(0.1, "Last")
                    ),
                new State("Last",
                    new Shoot(20, 1, projectileIndex: 1, coolDown: 1500),
                    new Shoot(20, 2, shootAngle: 25, projectileIndex: 2, coolDown: 2000),
                    new Shoot(20, 3, shootAngle: 15, projectileIndex: 3, coolDown: 750),
                    new Grenade(2, 50, 15, coolDown: 2000, effect: ConditionEffectIndex.Slowed, effectDuration: 1000)
                    )
                )
            )

        .Init("Golden Idol 3",
            new State(
                new TransformOnDeath("Gold Statue 3", 1, 1, 1),
                new ScaleHP2(30),
                new State("Start",
                    new ConditionalEffect(ConditionEffectIndex.Armored, true),
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                    new PlayerWithinTransition(15, "Taunt")
                    ),
                new State("Taunt",
                    new Taunt("Hello Warrior, We have some gold to give you."),
                    new TimedTransition(2000, "Start Shooting")
                    ),
                new State("Start Shooting",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, false, 0),
                    new Grenade(2, 25, 15, coolDown: 1500, effect: ConditionEffectIndex.Weak, effectDuration: 1500),
                    new Shoot(20, 1, projectileIndex: 1, coolDown: 1500),
                    new Shoot(20, 4, projectileIndex: 0, coolDown: 1000),
                    new HpLessTransition(0.6, "Prepare")
                    ),
                new State("Prepare",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, false, 2000),
                    new TimedTransition(2000, "Orbit")
                    ),
                new State("Orbit",
                    new Orbit(0.4, 2, 10, "Throne"),
                    new Shoot(20, 1, projectileIndex: 1, coolDown: 1500),
                    new Shoot(20, 4, projectileIndex: 0, coolDown: 1000),
                    new HpLessTransition(0.3, "Prepare v2")
                    ),
                new State("Prepare v2",
                    new Orbit(0.6, 2, 10, "Throne"),
                    new Shoot(20, 1, projectileIndex: 1, coolDown: 1500),
                    new Shoot(20, 4, projectileIndex: 0, coolDown: 1000),
                    new Shoot(20, 3, shootAngle: 15, projectileIndex: 3, coolDown: 750),
                    new HpLessTransition(0.1, "Last")
                    ),
                new State("Last",
                    new Shoot(20, 1, projectileIndex: 1, coolDown: 1500),
                    new Shoot(20, 2, shootAngle: 25, projectileIndex: 2, coolDown: 2000),
                    new Shoot(20, 3, shootAngle: 15, projectileIndex: 3, coolDown: 750),
                    new Grenade(2, 50, 15, coolDown: 2000, effect: ConditionEffectIndex.Slowed, effectDuration: 1000)
                    )
                )
            )

        #endregion Soul of Defense Boss (TCave) - Done

        #region Soul of Speed Boss (Snake Pit) - Done

        .Init("Soul of Speed Opener",
            new State(
                new State("Waiting",
                    new EntityNotExistsTransition("Stheno the Snake Queen", 50, "Open")
                    ),
                new State("Open",
                    new OpenGate("Brown Wall Candles Light", 2),
                    new Suicide()
                    )
                )
            )

        .Init("Soul of Speed Mob",
            new State(
                new TransformOnDeath("Soul Death", 1, 1, 1),
                new ScaleHP2(10),
                new Wander(0.4),
                new Follow(1.1, 15, 0.5),
                new State("No Rage",
                    new Shoot(20, 8, projectileIndex: 0, coolDown: 1000),
                    new HpLessTransition(0.5, "Rage")
                    ),
                new State("Rage",
                    new SetAltTexture(1, 1),
                    new Shoot(20, 16, projectileIndex: 1, coolDown: 1000)
                    )
                ),
            new Threshold(0.01,
                LootTemplates.DustLoot()
                ),
            new Threshold(0.05,
                new ItemLoot("Soul of Speed", 1)
                ),
            new Threshold(0.03,
                new ItemLoot("Queen's Prism", 0.0014),
                new ItemLoot("Soul of Speed", 0.1),
                new ItemLoot("Snake Venom Quiver", 0.0014)
                ),
            new Threshold(0.01,
                new ItemLoot("Stheno Queen Katana", 0.01),
                new ItemLoot("Stheno Queen Armor", 0.01),
                new ItemLoot("Stheno Queen Star", 0.01),
                new ItemLoot("Stheno Queen Ring", 0.01),
                new TierLoot(5, ItemType.Ability, 0.1),
                new TierLoot(11, ItemType.Weapon, 0.1),
                new TierLoot(10, ItemType.Weapon, 0.15),
                new TierLoot(11, ItemType.Armor, 0.1),
                new TierLoot(4, ItemType.Ring, 0.15)
                )
            )

        .Init("Undead Stheno",
            new State(
                new ScaleHP2(30),
                new TransformOnDeath("Soul of Speed Mob", 1, 1, 1),
                new State("Waiting",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                    new PlayerWithinTransition(10, "Start")
                    ),
                new State("Start",
                    new Taunt("Tssss... Do you want some poison?"),
                    new TimedTransition(2000, "Shoot")
                    ),
                new State("Shoot",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, false, 0),
                    new RingAttack(20, 6, 0, projectileIndex: 0, 0.2, 0, coolDown: 250, true),
                    new Grenade(3, 75, 15, null, coolDown: 750),
                    new HpLessTransition(0.75, "Phase 2")
                    ),
                new State("Phase 2",
                    new State("Prepare 1",
                        new Taunt("Tsss..."),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable, false, 2000),
                        new TimedTransition(2000, "Start 1")
                        ),
                    new State("Start 1",
                        new Shoot(20, 1, projectileIndex: 1, coolDown: 750),
                        new RingAttack(20, 6, 0, projectileIndex: 0, 0.2, 0, coolDown: 200, true),
                        new Grenade(3, 75, 15, null, coolDown: 700, effect: ConditionEffectIndex.Sick, effectDuration: 1500),
                        new Spawn("Stheno Pet", 3, 0.33, coolDown: 1000),
                        new HpLessTransition(0.5, "Phase 3")
                        )
                    ),
                new State("Phase 3",
                    new State("Shoot 1",
                        new Shoot(20, 1, projectileIndex: 1, coolDown: 750),
                        new RingAttack(20, 6, 0, projectileIndex: 0, 0.2, 0, coolDown: 200, true),
                        new Grenade(1.5, 100, 15, null, coolDown: 700, effect: ConditionEffectIndex.Sick, effectDuration: 1500),
                        new Shoot(20, 8, projectileIndex: 2, coolDown: 1000),
                        new HpLessTransition(0.20, "Rage")
                        )
                    ),
                new State("Rage",
                    new State("Prepare 2",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable, false, 2000),
                        new Flash(0xFF0000, 0.5, 4),
                        new TimedTransition(2000, "Shooting 1")
                        ),
                    new State("Shooting 1",
                        new RingAttack(20, 6, 0, projectileIndex: 0, 0.2, 0, coolDown: 200, true),
                        new Grenade(1, 125, 15, null, coolDown: 700, effect: ConditionEffectIndex.Sick, effectDuration: 1500),
                        new Shoot(20, 16, projectileIndex: 2, coolDown: 1000),
                        new Shoot(20, 3, shootAngle: 15, projectileIndex: 1, coolDown: 750)
                        )
                    )
                )
            )

        #endregion Soul of Speed Boss (Snake Pit) - Done

        #region Soul of Dexterity Boss (Sprite World) - Done

        .Init("Soul of Dexterity Opener",
            new State(
                new State("Waiting",
                    new EntityNotExistsTransition("Limon the Sprite God", 100, "Open")
                    ),
                new State("Open",
                    new OpenGate("All Black Wall", 2),
                    new Suicide()
                    )
                )
            )

        .Init("Soul of Dexterity Mob",
            new State(
                new ScaleHP2(10),
                new TransformOnDeath("Soul Death", 1, 1, 1),
                new Wander(0.4),
                new Charge(4, 10, 1000),
                new State("First",
                    new State("Shooting 1",
                        new Shoot(20, 3, shootAngle: 15, projectileIndex: 0, coolDown: 1000),
                        new PlayerWithinTransition(2, "Player 1")
                        ),
                    new State("Player 1",
                        new Shoot(20, 8, projectileIndex: 1, coolDown: 1000),
                        new NoPlayerWithinTransition(2, "Shooting 1")
                        ),
                    new HpLessTransition(0.5, "Rage")
                    ),
                new State("Rage",
                    new SetAltTexture(1, 1),
                    new State("Shooting 2",
                        new Shoot(20, 3, shootAngle: 15, projectileIndex: 0, coolDown: 750),
                        new PlayerWithinTransition(2, "Player 2")
                        ),
                    new State("Player 2",
                        new Shoot(20, 8, projectileIndex: 1, coolDown: 750),
                        new NoPlayerWithinTransition(2, "Shooting 2")
                        )
                    )
                ),
            new Threshold(0.005,
                new ItemLoot("Soul of Dexterity", 1)
                ),
            new Threshold(0.01,
                LootTemplates.DustLoot()
                ),
            new Threshold(0.03,
                new ItemLoot("Soul of Dexterity", 0.1),
                new ItemLoot("Chroma Spell", 0.0014)
                ),
            new Threshold(0.01,
                new TierLoot(4, ItemType.Ring, 0.15),
                new TierLoot(5, ItemType.Ring, 0.1),
                new TierLoot(11, ItemType.Armor, 0.1),
                new TierLoot(11, ItemType.Weapon, 0.1),
                new TierLoot(5, ItemType.Armor, 0.1)
                )
            )

        .Init("Undead Limon",
            new State(
                new ScaleHP2(30),
                new TransformOnDeath("Soul of Dexterity Mob", 1, 1, 1),
                new State("Start",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                    new PlayerWithinTransition(10, "Taunt")
                    ),
                new State("Taunt",
                    new Taunt("I love stars, do you want to see them?"),
                    new TimedTransition(2000, "Prepare")
                    ),
                new State("Prepare",
                    new TossObject(child: "Limon Element 1 v2", range: 9.5, angle: 315, coolDown: 1000000),
                    new TossObject(child: "Limon Element 2 v2", range: 9.5, angle: 225, coolDown: 1000000),
                    new TossObject(child: "Limon Element 3 v2", range: 9.5, angle: 135, coolDown: 1000000),
                    new TossObject(child: "Limon Element 4 v2", range: 9.5, angle: 45, coolDown: 1000000),
                    new TossObject(child: "Limon Element 1 v2", range: 14, angle: 315, coolDown: 1000000),
                    new TossObject(child: "Limon Element 2 v2", range: 14, angle: 225, coolDown: 1000000),
                    new TossObject(child: "Limon Element 3 v2", range: 14, angle: 135, coolDown: 1000000),
                    new TossObject(child: "Limon Element 4 v2", range: 14, angle: 45, coolDown: 1000000),
                    new TimedTransition(3000, "Start")
                    ),
                new State("Start",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, false, 0),
                    new Shoot(20, 3, shootAngle: 10, projectileIndex: 0, coolDown: 2000),
                    new Shoot(20, 16, projectileIndex: 1, coolDown: 250),
                    new HpLessTransition(0.75, "Spawn"),
                    new HpLessTransition(0.5, "Spawn"),
                    new State("Player",
                        new PlayerWithinTransition(3, "Found Player")
                        ),
                    new State("Found Player",
                        new Shoot(20, 12, projectileIndex: 2, coolDown: 250),
                        new NoPlayerWithinTransition(3, "Player")
                        ),
                    new State("Spawn",
                        new Spawn(children: "Magic Sprite", maxChildren: 10, initialSpawn: 0, coolDown: 500),
                        new Spawn(children: "Ice Sprite", maxChildren: 10, initialSpawn: 0, coolDown: 500)
                        )
                    )
                )
            )

        .Init("Limon Element 1 v2",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                new EntityNotExistsTransition(target: "Undead Limon", dist: 999, targetState: "Suicide"),
                new State("Setup",
                    new TimedTransition(time: 2000, targetState: "Attacking1")
                    ),
                new State("Attacking1",
                    new Shoot(radius: 999, fixedAngle: 180, defaultAngle: 180, coolDown: 100),
                    new Shoot(radius: 999, fixedAngle: 90, defaultAngle: 90, coolDown: 100),
                    new TimedTransition(time: 6000, targetState: "Attacking2")
                    ),
                new State("Attacking2",
                    new Shoot(radius: 999, fixedAngle: 180, defaultAngle: 180, coolDown: 100),
                    new Shoot(radius: 999, fixedAngle: 90, defaultAngle: 90, coolDown: 100),
                    new Shoot(radius: 999, fixedAngle: 135, defaultAngle: 135, coolDown: 100),
                    new TimedTransition(time: 6000, targetState: "Attacking3")
                    ),
                new State("Attacking3",
                    new Shoot(radius: 999, fixedAngle: 180, defaultAngle: 180, coolDown: 100),
                    new Shoot(radius: 999, fixedAngle: 90, defaultAngle: 90, coolDown: 100),
                    new TimedTransition(time: 6000, targetState: "Setup")
                    ),
                new State("Suicide",
                    new Suicide()
                    )
                )
            )
        .Init("Limon Element 2 v2",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                new EntityNotExistsTransition(target: "Undead Limon", dist: 999, targetState: "Suicide"),
                new State("Setup",
                    new TimedTransition(time: 2000, targetState: "Attacking1")
                    ),
                new State("Attacking1",
                    new Shoot(radius: 999, fixedAngle: 90, defaultAngle: 90, coolDown: 100),
                    new Shoot(radius: 999, fixedAngle: 0, defaultAngle: 0, coolDown: 100),
                    new TimedTransition(time: 6000, targetState: "Attacking2")
                    ),
                new State("Attacking2",
                    new Shoot(radius: 999, fixedAngle: 90, defaultAngle: 90, coolDown: 100),
                    new Shoot(radius: 999, fixedAngle: 0, defaultAngle: 0, coolDown: 100),
                    new Shoot(radius: 999, fixedAngle: 45, defaultAngle: 45, coolDown: 100),
                    new TimedTransition(time: 6000, targetState: "Attacking3")
                    ),
                new State("Attacking3",
                    new Shoot(radius: 999, fixedAngle: 90, defaultAngle: 90, coolDown: 100),
                    new Shoot(radius: 999, fixedAngle: 0, defaultAngle: 0, coolDown: 100),
                    new TimedTransition(time: 6000, targetState: "Setup")
                    ),
                new State("Suicide",
                    new Suicide()
                    )
                )
            )
        .Init("Limon Element 3 v2",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                new EntityNotExistsTransition(target: "Undead Limon", dist: 999, targetState: "Suicide"),
                new State("Setup",
                    new TimedTransition(time: 2000, targetState: "Attacking1")
                    ),
                new State("Attacking1",
                    new Shoot(radius: 999, fixedAngle: 0, defaultAngle: 0, coolDown: 100),
                    new Shoot(radius: 999, fixedAngle: 270, defaultAngle: 270, coolDown: 100),
                    new TimedTransition(time: 6000, targetState: "Attacking2")
                    ),
                new State("Attacking2",
                    new Shoot(radius: 999, fixedAngle: 0, defaultAngle: 0, coolDown: 100),
                    new Shoot(radius: 999, fixedAngle: 270, defaultAngle: 270, coolDown: 100),
                    new Shoot(radius: 999, fixedAngle: 315, defaultAngle: 315, coolDown: 100),
                    new TimedTransition(time: 6000, targetState: "Attacking3")
                    ),
                new State("Attacking3",
                    new Shoot(radius: 999, fixedAngle: 0, defaultAngle: 0, coolDown: 100),
                    new Shoot(radius: 999, fixedAngle: 270, defaultAngle: 270, coolDown: 100),
                    new TimedTransition(time: 6000, targetState: "Setup")
                    ),
                new State("Suicide",
                    new Suicide()
                    )
                )
            )
        .Init("Limon Element 4 v2",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                new EntityNotExistsTransition(target: "Undead Limon", dist: 999, targetState: "Suicide"),
                new State("Setup",
                    new TimedTransition(time: 2000, targetState: "Attacking1")
                    ),
                new State("Attacking1",
                    new Shoot(radius: 999, fixedAngle: 270, defaultAngle: 270, coolDown: 100),
                    new Shoot(radius: 999, fixedAngle: 180, defaultAngle: 180, coolDown: 100),
                    new TimedTransition(time: 6000, targetState: "Attacking2")
                    ),
                new State("Attacking2",
                    new Shoot(radius: 999, fixedAngle: 270, defaultAngle: 270, coolDown: 100),
                    new Shoot(radius: 999, fixedAngle: 180, defaultAngle: 180, coolDown: 100),
                    new Shoot(radius: 999, fixedAngle: 225, defaultAngle: 225, coolDown: 100),
                    new TimedTransition(time: 6000, targetState: "Attacking3")
                    ),
                new State("Attacking3",
                    new Shoot(radius: 999, fixedAngle: 270, defaultAngle: 270, coolDown: 100),
                    new Shoot(radius: 999, fixedAngle: 180, defaultAngle: 180, coolDown: 100),
                    new TimedTransition(time: 6000, targetState: "Setup")
                    ),
                new State("Suicide",
                    new Suicide()
                    )
                )
            )

        #endregion Soul of Dexterity Boss (Sprite World) - Done

        #region Soul of Vitality Boss (Abyss) - Done

        .Init("Soul of Vitality Opener",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                new State("Waiting",
                    new EntityNotExistsTransition("Archdemon Malphas", 40, "Open")
                    ),
                new State("Open",
                    new OpenGate("Red Torch Wall", 20),
                    new Suicide()
                    )
                )
            )

        .Init("Soul of Vitality Mob",
            new State(
                new Wander(0.4),
                new StayBack(1.1, 6),
                new State("Start",
                    new RingAttack(20, 3, 0, projectileIndex: 0, 0.03, 0, coolDown: 100),
                    new Shoot(20, 3, shootAngle: 15, projectileIndex: 1, coolDown: 1500),
                    new HpLessTransition(0.5, "Rage")
                    ),
                new State("Rage",
                    new SetAltTexture(1, 1),
                    new RingAttack(20, 3, 0, projectileIndex: 0, 0.03, 0, coolDown: -1),
                    new Shoot(20, 3, shootAngle: 15, projectileIndex: 1, coolDown: 1000)
                    )
                ),
            new Threshold(0.005,
                new ItemLoot("Soul of Vitality", 1)
                ),
            new Threshold(0.01,
                LootTemplates.DustLoot()
                ),
            new Threshold(0.03,
                new ItemLoot("Soul of Vitality", 0.1),
                new ItemLoot("Malphas's Bone", 0.0014)
                ),
            new Threshold(0.01,
                new ItemLoot("Malphas Sword", 0.02),
                new ItemLoot("Malphas Seal", 0.02),
                new ItemLoot("Malphas Helm", 0.02),
                new ItemLoot("Malphas Armor", 0.02),
                new ItemLoot("Malphas Juice", 0.02),
                new TierLoot(10, ItemType.Armor, 0.15),
                new TierLoot(11, ItemType.Armor, 0.1),
                new TierLoot(4, ItemType.Ring, 0.15),
                new TierLoot(4, ItemType.Ability, 0.15),
                new TierLoot(11, ItemType.Weapon, 0.1)
                )
            )

        .Init("Undead Malphas",
            new State(
                new MoveTo2(0.5f, 0.5f, 1, true, false, true),
                new TransformOnDeath("Soul of Vitality Mob", 1, 1, 1),
                new ScaleHP2(30),
                new State("Waiting",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                    new PlayerWithinTransition(10, "Start")
                    ),
                new State("Start",
                    new Taunt("Hell, a good place to rest ..."),
                    new TimedTransition(2000, "Wave 1")
                    ),
                new State("Wave 1",
                    new State("Done 0",
                        new Shoot(20, 3, shootAngle: 25, projectileIndex: 2, coolDown: 750),
                        new Spawn("Imp of the Abyss", 5, 0.1, coolDown: 100),
                        new TimedTransition(5000, "Check 0")
                        ),
                    new State("Check 0",
                        new Shoot(20, 3, shootAngle: 25, projectileIndex: 2, coolDown: 750),
                        new EntityNotExistsTransition("Imp of the Abyss", 20, "Wave 2")
                        )
                    ),
                new State("Wave 2",
                    new State("Prepare 1",
                        new Flash(0xFF0000, 0.5, 10),
                        new TimedTransition(5000, "Done 1")
                        ),
                    new State("Done 1",
                        new Shoot(20, 3, shootAngle: 25, projectileIndex: 2, coolDown: 750),
                        new Spawn("Demon Mage of the Abyss", 10, 0.1, coolDown: 100),
                        new TimedTransition(5000, "Check 1")
                        ),
                    new State("Check 1",
                        new Shoot(20, 3, shootAngle: 25, projectileIndex: 2, coolDown: 750),
                        new EntityNotExistsTransition("Demon Mage of the Abyss", 20, "Wave 3")
                        )
                    ),
                new State("Wave 3",
                    new State("Prepare 2",
                        new Flash(0xFF0000, 0.4, 10),
                        new TimedTransition(4000, "Done 2")
                        ),
                    new State("Done 2",
                        new Shoot(20, 3, shootAngle: 25, projectileIndex: 2, coolDown: 750),
                        new Spawn("Demon Warrior of the Abyss", 15, 0.15, coolDown: 100),
                        new TimedTransition(5000, "Check 2")
                        ),
                    new State("Check 2",
                        new Shoot(20, 3, shootAngle: 25, projectileIndex: 2, coolDown: 750),
                        new EntityNotExistsTransition("Demon Warrior of the Abyss", 20, "Wave 4")
                        )
                    ),
                new State("Wave 4",
                    new State("Prepare 3",
                        new Flash(0xFF0000, 0.3, 10),
                        new TimedTransition(3000, "Done 3")
                        ),
                    new State("Done 3",
                        new Shoot(20, 3, shootAngle: 25, projectileIndex: 2, coolDown: 750),
                        new Spawn("Brute of the Abyss", 15, 0.15, coolDown: 100),
                        new TimedTransition(5000, "Check 3")
                        ),
                    new State("Check 3",
                        new Shoot(20, 3, shootAngle: 25, projectileIndex: 2, coolDown: 750),
                        new EntityNotExistsTransition("Brute of the Abyss", 20, "Finish")
                        )
                    ),
                new State("Finish",
                    new State("Rage Prepare",
                        new Taunt("AAARRGGGGG!"),
                        new TimedTransition(1000, "Start 1")
                        ),
                    new State("Start 1",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable, false, 0),
                        new RingAttack(20, 6, 0, projectileIndex: 1, 0.03, 0, coolDown: 50),
                        new Shoot(20, 3, shootAngle: 25, projectileIndex: 2, coolDown: 1000)
                        )
                    )
                )
            )

        #endregion Soul of Vitality Boss (Abyss) - Done

        #region Soul of Wisdom Boss (UDL) - Done

        .Init("Soul of Wisdom Opener",
            new State(
                new State("Waiting",
                    new EntityNotExistsTransition("Septavius the Ghost God", 40, "Open")
                    ),
                new State("Open",
                    new OpenGate("Grey Wall Shelf Lf", 5),
                    new OpenGate("Grey Wall Shelf RT", 5),
                    new OpenGate("Grey Wall Shelf M", 5),
                    new Suicide()
                    )
                )
            )

        .Init("Soul of Wisdom Mob",
            new State(
                new Wander(0.4),
                new StayBack(1.1, 6),
                new ScaleHP2(10),
                new TransformOnDeath("Soul Death", 1, 1, 1),
                new State("First",
                    new HpLessTransition(0.5, "Rage"),
                    new Shoot(20, 8, shootAngle: 15, projectileIndex: 0, coolDown: 1500)
                    ),
                new State("Rage",
                    new SetAltTexture(1, 1),
                    new Shoot(20, 8, shootAngle: 15, projectileIndex: 0, coolDown: 1500),
                    new Grenade(3, 50, 10, coolDown: 1500, effect: ConditionEffectIndex.ArmorBroken, effectDuration: 1000)
                    )
                ),
            new Threshold(0.03,
                new ItemLoot("Cape of Septavius", 0.0014),
                new ItemLoot("Soul of Wisdom", 0.1),
                new ItemLoot("Ghost Trapper", 0.0014)
                ),
            new Threshold(0.005,
                new ItemLoot("Soul of Wisdom", 1)
                ),
            new Threshold(0.01,
                LootTemplates.DustLoot()
                ),
            new Threshold(0.01,
                new ItemLoot("Doom Bow", 0.013),
                new ItemLoot("Edictum Praetoris", 0.02),
                new ItemLoot("Memento Mori", 0.02),
                new ItemLoot("Toga Picta", 0.02),
                new ItemLoot("Interregnum", 0.02),
                new ItemLoot("Deathhail Wand", 0.015),
                new ItemLoot("Undead Lair Key", 0.015, 0, 0.03),
                new TierLoot(4, ItemType.Ability, 0.01),
                new TierLoot(5, ItemType.Ability, 0.005),
                new TierLoot(9, ItemType.Armor, 0.01),
                new TierLoot(9, ItemType.Weapon, 0.01)
                )
            )

        .Init("Undead Septavius Anchor",
            new State(
                new State("Waiting"),
                new State("Select Anchors",
                    new State("Selecting",
                        new TimedRandomTransition(150, false, "One", "Two", "Three", "Four")
                        ),
                    new State("One",
                        new Order(20, "Undead Septavius", "Check Anchor 1"),
                        new Order(20, "Undead Septavius Fake 1", "Check Anchor 2"),
                        new Order(20, "Undead Septavius Fake 2", "Check Anchor 3"),
                        new Order(20, "Undead Septavius Fake 3", "NotMove"),
                        new TimedTransition(2000, "Order Everyone")
                        ),
                    new State("Two",
                        new Order(20, "Undead Septavius", "Check Anchor 2"),
                        new Order(20, "Undead Septavius Fake 1", "Check Anchor 3"),
                        new Order(20, "Undead Septavius Fake 2", "NotMove"),
                        new Order(20, "Undead Septavius Fake 3", "Check Anchor 1"),
                        new TimedTransition(2000, "Order Everyone")
                        ),
                    new State("Three",
                        new Order(20, "Undead Septavius", "Check Anchor 3"),
                        new Order(20, "Undead Septavius Fake 1", "NotMove"),
                        new Order(20, "Undead Septavius Fake 2", "Check Anchor 1"),
                        new Order(20, "Undead Septavius Fake 3", "Check Anchor 2"),
                        new TimedTransition(2000, "Order Everyone")
                        ),
                    new State("Four",
                        new Order(20, "Undead Septavius", "NotMove"),
                        new Order(20, "Undead Septavius Fake 1", "Check Anchor 1"),
                        new Order(20, "Undead Septavius Fake 2", "Check Anchor 2"),
                        new Order(20, "Undead Septavius Fake 3", "Check Anchor 3"),
                        new TimedTransition(2000, "Order Everyone")
                        )
                    ),
                new State("Order Everyone",
                    new Order(5, "Undead Septavius", "Shoot 1"),
                    new Order(5, "Undead Septavius Fake 1", "Shoot 1"),
                    new Order(5, "Undead Septavius Fake 2", "Shoot 1"),
                    new Order(5, "Undead Septavius Fake 3", "Shoot 1"),
                    new Suicide()
                    )
                )
            )

        .Init("Undead Septavius",
            new State(
                new ScaleHP2(30),
                new TransformOnDeath("Soul of Wisdom Mob", 1, 1, 1),
                new State("Waiting",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                    new PlayerWithinTransition(10, "Starting")
                    ),
                new State("Starting",
                    new Protect(1, "Undead Septavius Anchor", 10, 2, 2),
                    new TimedTransition(2000, "Taunt_Two")
                    ),
                new State("Taunt_Two",
                    new Taunt("Hellooo... Here's a trick, Who i am?"),
                    new Spawn("Undead Septavius Fake 1", 1, 1, 99999),
                    new Spawn("Undead Septavius Fake 2", 1, 1, 99999),
                    new Spawn("Undead Septavius Fake 3", 1, 1, 99999),
                    new Order(20, "Undead Septavius Anchor", "Select Anchors")
                    ),

                new State("Check Anchor",
                    new State("Check Anchor 1",
                        new Protect(1, "Undead Septavius Anchor 1", 10, 0, 0)
                        ),
                    new State("Check Anchor 2",
                        new Protect(1, "Undead Septavius Anchor 2", 10, 0, 0)
                        ),
                    new State("Check Anchor 3",
                        new Protect(1, "Undead Septavius Anchor 3", 10, 0, 0)
                        ),
                    new State("NotMove")
                    ),

                //SHOOTS BEHAVIORS
                new State("Shoot 1",
                    new State("Prepare 1",
                        new Flash(0x00ff00, 0.5, 5),
                        new TimedTransition(2500, "Start 1")
                        ),
                    new State("Start 1",
                        new HpLessTransition(0.5, "Rage"),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable, false, 0),
                        new Follow(0.6),
                        new Wander(0.6),
                        new Shoot(20, 8, projectileIndex: 1, coolDown: 2000),
                        new Shoot(20, 1, projectileIndex: 0, coolDown: 1500)
                        )
                    ),
                new State("Rage",
                    new State("Prepare 2",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                        new Flash(0xFF0000, 0.5, 5),
                        new TimedTransition(2500, "Start 2")
                        ),
                    new State("Start 2",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable, false, 0),
                        new Follow(0.6),
                        new Wander(0.6),
                        new Shoot(20, 8, projectileIndex: 1, coolDown: 1500),
                        new Shoot(20, 1, projectileIndex: 2, coolDown: 2000),
                        new Shoot(20, 2, shootAngle: 25, projectileIndex: 0, coolDown: 1000)
                        )
                    )
                )
            )

        .Init("Undead Septavius Fake 1",
            new State(
                new ScaleHP2(30),
                new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                new EntityNotExistsTransition("Undead Septavius", 100, "Real Death"),
                new State("Taunt",
                    new Taunt("Hellooo... Here's a trick, Who i am?")
                    ),
                new State("Check Anchor",
                    new State("Check Anchor 1",
                        new Protect(1, "Undead Septavius Anchor 1", 10, 0, 0)
                        ),
                    new State("Check Anchor 2",
                        new Protect(1, "Undead Septavius Anchor 2", 10, 0, 0)
                        ),
                    new State("Check Anchor 3",
                        new Protect(1, "Undead Septavius Anchor 3", 10, 0, 0)
                        ),
                    new State("NotMove")
                    ),
                new State("Shoot 1",
                    new State("Prepare 1",
                        new Flash(0x00ff00, 0.5, 5),
                        new TimedTransition(2500, "Start 1")
                        ),
                    new State("Start 1",
                        new HpLessTransition(0.5, "Rage"),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable, false, 0),
                        new Follow(0.4),
                        new Wander(0.4),
                        new Shoot(20, 8, projectileIndex: 1, coolDown: 2500),
                        new Shoot(20, 1, projectileIndex: 0, coolDown: 2000)
                        )
                    ),
                new State("Rage",
                    new State("Prepare 2",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                        new Flash(0xFF0000, 0.5, 5),
                        new TimedTransition(2500, "Start 2")
                        ),
                    new State("Start 2",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable, false, 0),
                        new Follow(0.4),
                        new Wander(0.4),
                        new Shoot(20, 8, projectileIndex: 1, coolDown: 2000),
                        new Shoot(20, 1, projectileIndex: 2, coolDown: 1500),
                        new Shoot(20, 2, shootAngle: 25, projectileIndex: 0, coolDown: 1500)
                        )
                    ),
                new State("Real Death",
                    new State("Preparing to Die",
                        new Taunt("NOOOOOOOOOOOO!!"),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                        new TimedTransition(2000, "Death")
                        ),
                    new State("Death",
                        new SwirlingMistDeathParticles(),
                        new Suicide()
                        )
                    )
                )
            )

        .Init("Undead Septavius Fake 2",
            new State(
                new ScaleHP2(30),
                new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                new EntityNotExistsTransition("Undead Septavius", 100, "Real Death"),
                new State("Taunt",
                    new Taunt("Hellooo... Here's a trick, Who i am?")
                    ),
                new State("Check Anchor",
                    new State("Check Anchor 1",
                        new Protect(1, "Undead Septavius Anchor 1", 10, 0, 0)
                        ),
                    new State("Check Anchor 2",
                        new Protect(1, "Undead Septavius Anchor 2", 10, 0, 0)
                        ),
                    new State("Check Anchor 3",
                        new Protect(1, "Undead Septavius Anchor 3", 10, 0, 0)
                        ),
                    new State("NotMove")
                    ),
                new State("Shoot 1",
                    new State("Prepare 1",
                        new Flash(0x00ff00, 0.5, 5),
                        new TimedTransition(2500, "Start 1")
                        ),
                    new State("Start 1",
                        new HpLessTransition(0.5, "Rage"),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable, false, 0),
                        new Follow(0.4),
                        new Wander(0.4),
                        new Shoot(20, 8, projectileIndex: 1, coolDown: 2500),
                        new Shoot(20, 1, projectileIndex: 0, coolDown: 2000)
                        )
                    ),
                new State("Rage",
                    new State("Prepare 2",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                        new Flash(0xFF0000, 0.5, 5),
                        new TimedTransition(2500, "Start 2")
                        ),
                    new State("Start 2",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable, false, 0),
                        new Follow(0.4),
                        new Wander(0.4),
                        new Shoot(20, 8, projectileIndex: 1, coolDown: 2000),
                        new Shoot(20, 1, projectileIndex: 2, coolDown: 1500),
                        new Shoot(20, 2, shootAngle: 25, projectileIndex: 0, coolDown: 1500)
                        )
                    ),
                new State("Real Death",
                    new State("Preparing to Die",
                        new Taunt("NOOOOOOOOOOOO!!"),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                        new TimedTransition(2000, "Death")
                        ),
                    new State("Death",
                        new SwirlingMistDeathParticles(),
                        new Suicide()
                        )
                    )
                )
            )

        .Init("Undead Septavius Fake 3",
            new State(
                new ScaleHP2(30),
                new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                new EntityNotExistsTransition("Undead Septavius", 100, "Real Death"),
                new State("Taunt",
                    new Taunt("Hellooo... Here's a trick, Who i am?")
                    ),
                new State("Check Anchor",
                    new State("Check Anchor 1",
                        new Protect(1, "Undead Septavius Anchor 1", 10, 0, 0)
                        ),
                    new State("Check Anchor 2",
                        new Protect(1, "Undead Septavius Anchor 2", 10, 0, 0)
                        ),
                    new State("Check Anchor 3",
                        new Protect(1, "Undead Septavius Anchor 3", 10, 0, 0)
                        ),
                    new State("NotMove")
                    ),

                new State("Shoot 1",
                    new State("Prepare 1",
                        new Flash(0x00ff00, 0.5, 5),
                        new TimedTransition(2500, "Start 1")
                        ),
                    new State("Start 1",
                        new HpLessTransition(0.5, "Rage"),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable, false, 0),
                        new Follow(0.4),
                        new Wander(0.4),
                        new Shoot(20, 8, projectileIndex: 1, coolDown: 2500),
                        new Shoot(20, 1, projectileIndex: 0, coolDown: 2000)
                        )
                    ),
                new State("Rage",
                    new State("Prepare 2",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                        new Flash(0xFF0000, 0.5, 5),
                        new TimedTransition(2500, "Start 2")
                        ),
                    new State("Start 2",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable, false, 0),
                        new Follow(0.4),
                        new Wander(0.4),
                        new Shoot(20, 8, projectileIndex: 1, coolDown: 2000),
                        new Shoot(20, 1, projectileIndex: 2, coolDown: 1500),
                        new Shoot(20, 2, shootAngle: 25, projectileIndex: 0, coolDown: 1500)
                        )
                    ),
                new State("Real Death",
                    new State("Preparing to Die",
                        new Taunt("NOOOOOOOOOOOO!!"),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                        new TimedTransition(2000, "Death")
                        ),
                    new State("Death",
                        new SwirlingMistDeathParticles(),
                        new Suicide()
                        )
                    )
                )
            )

        #endregion Soul of Wisdom Boss (UDL) - Done

        ;
    }
}
