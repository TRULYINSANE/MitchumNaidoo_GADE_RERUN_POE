using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;


    public class FactoryBuilding : Building
    {
        //varibles
        private string unitType;
        private int spawnRate;
        private int xposofSpawnedunit;
        private int yposofSpawnedUnit;
        System.Random r = new System.Random();

    public string UnitType
    {
        get
        {
            return unitType;
        }

        set
        {
            unitType = value;
        }
    }

    public int SpawnRate
    {
        get
        {
            return spawnRate;
        }

        set
        {
            spawnRate = value;
        }
    }

    public int XposofSpawnedunit
    {
        get
        {
            return xposofSpawnedunit;
        }

        set
        {
            xposofSpawnedunit = value;
        }
    }

    public int YposofSpawnedUnit
    {
        get
        {
            return yposofSpawnedUnit;
        }

        set
        {
            yposofSpawnedUnit = value;
        }
    }



    //constructor
    public FactoryBuilding(int xpos, int ypos, int health, char faction, char symbol, string unitType, int spawnRate) : base(xpos, ypos, health, faction, symbol)
        {
            spawnRate = SpawnRate = 1;
            unitType = UnitType;
        }

        //deconstructor
        ~FactoryBuilding()
        {

        }

        //methods
        //creates units
        public Unit SpawnUnit()
        {
            int tempX = Xpos;
            int tempY = Ypos + 1;
            int Unittype = r.Next(0, 2);
            Unit k;
            if (Ypos == 19)
            {
                tempY = Ypos - 1;
            }
            if (Unittype == 0)
            {
                if (Faction == 'H')
                {
                    k = new MeleeUnit(tempX, tempY, 10, 1, 2, 1, 'H', 'M', "knight");
                    return k;
                }
                else
                {
                    k = new MeleeUnit(tempX, tempY, 10, 1, 2, 1, 'h', 'm', "samuri");
                    return k;
                }
            }
            else if (Unittype == 1)
            {
                if (Faction == 'H')
                {
                    k = new RangeUnit(tempX, tempY, 8, 1, 1, 2, 'H', 'R', "gunman");
                    return k;
                }
                else
                {
                    k = new RangeUnit(tempX, tempY, 8, 1, 1, 2, 'h', 'r', "bowman");
                    return k;
                }
            }
            else
            {
                return null;
            }

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
            return ("I am a FactoryBuilding I have the following stats" + Health + "My and display symbol and faction is" + Symbol + Faction);
        }

        //saving
        public string saveString()
        {
            return (Health + "," + Symbol + "," + Faction + "," + Xpos + "," + Ypos);
        }

        public override void Save()
        {            
            FileStream saveFactoryBuid = new FileStream("Saves/FactoryBuilding.txt", FileMode.Open, FileAccess.Write);
            StreamWriter save = new StreamWriter(saveFactoryBuid);
            save.WriteLine(saveString());
            save.Close();
            saveFactoryBuid.Close();
        }

        public override void Load()
        {
            FileStream readResourceBuild = new FileStream("Saves/FactoryBuilding.txt", FileMode.Open, FileAccess.Read);
            StreamReader read = new StreamReader(readResourceBuild);
            string line = read.ReadLine();
            while (line != null)
            {
                string[] fac = line.Split(',');
                FactoryBuilding newRes = new FactoryBuilding(Convert.ToInt32(fac[0]), Convert.ToInt32(fac[1]), Convert.ToInt32(fac[2]), Convert.ToChar(fac[3]), Convert.ToChar(fac[4]), fac[5], Convert.ToInt32(fac[6]));
                line = read.ReadLine();
            }
            readResourceBuild.Close();
        }
    }