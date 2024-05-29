// ----------------------------------------------- mods
	Mod WorldPlanet
	{
		OR:Perc 50
		{
			OR:Weight 2
			{
				Tiny_Moon:Weight 2
				Small_Moon:Weight 1
			}
			OR:Weight 2
			{
				Good_Iron:Weight 2
				Rich_Iron:Weight 1
			}
			OR:Weight 2
			{
				Strange:Weight 2
				Bizarre:Weight 1
			}
			OR:Weight 2
			{
				Beautiful:Weight 2
				Paradise:Weight 1
			}
			OR:Weight 2
			{
				Lush:Weight 2
				Fertile:Weight 1
			}
			High_Gravity:Weight 1
			Poor_Iron:Weight 1
			Boring:Weight 1
			Smelly:Weight 1
			Semi-Barren:Weight 1
		}
	}
	Mod Rings
	{
		Building:Ring_Minerals
	}