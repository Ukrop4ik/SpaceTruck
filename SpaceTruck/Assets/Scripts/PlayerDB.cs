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

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    void Start() {

        LoadAllMissions();
        if (PlayerPrefs.GetString("userID") == "")
            stats = new Stats(1000);
        stats.Money = PlayerPrefs.GetInt("Money");
        PlayerPrefs.SetString("userID", "1");
        PlayerPrefs.Save();

        SetCurrentMission();
    }

    public void StartCurrentMission()
    {
        SceneManager.LoadScene(2);
    }

    [System.Serializable]
    public class Stats
    {
        public int Money;

        public Stats(int money)
        {
            Money = money;
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
            public int MinHP;
            public int MaxHP;
            public int SpawnTime;

            public EnemyData(int enemyId, int minHP, int maxHP, int spawnTime)
            {
                EnemyId = enemyId;
                MinHP = minHP;
                MaxHP = maxHP;
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
}
