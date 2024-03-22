using Godot;
using Godot.Collections;
using System.Linq;

// Editor
public partial class UIPulse : Control
{
    [Export]
    public float Frequency = 2.0f;
    [Export]
    public float SizeMax = 1.0f;
    [Export]
    public float SizeMin = 0.65f;

    [Export]
    public bool On = false;

    public override void _Process(double delta)
    {
        float time = 0.001f * Time.GetTicksMsec();
        if (On)
        {
            //Scale = (SizeMin + 0.5f * (1.0f + Mathf.Sin(time * Frequency * Mathf.Pi)) * (SizeMax - SizeMin)) * Vector2.One;
            float level = (SizeMin + 0.5f * (1.0f + Mathf.Sin(time * Frequency * Mathf.Pi)) * (SizeMax - SizeMin));
            SelfModulate = new Color(level, level, level);
        }
    }

    public void TurnOn()
    {
        On = true;
    }

    public void TurnOff()
    {
        On = false;
        Scale = Vector2.One;
    }
}
