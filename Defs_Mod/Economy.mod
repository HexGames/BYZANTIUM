Economy
{
	Tax
	{
		Tax_0 
		{
			Percent 10
		}
		Tax_1 
		{
			Percent 25
		}
		Tax_2 
		{
			Percent 50
		}
	}
	Reinvest
	{
		Private_Pop
		{
			Percent 50
		}
		Private_Factory_1 
		{
			Percent 25
		}
		Private_Factory_2 
		{
			Percent 10
		}
		Private_Factory_3 
		{
			Percent 0 // will always be 0
		}
	}
	District
	{
		State_Pop
		{
			Resource 30
			Wealth 30
		}
		State_Factory_1
		{
			Resource 70
			Wealth 40
		}
		State_Factory_2
		{
			Resource 100
			Wealth 50
		}
		State_Factory_3
		{
			Resource 120
			Wealth 60
		}
		Private_Pop Tax_0
		{
			Resource 10
			PrivateBC 80
		}
		Private_Pop Tax_1
		{
			Resource 10
			PrivateBC 70
		}
		Private_Pop Tax_2
		{
			Resource 10
			PrivateBC 60
		}
		Private_Factory_1 Tax_0
		{
			Resource 20
			PrivateBC 130
		}
		Private_Factory_1 Tax_1
		{
			Resource 20
			PrivateBC 120
		}
		Private_Factory_1 Tax_2 
		{
			Resource 20
			PrivateBC 110
		}
		Private_Factory_2 Tax_0 
		{
			Resource 30
			PrivateBC 170
		}
		Private_Factory_2 Tax_1 
		{
			Resource 30
			PrivateBC 160
		}
		Private_Factory_2 Tax_2 
		{
			Resource 30
			PrivateBC 150
		}
		Private_Factory_3 Tax_0 
		{
			Resource 30
			PrivateBC 200
		}
		Private_Factory_3 Tax_1 
		{
			Resource 30
			PrivateBC 190
		}
		Private_Factory_3 Tax_2
		{
			Resource 30
			PrivateBC 180
		}
		Police_Pop
		{
			Resource 20
			Wealth 30
		}
		Police_Factory_1
		{
			Resource 30
			Wealth 40
		}
		Police_Factory_2
		{
			Resource 40
			Wealth 50
		}
		Police_Factory_3
		{
			Resource 50
			Wealth 60
		}
	}
	Invest 
	{
		State_Pop Level_1
		{
			Cost*BC 30
			Resource 10
		}
		State_Pop Level_2
		{
			Cost*BC 60
			Resource 20
		}
		State_Pop Level_3
		{
			Cost*BC 90
			Resource 30
		}
		State_Factory_1 Level_1
		{
			Cost*BC 30
			Resource 20
		}
		State_Factory_1 Level_2
		{
			Cost*BC 60
			Resource 30
		}
		State_Factory_1 Level_3
		{
			Cost*BC 90
			Resource 40
		}
		State_Factory_2 Level_1
		{
			Cost*BC 30
			Resource 20
		}
		State_Factory_2 Level_2
		{
			Cost*BC 60
			Resource 40
		}
		State_Factory_2 Level_3
		{
			Cost*BC 90
			Resource 50
		}
		State_Factory_3 Level_1
		{
			Cost*BC 30
			Resource 20
		}
		State_Factory_3 Level_2
		{
			Cost*BC 60
			Resource 40
		}
		State_Factory_3 Level_3
		{
			Cost*BC 90
			Resource 60
		}
		Private_Pop Level_1
		{
			Cost*BC 30
			Resource 10
			Reinvest 10
		}
		Private_Pop Level_2
		{
			Cost*BC 60
			Resource 20
			Reinvest 20
		}
		Private_Pop Level_3
		{
			Cost*BC 90
			Resource 30
			Reinvest 30
		}
		Private_Factory_1 Level_1
		{
			Cost*BC 30
			Resource 10
			Reinvest 10
		}
		Private_Factory_1 Level_2
		{
			Cost*BC 60
			Resource 20
			Wealth 10
			Reinvest 10
		}
		Private_Factory_1 Level_3
		{
			Cost*BC 90
			Resource 30
			Wealth 10
			Reinvest 20
		}
		Private_Factory_2 Level_1
		{
			Cost*BC 30
			Resource 10
			Reinvest 10
		}
		Private_Factory_2 Level_2
		{
			Cost*BC 60
			Resource 20
			Wealth 10
			Reinvest 10
		}
		Private_Factory_2 Level_3
		{
			Cost*BC 90
			Resource 30
			Wealth 20
			Reinvest 10
		}
		Private_Factory_3 Level_1
		{
			Cost*BC 30
			Resource 10
			Wealth 10
		}
		Private_Factory_3 Level_2
		{
			Cost*BC 60
			Resource 20
			Wealth 20
		}
		Private_Factory_3 Level_3
		{
			Cost*BC 90
			Resource 30
			Wealth 30
		}
	}
}