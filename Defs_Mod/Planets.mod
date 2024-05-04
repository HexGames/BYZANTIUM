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
			Building:Stable_Orbit
		}
	}
	Planet:Star White_Dwarf
	{
		Weight 4
		PlanetsCold
		Planets:Min 2
		Planets:Max 6
		Features
		{
			Building:Stable_Orbit
		}
	}
	Planet:Star Main_Sequence
	{
		Weight 4
		Planets:Min 5
		Planets:Max 9
		Features
		{
			Building:Stable_Orbit
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
			Building:Stable_Orbit
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
			Building:Stable_Orbit
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
			High_Energy
			Building:Stable_Orbit
		}
	}
	// ----------------------------------------------- specials
	Planet:Gas_Giant
	{
		Features
		{
			Mod:Rings:Perc 30
			Usefull_Gasses:Perc 20
			Building:Stable_Orbit
		}
	}
	Planet:Asteroid_Field
	{
		Features
		{
			Building:Possible_Asteroid_Mines
			OR:Perc 25
			{
				Rich_Asteroid_Minerals:Weight 1
				Gold_Asteroid:Weight 1
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
			Mod:Rings:Perc 10
			Building:Possible_Outpost
			OR:Perc 20
			{
				Trapped_Gasses:Weight 1
				Bacterial_Life:Weight 1
			}
			Uninhabitable
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
			Mod:Rings:Perc 10
			Building:Possible_Outpost
			OR:Perc 20
			{
				Trapped_Gasses:Weight 1
				MineralVeins:Weight 1
			}
			Uninhabitable
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
			Mod:Rings:Perc 10
			Building:Possible_Outpost
			OR:Perc 20
			{
				MineralVeins:Weight 1
				Bacterial_Life:Weight 1
			}
			Uninhabitable
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
			Mod:Rings:Perc 10
			Building:Possible_Outpost
			OR:Perc 30
			{
				Trapped_Gasses:Weight 1
				MineralVeins:Weight 1
				OR:Weight 1
				{
					Bacterial_Life:Weight 2
					Simple_Life:Weight 1
					Complex_Life:Weight 1
				}
			}
			Mod WorldPlanet
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
			Mod:Rings:Perc 10
			Building:Possible_Outpost
			OR:Perc 40
			{
				Trapped_Gasses:Weight 1
				MineralVeins:Weight 1
				OR:Weight 2
				{
					Bacterial_Life:Weight 2
					Simple_Life:Weight 1
					Complex_Life:Weight 1
				}
			}
			Mod WorldPlanet
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
			Mod:Rings:Perc 10
			Building:Possible_Outpost
			OR:Perc 20
			{
				Trapped_Gasses:Weight 1
				MineralVeins:Weight 1
			}
			OR
			{
				Bacterial_Life:Weight 1
				Simple_Life:Weight 1
				Complex_Life:Weight 1
			}
			Mod WorldPlanet
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
			Mod:Rings:Perc 10
			Building:Possible_Outpost
			OR
			{
				Bacterial_Life:Weight 1
				Simple_Life:Weight 2
				Complex_Life:Weight 3
			}
			Mod WorldPlanet
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
			Mod:Rings:Perc 10
			Building:Possible_Outpost
			Trapped_Gasses
			OR
			{
				Bacterial_Life:Weight 3
				Simple_Life:Weight 2
				Complex_Life:Weight 1
			}
			Mod WorldPlanet
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
			Mod:Rings:Perc 10
			Building:Possible_Outpost
			OR:Perc 40
			{
				Trapped_Gasses:Weight 1
				OR:Weight 3
				{
					Bacterial_Life:Weight 1
					Simple_Life:Weight 1
					Complex_Life:Weight 1
				}
			}
			Mod WorldPlanet
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
			Mod:Rings:Perc 10
			Building:Possible_Outpost
			OR:Perc 60
			{
				MineralVeins:Weight 5
				OR:Weight 1
				{
					Bacterial_Life:Weight 1
					Simple_Life:Weight 1
					Complex_Life:Weight 1
				}
			}
			Mod WorldPlanet
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
			Mod:Rings:Perc 10
			Mod:NormalPlanet
			Building:Trapped_Gasses:Perc 50
			MineralVeins
		}
	}
	// ----------------------------------------------- mods
	Mod WorldPlanet
	{
		OR:Perc 50
		{
			OR:Weight 2
			{
				Tiny_Moon:Weight 2
				Small_Moon:Weight 1
			}
			OR:Weight 2
			{
				Good_Iron:Weight 2
				Rich_Iron:Weight 1
			}
			OR:Weight 2
			{
				Strange:Weight 2
				Bizarre:Weight 1
			}
			OR:Weight 2
			{
				Beautiful:Weight 2
				Paradise:Weight 1
			}
			OR:Weight 2
			{
				Lush:Weight 2
				Fertile:Weight 1
			}
			High_Gravity:Weight 1
			Poor_Iron:Weight 1
			Boring:Weight 1
			Smelly:Weight 1
			Semi-Barren:Weight 1
		}
	}
	Mod Rings
	{
		Building:Ring_Minerals
	}
}
