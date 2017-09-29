using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class StaticMetods : MonoBehaviour {

    public static string ConvertTimeToString(int time)
    {
        int _timer = time;
        string minuts = "0";
        string seconds = "0";

        if (_timer <= 0) return "";

        if (_timer / 60 > 0)
            minuts = (_timer / 60).ToString("00");

        seconds = (_timer % 60).ToString("00");

        return minuts + " : " + seconds;
    }

    private List<PlayerDB.Mission> MissionGenerator()
    {
        List<PlayerDB.Mission> Missions = new List<PlayerDB.Mission>();

        for(int i = 1; i < 50; i++)
        {
            int ID = i;
            int Danger = Random.Range(1, 100);
            int _costBuffer = Random.Range((int)0, (int)1000);
            int product = 1;

            int env = 1;
            int bosscount = Danger % 10 == 0 ? Random.Range((int)0, (int)2) : 0;

            int enemycount = Random.Range((int)Danger, (int)Danger*2);
            int enemytype = enemycount > 0 ? Random.Range((int)1, (int)11) : 0;
            int enemyspaunperiod_min = enemycount > 0 ? Random.Range((int)10, (int)15) : 0;
            int enemyspaunperiod_max = enemycount > 0 ? Random.Range(enemyspaunperiod_min + 5, enemyspaunperiod_min + 10) : 0;
            int minasteroid = Random.Range((int)1, (int)5);
            int maxasteroid = Random.Range(minasteroid, minasteroid+3);
            int asteroidspaun_min = Random.Range((int)1, (int)5);
            int asteroidspaun_max = Random.Range(asteroidspaun_min, asteroidspaun_min + 5);
            int missiontime_buffer = (120 * Danger) / 60;
            if (missiontime_buffer < 30) missiontime_buffer = 30;
            if (missiontime_buffer > 300) missiontime_buffer = 300;

            int money = Danger * 500 + bosscount * 1000 + enemycount * 50;
            int mass = Random.Range(1, 50);
            List<PlayerDB.Mission.EnemyData> enemys = new List<PlayerDB.Mission.EnemyData>();
            if(enemycount > 0)
            {

                for(int e = 0; e < enemycount; e++)
                {
                    int iD = Random.Range((int)1, (int)11);
                    int minH = Random.Range((int)150, (int)250);
                    int maxH = Random.Range(minH, minH * 3);
                    int spawntime = Random.Range((int)10, (int)missiontime_buffer);

                    PlayerDB.Mission.EnemyData en = new PlayerDB.Mission.EnemyData(iD, minH, maxH, spawntime);
                    enemys.Add(en);
                }
            }
            List<PlayerDB.Mission.BossData> bosses = new List<PlayerDB.Mission.BossData>();
            if (bosscount > 0)
            {
                for (int b = 0; b < bosscount; b++)
                {
                    int iD = Random.Range((int)1, (int)11);
                    int minH = Random.Range((int)500, (int)1000);
                    int maxH = Random.Range(minH, minH * 3);
                    int spawntime = Random.Range((int)10, (int)missiontime_buffer);

                    PlayerDB.Mission.BossData en = new PlayerDB.Mission.BossData(iD, minH, maxH, spawntime);
                    bosses.Add(en);
                }
            }

            PlayerDB.Mission m = new PlayerDB.Mission(ID, _costBuffer, Danger, product, mass, env, bosscount, enemycount, enemytype, enemyspaunperiod_min, enemyspaunperiod_max, minasteroid, maxasteroid, asteroidspaun_min, asteroidspaun_max, missiontime_buffer, money, enemys, bosses);

            Missions.Add(m);
        }

        return Missions;

    }


    [ContextMenu("CreateMissionList")]
    public void CreateMissionList()
    {
        List<PlayerDB.Mission> Missions = new List<PlayerDB.Mission>();
        List<PlayerDB.Mission.EnemyData> enemydata = new List<PlayerDB.Mission.EnemyData>();
        List<PlayerDB.Mission.BossData> bossesdata = new List<PlayerDB.Mission.BossData>();
        enemydata.Add(new PlayerDB.Mission.EnemyData(0, 0, 0, 0));
        bossesdata.Add(new PlayerDB.Mission.BossData(0, 0, 0, 0));
        PlayerDB.Mission mission1 = new PlayerDB.Mission(11,1,0,1,1,1,0,0,0,0,0,2,3,2,4,60,500,enemydata,bossesdata);
        Missions.AddRange(MissionGenerator());

        MissionList list = new MissionList(Missions);

        File.WriteAllText(Application.persistentDataPath + "/missionsdata.json", JsonConvert.SerializeObject(list));

        Process.Start(Application.persistentDataPath);

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


