using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public class ActionBase
{
    public enum ID
    {
        DUMMY = -1,
        ECONOMY_COLONIZE
    }

    public ID ActionID = ID.DUMMY;

    public virtual void ExecuteOrder() { }

    public virtual string ToUI_Title(int ID = 0) {  return ""; }
}