Map > GameStats > Turn > int
Map > GameStats > Human > int
Map > GalaxySize > int
Map > Star_List []
Map > Star_List > Star > Planet_List []
Map > Player_List []
	


Star > name
Star > id
Star > GFX_RNG_1
Star > GFX_RNG_2
Star > X
Star > Y

Star > Planet_List []

	

Planet > Type
Planet > Size
Planet > Feature_1
Planet > Feature_2
Planet > Feature_3
Planet > Building ------------------ possible building



Player > Human
Player > Empire > Flag
Player > Resources ----------------- stockpiles
Player > Status  ------------------- X
Player > Civics  ------------------- X
Player > Bonuses  ------------------- X
Player > Designs []
Player > Sector_List []
Player > Fleets []


+ Player > Faction_List []
+ Player > Faction_List > Faction 
+ Player > Faction_List > Faction > Name Faction_Name
+ Player > Faction_List > Faction > Ethic_1 Lib_Ethic_Name
+ Player > Faction_List > Faction > Ethic_2 Lib_Ethic_Name
+ Player > Faction_List > Faction > Pops []
+ Player > Faction_List > Faction > Pops > Link:Player:Sector:System:Colony:PopGroup


Sector > Resources  ----------------- total sector incomes
Sector > ActionBuildQueue > Overflow
Sector > ActionBuildQueue > Queue []
Sector > ActionBuildQueue > Queue > Building
Sector > ActionBuildQueue > Queue > Building > Link:Star:Planet
Sector > ActionBuildQueue > Queue > Building > Progress:Max
Sector > ActionBuildQueue > Queue > Building > OldBuilding
Sector > System_List []
Sector > System_List > System
Sector > System_List > System > Resources ---- can be removed
Sector > System_List > System > Link:Star
Sector > System_List > System > Colony_List []



Colony > Type
Colony > Resources ----------------- total colony incomes
Colony > Buildings []
Colony > Buildings > Building BuldingName_1
Colony > Buildings > Building BuldingName_2
Colony > Buildings > Building Bulding_AboutToBeReplaced
Colony > Buildings > Building Bulding_AboutToBeReplaced > NewBuilding Bulding_InConstruction
Colony > Buildings > Building BuldingName_3
Colony > Buildings > Building BuldingName_4
Colony > Buildings > Building Bulding_InConstruction
Colony > Buildings > Building Bulding_InConstruction > InConstruction
Colony > Buildings > Building Bulding_InConstruction > InConstruction > Progress:Max
Colony > Buildings > Building Bulding_InConstruction > InConstruction > Progress
Colony > Support ------------------- X
Colony > Link:Star:Planet


+ Colony > Pops []
+ Colony > Pops > Pop_Group Faction_Name
+ Colony > Pops > Pop_Group Faction_Name > Link:Player
+ Colony > Pops > Pop_Group Faction_Name > Jobs []
+ Colony > Pops > Pop_Group Faction_Name > Jobs > Job
+ Colony > Pops > Pop_Group Faction_Name > Jobs > Job > Count int
+ Colony > Pops > Pop_Group Faction_Name > Jobs > Job > Link:Player:Sector:System:Colony:Building



Design > ShipType
Design > Modules []
Design > Modules > Computer
Design > Modules > Armor
Design > Modules > ECM
Design > Modules > Shield
Design > Modules > Warp_Drive
Design > Modules > Thrusters
Design > Modules > Weapon:1:Type
Design > Modules > Weapon:1:Count
Design > Modules > Weapon:2:Type
Design > Modules > Weapon:2:Count
Design > Modules > Weapon:3:Type
Design > Modules > Weapon:3:Count
Design > Modules > Special:1
Design > Modules > Special:2
			

Fleet > Link:Star Location_Star_Name
Fleet > Ship Ship_1_Name
Fleet > Ship Ship_1_Name > Design DsignName
Fleet > Ship Ship_1_Name > HP
Fleet > Ship Ship_1_Name > XP
Fleet > Ship Ship_1_Name > Marines
Fleet > Ship Ship_1_Name > Fighters
Fleet > Ship Ship_1_Name > Supply
Fleet > Ship Ship_2_Name
Fleet > Ship Ship_2_Name > ...
Fleet > Ship Ship_3_Name
Fleet > Ship Ship_3_Name > ...
Fleet > ActionMove
Fleet > ActionMove > Progress
Fleet > ActionMove > ProgressMax
Fleet > ActionMove > Link:Star Destination_Star_Name