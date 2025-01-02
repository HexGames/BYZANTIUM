Buildings 
{
	Building Capital_I
	{
		Level 1
		Type Capital
		Icon Capital_I
		Cost 
		{
			BC 500
		}
		Benefit 
		{
			Resource Control
			Income 30
			Wealth 30
			ExtraBC 50
		}
		Control 
		{
			Type State_Owned
		}
	}
	Building Capital_II
	{
		Level 2
		UpgradeOf Capital_I
		Type Capital
		Icon Capital_II
		Cost 
		{
			BC 1000
		}
		Benefit 
		{
			Resource Control
			Income 50
			Wealth 50
			ExtraBC 50
		}
		Control 
		{
			Type State_Owned
		}
	}
	Building Capital_III
	{
		Level 3
		UpgradeOf Capital_II
		Type Capital
		Icon Capital_III
		Cost 
		{
			BC 2000
		}
		Benefit 
		{
			Resource Control
			Income 70
			Wealth 70
			ExtraBC 50
		}
		Control 
		{
			Type State_Owned
		}
	}
	Building System_HQ_I
	{
		Level 1
		Type SystemHQ
		Icon System_HQ_I
		Cost 
		{
			BC 500
		}
		Benefit 
		{
			Resource Control
			Income 20
			Wealth 30
		}
		Control 
		{
			Type State_Owned
		}
	}
	Building System_HQ_II
	{
		Level 2
		UpgradeOf System_HQ_I
		Type SystemHQ
		Icon System_HQ_II
		Cost 
		{
			BC 1000
		}
		Benefit 
		{
			Resource Control
			Income 30
			Wealth 50
		}
		Control 
		{
			Type State_Owned
		}
	}
	Building System_HQ_III
	{
		Level 3
		UpgradeOf System_HQ_II
		Type SystemHQ
		Icon System_HQ_III
		Cost 
		{
			BC 2000
		}
		Benefit 
		{
			Resource Control
			Income 40
			Wealth 70
		}
		Control 
		{
			Type State_Owned
		}
	}
	Building Private_Business_I
	{
		Level 1
		Type District
		Icon Private_Business_I
		Cost 
		{
			BC 500
		}
		Benefit 
		{
			Resource BC
			Income 40
			Wealth 30
			Invest_1 
			{
				CostBC 30
				ExtraLocalBonus 50
			}
			Invest_2 
			{
				CostBC 60
				ExtraLocalBonus 75
			}
			Invest_3 
			{
				CostBC 90
				ExtraLocalBonus 100
			}
			Construction 1
		}
		Control 
		{
			Type Private
			NationalizeTo State_Business_I
		}
	}
	Building Private_Business_II
	{
		Level 2
		UpgradeOf Private_Business_I
		Type District
		Icon Private_Business_II
		Cost 
		{
			BC 1000
		}
		Benefit 
		{
			Resource BC
			Income 80
			Wealth 50
			Invest_1 
			{
				CostBC 60
				ExtraLocalBonus 50
			}
			Invest_2 
			{
				CostBC 120
				ExtraLocalBonus 75
			}
			Invest_3 
			{
				CostBC 180
				ExtraLocalBonus 100
			}
			Construction 1
		}
		Control 
		{
			Type Private
			NationalizeTo State_Business_II
		}
	}
	Building Private_Business_III
	{
		Level 3
		UpgradeOf Private_Business_II
		Type District
		Icon Private_Business_III
		Cost 
		{
			BC 2000
		}
		Benefit 
		{
			Resource BC
			Income 120
			Wealth 70
			Invest_1 
			{
				CostBC 90
				ExtraLocalBonus 50
			}
			Invest_2 
			{
				CostBC 180
				ExtraLocalBonus 75
			}
			Invest_3 
			{
				CostBC 360
				ExtraLocalBonus 100
			}
			Construction 1
		}
		Control 
		{
			Type Private
			NationalizeTo State_Business_III
		}
	}
	Building State_Business_I
	{
		Level 1
		UpgradeOf Private_Business_I
		Type District
		Icon State_Business_I
		Cost 
		{
			BC 500
		}
		Benefit 
		{
			Resource BC
			Income 20
			Wealth 30
			Construction 1
		}
		Control 
		{
			Type State_Owned
			PrivatizeTo Private_Business_I
		}
	}
	Building State_Business_II
	{
		Level 2
		UpgradeOf State_Business_I
		Type District
		Icon State_Business_II
		Cost 
		{
			BC 1000
		}
		Benefit 
		{
			Resource BC
			Income 50
			Wealth 50
			Construction 1
		}
		Control 
		{
			Type State_Owned
			PrivatizeTo Private_Business_II
		}
	}
	Building State_Business_III
	{
		Level 3
		UpgradeOf State_Business_II
		Type District
		Icon State_Business_III
		Cost 
		{
			BC 2000
		}
		Benefit 
		{
			Resource BC
			Income 80
			Wealth 70
			Construction 1
		}
		Control 
		{
			Type State_Owned
			PrivatizeTo Private_Business_III
		}
	}
	Building Private_Media_I
	{
		Level 1
		Type District
		Icon Private_Media_I
		Cost 
		{
			BC 500
		}
		Benefit 
		{
			Resource Influence
			Income 40
			Wealth 30
			Invest_1 
			{
				CostBC 30
				ExtraLocalBonus 50
			}
			Invest_2 
			{
				CostBC 60
				ExtraLocalBonus 75
			}
			Invest_3 
			{
				CostBC 90
				ExtraLocalBonus 100
			}
		}
		Control 
		{
			Type Private
			NationalizeTo State_Media_I
		}
	}
	Building Private_Media_II
	{
		Level 2
		UpgradeOf Private_Media_I
		Type District
		Icon Private_Media_II
		Cost 
		{
			BC 1000
		}
		Benefit 
		{
			Resource Influence
			Income 80
			Wealth 50
			Invest_1 
			{
				CostBC 60
				ExtraLocalBonus 50
			}
			Invest_2 
			{
				CostBC 120
				ExtraLocalBonus 75
			}
			Invest_3 
			{
				CostBC 180
				ExtraLocalBonus 100
			}
		}
		Control 
		{
			Type Private
			NationalizeTo State_Media_II
		}
	}
	Building Private_Media_III
	{
		Level 3
		UpgradeOf Private_Media_II
		Type District
		Icon Private_Media_III
		Cost 
		{
			BC 2000
		}
		Benefit 
		{
			Resource Influence
			Income 120
			Wealth 70
			Invest_1 
			{
				CostBC 90
				ExtraLocalBonus 50
			}
			Invest_2 
			{
				CostBC 180
				ExtraLocalBonus 75
			}
			Invest_3 
			{
				CostBC 360
				ExtraLocalBonus 100
			}
		}
		Control 
		{
			Type Private
			NationalizeTo State_Media_III
		}
	}
	Building State_Media_I
	{
		Level 1
		Type District
		Icon State_Media_I
		Cost 
		{
			BC 500
		}
		Benefit 
		{
			Resource Influence
			Income 20
			Wealth 30
		}
		Control 
		{
			Type State_Owned
			PrivatizeTo Private_Media_I
		}
	}
	Building State_Media_II
	{
		Level 2
		UpgradeOf State_Media_I
		Type District
		Icon State_Media_II
		Cost 
		{
			BC 1000
		}
		Benefit 
		{
			Resource Influence
			Income 50
			Wealth 50
		}
		Control 
		{
			Type State_Owned
			PrivatizeTo Private_Media_II
		}
	}
	Building State_Media_III
	{
		Level 3
		UpgradeOf State_Media_II
		Type District
		Icon State_Media_III
		Cost 
		{
			BC 2000
		}
		Benefit 
		{
			Resource Influence
			Income 80
			Wealth 70
		}
		Control 
		{
			Type State_Owned
			PrivatizeTo Private_Media_III
		}
	}
	Building Private_Shipyard_I
	{
		Level 1
		Type District
		Icon Private_Shipyard_I
		Cost 
		{
			BC 500
		}
		Benefit 
		{
			Resource Shipbuilding
			Income 40
			Wealth 30
			Invest_1 
			{
				CostBC 30
				ExtraLocalBonus 50
			}
			Invest_2 
			{
				CostBC 60
				ExtraLocalBonus 75
			}
			Invest_3 
			{
				CostBC 90
				ExtraLocalBonus 100
			}
		}
		Control 
		{
			Type Private
			NationalizeTo State_Shipyard_I
		}
	}
	Building Private_Shipyard_II
	{
		Level 2
		UpgradeOf Private_Shipyard_I
		Type District
		Icon Private_Shipyard_II
		Cost 
		{
			BC 1000
		}
		Benefit 
		{
			Resource Shipbuilding
			Income 80
			Wealth 50
			Invest_1 
			{
				CostBC 60
				ExtraLocalBonus 50
			}
			Invest_2 
			{
				CostBC 120
				ExtraLocalBonus 75
			}
			Invest_3 
			{
				CostBC 180
				ExtraLocalBonus 100
			}
		}
		Control 
		{
			Type Private
			NationalizeTo State_Shipyard_II
		}
	}
	Building Private_Shipyard_III
	{
		Level 3
		UpgradeOf Private_Shipyard_II
		Type District
		Icon Private_Shipyard_III
		Cost 
		{
			BC 2000
		}
		Benefit 
		{
			Resource Shipbuilding
			Income 120
			Wealth 70
			Invest_1 
			{
				CostBC 90
				ExtraLocalBonus 50
			}
			Invest_2 
			{
				CostBC 180
				ExtraLocalBonus 75
			}
			Invest_3 
			{
				CostBC 360
				ExtraLocalBonus 100
			}
		}
		Control 
		{
			Type Private
			NationalizeTo State_Shipyard_III
		}
	}
	Building State_Shipyard_I
	{
		Level 1
		Type District
		Icon State_Shipyard_I
		Cost 
		{
			BC 500
		}
		Benefit 
		{
			Resource Shipbuilding
			Income 20
			Wealth 30
		}
		Control 
		{
			Type State_Owned
			PrivatizeTo Private_Shipyard_I
		}
	}
	Building State_Shipyard_II
	{
		Level 2
		UpgradeOf State_Shipyard_I
		Type District
		Icon State_Shipyard_II
		Cost 
		{
			BC 1000
		}
		Benefit 
		{
			Resource Shipbuilding
			Income 50
			Wealth 50
		}
		Control 
		{
			Type State_Owned
			PrivatizeTo Private_Shipyard_II
		}
	}
	Building State_Shipyard_III
	{
		Level 3
		UpgradeOf State_Shipyard_II
		Type District
		Icon State_Shipyard_III
		Cost 
		{
			BC 2000
		}
		Benefit 
		{
			Resource Shipbuilding
			Income 80
			Wealth 70
		}
		Control 
		{
			Type State_Owned
			PrivatizeTo Private_Shipyard_III
		}
	}
	Building Private_Research_I
	{
		Level 1
		Type District
		Icon Private_Research_I
		Cost 
		{
			BC 500
		}
		Benefit 
		{
			Resource Research
			Income 40
			Wealth 30
			Invest_1 
			{
				CostBC 30
				ExtraLocalBonus 50
			}
			Invest_2 
			{
				CostBC 60
				ExtraLocalBonus 75
			}
			Invest_3 
			{
				CostBC 90
				ExtraLocalBonus 100
			}
		}
		Control 
		{
			Type Private
			NationalizeTo State_Research_I
		}
	}
	Building Private_Research_II
	{
		Level 2
		UpgradeOf Private_Research_I
		Type District
		Icon Private_Research_II
		Cost 
		{
			BC 1000
		}
		Benefit 
		{
			Resource Research
			Income 80
			Wealth 50
			Invest_1 
			{
				CostBC 60
				ExtraLocalBonus 50
			}
			Invest_2 
			{
				CostBC 120
				ExtraLocalBonus 75
			}
			Invest_3 
			{
				CostBC 180
				ExtraLocalBonus 100
			}
		}
		Control 
		{
			Type Private
			NationalizeTo State_Research_II
		}
	}
	Building Private_Research_III
	{
		Level 3
		UpgradeOf Private_Research_II
		Type District
		Icon Private_Research_III
		Cost 
		{
			BC 2000
		}
		Benefit 
		{
			Resource Research
			Income 120
			Wealth 70
			Invest_1 
			{
				CostBC 90
				ExtraLocalBonus 50
			}
			Invest_2 
			{
				CostBC 180
				ExtraLocalBonus 75
			}
			Invest_3 
			{
				CostBC 360
				ExtraLocalBonus 100
			}
		}
		Control 
		{
			Type Private
			NationalizeTo State_Research_III
		}
	}
	Building State_Research_I
	{
		Level 1
		Type District
		Icon State_Research_I
		Cost 
		{
			BC 500
		}
		Benefit 
		{
			Resource Research
			Income 20
			Wealth 30
		}
		Control 
		{
			Type State_Owned
			PrivatizeTo Private_Research_I
		}
	}
	Building State_Research_II
	{
		Level 2
		UpgradeOf State_Research_I
		Type District
		Icon State_Research_II
		Cost 
		{
			BC 1000
		}
		Benefit 
		{
			Resource Research
			Income 50
			Wealth 50
		}
		Control 
		{
			Type State_Owned
			PrivatizeTo Private_Research_II
		}
	}
	Building State_Research_III
	{
		Level 3
		UpgradeOf State_Research_II
		Type District
		Icon State_Research_III
		Cost 
		{
			BC 2000
		}
		Benefit 
		{
			Resource Research
			Income 80
			Wealth 70
		}
		Control 
		{
			Type State_Owned
			PrivatizeTo Private_Research_III
		}
	}
	Building Police_I
	{
		Level 1
		Type District
		Icon Police_I
		Cost 
		{
			BC 500
		}
		Benefit 
		{
			Resource Control
			Income 20
			Wealth 30
		}
		Control 
		{
			Type State_Owned
		}
	}
	Building Police_II
	{
		Level 2
		UpgradeOf Police_I
		Type District
		Icon Police_II
		Cost 
		{
			BC 1000
		}
		Benefit 
		{
			Resource Control
			Income 30
			Wealth 50
		}
		Control 
		{
			Type State_Owned
		}
	}
	Building Police_III
	{
		Level 3
		UpgradeOf Police_II
		Type District
		Icon Police_III
		Cost 
		{
			BC 2000
		}
		Benefit 
		{
			Resource Control
			Income 40
			Wealth 70
		}
		Control 
		{
			Type State_Owned
		}
	}
	Building Research_Station_I
	{
		Level 1
		Type Space_Station
		Icon ResearchS
		Cost 
		{
			BC 500
		}
		Benefit 
		{
			Resource Research
			Income 30
			Bonus 10
		}
		Control 
		{
			Type State_Owned
			PrivatizeTo Private_Research_Nexus_I
		}
	}
	Building Research_Station_II
	{
		Level 2
		UpgradeOf Research_Station_I
		Type Space_Station
		Icon ResearchS
		Cost 
		{
			BC 1000
		}
		Benefit 
		{
			Resource Research
			Income 50
			Bonus 20
		}
		Control 
		{
			Type State_Owned
			PrivatizeTo Private_Research_Nexus_II
		}
	}
	Building Research_Station_III
	{
		Level 3
		UpgradeOf Research_Station_II
		Type Space_Station
		Icon ResearchS
		Cost 
		{
			BC 2000
		}
		Benefit 
		{
			Resource Research
			Income 70
			Bonus 30
		}
		Control 
		{
			Type State_Owned
			PrivatizeTo Private_Research_Nexus_III
		}
	}
	Building Private_Research_Station_I
	{
		Level 1
		Type Space_Station
		Icon ResearchP
		Cost 
		{
			BC 500
		}
		Benefit 
		{
			Resource Research
			Bonus 10
			Invest_1 
			{
				CostBC 50
				ExtraLocalBonus 100
			}
		}
		Control 
		{
			Type Private
			NationalizeTo Research_Station_I
		}
	}
	Building Private_Research_Station_II
	{
		Level 2
		UpgradeOf Private_Research_Station_I
		Type Space_Station
		Icon ResearchP
		Cost 
		{
			BC 1000
		}
		Benefit 
		{
			Resource Research
			Bonus 20
			Invest_1 
			{
				CostBC 100
				ExtraLocalBonus 100
			}
		}
		Control 
		{
			Type Private
			NationalizeTo Research_Station_II
		}
	}
	Building Private_Research_Station_III
	{
		Level 3
		UpgradeOf Private_Research_Station_II
		Type Space_Station
		Icon ResearchP
		Cost 
		{
			BC 2000
		}
		Benefit 
		{
			Resource Research
			Bonus 30
			Invest_1 
			{
				CostBC 150
				ExtraLocalBonus 100
			}
		}
		Control 
		{
			Type Private
			NationalizeTo Research_Station_III
		}
	}
	Building Orbital_Forum_I
	{
		Level 1
		Type Space_Station
		Icon MediaS
		Cost 
		{
			BC 500
		}
		Benefit 
		{
			Resource Influence
			Income 30
			Bonus 10
		}
		Control 
		{
			Type State_Owned
		}
	}
	Building Orbital_Forum_II
	{
		Level 2
		UpgradeOf Orbital_Forum_I
		Type Space_Station
		Icon MediaS
		Cost 
		{
			BC 1000
		}
		Benefit 
		{
			Resource Influence
			Income 50
			Bonus 20
		}
		Control 
		{
			Type State_Owned
		}
	}
	Building Orbital_Forum_III
	{
		Level 3
		UpgradeOf Orbital_Forum_II
		Type Space_Station
		Icon MediaS
		Cost 
		{
			BC 2000
		}
		Benefit 
		{
			Resource Influence
			Income 70
			Bonus 30
		}
		Control 
		{
			Type State_Owned
		}
	}
	Building Trade_Station_I
	{
		Level 1
		Type Space_Station
		Icon BusinessS
		Cost 
		{
			BC 500
		}
		Benefit 
		{
			Resource BC
			Bonus 10
			Invest_1 
			{
				CostBC 50
				ExtraLocalBonus 100
			}
		}
		Control 
		{
			Type Private
		}
	}
	Building Trade_Station_II
	{
		Level 2
		UpgradeOf Trade_Station_I
		Type Space_Station
		Icon BusinessS
		Cost 
		{
			BC 1000
		}
		Benefit 
		{
			Resource BC
			Bonus 20
			Invest_1 
			{
				CostBC 100
				ExtraLocalBonus 100
			}
		}
		Control 
		{
			Type Private
		}
	}
	Building Trade_Station_III
	{
		Level 3
		UpgradeOf Trade_Station_II
		Type Space_Station
		Icon BusinessS
		Cost 
		{
			BC 2000
		}
		Benefit 
		{
			Resource BC
			Bonus 30
			Invest_1 
			{
				CostBC 150
				ExtraLocalBonus 100
			}
		}
		Control 
		{
			Type Private
		}
	}
	Building Asteroid_Shipyard_I
	{
		Level 1
		Type Asteroid_Base
		Icon ShipyardS
		Cost 
		{
			BC 500
		}
		Benefit 
		{
			Resource Shipbuilding
			Income 30
			Bonus 10
		}
		Control 
		{
			Type State_Owned
			PrivatizeTo Private_Asteroid_Shipyard_I
		}
	}
	Building Asteroid_Shipyard_II
	{
		Level 2
		UpgradeOf Asteroid_Shipyard_I
		Type Asteroid_Base
		Icon ShipyardS
		Cost 
		{
			BC 1000
		}
		Benefit 
		{
			Resource Shipbuilding
			Income 50
			Bonus 20
		}
		Control 
		{
			Type State_Owned
			PrivatizeTo Private_Asteroid_Shipyard_II
		}
	}
	Building Asteroid_Shipyard_III
	{
		Level 3
		UpgradeOf Asteroid_Shipyard_II
		Type Asteroid_Base
		Icon ShipyardS
		Cost 
		{
			BC 2000
		}
		Benefit 
		{
			Resource Shipbuilding
			Income 70
			Bonus 30
		}
		Control 
		{
			Type State_Owned
			PrivatizeTo Private_Asteroid_Shipyard_III
		}
	}
	Building Private_Asteroid_Shipyard_I
	{
		Level 1
		Type Asteroid_Base
		Icon ShipyardP
		Cost 
		{
			BC 500
		}
		Benefit 
		{
			Resource Shipbuilding
			Bonus 10
			Invest_1 
			{
				CostBC 50
				ExtraLocalBonus 100
			}
		}
		Control 
		{
			Type Private
			NationalizeTo Asteroid_Shipyard_I
		}
	}
	Building Private_Asteroid_Shipyard_II
	{
		Level 2
		UpgradeOf Private_Asteroid_Shipyard_I
		Type Asteroid_Base
		Icon ShipyardP
		Cost 
		{
			BC 1000
		}
		Benefit 
		{
			Resource Shipbuilding
			Bonus 20
			Invest_1 
			{
				CostBC 100
				ExtraLocalBonus 100
			}
		}
		Control 
		{
			Type Private
			NationalizeTo Asteroid_Shipyard_II
		}
	}
	Building Private_Asteroid_Shipyard_III
	{
		Level 3
		UpgradeOf Private_Asteroid_Shipyard_II
		Type Asteroid_Base
		Icon ShipyardP
		Cost 
		{
			BC 2000
		}
		Benefit 
		{
			Resource Shipbuilding
			Bonus 30
			Invest_1 
			{
				CostBC 150
				ExtraLocalBonus 100
			}
		}
		Control 
		{
			Type Private
			NationalizeTo Asteroid_Shipyard_III
		}
	}
	Building Asteroid_Forum_I
	{
		Level 1
		Type Asteroid_Base
		Icon MediaS
		Cost 
		{
			BC 500
		}
		Benefit 
		{
			Resource Influence
			Income 30
			Bonus 10
		}
		Control 
		{
			Type State_Owned
		}
	}
	Building Asteroid_Forum_II
	{
		Level 2
		UpgradeOf Asteroid_Forum_I
		Type Asteroid_Base
		Icon MediaS
		Cost 
		{
			BC 1000
		}
		Benefit 
		{
			Resource Influence
			Income 50
			Bonus 20
		}
		Control 
		{
			Type State_Owned
		}
	}
	Building Asteroid_Forum_III
	{
		Level 3
		UpgradeOf Asteroid_Forum_II
		Type Asteroid_Base
		Icon MediaS
		Cost 
		{
			BC 2000
		}
		Benefit 
		{
			Resource Influence
			Income 70
			Bonus 30
		}
		Control 
		{
			Type State_Owned
		}
	}
	Building Asteroid_Bazzar_I
	{
		Level 1
		Type Asteroid_Base
		Icon BusinessS
		Cost 
		{
			BC 500
		}
		Benefit 
		{
			Resource BC
			Bonus 10
			Invest_1 
			{
				CostBC 50
				ExtraLocalBonus 100
			}
		}
		Control 
		{
			Type Private
		}
	}
	Building Asteroid_Bazzar_II
	{
		Level 2
		UpgradeOf Asteroid_Bazzar_I
		Type Asteroid_Base
		Icon BusinessS
		Cost 
		{
			BC 1000
		}
		Benefit 
		{
			Resource BC
			Bonus 20
			Invest_1 
			{
				CostBC 100
				ExtraLocalBonus 100
			}
		}
		Control 
		{
			Type Private
		}
	}
	Building Asteroid_Bazzar_III
	{
		Level 3
		UpgradeOf Asteroid_Bazzar_II
		Type Asteroid_Base
		Icon BusinessS
		Cost 
		{
			BC 2000
		}
		Benefit 
		{
			Resource BC
			Bonus 30
			Invest_1 
			{
				CostBC 150
				ExtraLocalBonus 100
			}
		}
		Control 
		{
			Type Private
		}
	}
	Building Ship_Parts_Factory_I
	{
		Level 1
		Type Outpost
		Icon ShipyardS
		Cost 
		{
			BC 500
		}
		Benefit 
		{
			Resource Shipbuilding
			Income 30
		}
		Control 
		{
			Type State_Owned
			PrivatizeTo Private_Ship_Parts_Factory_I
		}
	}
	Building Ship_Parts_Factory_II
	{
		Level 2
		UpgradeOf Ship_Parts_Factory_I
		Type Outpost
		Icon ShipyardS
		Cost 
		{
			BC 1000
		}
		Benefit 
		{
			Resource Shipbuilding
			Income 50
		}
		Control 
		{
			Type State_Owned
			PrivatizeTo Private_Ship_Parts_Factory_II
		}
	}
	Building Ship_Parts_Factory_III
	{
		Level 3
		UpgradeOf Ship_Parts_Factory_II
		Type Outpost
		Icon ShipyardS
		Cost 
		{
			BC 2000
		}
		Benefit 
		{
			Resource Shipbuilding
			Income 70
		}
		Control 
		{
			Type State_Owned
			PrivatizeTo Private_Ship_Parts_Factory_III
		}
	}
	Building Private_Ship_Parts_Factory_I
	{
		Level 1
		Type Outpost
		Icon ShipyardP
		Cost 
		{
			BC 500
		}
		Benefit 
		{
			Resource Shipbuilding
			Income 30
			Invest_1 
			{
				CostBC 50
				ExtraLocalBonus 100
			}
		}
		Control 
		{
			Type Private
			NationalizeTo Ship_Parts_Factory_I
		}
	}
	Building Private_Ship_Parts_Factory_II
	{
		Level 2
		UpgradeOf Private_Ship_Parts_Factory_I
		Type Outpost
		Icon ShipyardP
		Cost 
		{
			BC 1000
		}
		Benefit 
		{
			Resource Shipbuilding
			Income 60
			Invest_1 
			{
				CostBC 100
				ExtraLocalBonus 100
			}
		}
		Control 
		{
			Type Private
			NationalizeTo Ship_Parts_Factory_II
		}
	}
	Building Private_Ship_Parts_Factory_III
	{
		Level 3
		UpgradeOf Private_Ship_Parts_Factory_II
		Type Outpost
		Icon ShipyardP
		Cost 
		{
			BC 2000
		}
		Benefit 
		{
			Resource Shipbuilding
			Income 90
			Invest_1 
			{
				CostBC 150
				ExtraLocalBonus 100
			}
		}
		Control 
		{
			Type Private
			NationalizeTo Ship_Parts_Factory_III
		}
	}
	Building Research_Labs_I
	{
		Level 1
		Type Outpost
		Icon Research
		Cost 
		{
			BC 500
		}
		Benefit 
		{
			Resource Research
			Income 30
		}
		Control 
		{
			Type State_Owned
			PrivatizeTo Private_Research_Labs_I
		}
	}
	Building Research_Labs_II
	{
		Level 2
		UpgradeOf Research_Labs_I
		Type Outpost
		Icon Research
		Cost 
		{
			BC 1000
		}
		Benefit 
		{
			Resource Research
			Income 50
		}
		Control 
		{
			Type State_Owned
			PrivatizeTo Private_Research_Labs_II
		}
	}
	Building Research_Labs_III
	{
		Level 3
		UpgradeOf Research_Labs_II
		Type Outpost
		Icon Research
		Cost 
		{
			BC 2000
		}
		Benefit 
		{
			Resource Research
			Income 70
		}
		Control 
		{
			Type State_Owned
			PrivatizeTo Private_Research_Labs_III
		}
	}
	Building Private_Research_Labs_I
	{
		Level 1
		Type Outpost
		Icon Research
		Cost 
		{
			BC 500
		}
		Benefit 
		{
			Resource Research
			Income 30
			Invest_1 
			{
				CostBC 50
				ExtraLocalBonus 100
			}
		}
		Control 
		{
			Type Private
			NationalizeTo Research_Labs_I
		}
	}
	Building Private_Research_Labs_II
	{
		Level 2
		UpgradeOf Private_Research_Labs_I
		Type Outpost
		Icon Research
		Cost 
		{
			BC 1000
		}
		Benefit 
		{
			Resource Research
			Income 60
			Invest_1 
			{
				CostBC 100
				ExtraLocalBonus 100
			}
		}
		Control 
		{
			Type Private
			NationalizeTo Research_Labs_II
		}
	}
	Building Private_Research_Labs_III
	{
		Level 3
		UpgradeOf Private_Research_Labs_II
		Type Outpost
		Icon Research
		Cost 
		{
			BC 2000
		}
		Benefit 
		{
			Resource Research
			Income 90
			Invest_1 
			{
				CostBC 150
				ExtraLocalBonus 100
			}
		}
		Control 
		{
			Type Private
			NationalizeTo Research_Labs_III
		}
	}
	Building Cultural_Retreat_I
	{
		Level 1
		Type Outpost
		Icon MediaS
		Cost 
		{
			BC 500
		}
		Benefit 
		{
			Resource Influence
			Income 30
		}
		Control 
		{
			Type State_Owned
		}
	}
	Building Cultural_Retreat_II
	{
		Level 2
		UpgradeOf Cultural_Retreat_I
		Type Outpost
		Icon MediaS
		Cost 
		{
			BC 1000
		}
		Benefit 
		{
			Resource Influence
			Income 50
		}
		Control 
		{
			Type State_Owned
		}
	}
	Building Cultural_Retreat_III
	{
		Level 3
		UpgradeOf Cultural_Retreat_II
		Type Outpost
		Icon MediaS
		Cost 
		{
			BC 2000
		}
		Benefit 
		{
			Resource Influence
			Income 70
		}
		Control 
		{
			Type State_Owned
		}
	}
	Building Trade_Outpost_I
	{
		Level 1
		Type Outpost
		Icon BusinessS
		Cost 
		{
			BC 500
		}
		Benefit 
		{
			Resource BC
			Income 30
			Invest_1 
			{
				CostBC 50
				ExtraLocalBonus 100
			}
		}
		Control 
		{
			Type Private
		}
	}
	Building Trade_Outpost_II
	{
		Level 2
		UpgradeOf Trade_Outpost_I
		Type Outpost
		Icon BusinessS
		Cost 
		{
			BC 1000
		}
		Benefit 
		{
			Resource BC
			Income 60
			Invest_1 
			{
				CostBC 100
				ExtraLocalBonus 100
			}
		}
		Control 
		{
			Type Private
		}
	}
	Building Trade_Outpost_III
	{
		Level 3
		UpgradeOf Trade_Outpost_II
		Type Outpost
		Icon BusinessS
		Cost 
		{
			BC 2000
		}
		Benefit 
		{
			Resource BC
			Income 90
			Invest_1 
			{
				CostBC 150
				ExtraLocalBonus 100
			}
		}
		Control 
		{
			Type Private
		}
	}
}
