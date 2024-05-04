Buildings 
{
	Building Star_Orbit
	{
		Upgrade Starport
		Slot Star
		Icon 0
		Idx 0
	}
	Building Starport
	{
		Upgrade Gate
		Cost 
		{
			Production 1500
		}
		Slot Star
		Level I
		Icon S
		Idx 0
		1 
		{
			Authority 1
		}
		Benefit 
		{
			SectorControl yes
		}
	}
	Building Gate
	{
		Upgrade Supergate
		Cost 
		{
			Production 10000
		}
		Slot Star
		Level II
		Icon G
		Idx 0
		1 
		{
			Authority 2
		}
		Benefit 
		{
			SectorControl yes
		}
	}
	Building Supergate
	{
		Cost 
		{
			Production 20000
		}
		Slot Star
		Level III
		Icon SG
		Idx 0
		1 
		{
			Authority 3
		}
		Benefit 
		{
			SectorControl yes
		}
	}
	Building Stable_Orbit
	{
		Upgrade Energy_Capture_I
		Slot Outpost
		Icon E
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
		Benefit 
		{
			Energy*Income 40
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
			Energy*Income 80
		}
	}
	Building Energy_Capture_III
	{
		Upgrade Energy_Capture_IV
		Cost 
		{
			Production 2500
		}
		Slot Outpost
		Level III
		Icon E
		Benefit 
		{
			Energy*Income 150
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
		Required Colony
		Benefit 
		{
			Energy*Income 500
		}
	}
	Building Energy_Capture_V
	{
		Cost 
		{
			Production 40000
		}
		Slot Outpost
		Level V
		Icon E
		Required Colony
		Benefit 
		{
			Energy*Income 1200
		}
	}
	Building Ring_Minerals
	{
		Upgrade Ring_Mine
		Slot Outpost
		Icon RM
	}
	Building Ring_Mine
	{
		Cost 
		{
			Production 1200
		}
		Slot Outpost
		Level I
		Icon RM
		Benefit 
		{
			Minerals*Income 100
		}
	}
	Building Possible_Asteroid_Mines
	{
		Upgrade Asteroid_Mines_I
		Slot Outpost
	}
	Building Asteroid_Mines_I
	{
		Upgrade Asteroid_Mines_II
		Cost 
		{
			Production 2000
		}
		Slot Outpost
		Level I
		Benefit 
		{
			Minerals*Income 150
		}
	}
	Building Asteroid_Mines_II
	{
		Upgrade Asteroid_Mines_III
		Cost 
		{
			Production 5000
		}
		Slot Outpost
		Level II
		Benefit 
		{
			Minerals*Income 400
		}
	}
	Building Asteroid_Mines_III
	{
		Cost 
		{
			Production 10000
		}
		Slot Outpost
		Level III
		Benefit 
		{
			Minerals*Income 800
		}
	}
	Building Possible_Outpost
	{
		Upgrade Mineral_Processers_I
		Upgrade Research_Lab_I
		Upgrade Trade_Post_I
		Upgrade Energy_Generators_I
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
		AddSlot Possible_Colony
		Benefit 
		{
			Minerals*Income 50
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
			Minerals*Income 100
		}
	}
	Building Mineral_Processers_III
	{
		Upgrade Mineral_Processers_IV
		Cost 
		{
			Production 2500
		}
		Slot Outpost
		Level III
		Icon M
		Benefit 
		{
			Minerals*Income 200
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
		Required Colony
		Benefit 
		{
			Minerals*Income 500
		}
	}
	Building Mineral_Processers_V
	{
		Cost 
		{
			Production 40000
		}
		Slot Outpost
		Level V
		Icon M
		Required Colony
		Benefit 
		{
			Minerals*Income 1200
		}
	}
	Building Research_Lab_I
	{
		Upgrade Research_Lab_II
		Cost 
		{
			Production 1200
		}
		Slot Outpost
		Level I
		Icon R
		AddSlot Possible_Colony
		Benefit 
		{
			Research*Income 20
		}
	}
	Building Research_Lab_II
	{
		Upgrade Research_Lab_III
		Cost 
		{
			Production 3000
		}
		Slot Outpost
		Level II
		Icon R
		Benefit 
		{
			Research*Income 50
		}
	}
	Building Research_Lab_III
	{
		Upgrade Research_Lab_IV
		Cost 
		{
			Production 5000
		}
		Slot Outpost
		Level III
		Icon R
		Benefit 
		{
			Research*Income 100
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
		Required Colony
		Benefit 
		{
			Research*Income 300
		}
	}
	Building Research_Lab_V
	{
		Cost 
		{
			Production 30000
		}
		Slot Outpost
		Level V
		Icon R
		Required Colony
		Benefit 
		{
			Research*Income 1000
		}
	}
	Building Trade_Post_I
	{
		Upgrade Trade_Post_II
		Cost 
		{
			Production 2000
		}
		Slot Outpost
		Level I
		Icon T
		AddSlot Possible_Colony
		Benefit 
		{
			BC*Income 50
		}
	}
	Building Trade_Post_II
	{
		Upgrade Trade_Post_III
		Cost 
		{
			Production 5000
		}
		Slot Outpost
		Level II
		Icon T
		Benefit 
		{
			BC*Income 150
		}
	}
	Building Trade_Post_III
	{
		Upgrade Trade_Post_IV
		Cost 
		{
			Production 10000
		}
		Slot Outpost
		Level III
		Icon T
		Benefit 
		{
			BC*Income 300
		}
	}
	Building Trade_Post_IV
	{
		Upgrade Trade_Post_V
		Cost 
		{
			Production 20000
		}
		Slot Outpost
		Level IV
		Icon T
		Required Colony
		Benefit 
		{
			BC*Income 800
		}
	}
	Building Trade_Post_V
	{
		Cost 
		{
			Production 50000
		}
		Slot Outpost
		Level V
		Icon T
		Required Colony
		Benefit 
		{
			BC*Income 2000
		}
	}
	Building Energy_Generators_I
	{
		Upgrade Energy_Generators_II
		Cost 
		{
			Production 800
		}
		Slot Outpost
		Level I
		Icon E
		AddSlot Possible_Colony
		Benefit 
		{
			Energy*Income 50
		}
	}
	Building Energy_Generators_II
	{
		Upgrade Energy_Generators_III
		Cost 
		{
			Production 1200
		}
		Slot Outpost
		Level II
		Icon E
		Benefit 
		{
			Energy*Income 80
		}
	}
	Building Energy_Generators_III
	{
		Cost 
		{
			Production 2500
		}
		Slot Outpost
		Level III
		Icon E
		Benefit 
		{
			Energy*Income 120
		}
	}
	Building Possible_Colony
	{
		Upgrade Factory_I
		Upgrade Shipyard_I
		Upgrade Cultural_Retreat_I
		Slot Colony
		Icon C
	}
	Building Factory_I
	{
		Upgrade Factory_II
		Cost 
		{
			Production 5000
		}
		Slot Colony
		Level I
		Icon F
		AddSlot World
		1 
		{
			Authority 1
		}
		Benefit 
		{
			Production*Income 100
		}
	}
	Building Factory_II
	{
		Upgrade Factory_III
		Cost 
		{
			Production 10000
		}
		Slot Colony
		Level II
		Icon F
		1 
		{
			Authority 1
		}
		Benefit 
		{
			Production*Income 300
		}
	}
	Building Factory_III
	{
		Cost 
		{
			Production 30000
		}
		Slot Colony
		Level III
		Icon F
		1 
		{
			Authority 1
		}
		Benefit 
		{
			Production*Income 1000
		}
	}
	Building Shipyard_I
	{
		Upgrade Shipyard_II
		Cost 
		{
			Production 10000
		}
		Slot Colony
		Level I
		Icon S
		AddSlot World
		1 
		{
			Authority 1
		}
		Benefit 
		{
			Shipbuilding*Income 100
		}
	}
	Building Shipyard_II
	{
		Upgrade Shipyard_III
		Cost 
		{
			Production 20000
		}
		Slot Colony
		Level II
		Icon S
		1 
		{
			Authority 1
		}
		Benefit 
		{
			Shipbuilding*Income 300
		}
	}
	Building Shipyard_III
	{
		Cost 
		{
			Production 50000
		}
		Slot Colony
		Level III
		Icon S
		1 
		{
			Authority 1
		}
		Benefit 
		{
			Shipbuilding*Income 1000
		}
	}
	Building Cultural_Retreat_I
	{
		Upgrade Cultural_Retreat_II
		Cost 
		{
			Production 3000
		}
		Slot Colony
		Level I
		Icon U
		AddSlot World
		1 
		{
			Authority 1
		}
		Benefit 
		{
			Culture*Income 50
		}
	}
	Building Cultural_Retreat_II
	{
		Upgrade Cultural_Retreat_III
		Cost 
		{
			Production 5000
		}
		Slot Colony
		Level II
		Icon U
		1 
		{
			Authority 1
		}
		Benefit 
		{
			Culture*Income 100
		}
	}
	Building Cultural_Retreat_III
	{
		Cost 
		{
			Production 10000
		}
		Slot Colony
		Level III
		Icon U
		1 
		{
			Authority 1
		}
		Benefit 
		{
			Culture*Income 200
		}
	}
	Building World
	{
		Upgrade Cities
		Slot Main
		Icon Y
	}
	Building Cities
	{
		Upgrade Global_Centers
		Cost 
		{
			Production 10000
		}
		Slot Main
		Level I
		Icon Y
		AddSlot District
		1 
		{
			Authority 2
		}
		Benefit 
		{
			Districts 1
			Energy*Income 20
			Minerals*Income 20
			Production*Income 20
			Influence*Income 3
			BC*PerPop 20
		}
	}
	Building Global_Centers
	{
		Upgrade Ecumenopolis
		Cost 
		{
			Production 50000
		}
		Slot Main
		Level II
		Icon Y
		AddSlot District
		1 
		{
			Authority 5
		}
		Benefit 
		{
			Districts 2
			Energy*Income 40
			Minerals*Income 40
			Production*Income 40
			Influence*Income 10
			BC*PerPop 30
			Bonus*MaxPop*Percent 100
		}
	}
	Building Ecumenopolis
	{
		Cost 
		{
			Production 250000
		}
		Slot Main
		Level III
		Icon Y
		Required Controled_Enviroment_III
		AddSlot District
		1 
		{
			Authority 20
		}
		Benefit 
		{
			Districts 3
			Energy*Income 100
			Minerals*Income 100
			Production*Income 100
			Influence*Income 50
			BC*PerPop 50
			Bonus*MaxPop*Percent 300
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
			BC*Income 500
			Influence*Income 1
			Bonus*Growth*Perc 100
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
			Production*Income 200
			BC*Income 500
			Production*PerPop 40
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
			Research*Income 1000
			Science*PerPop 40
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
			Influence*Income 3
			Culture*PerPop 40
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
