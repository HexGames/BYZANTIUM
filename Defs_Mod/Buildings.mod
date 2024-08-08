Buildings 
{
	Building Star_Orbit
	{
		Upgrade Starport
		Slot Star
		Icon O
		Idx 0
	}
	Building Starport
	{
		Upgrade Gate
		Cost 
		{
			Production 1000
		}
		Slot Star
		Level I
		Icon S
		Idx 0
		Benefit 
		{
			SectorControl yes
			Authority*Used 10
		}
	}
	Building Gate
	{
		Upgrade Supergate
		Cost 
		{
			Production 5000
		}
		Slot Star
		Level II
		Icon G
		Idx 0
		Benefit 
		{
			SectorControl yes
			Authority*Used 10
		}
	}
	Building Supergate
	{
		Cost 
		{
			Production 10000
		}
		Slot Star
		Level III
		Icon SG
		Idx 0
		Benefit 
		{
			SectorControl yes
			Authority*Used 10
		}
	}
	Building System_Infrastructure
	{
		Upgrade Mineral_Coordonation_Center
		Upgrade Energy_Coordonation_Center
		Upgrade Trade_Coordonation_Center
		Slot System
		Icon SI
	}
	Building Mineral_Coordonation_Center
	{
		Cost 
		{
			Production 2500
		}
		Slot System
		Icon MCC
		Benefit 
		{
			Minerals*PerLevelSystem 10
			Trade*Used 5
		}
	}
	Building Energy_Coordonation_Center
	{
		Cost 
		{
			Production 2500
		}
		Slot System
		Icon ECC
		Benefit 
		{
			Energy*PerLevelSystem 10
			Trade*Used 5
		}
	}
	Building Trade_Coordonation_Center
	{
		Cost 
		{
			Production 2500
		}
		Slot System
		Icon TCC
		Benefit 
		{
			BC*PerLevelSystem 10
			Trade*Used 5
		}
	}
	Building Possible_Outpost
	{
		Upgrade Energy_Capture_I
		Upgrade Factory_I
		Upgrade Mineral_Processers_I
		Upgrade Shipyard_I
		Upgrade Trade_Post_I
		Upgrade Research_Lab_I
		Upgrade Cultural_Retreat_I
		Slot Outpost
		Icon O
	}
	Building Mineral_Processers_I
	{
		Upgrade Mineral_Processers_II
		Cost 
		{
			Production 800
		}
		Slot Outpost
		Level I
		Icon M
		RequiredFeature Rocky|Rings
		AddSlot World
		Benefit 
		{
			Minerals*Income 20
			Minerals*Level 1
			Trade*Used 2
		}
	}
	Building Mineral_Processers_II
	{
		Upgrade Mineral_Processers_III
		Cost 
		{
			Production 1200
		}
		Slot Outpost
		Level II
		Icon M
		Benefit 
		{
			Minerals*Income 40
			Minerals*Level 2
			Trade*Used 4
		}
	}
	Building Mineral_Processers_III
	{
		Upgrade Mineral_Processers_IV
		Cost 
		{
			Production 3000
		}
		Slot Outpost
		Level III
		Icon M
		Benefit 
		{
			Minerals*Income 80
			Minerals*Level 3
			Trade*Used 8
		}
	}
	Building Mineral_Processers_IV
	{
		Upgrade Mineral_Processers_V
		Cost 
		{
			Production 10000
		}
		Slot Outpost
		Level IV
		Icon M
		Benefit 
		{
			Minerals*Income 200
			Minerals*Level 4
			Trade*Used 20
		}
	}
	Building Mineral_Processers_V
	{
		Cost 
		{
			Production 25000
		}
		Slot Outpost
		Level V
		Icon M
		Benefit 
		{
			Minerals*Income 500
			Minerals*Level 5
			Trade*Used 50
		}
	}
	Building Research_Lab_I
	{
		Upgrade Research_Lab_II
		Cost 
		{
			Production 800
		}
		Slot Outpost
		Level I
		Icon R
		AddSlot World
		Benefit 
		{
			Research*Income 10
			Trade*Used 1
		}
	}
	Building Research_Lab_II
	{
		Upgrade Research_Lab_III
		Cost 
		{
			Production 1200
		}
		Slot Outpost
		Level II
		Icon R
		Benefit 
		{
			Research*Income 20
			Trade*Used 2
		}
	}
	Building Research_Lab_III
	{
		Upgrade Research_Lab_IV
		Cost 
		{
			Production 3000
		}
		Slot Outpost
		Level III
		Icon R
		Benefit 
		{
			Research*Income 40
			Trade*Used 4
		}
	}
	Building Research_Lab_IV
	{
		Upgrade Research_Lab_V
		Cost 
		{
			Production 10000
		}
		Slot Outpost
		Level IV
		Icon R
		Benefit 
		{
			Research*Income 100
			Trade*Used 10
		}
	}
	Building Research_Lab_V
	{
		Cost 
		{
			Production 25000
		}
		Slot Outpost
		Level V
		Icon R
		Benefit 
		{
			Research*Income 250
			Trade*Used 25
		}
	}
	Building Trade_Post_I
	{
		Upgrade Trade_Post_II
		Cost 
		{
			Production 800
		}
		Slot Outpost
		Level I
		Icon T
		AddSlot World
		Benefit 
		{
			BC*Income 10
			Trade*Used 1
		}
	}
	Building Trade_Post_II
	{
		Upgrade Trade_Post_III
		Cost 
		{
			Production 1200
		}
		Slot Outpost
		Level II
		Icon T
		Benefit 
		{
			BC*Income 20
			Trade*Used 2
		}
	}
	Building Trade_Post_III
	{
		Upgrade Trade_Post_IV
		Cost 
		{
			Production 3000
		}
		Slot Outpost
		Level III
		Icon T
		Benefit 
		{
			BC*Income 40
			Trade*Used 4
		}
	}
	Building Trade_Post_IV
	{
		Upgrade Trade_Post_V
		Cost 
		{
			Production 10000
		}
		Slot Outpost
		Level IV
		Icon T
		Benefit 
		{
			BC*Income 100
			Trade*Used 10
		}
	}
	Building Trade_Post_V
	{
		Cost 
		{
			Production 25000
		}
		Slot Outpost
		Level V
		Icon T
		Benefit 
		{
			BC*Income 250
			Trade*Used 25
		}
	}
	Building Energy_Capture_I
	{
		Upgrade Energy_Capture_II
		Cost 
		{
			Production 800
		}
		Slot Outpost
		Level I
		Icon E
		RequiredFeature Gas_Giant
		AddSlot World
		Benefit 
		{
			Energy*Income 20
			Energy*Level 1
			Trade*Used 1
		}
	}
	Building Energy_Capture_II
	{
		Upgrade Energy_Capture_III
		Cost 
		{
			Production 1200
		}
		Slot Outpost
		Level II
		Icon E
		Benefit 
		{
			Energy*Income 40
			Energy*Level 2
			Trade*Used 2
		}
	}
	Building Energy_Capture_III
	{
		Upgrade Energy_Capture_IV
		Cost 
		{
			Production 3000
		}
		Slot Outpost
		Level III
		Icon E
		Benefit 
		{
			Energy*Income 80
			Energy*Level 3
			Trade*Used 4
		}
	}
	Building Energy_Capture_IV
	{
		Upgrade Energy_Capture_V
		Cost 
		{
			Production 10000
		}
		Slot Outpost
		Level IV
		Icon E
		Benefit 
		{
			Energy*Income 200
			Energy*Level 4
			Trade*Used 10
		}
	}
	Building Energy_Capture_V
	{
		Cost 
		{
			Production 25000
		}
		Slot Outpost
		Level V
		Icon E
		Benefit 
		{
			Energy*Income 500
			Energy*Level 5
			Trade*Used 25
		}
	}
	Building Factory_I
	{
		Upgrade Factory_II
		Cost 
		{
			Production 2500
		}
		Slot Outpost
		Level I
		Icon F
		RequiredFeature Rocky|Rings
		AddSlot World
		Benefit 
		{
			Production*Income 30
			Trade*Used 3
		}
	}
	Building Factory_II
	{
		Upgrade Factory_III
		Cost 
		{
			Production 8000
		}
		Slot Outpost
		Level II
		Icon F
		Benefit 
		{
			Production*Income 150
			Production*PerPop 1
			Trade*Used 15
		}
	}
	Building Factory_III
	{
		Cost 
		{
			Production 25000
		}
		Slot Outpost
		Level III
		Icon F
		Benefit 
		{
			Production*Income 500
			Production*PerPop 2
			Trade*Used 50
		}
	}
	Building Shipyard_I
	{
		Upgrade Shipyard_II
		Cost 
		{
			Production 2500
		}
		Slot Outpost
		Level I
		Icon S
		AddSlot World
		Benefit 
		{
			Shipbuilding*Income 30
			Trade*Used 3
		}
	}
	Building Shipyard_II
	{
		Upgrade Shipyard_III
		Cost 
		{
			Production 8000
		}
		Slot Outpost
		Level II
		Icon S
		Benefit 
		{
			Shipbuilding*Income 150
			Shipbuilding*PerPop 1
			Trade*Used 15
		}
	}
	Building Shipyard_III
	{
		Cost 
		{
			Production 25000
		}
		Slot Outpost
		Level III
		Icon S
		Benefit 
		{
			Shipbuilding*Income 500
			Shipbuilding*PerPop 2
			Trade*Used 50
		}
	}
	Building Cultural_Retreat_I
	{
		Upgrade Cultural_Retreat_II
		Cost 
		{
			Production 2500
		}
		Slot Outpost
		Level I
		Icon U
		AddSlot World
		Benefit 
		{
			Culture*Income 20
		}
	}
	Building Cultural_Retreat_II
	{
		Upgrade Cultural_Retreat_III
		Cost 
		{
			Production 8000
		}
		Slot Outpost
		Level II
		Icon U
		Benefit 
		{
			Culture*Income 100
			Culture*PerPop 1
		}
	}
	Building Cultural_Retreat_III
	{
		Cost 
		{
			Production 25000
		}
		Slot Outpost
		Level III
		Icon U
		Benefit 
		{
			Culture*Income 300
			Culture*PerPop 2
		}
	}
	Building Capital
	{
		Upgrade Grand_Capital
		Slot Capital
		Level I
		Icon HQ
		Benefit 
		{
			Production*Income 50
			Shipbuilding*Income 50
			Research*Income 50
			Culture*Income 50
			BC*Income 50
			Authority*Max 300
		}
	}
	Building Grand_Capital
	{
		Upgrade Galactic_Capital
		Cost 
		{
			Production 50000
		}
		Slot Capital
		Level II
		Icon HQ
		Benefit 
		{
			Production*Income 50
			Shipbuilding*Income 50
			Research*Income 50
			Culture*Income 50
			BC*Income 50
			Authority*Max 400
		}
	}
	Building Galactic_Capital
	{
		Cost 
		{
			Production 150000
		}
		Slot Capital
		Level III
		Icon HQ
		RequiredBuilding Global_Centers
		Benefit 
		{
			Production*Income 50
			Shipbuilding*Income 50
			Research*Income 50
			Culture*Income 50
			BC*Income 50
			Authority*Max 500
		}
	}
	Building Possible_Colony
	{
		Upgrade Cities
		Slot World
		Icon Y
		RequiredFeature Colonizable
	}
	Building Cities
	{
		Upgrade Global_Centers
		Cost 
		{
			Production 20000
		}
		Slot World
		Level I
		Icon Y
		AddSlot District
		Benefit 
		{
			Districts 1
			Energy*Income 100
			Minerals*Income 100
			Production*Income 50
			Production*PerCPop 10
			BC*PerPop 2
			Authority*Used 30
		}
	}
	Building Global_Centers
	{
		Upgrade Ecumenopolis
		Cost 
		{
			Production 50000
		}
		Slot World
		Level II
		Icon Y
		AddSlot District
		Benefit 
		{
			Districts 2
			Pops*MaxBonus 100
			Energy*Income 100
			Minerals*Income 100
			Production*Income 50
			Production*PerCPop 10
			BC*PerPop 2
			Authority*Used 30
		}
	}
	Building Ecumenopolis
	{
		Cost 
		{
			Production 250000
		}
		Slot World
		Level III
		Icon Y
		RequiredBuilding Controled_Enviroment_III
		AddSlot District
		Benefit 
		{
			Districts 3
			Pops*MaxBonus 300
			Energy*Income 100
			Minerals*Income 100
			Production*Income 50
			Production*PerCPop 10
			BC*PerPop 2
			Authority*Max 100
			Authority*Used 30
		}
	}
	Building Colonial_Oversight
	{
		Upgrade Minimal_Oversight
		Upgrade Local_Goverment
		Cost 
		{
			Production 1000
			Culture 10000
		}
		Slot Control
		Level II
		Icon CC
		Benefit 
		{
			Pops*Control 25
			Authority*Used 10
		}
	}
	Building Minimal_Oversight
	{
		Upgrade Colonial_Oversight
		Cost 
		{
			Production 500
			Culture 10000
		}
		Slot Control
		Level I
		Icon CC
		Benefit 
		{
			Pops*Control 0
			Authority*Used 0
		}
	}
	Building Local_Goverment
	{
		Upgrade Colonial_Oversight
		Upgrade Integrated_Economy
		Cost 
		{
			Production 2000
			Culture 10000
		}
		Slot Control
		Level III
		Icon CC
		Benefit 
		{
			Pops*Control 50
			Authority*Used 20
		}
	}
	Building Integrated_Economy
	{
		Upgrade Local_Goverment
		Upgrade Planned_Economy
		Cost 
		{
			Production 6000
			Culture 10000
		}
		Slot Control
		Level IV
		Icon CC
		Benefit 
		{
			Pops*Control 75
			Authority*Used 40
		}
	}
	Building Planned_Economy
	{
		Upgrade Integrated_Economy
		Cost 
		{
			Production 10000
			Culture 10000
		}
		Slot Control
		Level V
		Icon CC
		Benefit 
		{
			Pops*Control 100
			Authority*Used 60
		}
	}
	Building District
	{
		Upgrade Entertaiment_District
		Upgrade Industrial_District
		Upgrade Science_District
		Upgrade Culture_District
		Slot District
		Icon D
	}
	Building Entertaiment_District
	{
		Cost 
		{
			Production 25000
		}
		Slot District
		Icon ED
		Benefit 
		{
			Pops*GrowthBonus 100
			BC*Income 200
			BC*PerCPop 5
			Authority*Max 20
		}
	}
	Building Industrial_District
	{
		Cost 
		{
			Production 25000
		}
		Slot District
		Icon ID
		Benefit 
		{
			Production*Income 100
			Production*PerCPop 10
		}
	}
	Building Science_District
	{
		Cost 
		{
			Production 25000
		}
		Slot District
		Icon SD
		Benefit 
		{
			Research*Income 100
			Research*PerCPop 10
		}
	}
	Building Culture_District
	{
		Cost 
		{
			Production 25000
		}
		Slot District
		Icon CD
		Benefit 
		{
			Culture*PerCPop 20
		}
	}
	Building Terraform
	{
		Upgrade Controled_Enviroment_I
		Slot Geo
		Icon X
		Benefit 
		{
			GeoLevel 0
		}
	}
	Building Controled_Enviroment_I
	{
		Upgrade Controled_Enviroment_II
		Cost 
		{
			Production 10000
		}
		Slot Geo
		Level I
		Icon X
		Benefit 
		{
			GeoLevel 1
		}
	}
	Building Controled_Enviroment_II
	{
		Upgrade Controled_Enviroment_III
		Cost 
		{
			Production 50000
		}
		Slot Geo
		Level II
		Icon X
		Benefit 
		{
			GeoLevel 2
		}
	}
	Building Controled_Enviroment_III
	{
		Cost 
		{
			Production 100000
		}
		Slot Geo
		Level III
		Icon X
		Benefit 
		{
			GeoLevel 3
		}
	}
}
