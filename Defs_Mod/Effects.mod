Effects 
{
	Effect Economy_0
	{
		Name High_Investments
		Benefit 
		{
			Wealth 30
			Inequality 20
			BC*PerPop -40
			Production*PerPop 20
		}
	}
	Effect Economy_1
	{
		Name Invest
		Benefit 
		{
			Wealth 20
			Inequality 10
			BC*PerPop -20
			Production*PerPop 10
		}
	}
	Effect Economy_2
	{
		Name Low_Tax
		Benefit 
		{
			Wealth 10
			BC*PerPop 10
		}
	}
	Effect Economy_3
	{
		Name Medium_Tax
		Benefit 
		{
			BC*PerPop 30
			Production*PerPop -10
		}
	}
	Effect Economy_4
	{
		Name High_Tax
		Benefit 
		{
			Wealth -10
			Inequality -10
			BC*PerPop 50
			Production*PerPop -30
		}
	}
	Effect State_0
	{
		Name High_Autonomy
		Benefit 
		{
			Control -10
			Corruption 10
			Wealth 20
		}
	}
	Effect State_1
	{
		Name Limited_Autonomy
		Benefit 
		{
			Wealth 20
			Influence*PerPop -10
		}
	}
	Effect State_2
	{
		Name Low_Authority
		Benefit 
		{
			Control 10
			Wealth 10
			Influence*PerPop -20
		}
	}
	Effect State_3
	{
		Name Medium_Authority
		Benefit 
		{
			Control 20
			Wealth 10
			Influence*PerPop -30
		}
	}
	Effect State_4
	{
		Name High_Authority
		Benefit 
		{
			Control 30
			Corruption 10
			Influence*PerPop -40
		}
	}
	Effect Social_0
	{
		Name No_Social_Security
		Benefit 
		{
			Happiness -10
			Inequality 10
		}
	}
	Effect Social_1
	{
		Name Social_Programs
		Benefit 
		{
			BC*PerPop -10
		}
	}
	Effect Social_2
	{
		Name Social_Security
		Benefit 
		{
			Happiness 10
			BC*PerPop -20
		}
	}
	Effect Social_3
	{
		Name Universal_Basic_Income
		Benefit 
		{
			Corruption -10
			Happiness 20
			Inequality -10
			BC*PerPop -30
		}
	}
	Effect Social_4
	{
		Name Universal_Income
		Benefit 
		{
			Corruption -20
			Happiness 30
			Inequality -20
			BC*PerPop -40
		}
	}
	Effect Migration_0
	{
		Name No_Migration
		Benefit 
		{
			Happiness -10
		}
	}
	Effect Migration_1
	{
		Name Free_Migration
		Benefit 
		{
			Happiness 10
		}
	}
	Effect Migration_2
	{
		Name Leave_Incentives
		Benefit 
		{
			Migration -100
		}
	}
	Effect Migration_3
	{
		Name Settle_Incentives
		Benefit 
		{
			Migration 100
		}
	}
	Effect Migration_4
	{
		Name Forced_Exodus
		Benefit 
		{
			Happiness -40
			Migration -400
		}
	}
	Effect Migration_5
	{
		Name Forced_Settlement
		Benefit 
		{
			Happiness -40
			Migration 400
		}
	}
	Effect Control_0
	{
		Name Nominal_Control
		Disable 
		{
			Disable High_Tax
			Disable Medium_Tax
		}
		Benefit 
		{
			Production*PerPop 10
		}
	}
	Effect Control_1
	{
		Name Limited_Control
		Disable 
		{
			Disable High_Tax
		}
		Benefit 
		{
			BC*PerPop 10
			Production*PerPop 20
		}
	}
	Effect Control_2
	{
		Name State_Control
		Benefit 
		{
			Influence*PerPop 10
			BC*PerPop 20
			Production*PerPop 30
		}
	}
	Effect Control_3
	{
		Name Complex_Bureaucracy
		Benefit 
		{
			Influence*PerPop 20
			BC*PerPop 10
			Production*PerPop 40
		}
	}
	Effect Control_4
	{
		Name Planned_Economy
		Benefit 
		{
			Influence*PerPop 30
			Production*PerPop 50
		}
	}
	Effect Corruption_0
	{
		Name No_Corruption
		Benefit 
		{
			Happiness 10
		}
	}
	Effect Corruption_1
	{
		Name Some_Corruption
		Benefit 
		{
			Inequality 10
		}
		 25
	}
	Effect Corruption_2
	{
		Name High_Corruption
		Benefit 
		{
			Happiness -10
			Inequality 20
			Influence*Penalty 25
			Production*Penalty 25
		}
		 50
	}
	Effect Corruption_3
	{
		Name Rampant_Corruption
		Benefit 
		{
			Happiness -10
			Inequality 30
			Influence*Penalty 50
			Production*Penalty 50
		}
		 70
	}
	Effect Corruption_4
	{
		Name Systematc_Corruption
		Benefit 
		{
			Happiness -20
			Inequality 40
			Influence*Penalty 75
			Production*Penalty 75
		}
		 90
	}
	Effect Happiness_0
	{
		Name Rioting
		Benefit 
		{
			Control -20
		}
	}
	Effect Happiness_1
	{
		Name Unhappy
		Benefit 
		{
			Control -10
		}
	}
	Effect Happiness_2
	{
		Name Content
	}
	Effect Happiness_3
	{
		Name Happy
		Benefit 
		{
			Corruption -10
			Influence*PerPop 10
		}
	}
	Effect Happiness_4
	{
		Name Ecstatic
		Benefit 
		{
			Corruption -20
			Influence*PerPop 20
		}
	}
	Effect Wealth_0
	{
		Name Poor
		Benefit 
		{
			Corruption 20
		}
	}
	Effect Wealth_1
	{
		Name Austerity
		Benefit 
		{
			Corruption 10
			Happiness 10
			BC*PerPop 10
		}
	}
	Effect Wealth_2
	{
		Name Confort
		Benefit 
		{
			Happiness 20
			BC*PerPop 20
		}
	}
	Effect Wealth_3
	{
		Name Luxury
		Benefit 
		{
			Happiness 30
			BC*PerPop 30
		}
	}
	Effect Wealth_4
	{
		Name Abundance
		Benefit 
		{
			Happiness 40
			BC*PerPop 40
		}
	}
	Effect Inequality_0
	{
		Name Equal_Society
	}
	Effect Inequality_1
	{
		Name Low_Inequality
		Benefit 
		{
			Happiness -10
		}
	}
	Effect Inequality_2
	{
		Name Medium_Inequality
		Benefit 
		{
			Happiness -20
		}
	}
	Effect Inequality_3
	{
		Name High_Inequality
		Benefit 
		{
			Corruption 10
			Happiness -30
			Influence*PerPop 10
		}
	}
	Effect Inequality_4
	{
		Name Stratified_Society
		Benefit 
		{
			Control -10
			Corruption 20
			Happiness -40
			Influence*PerPop 30
		}
	}
}
