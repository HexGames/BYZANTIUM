using Godot;

// Per Turn
public partial class RelationData
{
    public PlayerData _Player_1 = null;
    public PlayerData _Player_2 = null;

    public DataBlock Data = null;

    public int Relation = 100;
    public bool War;
    public bool OpenBorders;

    public RelationData(DataBlock relationData)
    {
        Data = relationData;
        _Player_1 = Game.self.Map.Data.GetPlayer(relationData.GetSubValueS("Player_1"));
        _Player_2 = Game.self.Map.Data.GetPlayer(relationData.GetSubValueS("Player_2"));

        Relation = Data.GetSubValueI("Relation");
        War = Data.HasSub("War");
        OpenBorders = Data.HasSub("OpenBorders");
    }

    public PlayerData GetOtherPlayer(PlayerData player)
    { 
        return _Player_1 == player ? _Player_2 : _Player_1;
    }
}
