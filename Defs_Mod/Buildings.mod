Buildings 
{
	Building Research_Station
	{
		Type Space_Station
		Cost 10000
		SuggestedFocus Research
		Icon Research
		Benefit 
		{
			Research*Income 100
		}
	}
	Building Research_Nexus
	{
		Type Space_Station
		Cost 10000
		SuggestedFocus Research
		Icon ResearchPerc
		Benefit 
		{
			Research*Bonus 25
		}
	}
	Building Orbital_Forum
	{
		Type Space_Station
		Cost 10000
		SuggestedFocus Influence
		Icon Influence
		Benefit 
		{
			Influence*PerPop 10
		}
	}
	Building Trade_Station
	{
		Type Space_Station
		Cost 10000
		SuggestedFocus BC
		Icon BC
		Benefit 
		{
			BC*PerPop 30
		}
	}
	Building Asteroid_Shipyard
	{
		Type Asteroid_Base
		Cost 10000
		SuggestedFocus Shipbuilding
		Icon Shipbuilding
		Benefit 
		{
			Shipbuilding*Income 100
		}
	}
	Building Alloy_Foundry
	{
		Type Asteroid_Base
		Cost 10000
		SuggestedFocus Shipbuilding
		Icon ShipbuildingPerc
		Benefit 
		{
			Shipbuilding*Bonus 25
		}
	}
	Building Asteroid_Forum
	{
		Type Asteroid_Base
		Cost 10000
		SuggestedFocus Influence
		Icon Influence
		Benefit 
		{
			Influence*PerPop 20
		}
	}
	Building Asteroid_Bazzar
	{
		Type Asteroid_Base
		Cost 10000
		SuggestedFocus BC
		Icon BC
		Benefit 
		{
			BC*PerPop 20
		}
	}
	Building Ships_Factory
	{
		Type Outpost
		Cost 10000
		SuggestedFocus Shipbuilding
		Icon Shipbuilding
		Benefit 
		{
			Shipbuilding*Income 100
		}
	}
	Building Research_Labs
	{
		Type Outpost
		Cost 10000
		SuggestedFocus Research
		Icon Research
		Benefit 
		{
			Research*Income 100
		}
	}
	Building Trade_Outpost
	{
		Type Outpost
		Cost 10000
		SuggestedFocus BC
		Icon BC
		Benefit 
		{
			BC*PerPop 20
		}
	}
	Building Hydroponic_Farms
	{
		Type Outpost
		Cost 10000
		SuggestedFocus Growth
		Icon Growth
		Benefit 
		{
			Growth 25
		}
	}
	Building Automated_Shipyards
	{
		Type District
		Cost 10000
		SuggestedFocus Shipbuilding
		Icon Shipbuilding
		Benefit 
		{
			Shipbuilding*Income 150
		}
	}
	Building Shipbuilding_District
	{
		Type District
		Cost 10000
		SuggestedFocus Shipbuilding
		Icon ShipbuildingPerc
		Benefit 
		{
			Shipbuilding*Bonus 50
			Shipbuilding*Focus 5
		}
	}
	Building Automated_Labs
	{
		Type District
		Cost 10000
		SuggestedFocus Research
		Icon Research
		Benefit 
		{
			Research*Income 150
		}
	}
	Building Research_District
	{
		Type District
		Cost 10000
		SuggestedFocus Research
		Icon ResearchPerc
		Benefit 
		{
			Research*Bonus 50
			Research*Focus 5
		}
	}
	Building Culture_District
	{
		Type District
		Cost 10000
		SuggestedFocus Influence
		Icon Influence
		Benefit 
		{
			Influence*PerPop 20
		}
	}
	Building Trade_District
	{
		Type District
		Cost 10000
		SuggestedFocus BC
		Icon BC
		Benefit 
		{
			BC*PerPop 30
		}
	}
	Building Rural_District
	{
		Type District
		Cost 10000
		SuggestedFocus Growth
		Icon Growth
		Benefit 
		{
			Growth 100
			PopMaxPenalty 50
		}
	}
	Building Dense_Urbanization
	{
		Type District
		Cost 10000
		SuggestedFocus Pops
		Icon Pops
		Benefit 
		{
			PopMax 30000
		}
	}
	Building Capital
	{
		Type Capital
		Cost 10000
		SuggestedFocus none
		Icon Capital
		Benefit 
		{
			Shipbuilding*Income 50
			Research*Income 50
			Influence*PerPop 10
			BC*PerPop 10
			PopMax 10000
		}
	}
}
