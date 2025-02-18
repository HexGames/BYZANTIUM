// 2025-02-17T18:10:58
Buildings 
{
	Building Rural_District
	{
		Type Private_District
		DistrictIdx 0
		NationalizeTo Farm_District
		Color a2bf00
		Cost 
		{
			BC 5000
		}
		Benefit 
		{
			Extra 
			{
				Tax_1_PerPop 0
				Tax_2_PerPop 10
				Tax_3_PerPop 20
			}
			BC 
			{
				PerPop 20
			}
			Growth 
			{
				PerPop 20
			}
		}
		Control Private
	}
	Building Urban_District
	{
		Type Private_District
		DistrictIdx 1
		NationalizeTo Bank_District
		NationalizeTo Culture_District
		Color f0d67c
		Icon Dis_Urban
		Cost 
		{
			BC 10000
		}
		Benefit 
		{
			Extra 
			{
				Tax_1_PerPop 0
				Tax_2_PerPop 10
				Tax_3_PerPop 20
			}
			BC 
			{
				PerPop 30
				SystemPerPopMultiplier 100
			}
			Influence 
			{
				PerPop 10
				SystemPerPopMultiplier 100
			}
		}
		Control Private
	}
	Building Industrial_District
	{
		Type Private_District
		DistrictIdx 2
		NationalizeTo Tech_District
		NationalizeTo Shipyard_District
		Color bf7e00
		Icon Dis_Industrial
		Cost 
		{
			BC 10000
		}
		Benefit 
		{
			Extra 
			{
				Tax_1_PerPop 0
				Tax_2_PerPop 10
				Tax_3_PerPop 20
			}
			BC 
			{
				PerPop 20
				SystemPerPopMultiplier 100
			}
			Research 
			{
				PerPop 10
				SystemPerPopMultiplier 100
			}
			Shipbuilding 
			{
				PerPop 10
				SystemPerPopMultiplier 100
			}
		}
		Control Private
	}
	Building Farm_District
	{
		Type State_District
		DistrictIdx 3
		PrivatizeTo Rural_District
		Color c_00a50c
		Cost 
		{
			BC 5000
		}
		Benefit 
		{
			Growth 
			{
				PerPop 40
			}
		}
		Control State
	}
	Building Bank_District
	{
		Type State_District
		DistrictIdx 4
		PrivatizeTo Urban_District
		Color c_fbe52d
		Icon Dis_Business
		Cost 
		{
			BC 10000
		}
		Benefit 
		{
			BC 
			{
				PerPop 40
				SystemPerPopMultiplier 200
			}
		}
		Control State
	}
	Building Culture_District
	{
		Type State_District
		DistrictIdx 5
		PrivatizeTo Urban_District
		Color c_ff0084
		Icon Dis_Cultural
		Cost 
		{
			BC 10000
		}
		Benefit 
		{
			Influence 
			{
				PerPop 40
				SystemPerPopMultiplier 200
			}
		}
		Control State
	}
	Building Tech_District
	{
		Type State_District
		DistrictIdx 6
		PrivatizeTo Industrial_District
		Color c_00c6b6
		Icon Dis_Technological
		Cost 
		{
			BC 10000
		}
		Benefit 
		{
			Research 
			{
				PerPop 40
				SystemPerPopMultiplier 200
			}
		}
		Control State
	}
	Building Shipyard_District
	{
		Type State_District
		DistrictIdx 7
		PrivatizeTo Industrial_District
		Color c_e90000
		Icon Dis_Manufacturing
		Cost 
		{
			BC 10000
		}
		Benefit 
		{
			Shipbuilding 
			{
				PerPop 40
				SystemPerPopMultiplier 200
			}
		}
		Control State
	}
	Building Orbital_Forum
	{
		Type Space_Station
		PrivatizeTo Private_Orbital_Forum
		Color c_ff0084
		Cost 
		{
			BC 15000
		}
		Benefit 
		{
			Influence 
			{
				PerLevel 20
				SystemPerPop 10
			}
		}
		Control State
	}
	Building Research_Station
	{
		Type Space_Station
		PrivatizeTo Private_Research_Station
		Color c_00c6b6
		Cost 
		{
			BC 15000
		}
		Benefit 
		{
			Research 
			{
				PerLevel 20
				SystemPerPop 10
			}
		}
		Control State
	}
	Building Orbital_Shipyard
	{
		Type Space_Station
		PrivatizeTo Private_Orbital_Shipyard
		Color c_e90000
		Cost 
		{
			BC 15000
		}
		Benefit 
		{
			Shipbuilding 
			{
				PerLevel 20
				SystemPerPop 10
			}
		}
		Control State
	}
	Building Asteroid_Market
	{
		Type Asteroid_Base
		PrivatizeTo Private_Asteroid_Market
		Color c_fbe52d
		Cost 
		{
			BC 10000
		}
		Benefit 
		{
			BC 
			{
				PerLevel 20
				SystemPerPop 10
			}
		}
		Control State
	}
	Building Asteroid_Labs
	{
		Type Asteroid_Base
		PrivatizeTo Private_Asteroid_Labs
		Color c_00c6b6
		Cost 
		{
			BC 10000
		}
		Benefit 
		{
			Research 
			{
				PerLevel 20
				SystemPerPop 10
			}
		}
		Control State
	}
	Building Asteroid_Shipyard
	{
		Type Asteroid_Base
		PrivatizeTo Private_Asteroid_Shipyard
		Color c_e90000
		Cost 
		{
			BC 10000
		}
		Benefit 
		{
			Shipbuilding 
			{
				PerLevel 20
				SystemPerPop 10
			}
		}
		Control State
	}
	Building Trade_Outpost
	{
		Type Outpost
		PrivatizeTo Private_Trade_Outpost
		Color fc_be52d
		Cost 
		{
			BC 5000
		}
		Benefit 
		{
			BC 
			{
				PerLevel 20
				SystemPerPop 10
			}
		}
		Control State
	}
	Building Turism_Outpost
	{
		Type Outpost
		PrivatizeTo Private_Turism_Outpost
		Color c_ff0084
		Cost 
		{
			BC 5000
		}
		Benefit 
		{
			Influence 
			{
				PerLevel 20
				SystemPerPop 10
			}
		}
		Control State
	}
	Building Farming_Outpost
	{
		Type Outpost
		PrivatizeTo Private_Farming_Outpost
		Color c_00a50c
		Cost 
		{
			BC 5000
		}
		Benefit 
		{
			Growth 
			{
				Base 20
				PerLevel 20
			}
		}
		Control State
	}
	Building Private_Orbital_Forum
	{
		Type Space_Station
		NationalizeTo Orbital_Forum
		Color c_ff0084
		Cost 
		{
			BC 15000
		}
		Benefit 
		{
			Extra 
			{
				Tax_1_PerLevel 0
				Tax_2_PerLevel 5
				Tax_3_PerLevel 10
			}
			BC 
			{
				PerLevel 10
			}
			Influence 
			{
				PerLevel 10
				SystemPerPop 10
			}
		}
		Control Private
	}
	Building Private_Research_Station
	{
		Type Space_Station
		NationalizeTo Research_Station
		Color c_00c6b6
		Cost 
		{
			BC 15000
		}
		Benefit 
		{
			Extra 
			{
				Tax_1_PerLevel 0
				Tax_2_PerLevel 5
				Tax_3_PerLevel 10
			}
			BC 
			{
				PerLevel 10
			}
			Research 
			{
				PerLevel 10
				SystemPerPop 10
			}
		}
		Control Private
	}
	Building Private_Orbital_Shipyard
	{
		Type Space_Station
		NationalizeTo Orbital_Shipyard
		Color c_e90000
		Cost 
		{
			BC 15000
		}
		Benefit 
		{
			Extra 
			{
				Tax_1_PerLevel 0
				Tax_2_PerLevel 5
				Tax_3_PerLevel 10
			}
			BC 
			{
				PerLevel 10
			}
			Shipbuilding 
			{
				PerLevel 10
				SystemPerPop 10
			}
		}
		Control Private
	}
	Building Private_Asteroid_Market
	{
		Type Asteroid_Base
		NationalizeTo Asteroid_Market
		Color c_fbe52d
		Cost 
		{
			BC 10000
		}
		Benefit 
		{
			Extra 
			{
				Tax_1_PerLevel 0
				Tax_2_PerLevel 5
				Tax_3_PerLevel 10
			}
			BC 
			{
				PerLevel 20
				SystemPerPop 10
			}
		}
		Control Private
	}
	Building Private_Asteroid_Labs
	{
		Type Asteroid_Base
		NationalizeTo Asteroid_Labs
		Color c_00c6b6
		Cost 
		{
			BC 10000
		}
		Benefit 
		{
			Extra 
			{
				Tax_1_PerLevel 0
				Tax_2_PerLevel 5
				Tax_3_PerLevel 10
			}
			BC 
			{
				PerLevel 10
			}
			Research 
			{
				PerLevel 10
				SystemPerPop 10
			}
		}
		Control Private
	}
	Building Private_Asteroid_Shipyard
	{
		Type Asteroid_Base
		NationalizeTo Asteroid_Shipyard
		Color c_e90000
		Cost 
		{
			BC 10000
		}
		Benefit 
		{
			Extra 
			{
				Tax_1_PerLevel 0
				Tax_2_PerLevel 5
				Tax_3_PerLevel 10
			}
			BC 
			{
				PerLevel 10
			}
			Shipbuilding 
			{
				PerLevel 10
				SystemPerPop 10
			}
		}
		Control Private
	}
	Building Private_Trade_Outpost
	{
		Type Outpost
		NationalizeTo Trade_Outpost
		Color fc_be52d
		Cost 
		{
			BC 5000
		}
		Benefit 
		{
			Extra 
			{
				Tax_1_PerLevel 0
				Tax_2_PerLevel 5
				Tax_3_PerLevel 10
			}
			BC 
			{
				PerLevel 20
				SystemPerPop 10
			}
		}
		Control Private
	}
	Building Private_Turism_Outpost
	{
		Type Outpost
		NationalizeTo Turism_Outpost
		Color c_ff0084
		Cost 
		{
			BC 5000
		}
		Benefit 
		{
			Extra 
			{
				Tax_1_PerLevel 0
				Tax_2_PerLevel 5
				Tax_3_PerLevel 10
			}
			BC 
			{
				PerLevel 10
			}
			Influence 
			{
				PerLevel 10
				SystemPerPop 10
			}
		}
		Control Private
	}
	Building Private_Farming_Outpost
	{
		Type Outpost
		NationalizeTo Farming_Outpost
		Color c_00a50c
		Cost 
		{
			BC 5000
		}
		Benefit 
		{
			Extra 
			{
				Tax_1_PerLevel 0
				Tax_2_PerLevel 5
				Tax_3_PerLevel 10
			}
			BC 
			{
				PerLevel 10
			}
			Growth 
			{
				Base 10
				PerLevel 10
			}
		}
		Control Private
	}
}
