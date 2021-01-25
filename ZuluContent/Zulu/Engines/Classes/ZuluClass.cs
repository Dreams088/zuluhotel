using System;
using System.Collections.Generic;
using System.Linq;
using Server;
using Server.Mobiles;
using Server.Targeting;

namespace Scripts.Zulu.Engines.Classes
{
    [PropertyObject]
    public class ZuluClass
    {
        //reference original ZH Canada (ZH3) release
        private const double ClassPointsPerLevel = 120;
        private const double SkillBase = 480;
        private const double PercentPerLevel = 0.08;
        private const double PercentBase = 0.52;
        private const double PerLevel = 0.1; //10% per level
        private const int MaxLevel = 6;

        private static readonly double[] MinSkills =
            Enumerable
                .Range(0, MaxLevel + 1) // Technically lvl 0 (none) is a level
                .Select(i => SkillBase + ClassPointsPerLevel * i)
                .ToArray();

        private readonly IZuluClassed m_Parent;
        
        public static readonly IReadOnlyDictionary<ZuluClassType, SkillName[]> ClassSkills =
            new Dictionary<ZuluClassType, SkillName[]>
            {
                [ZuluClassType.Warrior] = new[]
                {
                    SkillName.Wrestling,
                    SkillName.Tactics,
                    SkillName.Healing,
                    SkillName.Anatomy,
                    SkillName.Swords,
                    SkillName.Macing,
                    SkillName.Fencing,
                    SkillName.Parry,
                },
                [ZuluClassType.Ranger] = new[]
                {
                    SkillName.Tracking,
                    SkillName.Archery,
                    SkillName.AnimalLore,
                    SkillName.Veterinary,
                    SkillName.AnimalTaming,
                    SkillName.Fishing,
                    SkillName.Camping,
                    SkillName.Cooking,
                },
                [ZuluClassType.Mage] = new[]
                {
                    SkillName.Alchemy,
                    SkillName.ItemID,
                    SkillName.EvalInt,
                    SkillName.Inscribe,
                    SkillName.MagicResist,
                    SkillName.Meditation,
                    SkillName.Magery,
                    SkillName.SpiritSpeak,
                },
                [ZuluClassType.Crafter] = new[]
                {
                    SkillName.Tinkering,
                    SkillName.ArmsLore,
                    SkillName.Fletching,
                    SkillName.Tailoring,
                    SkillName.Mining,
                    SkillName.Lumberjacking,
                    SkillName.Carpentry,
                    SkillName.Blacksmith,
                },
                [ZuluClassType.Thief] = new[]
                {
                    SkillName.Hiding,
                    SkillName.Stealth,
                    SkillName.Stealing,
                    SkillName.DetectHidden,
                    SkillName.RemoveTrap,
                    SkillName.Poisoning,
                    SkillName.Lockpicking,
                    SkillName.Snooping,
                },
                [ZuluClassType.Bard] = new[]
                {
                    SkillName.Provocation,
                    SkillName.Musicianship,
                    SkillName.Herding,
                    SkillName.Discordance,
                    SkillName.TasteID,
                    SkillName.Peacemaking,
                    SkillName.Cartography,
                    SkillName.Begging,
                }
            };

        [CommandProperty(AccessLevel.Counselor)]
        public int Level { get; set; } = 0;

        [CommandProperty(AccessLevel.Counselor)]
        public ZuluClassType Type { get; set; } = ZuluClassType.None;
        
        public ZuluClass(IZuluClassed parent)
        {
            m_Parent = parent;
            ComputeClass();
        }

        [CommandProperty(AccessLevel.Counselor)]
        public double Bonus => Type == ZuluClassType.PowerPlayer || Type == ZuluClassType.None
            ? 1.0
            : 1.0 + Level * PerLevel;

        public static double GetBonusFor(Mobile m, ZuluClassType name) =>
            m is IZuluClassed classed ? classed.ZuluClass.GetBonusFor(name) : 1.0;

        public double GetBonusFor(ZuluClassType name) => Type == name ? Bonus : 1.0;

