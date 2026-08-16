using System;

public class MenuPage
{
    public string Name { get; }
    public string[] Mods { get; }

    public MenuPage(string name, params string[] mods)
    {
        Name = name;
        Mods = mods;
    }
}
