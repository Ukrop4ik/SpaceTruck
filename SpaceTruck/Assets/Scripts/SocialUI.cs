using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SocialUI : MonoBehaviour {

    [SerializeField]
    private Text _moneyText;

    [SerializeField]
    private GameObject _missionPanel;
    private void Start()
    {
        StartCoroutine(UpdateUi());
    }

    public void StartMission()
    {
        PlayerDB.Instance().StartCurrentMission();
    }

    private IEnumerator UpdateUi()
    {
        yield return new WaitForSeconds(0.1f);
        _moneyText.text = PlayerDB.Instance().stats.Money.ToString();
        StartCoroutine(UpdateUi());
    }
}
