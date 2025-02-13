using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public abstract class ActionBase
{
    public abstract void ExecuteOrder();

    public abstract string ToUI_Title(int ID = 0);
}