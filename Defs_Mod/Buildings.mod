Def_Buildings 
{
	// Ej - Gerothermal_Vents x4
	// E - Active star x2
	// E - Rich gases
	// Mj - Mineral_Veins x4
	// M - Rings
	// M - Minerals x2
	// Rj - Life study x3
	// SR-E - Deuterium
	// SR-E - Anti-Matter

	// Colony
	// Infrastructure 5 / 10 / 20 / 50 / 100 / 200 / 500 / 1000 - accelerated growth until 5/10/20... pops
	// 

	// Empire Capitals

	// Active
	Building Flare_Catcher
	{
		Require
		{
			Planet:Feature Active
		}
		Cost 
		{
			Production 2500
		}
		Benefit Planet
		{
			Energy 100
		}
	}
	Building Flare_Catcher_II
	{
		Replace Flare_Catcher
		Cost 
		{
			Production 5000
		}
		Benefit Planet
		{
			Energy 200
		}
	}

	// HyperActive
	Building Hyper_Flare_Catcher
	{
		Require
		{
			Planet:Feature Active
		}
		Cost 
		{
			Production 2500
		}
		Benefit Planet
		{
			Energy 200
		}
	}
	Building Hyper_Flare_Catcher_II
	{
		Replace Hyper_Flare_Catcher
		Cost 
		{
			Production 5000
		}
		Benefit Planet
		{
			Energy 400
		}
	}
	Building Hyper_Flare_Catcher_III
	{
		Replace Hyper_Flare_Catcher_II
		Cost 
		{
			Production 10000
		}
		Benefit Planet
		{
			Energy 600
		}
	}

	// Rich_Gases
	Building Gas_Extractor
	{
		Require
		{
			Planet:Feature Rich_Gases
		}
		Cost 
		{
			Production 2500
		}
		Benefit Planet
		{
			Energy 150
		}
	}
	Building Gas_Extractor_II
	{
		Replace Gas_Extractor
		Cost 
		{
			Production 5000
		}
		Benefit Planet
		{
			Energy 300
		}
	}

	// Ultra_Rich_Gases
	Building Ultra_Gas_Extractor
	{
		Require
		{
			Planet:Feature Ultra_Rich_Gases
		}
		Cost 
		{
			Production 2500
		}
		Benefit Planet
		{
			Energy 250
		}
	}
	Building Ultra_Gas_Extractor_II
	{
		Replace Gas_Extractor
		Cost 
		{
			Production 5000
		}
		Benefit Planet
		{
			Energy 500
		}
	}
	Building Ultra_Gas_Extractor_III
	{
		Replace Ultra_Gas_Extractor_II
		Cost 
		{
			Production 10000
		}
		Benefit Planet
		{
			Energy 750
		}
	}

	// Rings
	Building Ring_Mineral_Collector
	{
		Require
		{
			Planet:Feature Rings
		}
		Cost 
		{
			Production 2500
		}
		Benefit Planet
		{
			Minerals 100
		}
	}
	Building Ring_Mineral_Collector_II
	{
		Replace Ring_Mineral_Collector
		Cost 
		{
			Production 5000
		}
		Benefit Planet
		{
			Minerals 200
		}
	}

	// Large_Rings
	Building Large_Ring_Mineral_Collector
	{
		Require
		{
			Planet:Feature Large_Rings
		}
		Cost 
		{
			Production 2500
		}
		Benefit Planet
		{
			Minerals 200
		}
	}
	Building Large_Ring_Mineral_Collector_II
	{
		Replace Ring_Mineral_Collector
		Cost 
		{
			Production 5000
		}
		Benefit Planet
		{
			Minerals 400
		}
	}
	Building Large_Ring_Mineral_Collector_III
	{
		Replace Large_Ring_Mineral_Collector_II
		Cost 
		{
			Production 1000
		}
		Benefit Planet
		{
			Minerals 600
		}
	}


	// Small_Asteroids
	Building Small_Asteroids_Mines
	{
		Require
		{
			Planet:Feature Small_Asteroids
		}
		Cost 
		{
			Production 2000
		}
		Benefit Planet
		{
			Minerals 150
		}
	}
	Building Small_Asteroids_Mines_II
	{
		Replace Small_Asteroids_Mines
		Cost 
		{
			Production 4000
		}
		Benefit Planet
		{
			Minerals 300
		}
	}

	// Average_Asteroids
	Building Asteroids_Mines
	{
		Require
		{
			Planet:Feature Average_Asteroids
		}
		Cost 
		{
			Production 4000
		}
		Benefit Planet
		{
			Minerals 300
		}
	}
	Building Asteroids_Mines_II
	{
		Replace Asteroids_Mines
		Cost 
		{
			Production 8000
		}
		Benefit Planet
		{
			Minerals 600
		}
	}
	Building Asteroids_Mines_III
	{
		Replace Asteroids_Mines_II
		Cost 
		{
			Production 16000
		}
		Benefit Planet
		{
			Minerals 900
		}
	}

	// Large_Asteroids
	Building Large_Asteroids_Mines
	{
		Require
		{
			Planet:Feature Large_Asteroids
		}
		Cost 
		{
			Production 6000
		}
		Benefit Planet
		{
			Minerals 500
		}
	}
	Building Large_Asteroids_Mines_II
	{
		Replace Large_Asteroids_Mines
		Cost 
		{
			Production 12000
		}
		Benefit Planet
		{
			Minerals 1000
		}
	}
	Building Large_Asteroids_Mines_III
	{
		Replace Large_Asteroids_Mines_II
		Cost 
		{
			Production 24000
		}
		Benefit Planet
		{
			Minerals 1500
		}
	}
	Building Large_Asteroids_Mines_IV
	{
		Replace Large_Asteroids_Mines_III
		Cost 
		{
			Production 36000
		}
		Benefit Planet
		{
			Minerals 2000
		}
	}
	Building Large_Asteroids_Mines_V
	{
		Replace Large_Asteroids_Mines_IV
		Cost 
		{
			Production 48000
		}
		Benefit Planet
		{
			Minerals 2500
		}
	}

	// Gold_Asteroid
	Building Gold_Asteroid_Mine
	{
		Require
		{
			Planet:Feature Gold_Asteroid
		}
		Cost 
		{
			Production 5000
		}
		Benefit Planet
		{
			BC 250
		}
	}
	// Gold_Deposit
	Building Gold_Deposit_Mine
	{
		Require
		{
			Planet:Feature Gold_Deposit
		}
		Cost 
		{
			Production 5000
		}
		Benefit Planet
		{
			Minerals 200
		}
	}
}
