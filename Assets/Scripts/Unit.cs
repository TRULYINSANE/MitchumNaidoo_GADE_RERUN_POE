using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

    public abstract class Unit : MonoBehaviour
    {
    // varibles
    protected int xpostion;
    protected int ypostion;
    protected int hp;
    protected int speed;
    protected int attack;
    protected int attackrange;
    protected char faction;
    protected char symbol;
    protected bool inCombat;
    protected int maxhp;
    protected string name;

    public int Xpostion
    {
        get
        {
            return xpostion;
        }

        set
        {
            xpostion = value;
        }
    }

    public int Ypostion
    {
        get
        {
            return ypostion;
        }

        set
        {
            ypostion = value;
        }
    }

    public int Hp
    {
        get
        {
            return hp;
        }

        set
        {
            hp = value;
        }
    }

    protected int Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }

    protected int Attack
    {
        get
        {
            return attack;
        }

        set
        {
            attack = value;
        }
    }

    protected int Attackrange
    {
        get
        {
            return attackrange;
        }

        set
        {
            attackrange = value;
        }
    }

    public char Faction
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

    protected bool InCombat
    {
        get
        {
            return inCombat;
        }

        set
        {
            inCombat = value;
        }
    }

    public int Maxhp
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

    protected string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }

    //Constructor
    public Unit(int xpostion, int ypostion, int hp, int speed, int attack, int attackrange, char faction, char symbol, string name)
        {
            this.Xpostion = xpostion;
            this.Ypostion = ypostion;
            this.Hp = hp;
            this.Speed = speed;
            this.Attack = attack;
            this.Attackrange = attackrange;
            this.Faction = faction;
            this.Symbol = symbol;
            this.Name = name;
        }

        //deconstructor
        ~Unit()
        {

        }

        //methords
        public abstract int Move(Unit enemy);
        public abstract int Combat(Unit enemy);
        public abstract int WhoisclosestUnit(Unit[] Array1);
        public abstract bool CheckingforAttackrange(Unit enemy);
        public abstract bool Fleeing();
        public abstract bool Isdead();
        public abstract string ToString();
        public abstract bool IsUnitinCombat(Unit enemy);
        public abstract void Save();
        public abstract string SaveString();
        public abstract void Read();
    public abstract int KillBuild(Building build);
    public abstract int NearBuilding(Building[] Array2);


    }
