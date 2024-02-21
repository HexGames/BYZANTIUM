Def_Buildings 
{
	// cities ----------------------------------
	Building Cities
	{
		Cost 
		{
			Minerals 2000
			if Planet:hostile
			{
				Minerals 10000
			}
		}
		Benefit 
		{
			Buildings:Max 1
		}
	}
	Building Megacities
	{
		Cost 
		{
			Minerals 20000
			if Planet:hostile
			{
				Minerals 100000
			}
		}
		Benefit 
		{
			Buildings:Max 10
		}
	}
	Building Capital_City
	{
		Cost 
		{
			Credits 100000
			if Planet:hostile
			{
				Minerals 100000
			}
		}
		Benefit 
		{
			Buildings:Max 10
			Authority 500
			Influence 500
		}
	}
	// construction sector ----------------------
	Building Construction_Sectors
	{
		Slot Building
		Cost 
		{
			Minerals 5000
		}
		Benefit 
		{
			Minerals:Income -1
			Construction 1
		}
	}
	// energy ------------------------------------
	Building Power_Plants
	{
		Slot Building
		Cost 
		{
			Minerals 2000
		}
		Benefit 
		{
			Energy:income 50
		}
	}
	Building Fusion_Reactors
	{
		Slot Building
		Cost 
		{
			Minerals 1000
		}
		Benefit 
		{
			Deuterium -100
			Energy:income 100
		}
	}
	Building Deuterium_Extractors
	{
		Starbase
		if Planet:Deuterium
		Cost 
		{
			Minerals 1000
		}
		Benefit 
		{
			Deuterium 100
			if Planet:Anti-Matter:Rich
			{
				Deuterium 100
			}
			if Planet:Minerals:Ultra-Rich
			{
				Deuterium 300
			}
		}
	}
	Building Anti-Matter_Reactors
	{
		Slot Building
		Cost 
		{
			Minerals 1000
		}
		ExtraBuilding Anti-Matter_Extractors
		Benefit 
		{
			Energy:income 200
		}
	}
	Building Anti-Matter_Extractors
	{
		Starbase
		if Planet:Anti-Matter
		Cost 
		{
			Minerals 1000
		}
		Benefit 
		{
			Anti-Matter 100
			if Planet:Anti-Matter:Rich
			{
				Anti-Matter 100
			}
			if Planet:Minerals:Ultra-Rich
			{
				Anti-Matter 300
			}
		}
	}
	// mines -------------------------------------
	Building Mines
	{
		Slot Building
		Cost 
		{
			Raw_Minerals 1000
		}
		Benefit 
		{
			Raw_Minerals 100
			if Planet:Minerals:Poor
			{
				Raw_Minerals -50
			}
			if Planet:Minerals:Rich
			{
				Raw_Minerals 100
			}
			if Planet:Minerals:Ultra-Rich
			{
				Raw_Minerals 300
			}
		}
	}
	Building Asteroid_Mines
	{
		Slot Asteroid
		if Planet:Asteroid_Field
		if Planet:Asteroid_Field:SlotAvailable
		Cost 
		{
			Minerals 1000
		}
		Benefit 
		{
			Raw_Minerals 150
			if Planet:Minerals:Rich
			{
				Raw_Minerals 150
			}
			if Planet:Minerals:Ultra-Rich
			{
				Raw_Minerals 450
			}
		}
	}
	Building Ring_Collectors
	{
		Slot Ring
		if Planet:Ring
		if Planet:Ring:SlotAvailable
		Cost 
		{
			Minerals 1000
		}
		ExtraBuilding Factories
		Benefit 
		{
			Raw_Minerals 200
			if Planet:Minerals:Rich
			{
				Raw_Minerals 200
			}
			if Planet:Minerals:Ultra-Rich
			{
				Raw_Minerals 600
			}
		}
	}
	Building Factory
	{
		Slot Building
		Cost 
		{
			Minerals 1000
		}
		Enable 
		{
			Minerals:Income 100
		}
	}
	// advanced -------------------------------------
	Building Research_Labs
	{	
		Slot Building
		Cost 
		{
			Minerals 2000
		}
		Benefit 
		{
			Energy:Income -50
			TechPoints:Income 100
		}
	}
	Building Culture_Centers
	{	
		Slot Building
		Cost 
		{
			Minerals 2000
		}
		Benefit 
		{
			CivicPoints:Income 50
		}
	}
	// bureaucracy -------------------------------------
	Building Goverment_Offices
	{
		Slot Bureaucracy
		Cost 
		{
			Minerals 5000
		}
		Benefit 
		{
			Authority 100
		}
	}
	Building Diplomatic_Offices
	{
		Slot Bureaucracy
		Cost 
		{
			Minerals 5000
		}
		Benefit 
		{
			Influence 100
		}
	}
	// shipyard -------------------------------------
	Building Shypyard
	{
		Starbase
		Cost 
		{
			Minerals 5000
		}
		Benefit 
		{
			Minerals:Income -1
			ShipConstruction 1
		}
	}
}
