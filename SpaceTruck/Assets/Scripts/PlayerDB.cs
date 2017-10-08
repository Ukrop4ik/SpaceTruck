using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDB : MonoBehaviour {

    private static PlayerDB instance;
    public static PlayerDB Instance() { return instance; }
    public StaticMetods.MissionList Missions;

    public Stats stats;
    public Mission _currentmission;
    public ShipData _actualship;
    public List<ShipData> ships = new List<ShipData>();
    public List<LevelCost> levelcost = new List<LevelCost>();
    [SerializeField]
    private List<BotData> BotsData = new List<BotData>();

    public enum UpgradeType
    {
        Weapon
    }

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }



    void Start() {

        LoadAllMissions();
        _actualship = ships[0];
        SetCurrentMission();
        stats.Money = PlayerPrefs.GetInt("Money");
        AddExp(PlayerPrefs.GetInt("Exp"));

    }

    public void StartCurrentMission()
    {
        SceneManager.LoadScene(2);
    }

    public void UpgradeShip(UpgradeType type)
    {
        switch(type)
        {
            case UpgradeType.Weapon:
                if (_actualship.WeaponUpgradeCosts.Count > _actualship.WeaponLVL - 1)
                {
                    if (stats.Money >= _actualship.WeaponUpgradeCosts[_actualship.WeaponLVL - 1].COST)
                    {
                        stats.Money -= _actualship.WeaponUpgradeCosts[_actualship.WeaponLVL - 1].COST;
                        _actualship.WeaponLVL++;
                    }
                }
                break;
            default:
                break;
        }
    }

    [System.Serializable]
    public class Stats
    {
        public int Money;
        public int Exp;
        public int LvL;

        public Stats(int money, int exp, int lvL)
        {
            Money = money;
            Exp = exp;
            LvL = lvL;
        }
    }

    public void Logout()
    {
        GooglePlayGames.PlayGamesPlatform.Instance.SignOut();
    }
    public void AddMoney(int value)
    {
        stats.Money += value;
        SaveMoney();
    }
    public void AddExp(int value)
    {
        stats.Exp += value;

        stats.LvL = 1;

        foreach(LevelCost cost in levelcost)
        {
            if(cost.Cost <= stats.Exp)
            {
                stats.LvL++;
            }
        }
    }
    public Vector2 GetExp()
    {
        return new Vector2(stats.Exp, levelcost[stats.LvL-1].Cost);
    }
    public void SaveAll()
    {
        SaveMoney();
    }
    public void SaveMoney()
    {
        PlayerPrefs.SetInt("Money", stats.Money);
        PlayerPrefs.Save();
    }


    public void SetCurrentMission()
    {
        _currentmission = Missions.Missions[0];
    }

    [ContextMenu("LoadAllMissions")]
    private void LoadAllMissions()
    { 
        Missions = Newtonsoft.Json.JsonConvert.DeserializeObject<StaticMetods.MissionList>(File.ReadAllText(Application.persistentDataPath + "/missionsdata.json"));
    }

    public GameObject GetBotObjFromId(int id)
    {
        GameObject ob = null;

        foreach (BotData bot in BotsData)
        {
            if(bot.BotId == id)
            {
                ob = bot.BotPrefab;
                break;
            }
        }

        return ob;
    }

    [System.Serializable]
    public struct UpgradeCost
    {
        public int LVL;
        public int COST;
        public float DAMAGE;

        public UpgradeCost(int lVL, int cOST, float dAMAGE)
        {
            LVL = lVL;
            COST = cOST;
            DAMAGE = dAMAGE;
        }
    }

    [System.Serializable]
    public class ShipData
    {
        public GameObject _shipPrefab;
        public int _shipID;

        public int WeaponLVL;
        public int ArmorLVL;
        public int CargoLVL;

        public int WeaponUpgradeSeparator;

        public int HP;
        public int Cargo;
        public int Speed;

        public List<UpgradeCost> WeaponUpgradeCosts;
        public List<UpgradeCost> ArmorUpgradeCosts;
        public List<UpgradeCost> CargoUpgradeCosts;

        public ShipData(GameObject shipPrefab, int shipID, int weaponLVL, int armorLVL, int cargoLVL, int weaponUpgradeSeparator, int hP, int cargo, int speed, List<UpgradeCost> weaponUpgradeCosts, List<UpgradeCost> armorUpgradeCosts, List<UpgradeCost> cargoUpgradeCosts)
        {
            _shipPrefab = shipPrefab;
            _shipID = shipID;
            WeaponLVL = weaponLVL;
            ArmorLVL = armorLVL;
            CargoLVL = cargoLVL;
            WeaponUpgradeSeparator = weaponUpgradeSeparator;
            HP = hP;
            Cargo = cargo;
            Speed = speed;
            WeaponUpgradeCosts = weaponUpgradeCosts;
            ArmorUpgradeCosts = armorUpgradeCosts;
            CargoUpgradeCosts = cargoUpgradeCosts;
        }
    }

    [System.Serializable]
    public class BotData
    {
        public int BotId;
        public string BotName;
        public int botcount;
        public GameObject BotPrefab;

        public BotData(int botId, string botName, int count, GameObject botPrefab)
        {
            BotId = botId;
            BotName = botName;
            BotPrefab = botPrefab;
            botcount = count;
        }
    }

    [System.Serializable]
    public class Mission
    {
        public int MissionId;
        public int Cost;
        public int DangerLvl;
        public int ProductID;
        public int ProductMass;
        public int EnviromentID;
        public int Bosses;
        public int EnemyCount;
        public int EnemyType;
        public int EnemySpawnPeriodMin;
        public int EnemySpawnPeriodMax;
        public int minasteroid;
        public int maxasteroid;
        public float minspawnperiod;
        public float maxspawnperiod;
        public float missiontime;
        public int money;
        public List<EnemyData> EnemysData;
        public List<BossData> BossesData;

        public Mission(int missionId, int cost, int dangerLvl, int productID, int productMass, int enviromentID, int bosses, int enemyCount, int enemyType, int enemySpawnPeriodMin, int enemySpawnPeriodMax, int minasteroid, int maxasteroid, float minspawnperiod, float maxspawnperiod, float missiontime, int money, List<EnemyData> enemysData, List<BossData> bossesData)
        {
            MissionId = missionId;
            Cost = cost;
            DangerLvl = dangerLvl;
            ProductID = productID;
            ProductMass = productMass;
            EnviromentID = enviromentID;
            Bosses = bosses;
            EnemyCount = enemyCount;
            EnemyType = enemyType;
            EnemySpawnPeriodMin = enemySpawnPeriodMin;
            EnemySpawnPeriodMax = enemySpawnPeriodMax;
            this.minasteroid = minasteroid;
            this.maxasteroid = maxasteroid;
            this.minspawnperiod = minspawnperiod;
            this.maxspawnperiod = maxspawnperiod;
            this.missiontime = missiontime;
            this.money = money;
            EnemysData = enemysData;
            BossesData = bossesData;
        }
        [System.Serializable]
        public class EnemyData
        {
            public int EnemyId;
            public int count;
            public int SpawnTime;

            public EnemyData(int enemyId, int count, int spawnTime)
            {
                EnemyId = enemyId;
                this.count = count;
                SpawnTime = spawnTime;
            }
        }
        [System.Serializable]
        public class BossData
        {
            public int EnemyId;
            public int MinHP;
            public int MaxHP;
            public int SpawnTime;

            public BossData(int enemyId, int minHP, int maxHP, int spawnTime)
            {
                EnemyId = enemyId;
                MinHP = minHP;
                MaxHP = maxHP;
                SpawnTime = spawnTime;
            }
        }
    }
    [System.Serializable]
    public class LevelCost
    {
        public int Cost;
    }
}
