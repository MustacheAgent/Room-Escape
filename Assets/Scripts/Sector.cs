using System;
using System.Collections.Generic;

[Serializable]
public class Sector
{
    public float angle;
    public List<Dissolve> dissolveObjects;

    public void SwitchObjects(bool hide)
    {
        foreach (var obj in dissolveObjects)
        {
            if (hide) obj.Hide();
            else obj.Show();
        }
    }
}