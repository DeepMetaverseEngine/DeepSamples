using DeepCore.GameData.Zone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class NpcMapData
{
    public int templateId;
    public string name;
    public string Icon;
    public float X;
    public float Y;
}

public class MonsterMapData
{
    public int templateId;
    public string name;
    public int level;
    public float X;
    public float Y;
}


public class PlayerMapData
{
    public int templateId;
    public string name;

    public int Level;
    public int Sro;
    public int Pro;
    public float X;
    public float Y;

}

public class TeamInfoMapData
{
    public string uuid;
    public string name;
    public float X;
    public float Y;
    
}

public class PlayMapUnitData
{
    public int templateId;
    public uint ID;
    public string Name;
    public string ICON;
    public float X;
    public float Y;
    public UnitInfo.UnitType UnitType;
    public int ForceType;
    public bool isTeamMate;

}