using System;
using System.Collections.Generic;
using MessagePack;
using Server;
using Server.Engines.Craft;
using Server.Engines.Magic;
using Server.Items;
using Server.Spells;
using Server.Network;
using ZuluContent.Zulu.Engines.Magic.Enums;
using static ZuluContent.Zulu.Items.SingleClick.SingleClickHandler;
using Server.Mobiles;
using ZuluContent.Zulu.Items;

namespace ZuluContent.Zulu.Engines.Magic
{
    [MessagePackObject]
    public abstract class Enchantment<TEnchantmentInfo> : IEnchantmentValue
        where TEnchantmentInfo : EnchantmentInfo, new()
    {
        public static readonly TEnchantmentInfo EnchantmentInfo = new();

        [IgnoreMember] public virtual EnchantmentInfo Info => EnchantmentInfo;

        [IgnoreMember] public abstract string AffixName { get; }

        [Key(0)] public CurseType Cursed { get; set; }
        
        public virtual bool GetShouldDye() => Info.Hue != 0;
        
        protected Enchantment(bool cursed = false)
        {
            Cursed = cursed ? CurseType.Unrevealed : CurseType.None;
        }

        protected virtual void NotifyMobile(Mobile above, string text)
        {
            NotifyMobile(above, above, text);
        }

        protected virtual void NotifyMobile(Mobile above, Mobile who, string text)
        {
            above.PrivateOverheadMessage(
                MessageType.Regular,
                who.SpeechHue,
                true,
                text,
                who.NetState
            );
        }

        public virtual void OnIdentified(IEntity entity)
        {
        }

        public virtual void OnAdded(IEntity entity)
        {
        }

        public virtual void OnRemoved(IEntity entity, IEntity parent)
        {
        }

        public virtual void OnBeforeRemoved(IEntity entity, Mobile from, ref bool canRemove)
        {
        }

        public void OnSpellAreaCalculation(Mobile caster, Spell spell, ElementalType damageType, ref double area)
        {
        }

        public virtual void OnSpellDamage(Mobile attacker, Mobile defender, Spell spell, ElementalType damageType,
            ref int damage)
        {
        }

        public virtual void OnGetCastDelay(Mobile mobile, Spell spell, ref double delay)
        {
        }

        public virtual void OnParalysis(Mobile mobile, ref TimeSpan duration, ref bool paralyze)
        {
        }

        public virtual void OnPoison(Mobile attacker, Mobile defender, Poison poison, ref bool immune)
        {
        }

        public virtual void OnHeal(Mobile healer, Mobile patient, object source, ref double healAmount)
        {
        }

        public virtual void OnAnimalTaming(Mobile tamer, BaseCreature creature, ref int chance)
        {
        }

        public virtual void OnTracking(Mobile tracker, ref int range)
        {
        }

        public virtual void OnExceptionalChance(Mobile crafter, ref double exceptionalChance,
            ref int exceptionalDifficulty)
        {
        }

        public virtual void OnQualityBonus(Mobile crafter, ref int multiplier)
        {
        }

        public virtual void OnMeditation(Mobile mobile, ref int regen, ref double tickIntervalSeconds)
        {
        }

        public virtual void OnModifyWithMagicEfficiency(Mobile mobile, ref double value)
        {
        }

        public virtual void OnToolHarvestColoredQualityChance(Mobile mobile, ref int bonus, ref int toMod)
        {
        }

        public virtual void OnHarvestColoredQualityChance(Mobile mobile, ref int bonus, ref int toMod)
        {
        }

        public virtual void OnHarvestColoredChance(Mobile mobile, ref int chance)
        {
        }

        public virtual void OnHarvestAmount(Mobile harvester, ref int amount)
        {
        }

        public virtual void OnToolHarvestBonus(Mobile harvester, ref int amount)
        {
        }

        public virtual void OnHarvestBonus(Mobile harvester, ref int amount)
        {
        }

        public virtual void OnBeforeSwing(Mobile attacker, Mobile defender)
        {
        }

        public virtual void OnSwing(Mobile attacker, Mobile defender, ref double damageBonus, ref TimeSpan result)
        {
        }

        public virtual void OnGetSwingDelay(ref double delayInSeconds, Mobile m)
        {
        }

        public virtual void OnCheckHit(Mobile attacker, Mobile defender, ref bool result)
        {
        }

        public virtual void OnMeleeHit(Mobile attacker, Mobile defender, BaseWeapon weapon, ref int damage)
        {
        }

        public virtual void OnAbsorbMeleeDamage(Mobile attacker, Mobile defender, BaseWeapon weapon, ref int damage)
        {
        }

        public virtual void OnShieldHit(Mobile attacker, Mobile defender, BaseWeapon weapon, BaseShield shield,
            ref int damage)
        {
        }

        public virtual void OnArmorHit(Mobile attacker, Mobile defender, BaseWeapon weapon, BaseArmor armor,
            ref int damage)
        {
        }

        public virtual void OnCraftSkillRequiredForFame(Mobile from, ref int craftSkillRequiredForFame)
        {
        }


        public void OnCraftItemCreated(Mobile @from, CraftSystem craftSystem, CraftItem craftItem, BaseTool tool,
            Item item)
        {
        }

        public void OnCraftItemAddToBackpack(Mobile from, CraftSystem craftSystem, CraftItem craftItem, BaseTool tool,
            Item item)
        {
        }

        public void OnSummonFamiliar(Mobile caster, BaseCreature familiar)
        {
        }
    }
}