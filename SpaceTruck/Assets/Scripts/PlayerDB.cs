using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDB : MonoBehaviour {

    private static PlayerDB instance;
    public static PlayerDB Instance() { return instance; }
    public List<Mission> _missions = new List<Mission>();

    public Stats stats;
    public Mission _currentmission;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    void Start () {

        if(PlayerPrefs.GetString("userID") == "")
            stats = new Stats(1000);
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
        _currentmission = _missions[0];
    }

    [System.Serializable]
    public class Mission
    {
        public int minasteroid;
        public int maxasteroid;
        public float minspawnperiod;
        public float maxspawnperiod;
        public float missiontime;
        public int money;

        public Mission(int minasteroid, int maxasteroid, float minspawnperiod, float maxspawnperiod, float missiontime, int money)
        {
            this.minasteroid = minasteroid;
            this.maxasteroid = maxasteroid;
            this.minspawnperiod = minspawnperiod;
            this.maxspawnperiod = maxspawnperiod;
            this.missiontime = missiontime;
            this.money = money;
        }
    }
}
