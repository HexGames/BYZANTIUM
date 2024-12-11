Buildings 
{
	Building Capital
	{
		Type Capital
		Icon Capital
		Resource Control
		Benefit 
		{
			Extra 
			{
				Construction 20
			}
		}
		Default 
		{
			Pop*Bonus 0
			Factory*Bonus 0
		}
		Control 
		{
			Cost 0
			Type Police
		}
	}
	Building Private_Business
	{
		Type District
		Icon BusinessP
		Resource BC
		Default 
		{
			Pop*Bonus 0
			Factory*Bonus 0
		}
		Control 
		{
			Cost 10
			Type Private
			Nationalize_to State_Business
		}
	}
	Building State_Business
	{
		Type District
		Icon BusinessS
		Resource BC
		Cost 50
		Default 
		{
			Pop*Bonus -10
			Factory*Bonus -10
		}
		Control 
		{
			Cost 20
			Type State_Owned
			Privatize_to Private_Business
		}
	}
	Building Private_Media
	{
		Type District
		Icon MediaP
		Resource Influence
		Default 
		{
			Pop*Bonus 10
			Factory*Bonus -10
		}
		Control 
		{
			Cost 10
			Type Private
			Nationalize_to State_Media
		}
	}
	Building State_Media
	{
		Type District
		Icon MediaS
		Resource Influence
		Cost 50
		Default 
		{
			Pop*Bonus 10
			Factory*Bonus -10
		}
		Control 
		{
			Cost 20
			Type State_Owned
			Privatize_to Private_Media
		}
	}
	Building Private_Shipyard
	{
		Type District
		Icon ShipyardP
		Resource Shipbuilding
		Default 
		{
			Pop*Bonus -10
			Factory*Bonus 0
		}
		Control 
		{
			Cost 10
			Type Private
			Nationalize_to State_Shypyard
		}
	}
	Building State_Shipyard
	{
		Type District
		Icon ShipyardS
		Resource Shipbuilding
		Cost 50
		Default 
		{
			Pop*Bonus -10
			Factory*Bonus 0
		}
		Control 
		{
			Cost 20
			Type State_Owned
			Privatize_to Private_Shipyard
		}
	}
	Building Private_Construction
	{
		Type District
		Icon ConstructionP
		Resource Construction
		Default 
		{
			Pop*Bonus 0
			Factory*Bonus 0
		}
		Control 
		{
			Cost 10
			Type Private
			Nationalize_to State_Construction
		}
	}
	Building State_Construction
	{
		Type District
		Icon ConstructionS
		Resource Construction
		Cost 50
		Default 
		{
			Pop*Bonus 0
			Factory*Bonus 0
		}
		Control 
		{
			Cost 20
			Type State_Owned
			Privatize_to Private_Construction
		}
	}
	Building Research_Center
	{
		Type District
		Icon Research
		Resource Research
		Cost 25
		Default 
		{
			Pop*Bonus -10
			Factory*Bonus 0
		}
		Control 
		{
			Cost 20
			Type State_Owned
		}
	}
	Building Police
	{
		Type District
		Icon Police
		Resource Control
		Cost 25
		Default 
		{
			Pop*Bonus 0
			Factory*Bonus 0
		}
		Control 
		{
			Cost 0
			Type Police
		}
	}
	Building Monastery
	{
		Type District
		Icon Religion
		Resource Control
		Default 
		{
			Pop*Bonus -10
			Factory*Bonus 0
		}
		Control 
		{
			Cost 0
			Type Police
		}
	}
	Building Research_Station
	{
		Type Space_Station
		Icon Research
		Resource Research
		Benefit 
		{
			Income 
			{
				Resource 20
			}
		}
	}
	Building Research_Nexus
	{
		Type Space_Station
		Icon Research
		Resource Research
		Benefit 
		{
			Bonus 
			{
				Resource 10
			}
		}
	}
	Building Orbital_Forum
	{
		Type Space_Station
		Icon MediaS
		Resource Influence
		Benefit 
		{
			Income 
			{
				Resource 20
			}
		}
	}
	Building Trade_Station
	{
		Type Space_Station
		Icon BusinessS
		Resource BC
		Benefit 
		{
			Income 
			{
				Resource 20
			}
		}
	}
	Building Asteroid_Shipyard
	{
		Type Asteroid_Base
		Icon ShipyardS
		Resource Shipbuilding
		Benefit 
		{
			Income 
			{
				Resource 20
			}
		}
	}
	Building Alloy_Foundry
	{
		Type Asteroid_Base
		Icon ShipyardS
		Resource Shipbuilding
		Benefit 
		{
			Bonus 
			{
				Resource 10
			}
		}
	}
	Building Asteroid_Forum
	{
		Type Asteroid_Base
		Icon MediaS
		Resource Influence
		Benefit 
		{
			Income 
			{
				Resource 20
			}
		}
	}
	Building Asteroid_Bazzar
	{
		Type Asteroid_Base
		Icon BusinessS
		Resource BC
		Benefit 
		{
			Income 
			{
				Resource 20
			}
		}
	}
	Building Ships_Factory
	{
		Type Outpost
		Icon ShipyardS
		Resource Shipbuilding
		Benefit 
		{
			Income 
			{
				Resource 20
			}
		}
	}
	Building Research_Labs
	{
		Type Outpost
		Icon Research
		Resource Research
		Benefit 
		{
			Income 
			{
				Resource 20
			}
		}
	}
	Building Trade_Outpost
	{
		Type Outpost
		Icon BusinessS
		Resource BC
		Benefit 
		{
			Income 
			{
				Resource 20
			}
		}
	}
	Building Waste_Management
	{
		Type Outpost
		Icon BusinessS
		Resource BC
		Benefit 
		{
			Bonus 
			{
				Resource 10
			}
		}
	}
}
