using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    [SerializeField]
    private Transform playerBars;
    [SerializeField]
    private Transform _player;
    [SerializeField]
    private Playership _playership;
    [SerializeField]
    private Image HpPlayerBar;
    [SerializeField]
    private Image _timelineBar;
    [SerializeField]
    private World _world;

    public Text asteroidtext;

    private float startWorldTime;
    private bool isReady;

    // Use this for initialization
    void Start() {

        startWorldTime = _world.worldTime;
        StartCoroutine(WaitPlayer());
    }

    // Update is called once per frame
    void Update() {

        if (!isReady) return;

        playerBars.transform.position = Camera.main.WorldToScreenPoint(_player.position);
        HpPlayerBar.fillAmount = _playership.GetMaxCurrHP().y / _playership.GetMaxCurrHP().x;
        _timelineBar.fillAmount = _world.worldTime / startWorldTime;
        asteroidtext.text = _world.AsteroidDestroyCount.ToString();
    }
    public bool isHUDReady()
    {
        return isReady;
    }
    private IEnumerator WaitPlayer()
    {
        yield return new WaitForSeconds(0.01f);

        _player = SpawnController.Instance().GetPlayerTransform();
        _playership = SpawnController.Instance().GetPlayership();

        if(_player && _playership)
        {
            StopCoroutine(WaitPlayer());
            isReady = true;
        }
        else
        {
            StartCoroutine(WaitPlayer());
        }
    } 
}
