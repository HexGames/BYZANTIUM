// 2025-02-04T17:53:17
Buildings 
{
	Building Agriculture_District
	{
		Type Rural_District
		Color c_00a50c
		Cost 
		{
			BC 2500
		}
		Benefit 
		{
			Extra 
			{
				MaxPop 2
			}
			Growth 
			{
				PerPop 40
			}
		}
		Control State_or_Private
	}
	Building Urban_District
	{
		Type Urban_District
		ChangeTo Business_District
		ChangeTo Media_District
		Color c_9090ff
		Cost 
		{
			BC 5000
		}
		Benefit 
		{
			BC 
			{
				PerPop 20
				SystemPerPopMultiplier 100
			}
			Influence 
			{
				PerPop 20
				SystemPerPopMultiplier 100
			}
		}
		Control State_or_Private
	}
	Building Industrial_District
	{
		Type Industrial_District
		ChangeTo Shipyard_District
		ChangeTo Research_District
		Color c_ff8c00
		Cost 
		{
			BC 5000
		}
		Benefit 
		{
			Research 
			{
				PerPop 20
				SystemPerPopMultiplier 100
			}
			Shipbuilding 
			{
				PerPop 20
				SystemPerPopMultiplier 100
			}
		}
		Control State_or_Private
	}
	Building Business_District
	{
		Type Urban_District
		ChangeTo Media_District
		ChangeTo Urban_District
		Color c_fbe52d
		Cost 
		{
			BC 10000
		}
		Benefit 
		{
			BC 
			{
				PerPop 30
				SystemPerPopMultiplier 200
			}
			Influence 
			{
				PerPop 10
			}
		}
		Control State_or_Private
	}
	Building Media_District
	{
		Type Urban_District
		ChangeTo Business_District
		ChangeTo Urban_District
		Color c_ff0084
		Cost 
		{
			BC 10000
		}
		Benefit 
		{
			BC 
			{
				PerPop 10
			}
			Influence 
			{
				PerPop 30
				SystemPerPopMultiplier 200
			}
		}
		Control State_or_Private
	}
	Building Research_District
	{
		Type Industrial_District
		ChangeTo Shipyard_District
		ChangeTo Industrial_District
		Color c_00c6b6
		Cost 
		{
			BC 10000
		}
		Benefit 
		{
			Research 
			{
				PerPop 30
				SystemPerPopMultiplier 200
			}
			Shipbuilding 
			{
				PerPop 10
			}
		}
		Control State_or_Private
	}
	Building Shipyard_District
	{
		Type Industrial_District
		ChangeTo Research_District
		ChangeTo Industrial_District
		Color c_e90000
		Cost 
		{
			BC 10000
		}
		Benefit 
		{
			Research 
			{
				PerPop 10
			}
			Shipbuilding 
			{
				PerPop 30
				SystemPerPopMultiplier 200
			}
		}
		Control State_or_Private
	}
	Building Orbital_Forum
	{
		Type Station
		ChangeTo Research_Station
		ChangeTo Orbital_Shipyard
		Color c_ff0084
		Cost 
		{
			BC 15000
		}
		Benefit 
		{
			Influence 
			{
				Base 20
				SystemPerPop 10
			}
		}
		Control State_or_Private
	}
	Building Research_Station
	{
		Type Station
		ChangeTo Orbital_Forum
		ChangeTo Orbital_Shipyard
		Color c_00c6b6
		Cost 
		{
			BC 15000
		}
		Benefit 
		{
			Research 
			{
				Base 20
				SystemPerPop 10
			}
		}
		Control State_or_Private
	}
	Building Orbital_Shipyard
	{
		Type Station
		ChangeTo Orbital_Forum
		ChangeTo Research_Station
		Color c_e90000
		Cost 
		{
			BC 15000
		}
		Benefit 
		{
			Shipbuilding 
			{
				Base 20
				SystemPerPop 10
			}
		}
		Control State_or_Private
	}
	Building Asteroid_Market
	{
		Type Base
		ChangeTo Asteroid_Labs
		ChangeTo Asteroid_Shipyard
		Color c_fbe52d
		Cost 
		{
			BC 10000
		}
		Benefit 
		{
			BC 
			{
				Base 20
				SystemPerPop 10
			}
		}
		Control State_or_Private
	}
	Building Asteroid_Labs
	{
		Type Base
		ChangeTo Asteroid_Market
		ChangeTo Asteroid_Shipyard
		Color c_00c6b6
		Cost 
		{
			BC 10000
		}
		Benefit 
		{
			Research 
			{
				Base 20
				SystemPerPop 10
			}
		}
		Control State_or_Private
	}
	Building Asteroid_Shipyard
	{
		Type Base
		ChangeTo Asteroid_Market
		ChangeTo Asteroid_Labs
		Color c_e90000
		Cost 
		{
			BC 10000
		}
		Benefit 
		{
			Shipbuilding 
			{
				Base 20
				SystemPerPop 10
			}
		}
		Control State_or_Private
	}
	Building Trade_Outpost
	{
		Type Outpost
		ChangeTo Turism_Outpost
		ChangeTo Farming_Outpost
		Color fc_be52d
		Cost 
		{
			BC 5000
		}
		Benefit 
		{
			BC 
			{
				Base 20
				SystemPerPop 10
			}
		}
		Control State_or_Private
	}
	Building Turism_Outpost
	{
		Type Outpost
		ChangeTo Trade_Outpost
		ChangeTo Farming_Outpost
		Color c_ff0084
		Cost 
		{
			BC 5000
		}
		Benefit 
		{
			Influence 
			{
				Base 20
				SystemPerPop 10
			}
		}
		Control State_or_Private
	}
	Building Farming_Outpost
	{
		Type Outpost
		ChangeTo Trade_Outpost
		ChangeTo Turism_Outpost
		Color c_00a50c
		Cost 
		{
			BC 5000
		}
		Benefit 
		{
			Growth 
			{
				Base 40
			}
		}
		Control State_or_Private
	}
}
