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
			//Red_Dwarf
		}
	}
	Planet:Star White_Dwarf
	{
		Weight 4
		PlanetsCold
		PlanetsFew
		Features
		{
			//White_Dwarf
		}
	}
	Planet:Star Main_Sequence
	{
		Weight 4
		Features
		{
			//Main_Sequence
		}
	}
	Planet:Star Red_Giant
	{
		Weight 2
		PlanetsHot
		PlanetsFew
		Features
		{
			//Red_Giant
		}
	}
	Planet:Star Blue_Giant
	{
		Weight 2
		PlanetsHot
		PlanetsFew
		Features
		{
			//Blue_Giant
		}
	}
	Planet:Star Pulsar
	{
		Weight 1
		PlanetsHot
		PlanetsFew
		Features
		{
			//Pulsar
		}
	}
	// ----------------------------------------------- specials
	Planet Gas_Giant
	{
		SlotType Space_Station
		Features
		{
			//Gas_Giant
			Bonus High_Enegy_Particles
			//Bonus Beautiful
			Bonus Refueling_Gasses
			//Building:Space_Stations
		}
	}
	Planet Asteroids
	{
		SlotType Asteroid_Base
		Features
		{
			//Asteroids
			Bonus Rich_Minerals
			//Bonus Beautiful
			Bonus Refueling_Gasses
			//Building:Asteoid_Base
		}
	}
	// ----------------------------------------------- Uninhabitable
	Planet Frozen
	{
		Weight 4
		Temperature:Min 1
		Temperature:Max 2
		Uninhabitable
		SlotType Outpost
		Features
		{
			High_Enegy_Particles
			//Bonus Refueling_Gasses
			//Building:Possible_Outpost
		}
	}
	Planet Barren
	{
		Weight 4
		Temperature:Min 1
		Temperature:Max 5
		Uninhabitable
		SlotType Outpost
		Features
		{
			//Bonus High_Enegy_Particles
			//Bonus Refueling_Gasses
			//Bonus Rich_Soil
			//Building:Possible_Outpost
		}
	}
	Planet Toxic
	{
		Weight 4
		Temperature:Min 2
		Temperature:Max 4
		Uninhabitable
		SlotType Outpost
		Features
		{
			//Bonus Rich_Minerals
			//Bonus High_Enegy_Particles
			Refueling_Gasses
			//Building:Possible_Outpost
		}
	}
	Planet Lava
	{
		Weight 4
		Temperature:Min 5
		Temperature:Max 5
		Uninhabitable
		SlotType Outpost
	    Features
		{
			Rich_Minerals
			//Bonus High_Enegy_Particles
			//Bonus Refueling_Gasses
			//Bonus Rich_Soil
			//Building:Possible_Outpost
		}
	}

	// ----------------------------------------------- Habitable
	// Building:District_1, 2 and 3 are added according to size and terraforming
	// Small/Medium/Large and Rings are special
	Planet Desert
	{
		Weight 2
		Temperature:Min 2
		Temperature:Max 3
		Habitable
		SlotType District
		Features
		{
			//?Size
			Desert
			Bonus Tiny_Moon
			Bonus Alien_Life
			//Bonus Rich_Soil
			Bonus Beautiful
		}
	}
	Planet Arid
	{
		Weight 2
		Temperature:Min 2
		Temperature:Max 3
		Habitable
		SlotType District
		Features
		{
			//?Size
			Arid
			Bonus Tiny_Moon
			Bonus Alien_Life
			//Bonus Rich_Soil
			Bonus Beautiful
		}
	}
	Planet Continents
	{
		Weight 2
		Temperature:Min 2
		Temperature:Max 4
		Habitable
		SlotType District
		Features
		{
			//?Size
			OR
			{
				Wet:Weight 9
				Fertile:Weight 3
				Gaia:Weight 1
			}
			Bonus Tiny_Moon
			Bonus Alien_Life
			//Bonus Rich_Soil
			Bonus Beautiful
		}
	}
	Planet Ocean
	{
		Weight 2
		Temperature:Min 2
		Temperature:Max 4
		Habitable
		SlotType District
		Features
		{
			//?Size
			Fertile
			Ocean
			Bonus Tiny_Moon
			Bonus Alien_Life
			//Bonus Rich_Soil
			Bonus Beautiful
		}
	}
	Planet Swamp
	{
		Weight 2
		Temperature:Min 2
		Temperature:Max 4
		Habitable
		SlotType District
		Features
		{
			//?Size
			OR
			{
				Wet:Weight 1
				Fertile:Weight 1
			}
			//Rich_Soil
			Bonus Tiny_Moon
			Bonus Alien_Life
			Bonus Beautiful
		}
	}
	Planet Artic
	{	
		Weight 4
		Temperature:Min 2
		Temperature:Max 2
		Habitable
		SlotType District
		Features
		{
			//?Size
			OR
			{
				Arid:Weight 1
				Wet:Weight 1			
			}
			Bonus Tiny_Moon
			Bonus Alien_Life
			//Bonus Rich_Soil
			Bonus Beautiful
		}
	}
	//Planet Vulcanic
	//{
	//	Weight 1
	//	Temperature:Min 2
	//	Temperature:Max 4
	//	Habitable
	//	SlotType District
	//	Features
	//	{
			//?Size
	//		Vulcanic
	//		Arid
	//		Bonus Tiny_Moon
	//		Bonus Alien_Life
	//		//Bonus Rich_Soil
	//		Bonus Beautiful
	//	}
	//}
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
