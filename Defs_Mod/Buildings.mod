Def_Buildings 
{
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
			Energy:income 100
		}
	}
	// mines -------------------------------------
	Building Factory
	{
		Slot Building
		Cost 
		{
			Minerals 2000
		}
		Enable 
		{
			Energy:income -50
			Minerals:Income 100
		}
	}
	// advanced -------------------------------------
	Building Research_Labs
	{	
		Slot Building
		Cost 
		{
			Minerals 5000
		}
		Benefit 
		{
			Energy:income -30
			TechPoints:Income 100
		}
	}
	Building Culture_Centers
	{	
		Slot Building
		Cost 
		{
			Minerals 5000
		}
		Benefit 
		{
			Energy:income -10
			CivicPoints:Income 100
		}
	}
	// bureaucracy -------------------------------------
	Building Goverment_Offices
	{
		Slot Bureaucracy
		Cost 
		{
			Minerals 10000
		}
		Benefit 
		{
			Energy:income -10
			Authority 100
		}
	}
	Building Diplomatic_Offices
	{
		Slot Bureaucracy
		Cost 
		{
			Minerals 10000
		}
		Benefit 
		{
			Energy:income -10
			Influence 100
		}
	}
}
