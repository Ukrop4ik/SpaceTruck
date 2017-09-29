using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionButton : MonoBehaviour {

    public PlayerDB.Mission _mission;

    [SerializeField]
    private Text money;
    [SerializeField]
    private Text cost;
    [SerializeField]
    private Text time;
    [SerializeField]
    private Text danger;

    public void Create(PlayerDB.Mission mission)
    {
        money.text = mission.money.ToString();
        cost.text = mission.Cost.ToString();
        time.text = StaticMetods.ConvertTimeToString((int)mission.missiontime);
        danger.text = mission.DangerLvl.ToString();


    }

    public void MissionStart()
    {
        PlayerDB.Instance()._currentmission = _mission;
        PlayerDB.Instance().AddMoney(-_mission.Cost);
        PlayerDB.Instance().StartCurrentMission();
    }
}
