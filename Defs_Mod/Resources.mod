Def_SpecialResources
{	
	Names Energy
	{
		Alloy-19
		Iridium-7
		Catalyst-3
		Hyperflux-8
		Omega-1
		Silicate-4
		Brom-2
		Carbon-6
	}
	Names Production
	{
		Durasteel
		Ceramometal
		Xylinium
		Tritonite
		Hyperium
		Tritanium
		Basteel
		Grogonite
	}
	Names Research
	{
		X-Substance
		Y-Matter
		Z-Crystal
		S-Plasma
		N-Solid
		L-Atoms
		M-Fluid
		P-Quarq
	}
	SpecialResource Energy_1
	{
		Building:Suffix Extractor_
		Cost
		{
			Production 20000
		}
		Benefits
		{
			Energy:Perc 5
		}
	}
	SpecialResource Energy_2
	{
		Upgrades Energy_1
		RevealDelay:Min 5
		RevealDelay:Max 10
		Reveal:MaxPerc 50
		RevealTech:Prefix Advanced_
		Building:Suffix _Advanced_Extractor
		Cost
		{
			Production 50000
		}
		Benefits
		{
			Energy:Perc 10
		}
	}
	SpecialResource Energy_3
	{
		Uprgrades Energy_1
		RevealDelay:Min 10
		RevealDelay:Max 20
		Reveal:MaxPerc 50
		RevealTech:Prefix Enhanced_
		Building:Suffix _Enhanced_Extractor
		Cost
		{
			Production 100000
		}
		Benefits
		{
			Energy:Perc 5
			Sector:Energy 10000
		}
	}
	SpecialResource Energy_4
	{
		Uprgrades Energy_1
		RevealDelay:Min 20
		RevealDelay:Max 40
		Reveal:MaxPerc 50
		RevealTech:Prefix Special_
		Building:Suffix _Special_Extractor
		Cost
		{
			Production 200000
		}
		Benefits
		{
			Energy:Perc 50
		}
	}
	SpecialResource Production_1
	{
		Building:Suffix Mine
		Cost
		{
			Production 20000
		}
		Benefits
		{
			Production:Perc 5
		}
	}
	SpecialResource Production_2
	{
		Uprgrades Production_1
		RevealDelay:Min 5
		RevealDelay:Max 10
		Reveal:MaxPerc 50
		RevealTech:Prefix Advanced_
		Building:Suffix _Advanced_Mine
		Cost
		{
			Production 50000
		}
		Benefits
		{
			Production:Perc 5
			Sector:Center:Production 5000
		}
	}
	SpecialResource Production_3
	{
		Uprgrades Production_1
		RevealDelay:Min 10
		RevealDelay:Max 20
		Reveal:MaxPerc 50
		RevealTech:Prefix Enhanced_
		Building:Suffix _Enhanced_Mine
		Cost
		{
			Production 100000
		}
		Benefits
		{
			Production:Perc 15
		}
	}
	SpecialResource Production_4
	{
		Uprgrades Production_1
		RevealDelay:Min 20
		RevealDelay:Max 40
		Reveal:MaxPerc 50
		RevealTech:Prefix Special_
		Building:Suffix _Special_Mine
		Cost
		{
			Production 200000
		}
		Benefits
		{
			Production:Perc 50
		}
	}
}