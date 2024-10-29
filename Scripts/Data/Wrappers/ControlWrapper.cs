using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;

public class ControlWrapper
{
    public SystemData _System = null;

    public int Economy_Level = 0;
    public int Economy_LockTimer = 0;
    public int State_Level = 0;
    public int State_LockTimer = 0;
    public int Social_Level = 0;
    public int Social_LockTimer = 0;
    public int Migration_Level = 0;
    public int Migration_LockTimer = 0;

    public int Control = 0;
    public int Corruption = 0;
    public int Happiness = 0;
    public int Wealth = 0;
    public int Inequality = 0;

    public ControlWrapper(SystemData system)
    {
        _System = system;

        //Refresh();
    }

    public void Clear()
    {
        Economy_Level = 0;
        Economy_LockTimer = 0;
        State_Level = 0;
        State_LockTimer = 0;
        Social_Level = 0;
        Social_LockTimer = 0;
        Migration_Level = 0;
        Migration_LockTimer = 0;

        Control = 0;
        Corruption = 0;
        Happiness = 0;
        Wealth = 0;
        Inequality = 0;
    }

    public void Refresh()
    {
        Clear();

        if (_System != null)
        {
            DataBlock controlData =_System.Data.GetSub("Control");
            Economy_Level = controlData.GetSub("Economy_Level").ValueI;
            Economy_LockTimer = controlData.GetSub("Economy_Lock_Timer").ValueI;
            State_Level = controlData.GetSub("State_Level").ValueI;
            State_LockTimer = controlData.GetSub("State_Lock_Timer").ValueI;
            Social_Level = controlData.GetSub("Social_Level").ValueI;
            Social_LockTimer = controlData.GetSub("Social_Lock_Timer").ValueI;
            Migration_Level = controlData.GetSub("Migration_Level").ValueI;
            Migration_LockTimer = controlData.GetSub("Migration_Lock_Timer").ValueI;

            Control = controlData.GetSub("Control").ValueI;
            Corruption = controlData.GetSub("Corruption").ValueI;
            Happiness = controlData.GetSub("Happiness").ValueI;
            Wealth = controlData.GetSub("Wealth").ValueI;
            Inequality = controlData.GetSub("Inequality").ValueI;
        }
    }

    public DefEffectWrapper GetEconomyEffect()
    {
        return Game.self.Def.GetEffectInfo("Economy_" + Economy_Level.ToString());
    }

    public DefEffectWrapper GetStateEffect()
    {
        return Game.self.Def.GetEffectInfo("State_" + State_Level.ToString());
    }

    public DefEffectWrapper GetSocialEffect()
    {
        return Game.self.Def.GetEffectInfo("Social_" + Social_Level.ToString());
    }

    public DefEffectWrapper GetMigrationEffect()
    {
        return Game.self.Def.GetEffectInfo("Migration_" + Migration_Level.ToString());
    }

    public DefEffectWrapper GetControlEffect()
    {
        return Game.self.Def.GetEffectInfo("Control_" + ((Control - 10) / 200).ToString());
    }

    public DefEffectWrapper GetCorruptionEffect()
    {
        return Game.self.Def.GetEffectInfo("Corruption_" + ((Corruption - 10) / 200).ToString());
    }

    public DefEffectWrapper GetHappinessEffect()
    {
        return Game.self.Def.GetEffectInfo("Happiness_" + ((Happiness - 10) / 200).ToString());
    }

    public DefEffectWrapper GetWealthEffect()
    {
        return Game.self.Def.GetEffectInfo("Wealth_" + ((Wealth - 10) / 200).ToString());
    }

    public DefEffectWrapper GetInequalityEffect()
    {
        return Game.self.Def.GetEffectInfo("Inequality_" + ((Inequality - 10) / 200).ToString());
    }

    public string ToString_Control() { return Helper.ResValueToString(Control); }
    public string ToString_Corruption() { return Helper.ResValueToString(Corruption); }
    public string ToString_Happiness() { return Helper.ResValueToString(Happiness); }
    public string ToString_Wealth() { return Helper.ResValueToString(Wealth); }
    public string ToString_Inequality() { return Helper.ResValueToString(Inequality); }
}
