package Human

TaintedWarrior: {
	Name:               "<random> the Tainted Warrior"
	CorpseNameOverride: "corpse of <random> the Tainted Warrior"
	Str:                400
	Int:                200
	Dex:                250
	AlwaysMurderer:     true
	BaseSoundID:        565
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
		Speed: 40
		Damage: {
			Min: 33
			Max: 65
		}
		Animation: "Bash2H"
		HitSound:  567
		MissSound: 562
		Ability: {
			SpellType: "Darkness"
		}
		AbilityChance: 1
	}
	Equipment: [{
		ItemType: "SHalberd"
		Name:     "a stygian-bladed halberd"
		Hue:      1283
	}, {
		ItemType:    "SBoneHelm"
		Name:        "the bones of the damned"
		Hue:         1302
		ArmorRating: 1
	}, {
		ItemType:    "SBoneGloves"
		Name:        "the bones of the damned"
		Hue:         1302
		ArmorRating: 1
	}, {
		ItemType:    "SBoneLegs"
		Name:        "the bones of the damned"
		Hue:         1302
		ArmorRating: 1
	}, {
		ItemType:    "SBoneChest"
		Name:        "the bones of the damned"
		Hue:         1302
		ArmorRating: 1
	}]
}
