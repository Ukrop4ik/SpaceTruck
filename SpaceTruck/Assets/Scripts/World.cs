using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class World : MonoBehaviour {

    public enum LevelState
    {
        Normal,
        Pause,
        Win,
        Lose
    }

    public int MoneyReward;

    public int AsteroidDestroyCount;

    [Range(0f,5f)]
    public float _timeSpeed = 1f;

    private static World instance;
    public static World Instance() { return instance; }

    public float worldTime = 60f;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        MoneyReward = PlayerDB.Instance()._currentmission.money;
        worldTime = PlayerDB.Instance()._currentmission.missiontime;
    }

    public void SetTimeSpeed(float speed)
    {

    }

    private void Update()
    {
        worldTime -= Time.deltaTime * _timeSpeed;
        if(worldTime <= 0f)
        {
            PlayerDB.Instance().AddMoney(MoneyReward);
            SceneManager.LoadScene(1);
        }
    }
}
