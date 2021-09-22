package Plant

Soloman: {
	Name:               "Soloman"
	CorpseNameOverride: "corpse of Soloman"
	BaseType:           "Server.Mobiles.BaseCreatureTemplate"
	Str:                2250
	Int:                55
	Dex:                400
	AlwaysMurderer:     true
	AutoDispel:         true
	BaseSoundID:        362
	Body:               301
	CreatureType:       "Plant"
	VirtualArmor:       45
	FightMode:          "Aggressor"
	HitsMax:            2250
	Hue:                1497
	LootItemChance:     90
	LootItemLevel:      8
	LootTable:          "9"
	ManaMaxSeed:        0
	StamMaxSeed:        200
	Resistances: {
		Fire:          75
		Air:           100
		Water:         100
		Earth:         100
		Necro:         100
		MagicImmunity: 8
	}
	Skills: {
		Tactics:      160
		Wrestling:    185
		MagicResist:  60
		DetectHidden: 200
	}
	Attack: {
		Damage: {
			Min: 10
			Max: 60
		}
		HitSound: 364
	}
}
