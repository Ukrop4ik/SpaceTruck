using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDB : MonoBehaviour {

    private static PlayerDB instance;
    public static PlayerDB Instance() { return instance; }

    public Stats stats;

    private void Awake()
    {
        GooglePlayGames.PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool succes) => { if (succes) Debug.Log("Login"); else Debug.Log("Falsre"); });
        instance = this;
        DontDestroyOnLoad(this);
    }

    void Start () {

        stats = new Stats(1000);
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
}
