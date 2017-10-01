﻿using System.Collections;
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

    public List<MissionTimelineEvent> TimeLine = new List<MissionTimelineEvent>();

    public int MoneyReward;

    public int AsteroidDestroyCount;

    [Range(0f,5f)]
    public float _timeSpeed = 1f;

    private static World instance;
    public static World Instance() { return instance; }

    public float worldTime = 60f;

    private PlayerDB.Mission missionData;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        MoneyReward = PlayerDB.Instance()._currentmission.money;
        worldTime = PlayerDB.Instance()._currentmission.missiontime;

        missionData = PlayerDB.Instance()._currentmission;

        if (missionData.EnemyCount > 0)
        {
            foreach (PlayerDB.Mission.EnemyData enemyData in missionData.EnemysData)
            {
                MissionTimelineEvent newevent = new MissionTimelineEvent(MissionTimelineEventType.Bot, enemyData.SpawnTime, new PlayerDB.BotData(enemyData.EnemyId, "NAME", PlayerDB.Instance().GetBotObjFromId(enemyData.EnemyId)));
                TimeLine.Add(newevent);
            }
        }

        StartCoroutine(UpdateTimeline());

    }

    private IEnumerator UpdateTimeline()
    {
        yield return new WaitForSeconds(1f);

        List<MissionTimelineEvent> buffer = new List<MissionTimelineEvent>();

        foreach(MissionTimelineEvent eve in TimeLine)
        {
            eve.EventTime -= 1f * _timeSpeed;

            if (eve.EventTime <= 0)
            {
                eve.Activate();
            }
            else buffer.Add(eve);
        }

        TimeLine.Clear();
        TimeLine.AddRange(buffer);

        StartCoroutine(UpdateTimeline());
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

    [System.Serializable]
    public class MissionTimelineEvent
    {
        public MissionTimelineEventType Type;
        public float EventTime;
        public PlayerDB.BotData Bot;
        public bool toRemove = false;
        public MissionTimelineEvent(MissionTimelineEventType type, float eventTime)
        {
            Type = type;
            EventTime = eventTime;
        }
        public MissionTimelineEvent(MissionTimelineEventType type, float eventTime, PlayerDB.BotData bot)
        {
            Type = type;
            EventTime = eventTime;
            Bot = bot;

        }

        public void Activate()
        {
            switch(Type)
            {
                case MissionTimelineEventType.Bot:
                    SpaWnBot();
                    break;
                default:
                    break;
            }
        }

        private void SpaWnBot()
        {
            Transform spawnposition = SpawnController.Instance().GetRandomBotSpawn();
            GameObject bot = Instantiate(Bot.BotPrefab, spawnposition.position, spawnposition.rotation);
            BOT bots = bot.GetComponent<BOT>();
            bots.Create(200);
        }
    }
}
