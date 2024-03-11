using Godot;
using Godot.Collections;
using System;

[Tool]
public partial class DefUIPlanets : Resource
{
    [Export]
    public Texture2D BacgroundTexture = null;
    [Export]
    public Texture2D BacgroundMoonTexture = null;
    [Export]
    public Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();


    [Export]
    public bool LoadTexures
    {
        get => false;
        set
        {
            if (value)
            {
                LoadTexuresFunc();
            }
        }
    }

    private void LoadTexuresFunc()
    {
        BacgroundTexture = (Texture2D)GD.Load("res://Assets/Planets/Background.png");
        BacgroundMoonTexture = (Texture2D)GD.Load("res://Assets/Planets/BackgroundMoon.png");

        Textures.Clear();

        Textures.Add("Star", (Texture2D)GD.Load("res://Assets/Planets/Star.png"));
        Textures.Add("Lava", (Texture2D)GD.Load("res://Assets/Planets/Lava.png"));
        Textures.Add("Vulcanic", (Texture2D)GD.Load("res://Assets/Planets/Vulcanic.png"));
        Textures.Add("Barren", (Texture2D)GD.Load("res://Assets/Planets/Barren.png"));
        Textures.Add("Toxic", (Texture2D)GD.Load("res://Assets/Planets/Toxic.png"));
        Textures.Add("Desert", (Texture2D)GD.Load("res://Assets/Planets/Desert.png"));
        Textures.Add("Arid", (Texture2D)GD.Load("res://Assets/Planets/Arid.png"));
        Textures.Add("Continents", (Texture2D)GD.Load("res://Assets/Planets/Temperate.png"));
        Textures.Add("Swamp", (Texture2D)GD.Load("res://Assets/Planets/Swamp.png"));
        Textures.Add("Jungle", (Texture2D)GD.Load("res://Assets/Planets/Jungle.png"));
        Textures.Add("Ocean", (Texture2D)GD.Load("res://Assets/Planets/Ocean.png"));
        Textures.Add("Artic", (Texture2D)GD.Load("res://Assets/Planets/Artic.png"));
        Textures.Add("Frozen", (Texture2D)GD.Load("res://Assets/Planets/Frozen.png"));
        Textures.Add("Gas_Giant", (Texture2D)GD.Load("res://Assets/Planets/GasGiant.png"));
        Textures.Add("Asteroid_Field", (Texture2D)GD.Load("res://Assets/Planets/AsteroidBelt.png"));
        Textures.Add("Outer_System", (Texture2D)GD.Load("res://Assets/Planets/OuterSystem.png"));

        Textures.Add("Terra", (Texture2D)GD.Load("res://Assets/Planets/Terra.png"));
    }

    public Texture2D GetPlanetTexture(string type)
    {
        Texture2D tex;
        Textures.TryGetValue(type, out tex);
        return tex;
    }
}