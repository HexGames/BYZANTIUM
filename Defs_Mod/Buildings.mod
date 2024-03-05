Def_Buildings 
{
	// mines -------------------------------------
	Building Mine
	{
		Cost 
		{
			Prod 5000
		}
		Benefit 
		{
			Energy*Used 50
			Minerals 500
		}
	}
	Building Factory_I
	{
		Obsolete Player:Tech:Factory_II
		Cost 
		{
			Prod 5000
		}
		Benefit 
		{
			Minerals*Used 100
			Energy*Used 50
			Prod*Income 100
		}
	}
	Building Factory_II
	{
		Obsolete Player:Tech:Factory_III
		Requires 
		{
			Player:Tech:Factory_II
		}
		Cost 
		{
			Prod 7500
		}
		Benefit 
		{
			Minerals*Used 150
			Energy*Used 75
			Prod*Income 150
		}
	}
	Building Factory_III
	{
		Obsolete Player:Tech:Factory_IV
		Requires 
		{
			Requires Player:Tech:Factory_III
		}
		Cost 
		{
			Prod 10000
		}
		Benefit 
		{
			Minerals*Used 200
			Energy*Used 100
			Prod*Income 200
		}
	}
	Building Factory_IV
	{
		Obsolete Player:Tech:Factory_V
		Requires 
		{
			Player:Tech:Factory_IV
		}
		Cost 
		{
			Prod 12500
		}
		Benefit 
		{
			Minerals*Used 250
			Energy*Used 125
			Prod*Income 250
		}
	}
	Building Factory_V
	{
		Requires 
		{
			Player:Tech:Factory_V
		}
		Cost 
		{
			Prod 15000
		}
		Benefit 
		{
			Minerals*Used 300
			Energy*Used 150
			Prod*Income 300
		}
	}
	// energy ------------------------------------
	Building Power_Plants
	{
		Obsolete Resource:Deuterium
		Cost 
		{
			Prod 5000
		}
		Benefit 
		{
			Energy 100
		}
	}
	Building Fusion_Reactors
	{
		Obsolete Resource:Anti-Matter
		Requires 
		{
			Player:SpecialResource:Deuterium
		}
		Cost 
		{
			Prod 10000
		}
		Benefit 
		{
			Energy 200
		}
	}
	Building Anti-Matter_Reactors
	{
		Requires 
		{
			Player:SpecialResource:Anti-Matter
		}
		Cost 
		{
			Prod 15000
		}
		Benefit 
		{
			Energy 300
		}
	}
	// advanced -------------------------------------
	Building Research_Labs
	{	
		Cost 
		{
			Prod 10000
		}
		Benefit 
		{
			Energy*Used 50
			TechPoints*Income 50
			//Link:Projects:Research_Station
		}
	}
	Building Culture_Centers
	{	
		Cost 
		{
			Prod 10000
		}
		Benefit 
		{
			Energy*Used 10
			CivicPoints*Income 50
		}
	}
	// advanced -------------------------------------
	Building Gouverment_Offices
	{	
		Cost 
		{
			Prod 10000
		}
		Benefit 
		{
			Energy*Used 10
			Authority 50
		}
	}
	Building Diplomatic_Offices
	{	
		Cost 
		{
			Prod 10000
		}
		Benefit 
		{
			Energy*Used 10
			Influence 50
		}
	}
	// ------------------------------------------------
	Building Constructor
	{	
		Require
		{
			Player:Resource:Prod*Income 1000
		}
		Cost 
		{
			Prod 10000
		}
		Benefit 
		{
			Energy*Used 100
			Constructor 1
		}
	}
	// ------------------------------------------------
	Building Shipyard
	{	
		Require
		{
			Player:Resource:Prod*Income 1000
		}
		Cost 
		{
			Prod 10000
		}
		Benefit 
		{
			Energy*Used 100
			Shipyard 1
		}
	}
}
