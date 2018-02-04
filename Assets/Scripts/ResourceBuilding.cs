using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Timers;
using System.IO;

    public class ResourceBuilding : Building
    {
    // varibles
    protected int amount;
    protected string resourceType = "gold";
    private int maxResources = 1000;
    private int resourcesLeft;
    private int resourcesPerTick = 5;

    //properties
    protected int Amount
    {
        get
        {
            return amount;
        }

        set
        {
            amount = value;
        }
    }

    public string ResourceType
    {
        get
        {
            return resourceType;
        }

        set
        {
            resourceType = value;
        }
    }

    public int MaxResources
    {
        get
        {
            return maxResources;
        }

        set
        {
            maxResources = value;
        }
    }

    public int ResourcesLeft
    {
        get
        {
            return resourcesLeft;
        }

        set
        {
            resourcesLeft = value;
        }
    }

    public int ResourcesPerTick
    {
        get
        {
            return resourcesPerTick;
        }

        set
        {
            resourcesPerTick = value;
        }
    }

    //constructor
    public ResourceBuilding(int xpos, int ypos, int health, char faction, char symbol, string resourceType, int resourcesLeft, int resourcesPerTick, int maxResources) : base(xpos, ypos, health, faction, symbol)
        {
        }

        //deconstructor
        ~ResourceBuilding()
        {

        }

        //methords
        //resource generation
        public void GenerateResources()
        {
            Amount += ResourcesPerTick;
            ResourcesLeft = ResourcesLeft - ResourcesPerTick;
            Console.WriteLine(ToString());
        }

        public override bool IsDead()
        {
            if (Health <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return ("I can generate" + MaxResources + ResourceType + ". I have" + ResourcesLeft + "resources remaining, so I have generated" + Amount + ResourceType + "I have the Following stats and display symbols and faction" + Health + Symbol + Faction);
        }
        
        public void ConCode()
        {
            Console.WriteLine("I can generate" + MaxResources + ResourceType + ". I have" + ResourcesLeft + "resources remaining, so I have generated" + Amount + ResourceType + "I have the Following stats and display symbols and faction" + Health + Symbol + Faction);
        }

        //saving
        public string saveString()
        {
            return (Health + "," + Symbol + "," + Faction + "," + Xpos + "," + Ypos + "," + ResourceType + "," + ResourcesLeft + "," + Amount + "," + MaxResources);
        }


        public override void Save()
        {
           
            FileStream saveResourceBuild = new FileStream("Saves/ResourceBuilding.txt", FileMode.Open, FileAccess.Write);
            StreamWriter save = new StreamWriter(saveResourceBuild);
            save.WriteLine(saveString());
            save.Close();
            saveResourceBuild.Close();
        }

        public override void Load()
        {
            FileStream readResourceBuild = new FileStream("Saves/ResourceBuilding.txt", FileMode.Open, FileAccess.Read);
            StreamReader read = new StreamReader(readResourceBuild);
            string line = read.ReadLine();
            while (line != null)
            {
                string[] resc = line.Split(',');
                ResourceBuilding newRes =new ResourceBuilding(Convert.ToInt32(resc[0]), Convert.ToInt32(resc[1]), Convert.ToInt32(resc[2]), Convert.ToChar(resc[3]), Convert.ToChar(resc[4]), resc[5], Convert.ToInt32(resc[6]), Convert.ToInt32(resc[7]),Convert.ToInt32(resc[8]));
                line = read.ReadLine();
            }
            readResourceBuild.Close();
        }
    }
