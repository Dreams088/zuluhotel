using Server.Targeting;

namespace Server.Spells.Third
{
    public class BlessSpell : MagerySpell
    {
        public BlessSpell(Mobile caster, Item spellItem) : base(caster, spellItem)
        {
        }


        public override void OnCast()
        {
            Caster.Target = new InternalTarget(this);
        }

        public void Target(Mobile m)
        {
            if (!Caster.CanSee(m))
            {
                Caster.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (CheckBeneficialSequence(m))
            {
                SpellHelper.Turn(Caster, m);

                SpellHelper.AddStatBonus(Caster, m, StatType.Str);
                SpellHelper.DisableSkillCheck = true;
                SpellHelper.AddStatBonus(Caster, m, StatType.Dex);
                SpellHelper.AddStatBonus(Caster, m, StatType.Int);
                SpellHelper.DisableSkillCheck = false;

                m.FixedParticles(0x373A, 10, 15, 5018, EffectLayer.Waist);
                m.PlaySound(0x1EA);

                var percentage = (int) (SpellHelper.GetOffsetScalar(Caster, m, false) * 100);
                var length = SpellHelper.GetDuration(Caster, m);

                var args = $"{percentage}\t{percentage}\t{percentage}";
            }

            FinishSequence();
        }

        public class InternalTarget : Target
        {
            private readonly BlessSpell m_Owner;

            public InternalTarget(BlessSpell owner) : base(12, false, TargetFlags.Beneficial)
            {
                m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Mobile) m_Owner.Target((Mobile) o);
            }

            protected override void OnTargetFinish(Mobile from)
            {
                m_Owner.FinishSequence();
            }
        }
    }
}