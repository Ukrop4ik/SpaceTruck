using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SocialUI : MonoBehaviour {

    [SerializeField]
    private Text _moneyText;

    [SerializeField]
    private Text _weaponLvl;

    [SerializeField]
    private GameObject _missionPanel;
    private void Start()
    {
        StartCoroutine(UpdateUi());
        CreateMissionPanel();
    }

    public void CreateMissionPanel()
    {
        for(int i = 0; i < _missionPanel.transform.childCount; i++)
        {
            MissionButton butt = _missionPanel.transform.GetChild(i).GetComponent<MissionButton>();
            butt._mission = PlayerDB.Instance().Missions.Missions[Random.Range(0, PlayerDB.Instance().Missions.Missions.Count)];
            butt.Create(butt._mission);
        }
    }

    public void StartMission()
    {
        PlayerDB.Instance().StartCurrentMission();
    }

    private IEnumerator UpdateUi()
    {
        yield return new WaitForSeconds(0.1f);
        _moneyText.text = PlayerDB.Instance().stats.Money.ToString();
        _weaponLvl.text = PlayerDB.Instance()._actualship.WeaponLVL.ToString();
        StartCoroutine(UpdateUi());
    }
}
