using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;

public class ControlWrapper
{
    public SystemData _System = null;

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
       
    }

    public void Refresh()
    {
        Clear();


    }
}
