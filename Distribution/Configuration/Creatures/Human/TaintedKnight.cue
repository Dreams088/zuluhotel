package Human

TaintedKnight: {
	Name:               "<random> the Tainted Knight"
	CorpseNameOverride: "corpse of <random> the Tainted Knight"
	BaseType:           "Server.Mobiles.BaseCreatureTemplate"
	Str:                400
	Int:                200
	Dex:                250
	AlwaysMurderer:     true
	BaseSoundID:        569
	Body:               554
	ClassLevel:         3
	ClassType:          "Warrior"
	CreatureType:       "Human"
	VirtualArmor:       50
	FightMode:          "Aggressor"
	HitsMax:            400
	Hue:                1302
	LootItemChance:     50
	LootItemLevel:      4
	LootTable:          "131"
	ManaMaxSeed:        100
	RiseCreatureDelay:  "00:00:02"
	StamMaxSeed:        50
	Skills: {
		Tactics:     120
		MagicResist: 100
	}
	Attack: {
		Speed: 50
		Damage: {
			Min: 25
			Max: 45
		}
		HitSound:  571
		MissSound: 569
		Ability: {
			SpellType: "Darkness"
		}
		AbilityChance: 1
	}
	Equipment: [{
		ItemType: "Server.Items.Longsword"
		Name:     "a stygian-bladed sword"
		Hue:      1283
	}, {
		ItemType:    "Server.Items.BoneHelm"
		Name:        "the bones of the damned"
		Hue:         1302
		ArmorRating: 1
	}, {
		ItemType:    "Server.Items.BoneGloves"
		Name:        "the bones of the damned"
		Hue:         1302
		ArmorRating: 1
	}, {
		ItemType:    "Server.Items.BoneLegs"
		Name:        "the bones of the damned"
		Hue:         1302
		ArmorRating: 1
	}, {
		ItemType:    "Server.Items.BoneChest"
		Name:        "the bones of the damned"
		Hue:         1302
		ArmorRating: 1
	}, {
		ItemType:    "Server.Items.MetalShield"
		Name:        "a shield of stygian darkness"
		ArmorRating: 30
	}]
}
