Def_Planets
{
	// ----------------------------------------------- stars

	// Bonus: Hyper_Active +5

	Planet:Star Red_Dwarf
	{
		Weight 10
		PlanetsCold
		PlanetsFew
		Features
		{
			Red_Dwarf
			Building:Nonplanetary
		}
	}
	Planet:Star White_Dwarf
	{
		Weight 4
		PlanetsCold
		PlanetsFew
		Features
		{
			White_Dwarf
			Building:Nonplanetary
		}
	}
	Planet:Star Main_Sequence
	{
		Weight 4
		Features
		{
			Main_Sequence
			Building:Nonplanetary
		}
	}
	Planet:Star Red_Giant
	{
		Weight 2
		PlanetsHot
		PlanetsFew
		Features
		{
			Red_Giant
			Building:Nonplanetary
		}
	}
	Planet:Star Blue_Giant
	{
		Weight 2
		PlanetsHot
		PlanetsFew
		Features
		{
			Blue_Giant
			Building:Nonplanetary
		}
	}
	Planet:Star Pulsar
	{
		Weight 1
		PlanetsHot
		PlanetsFew
		Features
		{
			Pulsar
			Building:Nonplanetary
		}
	}
	// ----------------------------------------------- specials

	// Bonus: Rich_Gasses +5
	// Bonus: Rings +5
	// Bonus: Tiny_Moon +5

	Planet Gas_Giant
	{
		Features
		{
			Gas_Giant
			Bonus Rich_Gasses
			Bonus Rings
			Bonus Tiny_Moon
			Building:Nonplanetary
		}
	}

	// Bonus: Big_Asteroid +5
	// Bonus: Rich_Minerals +5

	Planet Asteroids
	{
		Features
		{
			Asteroids
			Bonus Big_Asteroid
			Bonus Rich_Minerals
			Building:Asteroids
		}
	}
	// ----------------------------------------------- Uninhabitable

	// Bonus: Rings +5
	// Bonus: Tiny_Moon +5
	// Bonus: Rich_Minerals +5

	Planet Frozen
	{
		Weight 4
		Temperature:Min 1
		Temperature:Max 1
		Features
		{
			Uninhabitable
			Bonus Rich_Minerals
			Bonus Rings
			Bonus Tiny_Moon
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
			Uninhabitable
			Bonus Rich_Minerals
			Bonus Rings
			Bonus Tiny_Moon
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
			Uninhabitable
			Bonus Rich_Minerals
			Bonus Rings
			Bonus Tiny_Moon
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
			Uninhabitable
			Bonus Rich_Minerals
			Bonus Rings
			Bonus Tiny_Moon
			Building:Possible_Outpost
		}
	}

	// ----------------------------------------------- Habitable

	// Bonus: Rings +5
	// Bonus: Tiny_Moon +5
	// Bonus: Rich_Minerals +5
	// Bonus: Life +10

	Planet Desert
	{
		Weight 2
		Temperature:Min 2
		Temperature:Max 3
		Features
		{
			Habitable
			PopMaxPerSize 10
			Bonus Rich_Minerals
			Bonus Rings
			Bonus Tiny_Moon
			Building:Possible_World
		}
	}
	Planet Arid
	{
		Weight 2
		Temperature:Min 2
		Temperature:Max 3
		Features
		{
			Habitable
			PopMaxPerSize 15
			Bonus Rich_Minerals
			Bonus Rings
			Bonus Tiny_Moon
			Building:Possible_World
		}
	}
	Planet Continents
	{
		Weight 2
		Temperature:Min 2
		Temperature:Max 4
		Features
		{
			Habitable
			PopMaxPerSize 40
			Bonus Rich_Minerals
			Bonus Rings
			Bonus Tiny_Moon
			Building:Possible_World
		}
	}
	Planet Ocean
	{
		Weight 2
		Temperature:Min 2
		Temperature:Max 4
		Features
		{
			Habitable
			PopMaxPerSize 25
			Bonus Rich_Minerals
			Bonus Rings
			Bonus Tiny_Moon
			Building:Possible_World
		}
	}
	Planet Swamp
	{
		Weight 2
		Temperature:Min 2
		Temperature:Max 4
		Features
		{
			Habitable
			PopMaxPerSize 30
			Bonus Rich_Minerals
			Bonus Rings
			Bonus Tiny_Moon
			Building:Possible_World
		}
	}
	Planet Artic
	{	
		Weight 4
		Temperature:Min 2
		Temperature:Max 2
		Features
		{
			Habitable
			PopMaxPerSize 20
			Bonus Rich_Minerals
			Bonus Rings
			Bonus Tiny_Moon
			Building:Possible_World
		}
	}
	Planet Vulcanic
	{
		Weight 1
		Temperature:Min 2
		Temperature:Max 4
		Features
		{
			Habitable
			PopMaxPerSize 10
			Bonus Rich_Minerals
			Bonus Rings
			Bonus Tiny_Moon
			Building:Possible_World
		}
	}
	// ----------------------------------------------- custom
	Custom Mercury
	{
		Type Barren
		Size 1
		Temperature 5
		Features
		{
			Barren
			OR:Perc 50
			{
				Rocky:Weight 4
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
			OR
			{
				Rocky:Weight 7
				Rich_Minerals:Weight 2
				Vulcanic:Weight 1
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
			Complex_Life
			Building:Possible_Outpost
		}
	}
	Custom Moon
	{
		Type Barren
		Size 1
		Temperature 3
		Features
		{
			Barren
			Rocky
			Building:Possible_Outpost
		}
		Moon
	}
	Custom Mars
	{
		Type Desert
		Size 1
		Temperature 2
		Features
		{
			Semi-Barren
			OR
			{
				Rocky:Weight 7
				Rich_Minerals:Weight 2
				Vulcanic:Weight 1
			}
			Tiny_Moon
			Building:Possible_Outpost
		}
	}
	Custom Asteroids
	{
		Type Asteroids
		Features
		{
			Asteroids
			Rich_Minerals
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
			Rich_Gasses
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
			OR
			{
				Rocky:Weight 7
				Rich_Minerals:Weight 2
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
			OR
			{
				Rocky:Weight 7
				Rich_Minerals:Weight 2
			}
			Building:Possible_Outpost
		}
	}
}
