using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;

    public class RangeUnit : Unit
    {

        // constructor
        public RangeUnit(int xpostion, int ypostion, int hp, int speed, int attack, int attackrange, char faction, char symbol, string name) : base(xpostion, ypostion, hp, speed, attack, attackrange, faction, symbol, name)
        {
        }

        //deconstructor
        ~RangeUnit()
        {

        }

        //methords
        //movement
        public override int Move(Unit enemy)
        {
            //0 is no movement, 1 is up,2 is down, 3 is left, 4 is right
            int moveonMap = 0;
            int distanceX;
            int distanceY;
            int tempX = Xpostion;
            int tempY = Ypostion;
            distanceX = Math.Abs(this.Xpostion - enemy.Xpostion);
            distanceY = Math.Abs(this.Ypostion - enemy.Ypostion);
            //move in x
            if (distanceX > distanceY && distanceX > 0 && this.Xpostion != 0)
            {
                return moveonMap = 3;
            }
            else if (this.Xpostion != 19)
            {
                return moveonMap = 4;
            }

            if (distanceY == 0 && this.Xpostion != 0)
            {
                return moveonMap = 3;
            }
            else if (this.Xpostion != 19)
            {
                return moveonMap = 4;
            }

            if (distanceX == distanceY && this.Xpostion != 0)
            {
                return moveonMap = 3;
            }
            else if (this.Xpostion != 19)
            {
                return moveonMap = 4;
            }

            //move in y
            if (distanceX < distanceY && distanceY > 0 && this.Ypostion != 0)
            {
                return moveonMap = 1;
            }
            else if (this.Ypostion != 19)
            {
                return moveonMap = 2;
            }

            if (distanceX == 0 && this.Ypostion != 0)
            {
                return moveonMap = 1;
            }
            else if (this.Ypostion != 19)
            {
                return moveonMap = 2;
            }

            return moveonMap;
        }

        //does damage
        public override int Combat(Unit enemy)
        {
            int Hpleft;
            Hpleft = enemy.Hp - this.Attack;
            return Hpleft;
        }

        // checks for attack range
        public override bool CheckingforAttackrange(Unit enemy)
        {
            int distance;
            distance = (Math.Abs(this.Xpostion - enemy.Xpostion) + Math.Abs(this.Ypostion - enemy.Ypostion));
            if (distance == this.Attackrange)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        // checks for closest unit
        public override int WhoisclosestUnit(Unit[] Array1)
        {
            int TempClosest = 40;
            int distance;
            int unit = 1;
            int count = 0;
            for (int i = 0; i < Array1.Length; i++)
            {
                if (Array1[i] != null && Array1[i].Isdead() != true && Array1[i] != this && Array1[i].Faction != this.Faction)
                {
                    distance = (Math.Abs(this.Xpostion - Array1[i].Xpostion) + Math.Abs(this.Ypostion - Array1[i].Ypostion));

                    if (distance < TempClosest)
                    {
                        distance = TempClosest;
                        unit = count;
                       
                    }
                }
                count++;
            }
            return unit;
        }

        //checks for unit to flee
        public override bool Fleeing()
        {
            if (Hp <= Maxhp / 0.25)
            {

                return true;
            }
            else
            {
                return false;
            }
        }

        //checks if unit is dead
        public override bool Isdead()
        {
            if (Hp <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Checks if unit is in combat
        public override bool IsUnitinCombat(Unit enemy)
        {
            if (this.CheckingforAttackrange(enemy) == true)
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
            return ("I am " + Name + "I have the following stats" + Hp + Speed + Attack + Attackrange + "My and display symbol and faction is" + Symbol + Faction + "My cordinates are" + Xpostion + Ypostion);
        }

        //saving
        public override string SaveString()
        {
            return (Name + "," + Speed + "," + Attack + "," + Attackrange + "," + Symbol + "," + Faction + "," + Xpostion + "," + Ypostion);
        }
        public override void Save()
        {
            if (!Directory.Exists("Saves"))
            {
                Directory.CreateDirectory("Saves");
                Console.WriteLine("New Directory Created");
            }
            if (!File.Exists("Saves/RangeUnit.txt"))
            {
                File.Create("Saves/RangeUnit.txt").Close();
                Console.WriteLine("Created file");
            }
            FileStream saveRangeUnit = new FileStream("Saves/RangeUnit.txt", FileMode.Open, FileAccess.Write);
            StreamWriter save = new StreamWriter(saveRangeUnit);
            StreamReader read = new StreamReader(saveRangeUnit);
            if (saveRangeUnit.Length != 0)
            {
                string line = read.ReadLine();
                while (line != null)
                {
                    line = read.ReadLine();
                }
                save.WriteLine(SaveString());
                save.Close();
                saveRangeUnit.Close();
            }
            save.WriteLine(SaveString());
            save.Close();
            saveRangeUnit.Close();
        }

        public override void Read()
        {
            FileStream readRangeUnit = new FileStream("Saves/RangeUnit.txt", FileMode.Open, FileAccess.Read);
            StreamReader read = new StreamReader(readRangeUnit);
            string line = read.ReadLine();
            while (line != null)
            {
                string[] range = line.Split(',');
                RangeUnit newRange = new RangeUnit(Convert.ToInt32(range[0]), Convert.ToInt32(range[1]), Convert.ToInt32(range[2]), Convert.ToInt32(range[3]), Convert.ToInt32(range[4]), Convert.ToInt32(range[5]), Convert.ToChar(range[6]), Convert.ToChar(range[7]), range[8]);
                line = read.ReadLine();
            }
            readRangeUnit.Close();
        }
    }
