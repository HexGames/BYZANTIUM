using Godot;

public partial class MapCamera : Camera3D
{
    private float LODTransition = 0.0f;
    public void ProcessLOD(float delta)
    {
        bool isFar = Position.Y > 40;
        if (isFar)
        {
            LODTransition += Mathf.Clamp(1.0f + (Position.Y - 40.0f) / 5.0f, 1.0f, 8.0f) * delta;
        }
        else
        {
            LODTransition -= Mathf.Clamp(1.0f + (40.0f - Position.Y) / 2.5f, 1.0f, 8.0f) * delta;
        }

        if (LODTransition <= 0.0f)
        {
            LODTransition = 0.0f;
            if (LOD != 0)
            {
                LOD = 0;
                LODClose();
                LODAlpha(0.0f);
            }
            // else do nothing - most of the cases
        }
        else if (LODTransition >= 1.0f)
        {
            LODTransition = 1.0f;
            if (LOD != 2)
            {
                LOD = 2;
                LODFar();
                LODAlpha(1.0f);
            }
            // else do nothing - most of the cases
        }
        else
        {
            LOD = 1;
            LODAlpha(LODTransition);
        }
    }

    private void LODClose()
    {
        if (Game.self == null)
            return;

        for (int idx = 0; idx < Game.self.Map.Data.Stars.Count; idx++)
        {
            Game.self.Map.Data.Stars[idx]._Node.GFX.LODClose();
        }
    }

    private void LODFar()
    {
        if (Game.self == null)
            return;

        for (int idx = 0; idx < Game.self.Map.Data.Stars.Count; idx++)
        {
            Game.self.Map.Data.Stars[idx]._Node.GFX.LODFar();
        }
    }

    private void LODAlpha(float alpha)
    {
        if (Game.self == null)
            return;

        Game.self.Assets.LODPlanetsAlpha(1.0f - alpha);

        for (int idx = 0; idx < Game.self.Map.Data.Stars.Count; idx++)
        {
            Game.self.Map.Data.Stars[idx]._Node.GFX.LODAlpha(alpha);
        }

        GD.Print("LOD ALPHA: " + alpha.ToString());
    }
}