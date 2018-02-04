﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameEngine : MonoBehaviour {

    //varibles
    public int tick = 1;
    protected int i;
    protected int k;
    float offset = 2f;
    private int gametime = 0;
    private const int Refreash = 60;
    System.Random r = new System.Random();

    //Arrays
    //unit
    private Unit[] unitsArray = new Unit[10];
    public Unit[] UnitsArray
    {
        get
        {
            return unitsArray;
        }

        set
        {
            unitsArray = value;
        }
    }
    //building
    private Building[] buildArray = new Building[6];
    public Building[] BuildArray
    {
        get
        {
            return buildArray;
        }

        set
        {
            buildArray = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        float size = Camera.main.orthographicSize;

        float X = size + 4 * size + size + 2.16f;
        float Y = size + 1;
        CreateMap(X, Y);
        MakeUnits();
        MakeBuilding();
        FillMap();
        DrawUnits();
        DrawBuildings();
        playGame();

    }

    // puts units and buildings on map
    public void FillMap()
    {
        foreach (Unit Heros in UnitsArray)
        {
            Instantiate(Resources.Load("grass"), new Vector3((i * offset), (k * offset), -1), Quaternion.identity);
        }
        //puts buildings on map
        foreach (Building House in BuildArray)
        {
            DrawBuildings();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if ((gametime % Refreash == 0) && (UnitsArray[i].Hp >= 0 || BuildArray[i].Health >= 0))
        {
            playGame();
            Redraw();
        }
        gametime++;
    }

    //methods
    public void playGame()
    {
        tick++;
        //building parts
        foreach (Building Names in BuildArray)
        {
            if (Names.GetType().ToString() == "MitchumNaidoo_GADE_RERUN_POE.ResourceBuilding.cs")
            {
                //checks if there are resourseleft if yes then it generates rescources
                if ((Names as ResourceBuilding).ResourcesLeft <= 0)
                {
                    (Names as ResourceBuilding).GenerateResources();
                }
            }
            else if (Names.GetType().ToString() == "MitchumNaidoo_GADE_RERUN_POE.FactoryBuilding")
            {
                if (tick % (Names as FactoryBuilding).SpawnRate == 0)
                {
                    // inceases Unit array size
                    Unit[] TempUnit = new Unit[UnitsArray.Length + 1];
                    for (int i = 0; i < UnitsArray.Length; i++)
                    {
                        TempUnit[i] = UnitsArray[i];
                    }
                    //sets new units to map arrary
                    TempUnit[UnitsArray.Length] = (Names as FactoryBuilding).SpawnUnit();
                    //TempUnit[TempUnit.Length - 1].Ypostion, TempUnit[TempUnit.Length - 1].Xpostion = TempUnit[TempUnit.Length - 1].Symbol;
                    UnitsArray = TempUnit;
                }
            }
        }
        MovementCombat();
    }

    //mapcrearion
    public void CreateMap(float X, float Y)
    {
        for (int i = 0; i < 10; i++)
        {
            for (int k = 0; k < 10; k++)
            {
                Instantiate(Resources.Load("grass"), new Vector3((i * offset), (k * offset), -1), Quaternion.identity);
            }
        }
    }
    //units
    public void DrawUnits()
    {
        if (UnitsArray[i].GetType().ToString() == "MeleeUnit")
        {
            if (UnitsArray[i].Symbol == 'M')
            {
                Instantiate(Resources.Load("heromelee"), new Vector3(offset * UnitsArray[i].Xpostion, offset * UnitsArray[i].Ypostion, -2), Quaternion.identity);
                Instantiate(Resources.Load(getHpUnit(UnitsArray[i])), new Vector3(offset * UnitsArray[i].Xpostion, offset * UnitsArray[i].Ypostion, -3), Quaternion.identity);
            }
            if (UnitsArray[i].Symbol == 'm')
            {
                Instantiate(Resources.Load("enemymelee"), new Vector3(offset * UnitsArray[i].Xpostion, offset * UnitsArray[i].Ypostion, -2), Quaternion.identity);
                Instantiate(Resources.Load(getHpUnit(UnitsArray[i])), new Vector3(offset * UnitsArray[i].Xpostion, offset * UnitsArray[i].Ypostion, -3), Quaternion.identity);
            }
        }
        if (UnitsArray[i].GetType().ToString() == "RangeUnit")
        {
            if (UnitsArray[i].Symbol == 'R')
            {
                Instantiate(Resources.Load("herorange"), new Vector3(offset * UnitsArray[i].Xpostion, offset * UnitsArray[i].Ypostion, -2), Quaternion.identity);
                Instantiate(Resources.Load(getHpUnit(UnitsArray[i])), new Vector3(offset * UnitsArray[i].Xpostion, offset * UnitsArray[i].Ypostion, -3), Quaternion.identity);
            }
            if (UnitsArray[i].Symbol == 'r')
            {
                Instantiate(Resources.Load("enemyrange"), new Vector3(offset * UnitsArray[i].Xpostion, offset * UnitsArray[i].Ypostion, -2), Quaternion.identity);
                Instantiate(Resources.Load(getHpUnit(UnitsArray[i])), new Vector3(offset * UnitsArray[i].Xpostion, offset * UnitsArray[i].Ypostion, -3), Quaternion.identity);
            }
        }
    }
    // building
    public void DrawBuildings()
    {

        if (BuildArray.GetType().ToString() == "FactoryBuilding")
        {
            if (BuildArray[i].Symbol == 'F')
            {
                Instantiate(Resources.Load("herofactory"), new Vector3(offset * BuildArray[i].Xpos, offset * BuildArray[i].Ypos, -2), Quaternion.identity);
                Instantiate(Resources.Load(getHpBuild(BuildArray[i])), new Vector3(offset * BuildArray[i].Xpos, offset * BuildArray[i].Ypos, -3), Quaternion.identity);
            }
            if (BuildArray[i].Symbol == 'f')
            {
                Instantiate(Resources.Load("enemyfactory"), new Vector3(offset * BuildArray[i].Xpos, offset * BuildArray[i].Ypos, -2), Quaternion.identity);
                Instantiate(Resources.Load(getHpBuild(BuildArray[i])), new Vector3(offset * BuildArray[i].Xpos, offset * BuildArray[i].Ypos, -3), Quaternion.identity);
            }
        }
        if (BuildArray[i].GetType().ToString() == "ResourceBuilding")
        {
            if (BuildArray[i].Symbol == 'R')
            {
                Instantiate(Resources.Load("heroresource"), new Vector3(offset * BuildArray[i].Xpos, offset * BuildArray[i].Ypos, -2), Quaternion.identity);
                Instantiate(Resources.Load(getHpBuild(BuildArray[i])), new Vector3(offset * BuildArray[i].Xpos, offset * BuildArray[i].Ypos, -3), Quaternion.identity);
            }
            if (BuildArray[i].Symbol == 'r')
            {
                Instantiate(Resources.Load("enemyresource"), new Vector3(offset * BuildArray[i].Xpos, offset * BuildArray[i].Ypos, -2), Quaternion.identity);
                Instantiate(Resources.Load(getHpBuild(BuildArray[i])), new Vector3(offset * BuildArray[i].Xpos, offset * BuildArray[i].Ypos, -3), Quaternion.identity);
            }
        }
    }

    //unit hp bars
    public string getHpUnit(Unit hero)
    {
        string returnval = "hp";
        double hpPercent = ((double)hero.Hp / (double)hero.Maxhp) * 10;
        float roundedHp = Mathf.CeilToInt((float)hpPercent);
        returnval = returnval + roundedHp;
        if (roundedHp > 9 && roundedHp <= 10)
        {
            return returnval = "hp10";
        }
        if (roundedHp <= 9 && roundedHp > 8)
        {
            return returnval = "hp9";
        }
        if (roundedHp <= 8 && roundedHp > 7)
        {
            return returnval = "hp8";
        }
        if (roundedHp <= 7 && roundedHp > 6)
        {
            return returnval = "hp7";
        }
        if (roundedHp <= 6 && roundedHp > 5)
        {
            return returnval = "hp6";
        }
        if (roundedHp <= 5 && roundedHp > 4)
        {
            return returnval = "hp5";
        }
        if (roundedHp <= 4 && roundedHp > 3)
        {
            return returnval = "hp4";
        }
        if (roundedHp <= 3 && roundedHp > 2)
        {
            return returnval = "hp3";
        }
        if (roundedHp <= 2 && roundedHp > 1)
        {
            return returnval = "hp2";
        }
        if (roundedHp <= 1 && roundedHp > 0)
        {
            return returnval = "hp1";
        }
        if (roundedHp <= 0)
        {
            return returnval = "hp0";
        }
        return returnval;
    }

    //building hp bars
    public string getHpBuild(Building Build)
    {
        string returnval = "hp";
        double hpPercent = ((double)Build.Health / (double)Build.MaxHp) * 10;
        float roundedHp = Mathf.CeilToInt((float)hpPercent);
        returnval = returnval + roundedHp;
        if (roundedHp > 9 && roundedHp <= 10)
        {
            return returnval = "hp10";
        }
        if (roundedHp <= 9 && roundedHp > 8)
        {
            return returnval = "hp9";
        }
        if (roundedHp <= 8 && roundedHp > 7)
        {
            return returnval = "hp8";
        }
        if (roundedHp <= 7 && roundedHp > 6)
        {
            return returnval = "hp7";
        }
        if (roundedHp <= 6 && roundedHp > 5)
        {
            return returnval = "hp6";
        }
        if (roundedHp <= 5 && roundedHp > 4)
        {
            return returnval = "hp5";
        }
        if (roundedHp <= 4 && roundedHp > 3)
        {
            return returnval = "hp4";
        }
        if (roundedHp <= 3 && roundedHp > 2)
        {
            return returnval = "hp3";
        }
        if (roundedHp <= 2 && roundedHp > 1)
        {
            return returnval = "hp2";
        }
        if (roundedHp <= 1 && roundedHp > 0)
        {
            return returnval = "hp1";
        }
        if (roundedHp <= 0)
        {
            return returnval = "hp0";
        }
        return returnval;
    }

    //methords from map
    //creates units
    public void CreateUnits()
    {
        for (int i = 0; i < UnitsArray.Length; i++)
        {
            int tempX = r.Next(0, 20);
            int tempY = r.Next(0, 20);
            int Unittype = r.Next(0, 4);
            if (Unittype == 0)
            {
                UnitsArray[i] = new MeleeUnit(tempX, tempY, 10, 1, 2, 1, 'h', 'm', "samuri");
                UnitsArray[i].Xpostion = tempX;
                UnitsArray[i].Ypostion = tempY;
            }
            else if (Unittype == 1)
            {
                UnitsArray[i] = new RangeUnit(tempX, tempY, 8, 1, 1, 2, 'h', 'r', "bowman");
                UnitsArray[i].Xpostion = tempX;
                UnitsArray[i].Ypostion = tempY;
            }
            else if (Unittype == 2)
            {
                UnitsArray[i] = new MeleeUnit(tempX, tempY, 10, 1, 2, 1, 'H', 'M', "knight");
                UnitsArray[i].Xpostion = tempX;
                UnitsArray[i].Ypostion = tempY;
            }
            else if (Unittype == 3)
            {
                UnitsArray[i] = new RangeUnit(tempX, tempY, 8, 1, 1, 2, 'H', 'R', "gunman");
                UnitsArray[i].Xpostion = tempX;
                UnitsArray[i].Ypostion = tempY;
            }
        }
    }
    //ceates buildings
    public void createBuilding()
    {
        for (int i = 0; i < BuildArray.Length; i++)
        {

            int tempX = r.Next(0, 20);
            int tempY = r.Next(0, 20);
            int BuildingType = r.Next(0, 4);
            if (BuildingType == 0)
            {
                BuildArray[i] = new FactoryBuilding(tempX, tempY, 20, 'h', 'f', "K", 5);

            }
            else if (BuildingType == 1)
            {
                BuildArray[i] = new ResourceBuilding(tempX, tempY, 20, 'h', 'e', "gold", 100, 5, 1000);
            }
            else if (BuildingType == 2)
            {
                BuildArray[i] = new ResourceBuilding(tempX, tempY, 20, 'H', 'E', "gold", 100, 5, 1000);
            }
            else if (BuildingType == 3)
            {
                BuildArray[i] = new FactoryBuilding(tempX, tempY, 20, 'H', 'F', "K", 5);
            }
        }
    }
    //movement of units
    public void MoveUnit(int Hero, int move)
    {
        switch (move)
        {
            case 1:
                //up
                UnitsArray[Hero].Ypostion = (UnitsArray[Hero].Ypostion - 1);
                break;

            case 2:
                //down
                UnitsArray[Hero].Ypostion = (UnitsArray[Hero].Ypostion + 1);
                break;

            case 3:
                // left
                UnitsArray[Hero].Xpostion = (UnitsArray[Hero].Xpostion - 1);
                break;

            case 4:
                //right
                UnitsArray[Hero].Xpostion = (UnitsArray[Hero].Xpostion + 1);
                break;
        }
        FillMap();
        Redraw();
    }
    public void MovementCombat()
    {
        //Unitparts
        for (int i = 0; i < UnitsArray.Length; i++)
        {
            if (UnitsArray[i] != null)
            {
                if (UnitsArray[i].Isdead() == false)
                {
                    int closestFoe = UnitsArray[i].WhoisclosestUnit(UnitsArray);
                    //check to see if unit flees
                    if (UnitsArray[i].Fleeing() == false)
                    {
                        //all of this is to make units seek combat
                        if (UnitsArray[i].IsUnitinCombat(UnitsArray[closestFoe]) == false && UnitsArray[i].CheckingforAttackrange(UnitsArray[closestFoe]) == true)
                        {
                            UnitsArray[i].Combat(UnitsArray[closestFoe]);
                        }
                        if (UnitsArray[i].IsUnitinCombat(UnitsArray[closestFoe]) == true)
                        {
                            UnitsArray[i].Combat(UnitsArray[closestFoe]);
                        }
                        if (UnitsArray[i].IsUnitinCombat(UnitsArray[closestFoe]) == false && UnitsArray[i].CheckingforAttackrange(UnitsArray[closestFoe]) == false)
                        {
                            int move = UnitsArray[i].Move(UnitsArray[closestFoe]);
                            MoveUnit(i, move);
                        }
                    }
                }
            }
        }
    }

    //creates units
    public void MakeUnits()
    {
        for (int i = 0; i < UnitsArray.Length; i++)
        {
            int tempX = r.Next(0, 20);
            int tempY = r.Next(0, 20);
            int Unittype = r.Next(0, 4);
            if (Unittype == 0)
            {
                UnitsArray[i] = new MeleeUnit(tempX, tempY, 10, 1, 2, 1, 'h', 'm', "samuri");
                UnitsArray[i].Xpostion = tempX;
                UnitsArray[i].Ypostion = tempY;
            }
            else if (Unittype == 1)
            {
                UnitsArray[i] = new RangeUnit(tempX, tempY, 8, 1, 1, 2, 'h', 'r', "bowman");
                UnitsArray[i].Xpostion = tempX;
                UnitsArray[i].Ypostion = tempY;
            }
            else if (Unittype == 2)
            {
                UnitsArray[i] = new MeleeUnit(tempX, tempY, 10, 1, 2, 1, 'H', 'M', "knight");
                UnitsArray[i].Xpostion = tempX;
                UnitsArray[i].Ypostion = tempY;
            }
            else if (Unittype == 3)
            {
                UnitsArray[i] = new RangeUnit(tempX, tempY, 8, 1, 1, 2, 'H', 'R', "gunman");
                UnitsArray[i].Xpostion = tempX;
                UnitsArray[i].Ypostion = tempY;
            }
        }
    }

    //ceates buildings
    public void MakeBuilding()
    {
        for (int i = 0; i < BuildArray.Length; i++)
        {

            int tempX = r.Next(0, 20);
            int tempY = r.Next(0, 20);
            int BuildingType = r.Next(0, 4);
            if (BuildingType == 0)
            {
                BuildArray[i] = new FactoryBuilding(tempX, tempY, 20, 'h', 'f', "K", 5);

            }
            else if (BuildingType == 1)
            {
                BuildArray[i] = new ResourceBuilding(tempX, tempY, 20, 'h', 'e', "gold", 100, 5, 1000);
            }
            else if (BuildingType == 2)
            {
                BuildArray[i] = new ResourceBuilding(tempX, tempY, 20, 'H', 'E', "gold", 100, 5, 1000);
            }
            else if (BuildingType == 3)
            {
                BuildArray[i] = new FactoryBuilding(tempX, tempY, 20, 'H', 'F', "K", 5);
            }
        }
    }

    //redaws map
    void Redraw()
    {
        GameObject[] Deletethis = GameObject.FindGameObjectsWithTag("redraw");
        foreach (GameObject temp in Deletethis)
        {
            Destroy(temp.gameObject);
        }
        CreateUnits();
        createBuilding();
    }

}