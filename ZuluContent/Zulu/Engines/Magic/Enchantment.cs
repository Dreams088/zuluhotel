using System;
using MessagePack;
using Server;
using Server.Engines.Magic;
using Server.Items;
using Server.Spells;
using Server.Network;
using ZuluContent.Zulu.Engines.Magic.Enchantments;
using ZuluContent.Zulu.Engines.Magic.Hooks;

namespace ZuluContent.Zulu.Engines.Magic
{
    public abstract class Enchantment<TEnchantmentInfo> : IEnchantmentValue
        where TEnchantmentInfo : EnchantmentInfo, new()
    {
        public static readonly TEnchantmentInfo EnchantmentInfo = new TEnchantmentInfo();
        [IgnoreMember] public virtual EnchantmentInfo Info => EnchantmentInfo;

        [IgnoreMember] public abstract string AffixName { get; }
        [Key(0)] public bool Cursed { get; set; }

        public virtual bool GetShouldDye() => Info.Hue != 0;

        protected Enchantment(bool cursed = false)
        {
            Cursed = cursed;
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

        public virtual void OnSpellDamage(Mobile attacker, Mobile defender, SpellCircle circle, ElementalType damageType, ref int damage)
        {
        }

        public virtual void OnGetCastDelay(Mobile mobile, Spell spell, ref double delay)
        {
        }

        public virtual void OnParalysis(Mobile mobile, ref bool paralyze)
        {
        }

        public virtual void OnPoison(Mobile attacker, Mobile defender, Poison poison, ref bool immune)
        {
        }

        public virtual void OnHeal(Mobile healer, Mobile patient, ref double healAmount)
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

        public virtual void OnShieldHit(Mobile attacker, Mobile defender, BaseWeapon weapon, BaseShield shield, ref int damage)
        {
        }

        public virtual void OnArmorHit(Mobile attacker, Mobile defender, BaseWeapon weapon, BaseArmor armor, ref int damage)
        {
        }
    }
}