        public static void Initialize()
        {
            CommandSystem.Register("class", AccessLevel.Player, ClassOnCommand);
            CommandSystem.Register("showclasse", AccessLevel.Player, ClassOnCommand);
            CommandSystem.Register("setclass", AccessLevel.GameMaster, SetClass);
        }

        public static void ClassOnCommand(CommandEventArgs e)
        {
            if (!(e.Mobile is PlayerMobile pm))
                return;

            if (pm.AccessLevel >= AccessLevel.Counselor)
            {
                pm.Target = new InternalTarget();
                return;
            }

            pm.ZuluClass.ComputeClass();

            var message = pm.ZuluClass.Type == ZuluClassType.None
                ? "You aren't a member of any particular class."
                : $"You are a qualified level {pm.ZuluClass.Level} {pm.ZuluClass.Type.FriendlyName()}.";

            pm.SendMessage(message);
        }

        [Usage("SetClass <class> <level>")]
        [Description("Sets you to the desired class and level, sets all other skills to 0.")]
        public static void SetClass(CommandEventArgs e)
        {
            if (!(e.Mobile is PlayerMobile pm))
                return;
            
            if(e.Length == 2 && Enum.TryParse(e.GetString(0), out ZuluClassType classType))
            {
                var level = e.GetInt32(1);

                if (level > MaxLevel || level < 0)
                    level = 0;
                
                foreach (var skill in pm.Skills)
                {
                    skill.Base = ClassSkills[classType].Contains(skill.SkillName) 
                        ? MinSkills[level] / ClassSkills[classType].Length
                        : 0.0;
                }
            }

        }

        public static ZuluClass GetClass(Mobile m) => m is IZuluClassed classed ? classed.ZuluClass : null;

        public void ComputeClass()
        {
            if (m_Parent is BaseCreature)
                return;
            
            var allSkillsTotal = 0.0;
            foreach (var skill in m_Parent.Skills)
            {
                allSkillsTotal += skill.Value;
            }

            Type = ZuluClassType.None;
            Level = 0;
            
            double total = m_Parent.Skills.Total;
            total *= 0.1;

            switch (total)
            {
                case < 600.0:
                    Level = 0;
                    Type = ZuluClassType.None;
                    return;
                case >= 3920.0:
                {
                    Type = ZuluClassType.PowerPlayer;
                    Level = 1;

                    if (total >= 5145)
                    {
                        Level = 2;

                        if (total >= 6370)
                        {
                            Level = 3;
                        }
                    }

                    //we're a pp so:
                    return;
                }
            }
            
            foreach (var (classType, classSkills) in ClassSkills)
            {
                var classTotal = classSkills.Select(s => m_Parent.Skills[s].Value).Sum();
                
                var level = GetClassLevel(classTotal, allSkillsTotal);
                
                if (level > 0)
                {
                    Type = classType;
                    Level = level;
                }
            }

            if (Level > MaxLevel) 
                Level = MaxLevel;

            if (Level <= 0)
            {
                Level = 0;
                Type = ZuluClassType.None;
            }
        }

        //idx:    0    1     2     3     4     5      6
        //Min: [ 480, 600,  720,  840,  960,  1080, 1200 ]
        //Max: [ 923, 1000, 1058, 1105, 1142, 1173, 1200 ]
        private int GetClassLevel(double classTotal, double allSkillsTotal)
        {
            for (int level = MinSkills.Length - 1; level >= 0; level--)
            {
                var levelReq = PercentBase + PercentPerLevel * level;
                var classPct = classTotal / allSkillsTotal;

                if (classTotal >= MinSkills[level] && classPct > levelReq)
                    return level;
            }

            return 0;
        }
        
        public bool IsSkillInClass(SkillName sn)
        {
            return ClassSkills.FirstOrDefault(kv => kv.Value.Contains(sn)).Key == Type;
        }

        private class InternalTarget : Target
        {
            public InternalTarget() : base(12, false, TargetFlags.None)
            {
            }

            protected override void OnTarget(Mobile from, object target)
            {
                if (from is IZuluClassed classed)
                {
                    classed.ZuluClass?.ComputeClass();
                    from.SendMessage("{0}: {1}, level {2}", from.Name, classed.ZuluClass?.Type, classed.ZuluClass?.Level);
                }
            }
        }
    }
}