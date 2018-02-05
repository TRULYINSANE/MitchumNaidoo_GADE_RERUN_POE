using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameEngine : MonoBehaviour {

    //varibles
    public int tick = 1;
    protected int i;
    protected int k;
    float offset = 2.5f;
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
      //  FillMap();
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
            Instantiate(Resources.Load("grass"), new Vector3((i * offset), (k * offset), -1), Quaternion.identity);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
        if ((gametime % Refreash == 0) && (UnitsArray[i].Hp >= 0 || BuildArray[i].Health >= 0))
        {
            playGame();
            Redraw();
            for (int i = 0; i < UnitsArray.Length; i++)
            {
                DrawUnits(UnitsArray[i]);
            }
            for (int k = 0; k < BuildArray.Length; k++)
            {
                DrawBuildings(BuildArray[i]);
            }
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
                    UnitsArray = TempUnit;
                }
            }
        }
        MovementCombat();
    }

    //mapcrearion
    public void CreateMap(float X, float Y)
    {
        for (int i = -12; i < 8; i++)
        {

            for (int k = -2; k < 18; k++)
            {
                Instantiate(Resources.Load("grass"), new Vector3((X + i * offset), (Y + k * offset), -1), Quaternion.identity);
            }
        }
    }
    //units
    public void DrawUnits(Unit Hero)
    {
        if (Hero.GetType().ToString() == "MeleeUnit")
        {
            if (Hero.Symbol == 'M')
            {
                Instantiate(Resources.Load("heromelee"), new Vector3(offset * Hero.Xpostion, offset * Hero.Ypostion, -2), Quaternion.identity);
                Instantiate(Resources.Load(getHpUnit(Hero)), new Vector3(offset * UnitsArray[i].Xpostion, offset * UnitsArray[i].Ypostion, -3), Quaternion.identity);
            }
            if (Hero.Symbol == 'm')
            {
                Instantiate(Resources.Load("enemymelee"), new Vector3(offset * Hero.Xpostion, offset * Hero.Ypostion, -2), Quaternion.identity);
                Instantiate(Resources.Load(getHpUnit(Hero)), new Vector3(offset * Hero.Xpostion, offset * Hero.Ypostion, -3), Quaternion.identity);
            }
        }
        if (Hero.GetType().ToString() == "RangeUnit")
        {
            if (Hero.Symbol == 'R')
            {
                Instantiate(Resources.Load("herorange"), new Vector3(offset * Hero.Xpostion, offset * Hero.Ypostion, -2), Quaternion.identity);
                Instantiate(Resources.Load(getHpUnit(Hero)), new Vector3(offset * Hero.Xpostion, offset * Hero.Ypostion, -3), Quaternion.identity);
            }
            if (Hero.Symbol == 'r')
            {
                Instantiate(Resources.Load("enemyrange"), new Vector3(offset * Hero.Xpostion, offset * Hero.Ypostion, -2), Quaternion.identity);
                Instantiate(Resources.Load(getHpUnit(Hero)), new Vector3(offset * Hero.Xpostion, offset * Hero.Ypostion, -3), Quaternion.identity);
            }
        }
    }
    // building
    public void DrawBuildings(Building Build)
    {

        if (Build.GetType().ToString() == "FactoryBuilding")
        {
            if (Build.Symbol == 'F')
            {
                Instantiate(Resources.Load("herofactory"), new Vector3(offset * Build.Xpos, offset * Build.Ypos, -2), Quaternion.identity);
                Instantiate(Resources.Load(getHpBuild(Build)), new Vector3(offset * Build.Xpos, offset * Build.Ypos, -3), Quaternion.identity);
            }
            if (Build.Symbol == 'f')
            {
                Instantiate(Resources.Load("enemyfactory"), new Vector3(offset * Build.Xpos, offset * Build.Ypos, -2), Quaternion.identity);
                Instantiate(Resources.Load(getHpBuild(Build)), new Vector3(offset * Build.Xpos, offset * Build.Ypos, -3), Quaternion.identity);
            }
        }
        if (Build.GetType().ToString() == "ResourceBuilding")
        {
            if (Build.Symbol == 'R')
            {
                Instantiate(Resources.Load("heroresource"), new Vector3(offset * Build.Xpos, offset * Build.Ypos, -2), Quaternion.identity);
                Instantiate(Resources.Load(getHpBuild(Build)), new Vector3(offset * Build.Xpos, offset * Build.Ypos, -3), Quaternion.identity);
            }
            if (Build.Symbol == 'r')
            {
                Instantiate(Resources.Load("enemyresource"), new Vector3(offset * Build.Xpos, offset * Build.Ypos, -2), Quaternion.identity);
                Instantiate(Resources.Load(getHpBuild(Build)), new Vector3(offset * Build.Xpos, offset * Build.Ypos, -3), Quaternion.identity);
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
       // FillMap();
        //Redraw();
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
                        for (int k = 0; k < BuildArray.Length; k++)
                        {
                            int closestBuild = UnitsArray[i].NearBuilding(BuildArray);
                           // if (UnitsArray[i].NearBuilding(closestBuild) == true)
                            {
                                //UnitsArray[i].KillBuild(build);
                            }
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
       
    }

}
