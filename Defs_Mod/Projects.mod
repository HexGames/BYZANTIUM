Def_Projects
{
	Project Research_Station
	{
		Cost
		{
			Minerals 10000
		}
		Benefits
		{
			TechPoints:FixedBonus 5
			{
				Condition:PlanetFeature Unique
				{
					Type
					Athmosphere
				}
			}
			TechPoints:FixedBonus 5
			{
				Condition:PlanetFeature
				{
					Life
				}
			}
		}
	}
	Project Asteroid_Miners
	{
		Cost
		{
			Minerals 10000
		}
		Benefits
		{
			Energy:income -100
			Minerals:Income 300
		}
	}
	Project Deuterium_Extractor
	{
		Cost
		{
			Minerals 25000
		}
		Benefits
		{
			Resource:Deuterium
		}
	}
	// gouverment offices
	Project Gouverment_Offices_I
	{
		Requires Colony:Capital
		Cost
		{
			5000
		}
		Benefits
		{
			Authoriy 10
		}
	}
	Project Gouverment_Offices_II
	{
		Replaces Gouverment_Offices_I
		Cost
		{
			10000
		}
		Benefits
		{
			Authoriy 20
		}
	}
	Project Gouverment_Offices_III
	{
		Replaces Gouverment_Offices_II
		Cost
		{
			20000
		}
		Benefits
		{
			Authoriy 30
		}
	}
	// Diplomatic Offices
	Project Diplomatic_Offices_I
	{
		Requires Capital
		Cost
		{
			5000
		}
		Benefits
		{
			Influence 10
		}
	}
	Project Diplomatic_Offices_II
	{
		Replaces Diplomatic_Offices_I
		Cost
		{
			10000
		}
		Benefits
		{
			Influence 20
		}
	}
	Project Diplomatic_Offices_III
	{
		Replaces Diplomatic_Offices_II
		Cost
		{
			20000
		}
		Benefits
		{
			Influence 30
		}
	}

	//Project Anti-Matter_Extractor
	//Project Land_Reclamation // moved to campaigns
	//Project Hipodronic_Farms
	//Project Underground_Cities
	//Project Floating_Cities
	//Project Biodomes
	//Project Floating_Biodomes
	//Project Mining_Colony
	//Project Magma_Extractor
	//Project Cloning_Centers

	//Project Culture_Sharing_Cener // diplo
	//Project Science_Sharing_Cener // diplo
	//Project Propaganda_Cener // diplo

	//Project Automated_Factories // upg
	//Project Nano-tech_Factories // upg
	//Project Fusion_Reactors // upg
	//Project Anti-Matter_Reactors // upg

	//Project Deep_Core_Forges // great
	//Project Dyson_Sphere // great
	//Project Matter_Extractor // great
}
