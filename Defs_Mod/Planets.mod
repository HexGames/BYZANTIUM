Def_Planets
{
	// ----------------------------------------------- stars
	Planet:Star Red_Dwarf
	{
		Weight 10
		PlanetsCold
		GasGiantsFirst
		Planets:Min 2
		Planets:Max 6
		Features
		{
			GasGiantsFirst
		}
	}
	Planet:Star White_Dwarf
	{
		Weight 4
		PlanetsCold
		Planets:Min 2
		Planets:Max 6
	}
	Planet:Star Main_Sequence
	{
		Weight 4
		Planets:Min 5
		Planets:Max 9
		Features
		{
			OR
			{
				Active:Weight 9
				HyperActive:Weight 1
			}
		}
	}
	Planet:Star Red_Giant
	{
		Weight 2
		PlanetsHot
		Planets:Min 2
		Planets:Max 6
		Features
		{
			OR
			{
				Active:Weight 4
				HyperActive:Weight 1
			}
		}
	}
	Planet:Star Blue_Giant
	{
		Weight 2
		PlanetsHot
		Planets:Min 2
		Planets:Max 6
		Features
		{
			OR
			{
				Active:Weight 4
				HyperActive:Weight 1
			}
		}
	}
	Planet:Star Pulsar
	{
		Weight 1
		PlanetsHot
		Planets:Min 1
		Planets:Max 3
		Features
		{
			Anti-Matter
		}
	}
	// ----------------------------------------------- specials
	Planet:Gas_Giant
	{
		Features
		{
			Rings:Perc 30
			RichGases:Perc 20
			Deuterium:Perc 5
		}
	}
	Planet:Asteroid_Field
	{
		Features
		{
			OR
			{
				Minerals:Weight 2
				RichMinerals:Weight 1
			}
		}
	}
	// ----------------------------------------------- planets
	Planet Frozen
	{
		Weight 4
		Temperature:Min 1
		Temperature:Max 1
		ExoticResourceFlag
		Features
		{
			Low_Atmosphere:Perc 90
			Mod:MineralVeins:Perc 10
			Bacterial_Life:Perc 20
		}
	}
	Planet Barren
	{
		Weight 4
		Temperature:Min 1
		Temperature:Max 5
		ExoticResourceFlag
		Features
		{
			Rings:Perc 10
			Low_Atmosphere
			OR:Perc 25
			{
				Mod:MineralVeins:Weight 1
				Mod:GeotermalVents:Weight 1
			}
		}
	}
	Planet Toxic
	{
		Weight 2
		Temperature:Min 2
		Temperature:Max 4
		ExoticResourceFlag
		Features
		{
			Rings:Perc 10
			OR:Perc 25
			{
				Mod:MineralVeins:Weight 1
				Mod:GeotermalVents:Weight 1
			}
			Bacterial_Life:Perc 50
		}
	}
	Planet Desert
	{
		Weight 2
		Temperature:Min 2
		Temperature:Max 4
		ExoticResourceFlag
		Features
		{
			Rings:Perc 10
			Mod NormalPlanet
			OR:Perc 50
			{
				Low_Atmosphere:Weight 1
				Hostile_Fauna:Weight 1
			}
			OR:Perc 25
			{
				Mod:MineralVeins:Weight 1
				Mod:GeotermalVents:Weight 1
			}
			OR:Perc 20
			{
				Bacterial_Life:Weight 2
				Simple_Life:Weight 1
				Complex_Life:Weight 1
			}
		}
	}
	Planet Arid
	{
		Weight 2
		Temperature:Min 2
		Temperature:Max 4
		ExoticResourceFlag
		Features
		{
			Rings:Perc 10
			Ice_Caps:Perc 50
			Mod NormalPlanet
			OR:Perc 50
			{
				Low_Atmosphere:Weight 1
				Hostile_Fauna:Weight 1
			}
			OR:Perc 25
			{
				Mod:MineralVeins:Weight 1
				Mod:GeotermalVents:Weight 1
			}
			OR:Perc 50
			{
				Bacterial_Life:Weight 2
				Simple_Life:Weight 1
				Complex_Life:Weight 1
			}
		}
	}
	Planet Continents
	{
		Weight 2
		Temperature:Min 2
		Temperature:Max 4
		ExoticResourceFlag
		Features
		{
			Rings:Perc 10
			Hostile_Fauna:Perc 50
			Big_Oceans:Perc 50
			Mod NormalPlanet
			OR:Perc 25
			{
				Mod:MineralVeins:Weight 1
				Mod:GeotermalVents:Weight 1
			}
			OR:Perc 80
			{
				Bacterial_Life:Weight 2
				Simple_Life:Weight 1
				Complex_Life:Weight 1
			}
		}
	}
	Planet Ocean
	{
		Weight 2
		Temperature:Min 2
		Temperature:Max 4
		ExoticResourceFlag
		Features
		{
			Rings:Perc 10
			No_Land:Perc 50
			Mod NormalPlanet
			Low_Atmosphere:Perc 50
			OR:Perc 25
			{
				Mod:MineralVeins:Weight 1
				Mod:GeotermalVents:Weight 1
			}
			OR:Perc 80
			{
				Bacterial_Life:Weight 2
				Simple_Life:Weight 1
				Complex_Life:Weight 1
			}
		}
	}
	Planet Swamp
	{
		Weight 2
		Temperature:Min 2
		Temperature:Max 4
		ExoticResourceFlag
		Features
		{
			Rings:Perc 10
			Mod NormalPlanet
			Hostile_Fauna
			OR:Perc 25
			{
				Mod:MineralVeins:Weight 1
				Mod:GeotermalVents:Weight 1
			}
			OR
			{
				Bacterial_Life:Weight 1
				Simple_Life:Weight 2
				Complex_Life:Weight 3
			}
		}
	}
	Planet Artic
	{	
		Weight 4
		Temperature:Min 2
		Temperature:Max 2
		ExoticResourceFlag
		Features
		{
			Rings:Perc 10
			Mod NormalPlanet
			OR:Perc 50
			{
				Low_Atmosphere:Weight 1
				Hostile_Fauna:Weight 1
			}
			OR:Perc 25
			{
				Mod:MineralVeins:Weight 1
				Mod:GeotermalVents:Weight 1
			}
			OR:Perc 50
			{
				Bacterial_Life:Weight 2
				Simple_Life:Weight 1
				Complex_Life:Weight 1
			}
		}
	}
	Planet Vulcanic
	{
		Weight 1
		Temperature:Min 2
		Temperature:Max 5
		ExoticResourceFlag
		Features
		{
			Rings:Perc 10
			Mod:NormalPlanet
			LowA_tmosphere:Perc 50
			OR
			{
				Mod:MineralVeins:Weight 1
				Mod:GeotermalVents:Weight 1
			}
			OR:Perc 30
			{
				Bacterial_Life:Weight 2
				Simple_Life:Weight 1
				Complex_Life:Weight 1
			}
		}
	}
	Planet Lava
	{
		Weight 1
		Temperature:Min 5
		Temperature:Max 5
		ExoticResourceFlag
		Features
		{
			Rings:Perc 10
			Mod:NormalPlanet
			LowA_tmosphere:Perc 50
			Mod:MineralVeins
			Mod:GeotermalVents
			OR:Perc 10
			{
				Bacterial_Life:Weight 4
				Simple_Life:Weight 1
			}
		}
	}
	// ----------------------------------------------- mods
	Mod NormalPlanet
	{
		OR:Perc 50
		{
			High_Gravity:Weight 1
			Poor_Iron:Weight 1
			Boring:Weight 1
			Smelly:Weight 1
			Semi-Barren:Weight 1
		}
		OR:Perc 80
		{
			OR:Weight 1
			{
				Tiny_Moon:Weight 2
				Small_Moon:Weight 1
			}
			OR:Weight 1
			{
				Good_Iron:Weight 2
				Rich_Iron:Weight 1
			}
			OR:Weight 1
			{
				Strange:Weight 2
				Bizarre:Weight 1
			}
			OR:Weight 1
			{
				Beautiful:Weight 2
				Paradise:Weight 1
			}
			OR:Weight 1
			{
				Lush:Weight 2
				Fertile:Weight 1
			}
		}
		OR:Perc 20
		{
			OR:Weight 1
			{
				Tiny_Moon:Weight 2
				Small_Moon:Weight 1
			}
			OR:Weight 1
			{
				Good_Iron:Weight 2
				Rich_Iron:Weight 1
			}
			OR:Weight 1
			{
				Strange:Weight 2
				Bizarre:Weight 1
			}
			OR:Weight 1
			{
				Beautiful:Weight 2
				Paradise:Weight 1
			}
			OR:Weight 1
			{
				Lush:Weight 2
				Fertile:Weight 1
			}
		}
	}
	Mod MineralVeins
	{
		OR
		{
			Mineral_Veins:Weight 9
			Big_Mineral_Veins:Weight 4
			Rich_Mineral_Veins:Weight 4
			Super_Mineral_Veins:Weight 1
		}
	}
	Mod GeotermalVents
	{
		OR
		{
			Gerothermal_Vents:Weight 9
			Many_Gerothermal_Vents:Weight 4
			Powerful_Gerothermal_Vents:Weight 4
			Super_Gerothermal_Vents:Weight 1
		}
	}
}
