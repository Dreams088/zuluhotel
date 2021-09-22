using System;
using System.Collections.Generic;
using Server;
using Server.Misc;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedType.Global UnusedMember.Global ClassNeverInstantiated.Global
namespace Scripts.Configuration
{
    public class CraftConfiguration : BaseSingleton<CraftConfiguration>
    {
        public readonly AutoLoopSettings AutoLoop;
        public readonly CraftSettings Alchemy;
        public readonly CraftSettings AlchemyPlus;
        public readonly CraftSettings Blacksmithy;
        public readonly CraftSettings Carpentry;
        public readonly CraftSettings Cartography;
        public readonly CraftSettings Cooking;
        public readonly CraftSettings Fletching;
        public readonly CraftSettings Inscription;
        public readonly CraftSettings Tailoring;
        public readonly CraftSettings Tinkering;
        
        protected CraftConfiguration()
        {
            const string baseDir = "Data/Crafting";
            AutoLoop = ZhConfig.DeserializeJsonConfig<AutoLoopSettings>($"{baseDir}/autoloop.json");
            Alchemy = ZhConfig.DeserializeJsonConfig<CraftSettings>($"{baseDir}/alchemy.json");
            AlchemyPlus = ZhConfig.DeserializeJsonConfig<CraftSettings>($"{baseDir}/alchemyplus.json");
            Blacksmithy = ZhConfig.DeserializeJsonConfig<CraftSettings>($"{baseDir}/blacksmithy.json");
            Carpentry = ZhConfig.DeserializeJsonConfig<CraftSettings>($"{baseDir}/carpentry.json");
            Cartography = ZhConfig.DeserializeJsonConfig<CraftSettings>($"{baseDir}/cartography.json");
            Cooking = ZhConfig.DeserializeJsonConfig<CraftSettings>($"{baseDir}/cooking.json");
            Fletching = ZhConfig.DeserializeJsonConfig<CraftSettings>($"{baseDir}/fletching.json");
            Inscription = ZhConfig.DeserializeJsonConfig<CraftSettings>($"{baseDir}/inscription.json");
            Tailoring = ZhConfig.DeserializeJsonConfig<CraftSettings>($"{baseDir}/tailoring.json");
            Tinkering = ZhConfig.DeserializeJsonConfig<CraftSettings>($"{baseDir}/tinkering.json");
        }
    }
    
    public record AutoLoopSettings
    {
        public double Delay { get; init; }
    }
    
    public record CraftSettings
    {
        public SkillName MainSkill { get; init; }
        public TextDefinition GumpTitleId { get; init; }
        public List<CraftEntry> CraftEntries { get; init; }
        public int MinCraftDelays { get; init; }
        public int MaxCraftDelays { get; init; }
        public double Delay { get; init; }
        public double MinCraftChance { get; init; }
        public int CraftWorkSound { get; init; }
        public int CraftEndSound { get; init; }
    }
    
    public record CraftResource
    {
        public Type ItemType { get; init; }
        public TextDefinition Name { get; init; }
        public int Amount { get; init; }
        public TextDefinition Message { get; init; }
    }
    
    public record CraftEntry
    {
        public Type ItemType { get; init; }
        public TextDefinition Name { get; init; }
        public TextDefinition GroupName { get; init; }
        public double Skill { get; init; }
        public SkillName? SecondarySkill { get; init; }
        public double? Skill2 { get; init; }

        public List<CraftResource> Resources { get; init; }
        public bool UseAllRes { get; init; }
        public bool NeedHeat { get; init; }
        public bool NeedOven { get; init; }
        public bool NeedMill { get; init; }

    }
}