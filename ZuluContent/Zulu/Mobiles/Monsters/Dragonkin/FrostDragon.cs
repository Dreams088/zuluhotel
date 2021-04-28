using System;
using System.Collections.Generic;
using Scripts.Zulu.Spells.Earth;
using Server;
using Server.Misc;
using Server.Items;
using static Server.Mobiles.CreatureProp;
using Server.Engines.Magic;
using Server.Engines.Harvest;
using Server.Engines.Magic.HitScripts;

namespace Server.Mobiles
{
    public class FrostDragon : BaseCreature
    {
        static FrostDragon()
        {
            CreatureProperties.Register<FrostDragon>(new CreatureProperties
            {
                // cast_pct = 40,
                // DataElementId = frostdragon,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = frostdragon,
                // food = meat,
                // Graphic = 0x0ec4 /* Weapon */,
                // Hitscript = :combat:spellstrikescript /* Weapon */,
                // HitSound = 0x16D /* Weapon */,
                // hostile = 1,
                LootTable = "37",
                LootItemChance = 75,
                LootItemLevel = 5,
                // MissSound = 0x239 /* Weapon */,
                // num_casts = 8,
                // script = killpcs,
                // Speed = 65 /* Weapon */,
                // spell = fireball,
                // spell_0 = flamestrike,
                // spell_1 = ebolt,
                // spell_2 = lightning,
                // spell_3 = harm,
                // spell_4 = mindblast,
                // spell_5 = magicarrow,
                // spell_6 = chainlightning,
                // spell_7 = weaken,
                // spell_8 = masscurse,
                // TrueColor = 1176,
                // virtue = 8,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                BaseSoundID = 362,
                Body = 0xc,
                CanFly = true,
                CorpseNameOverride = "corpse of a Frost Dragon",
                CreatureType = CreatureType.Dragonkin,
                DamageMax = 75,
                DamageMin = 25,
                Dex = 60,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                Hides = 5,
                HideType = HideType.IceCrystal,
                HitsMax = 800,
                Hue = 1176,
                Int = 400,
                ManaMaxSeed = 200,
                MinTameSkill = 140,
                Name = "a Frost Dragon",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.Third.FireballSpell),
                    typeof(Spells.Sixth.EnergyBoltSpell),
                    typeof(Spells.Fourth.LightningSpell),
                    typeof(Spells.Second.HarmSpell),
                    typeof(Spells.Fifth.MindBlastSpell),
                    typeof(Spells.First.MagicArrowSpell),
                    typeof(Spells.First.WeakenSpell),
                    typeof(Spells.Sixth.MassCurseSpell)
                },
                ProvokeSkillOverride = 140,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Water, 100},
                    {ElementalType.MagicImmunity, 4}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 80},
                    {SkillName.MagicResist, 75},
                    {SkillName.Tactics, 120},
                    {SkillName.Macing, 130},
                    {SkillName.DetectHidden, 130}
                },
                StamMaxSeed = 140,
                Str = 800,
                Tamable = true,
                VirtualArmor = 40,
                WeaponAbility = new SpellStrike(typeof(IceStrikeSpell)),
                WeaponAbilityChance = 0.8
            });
        }


        [Constructible]
        public FrostDragon() : base(CreatureProperties.Get<FrostDragon>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Frost Dragon Weapon",
                Speed = 65,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x16D,
                MissSound = 0x239
            });
        }

        [Constructible]
        public FrostDragon(Serial serial) : base(serial)
        {
        }


        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int) 0);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);
            var version = reader.ReadInt();
        }
    }
}