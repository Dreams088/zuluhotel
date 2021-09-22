package Undead

GwennoUndeadBard: {
	Name:               "Gwenno the Undead Bard"
	CorpseNameOverride: "corpse of Gwenno the Undead Bard"
	BaseType:           "Server.Mobiles.BaseCreatureTemplate"
	Str:                510
	Int:                510
	Dex:                510
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	Body:               401
	CreatureType:       "Undead"
	FightMode:          "Closest"
	HitsMax:            510
	LootItemChance:     70
	LootItemLevel:      5
	LootTable:          "35"
	ManaMaxSeed:        500
	StamMaxSeed:        500
	PreferredSpells: ["Paralyze", "MassDispel", "Curse", "ManaDrain"]
	Resistances: {
		Poison:        6
		MagicImmunity: 4
	}
	Skills: {
		MagicResist: 80
		Wrestling:   100
	}
	Attack: {
		Damage: {
			Min: 5
			Max: 50
		}
	}
}
