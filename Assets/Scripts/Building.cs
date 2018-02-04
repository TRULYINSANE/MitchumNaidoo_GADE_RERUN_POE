using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

    public abstract class Building : MonoBehaviour
    {
    // varibles
    protected int xpos;
    protected int ypos;
    protected int health;
    protected char faction;
    protected char symbol;
    protected int maxhp;

    public int MaxHp
    {
        get
        {
            return maxhp;
        }

        set
        {
            maxhp = value;
        }
    }

    public int Xpos
    {
        get
        {
            return xpos;
        }

        set
        {
            xpos = value;
        }
    }

    public int Ypos
    {
        get
        {
            return ypos;
        }

        set
        {
            ypos = value;
        }
    }

    public int Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;
        }
    }

    protected char Faction
    {
        get
        {
            return faction;
        }

        set
        {
            faction = value;
        }
    }

    public char Symbol
    {
        get
        {
            return symbol;
        }

        set
        {
            symbol = value;
        }
    }


    // construcor
    public Building(int xpos, int ypos, int health, char faction, char symbol)
    {
        this.Xpos = xpos;
        this.Ypos = ypos;
        this.Health = health;
        this.Faction = faction;
        this.Symbol = symbol;
    }
    //deconstructor
    ~Building()
    {

    }

    // methords
    public abstract bool IsDead();
    public abstract string ToString();
    public abstract void Save();
    public abstract void Load();
}
