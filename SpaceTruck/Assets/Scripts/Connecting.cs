using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Connecting : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GooglePlayGames.PlayGamesPlatform.Activate();
        
        Social.localUser.Authenticate((bool succes) => { if (succes) Debug.Log("Login"); else Debug.Log("Falsre"); SceneManager.LoadScene(1); });

        CreateMissionList();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public PlayerDB.Mission GenerateMission(int playerLVL, bool isTax, bool isBosses)
    {
        int ID = Random.Range(1, 99999999) * Time.frameCount;
        int Cost = isTax ? Random.Range(1, 10) * playerLVL : 0;
        int Danger = 1;
        int ProductID = Random.Range(1, 10);
        int Mass = 0;
        int Enviroment = 1;
        int Bosses = isBosses ? 1 : 0;
        Danger += Bosses != 0 ? 3 : 0;
        int EnemyCount = Random.Range(1, 5) * playerLVL;
        Danger += EnemyCount / playerLVL;
        int EnemyType = 1;
        int EnemySpawnMin = EnemyCount > 0 ? Random.Range((int)10, (int)15) : 0;
        int EnemySpawnMax = EnemyCount > 0 ? Random.Range(EnemySpawnMin + 5, EnemySpawnMin + 10) : 0;
        int minasteroid = Random.Range((int)1, (int)5);
        int maxasteroid = Random.Range(minasteroid, minasteroid + 3);
        int asteroidspaun_min = Random.Range((int)1, (int)5);
        int asteroidspaun_max = Random.Range(asteroidspaun_min, asteroidspaun_min + 5);

        int missiontime_buffer = (120 * Danger) / 60;
        if (missiontime_buffer < 30) missiontime_buffer = 30;
        if (missiontime_buffer > 300) missiontime_buffer = 300;

        int Money = Danger * 100 + playerLVL * 100 + Random.Range(0, 5) * 100;

        List<PlayerDB.Mission.EnemyData> enemys = new List<PlayerDB.Mission.EnemyData>();
        if (EnemyCount > 0)
        {

            for (int e = 0; e < EnemyCount; e++)
            {
                int iD = 1;
                int spawntime = Random.Range((int)10, (int)missiontime_buffer);
                PlayerDB.Mission.EnemyData en = new PlayerDB.Mission.EnemyData(iD, Random.Range(1, 10), spawntime);
                enemys.Add(en);
            }
        }
        List<PlayerDB.Mission.BossData> bosses = new List<PlayerDB.Mission.BossData>();
        if (Bosses > 0)
        {
            for (int b = 0; b < Bosses; b++)
            {
                int iD = Random.Range((int)1, (int)11);
                int minH = Random.Range((int)500, (int)1000);
                int maxH = Random.Range(minH, minH * 3);
                int spawntime = Random.Range((int)10, (int)missiontime_buffer);

                PlayerDB.Mission.BossData en = new PlayerDB.Mission.BossData(iD, minH, maxH, spawntime);
                bosses.Add(en);
            }
        }

        return new PlayerDB.Mission(ID, Cost, Danger, ProductID, Mass, Enviroment, Bosses, EnemyCount, EnemyType, EnemySpawnMin, EnemySpawnMax, maxasteroid, maxasteroid, asteroidspaun_min, asteroidspaun_max, missiontime_buffer, Money, enemys, bosses);
    }

    private List<PlayerDB.Mission> MissionGenerator()
    {
        List<PlayerDB.Mission> Missions = new List<PlayerDB.Mission>();

        for (int i = 1; i < 50; i++)
        {
            Missions.Add(GenerateMission(1, false, false));
        }

        return Missions;

    }

    [ContextMenu("CreateMissionList")]
    public void CreateMissionList()
    {
        List<PlayerDB.Mission> Missions = new List<PlayerDB.Mission>();
        List<PlayerDB.Mission.EnemyData> enemydata = new List<PlayerDB.Mission.EnemyData>();
        List<PlayerDB.Mission.BossData> bossesdata = new List<PlayerDB.Mission.BossData>();
        enemydata.Add(new PlayerDB.Mission.EnemyData(0, 0, 0));
        bossesdata.Add(new PlayerDB.Mission.BossData(0, 0, 0, 0));
        PlayerDB.Mission mission1 = new PlayerDB.Mission(11, 1, 0, 1, 1, 1, 0, 0, 0, 0, 0, 2, 3, 2, 4, 60, 500, enemydata, bossesdata);
        Missions.AddRange(MissionGenerator());

        MissionList list = new MissionList(Missions);

        File.WriteAllText(Application.persistentDataPath + "/missionsdata.json", JsonConvert.SerializeObject(list));


    }
    [System.Serializable]
    public class MissionList
    {
        public List<PlayerDB.Mission> Missions;

        public MissionList()
        {

        }

        public MissionList(List<PlayerDB.Mission> missions)
        {
            Missions = missions;
        }
    }
}
