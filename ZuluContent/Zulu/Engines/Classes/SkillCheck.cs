using System;
using Server;
using Server.Mobiles;

namespace Scripts.Zulu.Engines.Classes
{
    public static class SkillCheck
    {
        public static bool ShilCheckSkill(this Mobile mobile, SkillName skill, int? difficulty = null,
            int? points = null)
        {
            return (mobile as IShilCheckSkill)?.CheckSkill(
                skill, 
                difficulty ?? -1,
                points ?? ZhConfig.Skills.Entries[skill].DefaultPoints
            ) == true;
        }

        public static int GetSkillCheckChance(Mobile from, SkillName skillName, int difficulty)
        {
            var skill = from.Skills[skillName];

            var skillValue = (int) skill.Value;

            if (skillValue == 0 && from is BaseCreature)
                return 0;

            var chance = skillValue - difficulty + 20;

            if (chance == 0)
                chance = 2;
            else
            {
                chance = (int) (chance * 2.5);
                if (chance > 98)
                    chance = 98;
            }

            return chance;
        }

        public static bool DifficultySkillCheck(Mobile from, SkillName skillName, int difficulty, int points)
        {
            // In case skill arrow is down
            if (from.Skills[skillName].Value - difficulty + 20 < 0)
            {
                AwardSkillPoints(from, skillName, 0);
                return false;
            }

            var chance = GetSkillCheckChance(from, skillName, difficulty);

            var check = Utility.Random(100);
            if (check >= chance)
            {
                if ((from as IZuluClassed)?.ZuluClass.IsSkillInClass(skillName) ?? false)
                {
                    check = Utility.Random(100);
                    // In case they have the skill arrow down
                    if (check >= chance)
                    {
                        AwardSkillPoints(from, skillName, 0);
                        return false;
                    }

                    points = 0;
                }
                else
                {
                    // In case they have the skill arrow down
                    AwardSkillPoints(from, skillName, 0);
                    return false;
                }
            }

            if (from is PlayerMobile)
            {
                points = (int) (points * (1.0 - ((chance) / 100.0)));
                AwardSkillPoints(from, skillName, points);
            }

            return true;
        }

        public static bool PercentSkillCheck(Mobile from, SkillName skillName, int points)
        {
            Skill skill = from.Skills[skillName];

            var chance = (int) skill.Value;

            if (chance == 0 && from is BaseCreature)
                return false;

            if (chance < 2)
                chance = 2;
            else if (chance > 98)
                chance = 98;

            var check = Utility.Random(100);
            if (check >= chance)
            {
                if ((from as IZuluClassed)?.ZuluClass.IsSkillInClass(skillName) ?? false)
                {
                    check = Utility.Random(100);
                    if (check >= chance)
                    {
                        if (chance < 10)
                        {
                            AwardSkillPoints(from, skillName, points / 2);
                        }

                        return false;
                    }

                    points = 0;
                }
                else
                {
                    if (chance < 10)
                    {
                        AwardSkillPoints(from, skillName, points / 2);
                    }

                    return false;
                }
            }

            if (from is PlayerMobile)
            {
                AwardSkillPoints(from, skillName, points);
            }

            return true;
        }

        public static void AwardSkillPoints(Mobile from, SkillName skillName, int points)
        {
            Skill skill = from.Skills[skillName];

            // TODO: Add cmd level check
            if (skill.Lock == SkillLock.Locked || from is BaseCreature)
                return;

            if (skill.Lock == SkillLock.Down)
            {
                var baseSkill = (int) (skill.Base);
                var amount = 1;
                if (amount > baseSkill)
                {
                    amount = baseSkill;
                    skill.SetLockNoRelay(SkillLock.Locked);
                    skill.Update();
                }

                skill.BaseFixedPoint -= amount * 10;
                return;
            }

            if (points == 0)
                return;

            points = (int) (points * GetSkillPointsMultiplier(from as PlayerMobile, skillName));

            // TODO: Should we implement global multipliers?

            AwardPoints(from as PlayerMobile, skillName, points);
        }

