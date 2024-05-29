Def_Planets 
{
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
			Building:Stable_Orbit 
		}
	}
	Planet:Gas_Giant 
	{
		Features 
		{
			Gas_Giant 
			OR 
			{
				Rings:Weight 1
				Tiny_Moon:Weight 1
			}
			OR:Perc 50
			{
				Useful_Gasses:Weight 2
				Rich_Gasses:Weight 1
			}
			Building:Possible_Outpost 
		}
	}
	Planet:Asteroids 
	{
		Features 
		{
			OR 
			{
				Useful_Minerals:Weight 2
				Rich_Minerals:Weight 1
			}
			Building:Possible_Outpost 
		}
	}
	Planet Frozen
	{
		Weight 4
		Temperature:Min 1
		Temperature:Max 1
		Features 
		{
			Extreme_Temps 
			Barren 
			Rocky:Perc 25
			OR:Perc 20
			{
				Rings:Weight 1
				Tiny_Moon:Weight 1
			}
			Building:Possible_Outpost 
		}
	}
	Planet Barren
	{
		Weight 4
		Temperature:Min 1
		Temperature:Max 5
		Features 
		{
			Barren 
			Rocky 
			OR:Perc 20
			{
				Rings:Weight 1
				Tiny_Moon:Weight 1
			}
			OR:Perc 15
			{
				Useful_Minerals:Weight 2
				Rich_Minerals:Weight 1
			}
			Building:Possible_Outpost 
		}
	}
	Planet Toxic
	{
		Weight 2
		Temperature:Min 2
		Temperature:Max 4
		Features 
		{
			Toxic 
			OR:Perc 50
			{
				Semi-Barren:Weight 2
				Barren:Weight 1
			}
			Rocky 
			OR:Perc 20
			{
				Rings:Weight 1
				Tiny_Moon:Weight 1
			}
			OR:Perc 15
			{
				Useful_Minerals:Weight 2
				Rich_Minerals:Weight 1
			}
			Building:Possible_Outpost 
		}
	}
	Planet Desert
	{
		Weight 2
		Temperature:Min 2
		Temperature:Max 4
		Features 
		{
			OR 
			{
				Semi-Barren:Weight 2
				Barren:Weight 1
			}
			Rocky 
			OR:Perc 20
			{
				Rings:Weight 1
				Tiny_Moon:Weight 1
			}
			OR:Perc 15
			{
				Useful_Minerals:Weight 2
				Rich_Minerals:Weight 1
			}
			OR:Perc 15
			{
				Bacterial_Life:Weight 2
				Complex_Life:Weight 1
			}
			Colonizable 
			Building:Possible_Outpost 
		}
	}
	Planet Arid
	{
		Weight 2
		Temperature:Min 2
		Temperature:Max 4
		Features 
		{
			Rocky 
			OR:Perc 20
			{
				Rings:Weight 1
				Tiny_Moon:Weight 1
			}
			OR:Perc 15
			{
				Useful_Minerals:Weight 2
				Rich_Minerals:Weight 1
			}
			OR:Perc 15
			{
				Bacterial_Life:Weight 2
				Complex_Life:Weight 1
			}
			Colonizable 
			Building:Possible_Outpost 
		}
	}
	Planet Continents
	{
		Weight 2
		Temperature:Min 2
		Temperature:Max 4
		Features 
		{
			Rocky 
			OR:Perc 20
			{
				Rings:Weight 1
				Tiny_Moon:Weight 1
			}
			Oceans 
			OR:Perc 15
			{
				Useful_Minerals:Weight 2
				Rich_Minerals:Weight 1
			}
			OR:Perc 75
			{
				Bacterial_Life:Weight 2
				Complex_Life:Weight 1
			}
			Colonizable 
			Building:Possible_Outpost 
		}
	}
	Planet Ocean
	{
		Weight 2
		Temperature:Min 2
		Temperature:Max 4
		Features 
		{
			OR:Perc 20
			{
				Rings:Weight 1
				Tiny_Moon:Weight 1
			}
			Oceans 
			OR:Perc 15
			{
				Useful_Minerals:Weight 2
				Rich_Minerals:Weight 1
			}
			OR:Perc 75
			{
				Bacterial_Life:Weight 2
				Complex_Life:Weight 1
			}
			Colonizable 
			Building:Possible_Outpost 
		}
	}
	Planet Swamp
	{
		Weight 2
		Temperature:Min 2
		Temperature:Max 4
		Features 
		{
			Rocky 
			OR:Perc 20
			{
				Rings:Weight 1
				Tiny_Moon:Weight 1
			}
			OR:Perc 15
			{
				Useful_Minerals:Weight 2
				Rich_Minerals:Weight 1
			}
			OR:Perc 75
			{
				Bacterial_Life:Weight 2
				Complex_Life:Weight 1
			}
			Colonizable 
			Building:Possible_Outpost 
		}
	}
	Planet Artic
	{
		Weight 4
		Temperature:Min 2
		Temperature:Max 2
		Features 
		{
			Rocky 
			OR:Perc 20
			{
				Rings:Weight 1
				Tiny_Moon:Weight 1
			}
			OR:Perc 15
			{
				Useful_Minerals:Weight 2
				Rich_Minerals:Weight 1
			}
			OR:Perc 30
			{
				Bacterial_Life:Weight 2
				Complex_Life:Weight 1
			}
			Colonizable 
			Building:Possible_Outpost 
		}
	}
	Planet Vulcanic
	{
		Weight 1
		Temperature:Min 2
		Temperature:Max 4
		Features 
		{
			Rocky 
			OR:Perc 20
			{
				Rings:Weight 1
				Tiny_Moon:Weight 1
			}
			OR 
			{
				Useful_Minerals:Weight 2
				Rich_Minerals:Weight 1
			}
			OR:Perc 15
			{
				Bacterial_Life:Weight 2
				Complex_Life:Weight 1
			}
			Colonizable 
			Building:Possible_Outpost 
		}
	}
	Planet Lava
	{
		Weight 1
		Temperature:Min 5
		Temperature:Max 5
		Features 
		{
			Extreme_Temps 
			Rocky 
			OR:Perc 20
			{
				Rings:Weight 1
				Tiny_Moon:Weight 1
			}
			OR 
			{
				Useful_Minerals:Weight 2
				Rich_Minerals:Weight 1
			}
			OR:Perc 15
			{
				Bacterial_Life:Weight 2
				Complex_Life:Weight 1
			}
			Building:Possible_Outpost 
		}
	}
	Custom Mercury
	{
		Type Barren
		Size 1
		Temperature 5
		Features 
		{
			Barren 
			Rocky 
			OR:Perc 15
			{
				Useful_Minerals:Weight 2
				Rich_Minerals:Weight 1
			}
			Building:Possible_Outpost 
		}
	}
	Custom Venus
	{
		Type Toxic
		Size 2
		Temperature 4
		Features 
		{
			Toxic 
			Rocky 
			OR:Perc 15
			{
				Useful_Minerals:Weight 2
				Rich_Minerals:Weight 1
			}
			Building:Possible_Outpost 
		}
	}
	Custom Terra
	{
		Custom 
		Type Continants
		Size 2
		Temperature 3
		Features 
		{
			Rocky 
			Oceans 
			OR:Perc 15
			{
				Useful_Minerals:Weight 2
				Rich_Minerals:Weight 1
			}
			Complex_Life 
			Colonizable 
			Building:Possible_Outpost 
		}
	}
	Custom Moon
	{
		Type Barren
		Size 1
		Temperature 3
		Moon 
		Features 
		{
			Barren 
			Rocky 
			OR:Perc 15
			{
				Useful_Minerals:Weight 2
				Rich_Minerals:Weight 1
			}
			Building:Possible_Outpost 
		}
	}
	Custom Mars
	{
		Type Desert
		Size 1
		Temperature 2
		Features 
		{
			Semi-Barren 
			Rocky 
			Tiny_Moon 
			OR:Perc 15
			{
				Useful_Minerals:Weight 2
				Rich_Minerals:Weight 1
			}
			OR:Perc 10
			{
				Bacterial_Life:Weight 2
			}
			Colonizable 
			Building:Possible_Outpost 
		}
	}
	Custom Asteroids
	{
		Type Asteroids
		Features 
		{
			OR 
			{
				Useful_Minerals:Weight 2
				Rich_Minerals:Weight 1
			}
			Building:Possible_Outpost 
		}
	}
	Custom Jupiter
	{
		Type Gas_Giant
		Size 6
		Temperature 2
		Features 
		{
			Gas_Giant 
			Tiny_Moon 
			OR:Perc 50
			{
				Useful_Gasses:Weight 2
				Rich_Gasses:Weight 1
			}
			Building:Possible_Outpost 
		}
	}
	Custom Ganymede
	{
		Type Barren
		Size 1
		Temperature 2
		Moon 
		Features 
		{
			Barren 
			Rocky 
			OR:Perc 15
			{
				Useful_Minerals:Weight 2
				Rich_Minerals:Weight 1
			}
			Building:Possible_Outpost 
		}
	}
	Custom Saturn
	{
		Type Gas_Giant
		Size 5
		Temperature 1
		Features 
		{
			Gas_Giant 
			Rings 
			OR:Perc 50
			{
				Useful_Gasses:Weight 2
				Rich_Gasses:Weight 1
			}
			Building:Possible_Outpost 
		}
	}
	Custom Titan
	{
		Type Frozen
		Size 1
		Temperature 1
		Moon 
		Features 
		{
			Extreme_Temps 
			Barren 
			Rocky 
			Building:Possible_Outpost 
		}
	}
}