        public static double GetSkillPointsMultiplier(PlayerMobile from, SkillName skillName)
        {
            if (from.ZuluClass.IsSkillInClass(skillName))
                return from.ZuluClass.Bonus;
            return 1.0;
        }

        public static void AwardPoints(PlayerMobile from, SkillName skillName, int points)
        {
            var config = ZhConfig.Skills.Entries[skillName];

            Skill skill = from.Skills[skillName];

            AwardRawAttributePoints(from, skill, points);

            // Check the strength advancement
            if (config.StrAdvancement != null && Utility.Random(1000) < config.StrAdvancement.Chance * 10)
            {
                AwardRawAttributePoints(from, StatType.Str,
                    Utility.Dice(config.StrAdvancement.PointsAmount, config.StrAdvancement.PointsSides,
                        config.StrAdvancement.PointsBonus));
            }

            // Check the dexterity advancement
            if (config.DexAdvancement != null && Utility.Random(1000) < config.DexAdvancement.Chance * 10)
            {
                AwardRawAttributePoints(from, StatType.Dex,
                    Utility.Dice(config.DexAdvancement.PointsAmount, config.DexAdvancement.PointsSides,
                        config.DexAdvancement.PointsBonus));
            }

            // Check intelligence advancement
            if (config.IntAdvancement != null && Utility.Random(1000) < config.IntAdvancement.Chance * 10)
            {
                AwardRawAttributePoints(from, StatType.Int,
                    Utility.Dice(config.IntAdvancement.PointsAmount, config.IntAdvancement.PointsSides,
                        config.IntAdvancement.PointsBonus));
            }
        }

        public static void AwardRawAttributePoints(PlayerMobile from, Skill skill, double rawPoints)
        {
            if (rawPoints == 0.0)
                return;

            var initial = (int) (skill.Base * 10.0);
            var current = initial;

            var toIncrease = GetNeededRawPointsToIncrease(current);

            while (rawPoints >= toIncrease)
            {
                rawPoints -= toIncrease;
                ++current;
                toIncrease = GetNeededRawPointsToIncrease(current);
            }

            if (Utility.Random((int) toIncrease) < rawPoints)
                ++current;

            if (current != initial)
            {
                if (current > ZhConfig.Skills.MaxStatCap)
                    current = ZhConfig.Skills.MaxStatCap;

                // TODO: Add cmd level check
                if (current > ZhConfig.Skills.StatCap)
                    current = ZhConfig.Skills.StatCap;

                skill.BaseFixedPoint = current;
            }
        }

        public static void AwardRawAttributePoints(PlayerMobile from, StatType stat, double rawPoints)
        {
            if (rawPoints == 0.0)
                return;

            var initial = 0;
            if (stat == StatType.Str)
                initial = from.RawStr * 10;
            else if (stat == StatType.Dex)
                initial = from.RawDex * 10;
            else if (stat == StatType.Int)
                initial = from.RawInt * 10;
            var current = initial;

            var toIncrease = GetNeededRawPointsToIncrease(current);

            while (rawPoints >= toIncrease)
            {
                rawPoints -= toIncrease;
                current += 10;
                toIncrease = GetNeededRawPointsToIncrease(current);
            }

            if (Utility.Random((int) toIncrease) < rawPoints)
                current += 10;

            if (current != initial)
            {
                if (current > ZhConfig.Skills.MaxStatCap)
                    current = ZhConfig.Skills.MaxStatCap;

                // TODO: Add cmd level check
                if (current > ZhConfig.Skills.StatCap)
                    current = ZhConfig.Skills.StatCap;

                if (stat == StatType.Str)
                    from.RawStr = current / 10;
                else if (stat == StatType.Dex)
                    from.RawDex = current / 10;
                else if (stat == StatType.Int)
                    from.RawInt = current / 10;
            }
        }

        public static double GetNeededRawPointsToIncrease(double baseValue)
        {
            if (baseValue < 200.0)
                return 20.48;

            if (baseValue >= 2800)
                return 0x7fffffff;

            return Math.Pow(2.0, (int) baseValue / 100) * 10.24;
        }
    }
}