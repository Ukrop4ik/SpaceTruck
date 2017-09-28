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

	// Use this for initialization
	void Start () {

        startWorldTime = _world.worldTime;
		
	}
	
	// Update is called once per frame
	void Update () {

        playerBars.transform.position = Camera.main.WorldToScreenPoint(_player.position);
        HpPlayerBar.fillAmount = _playership.GetMaxCurrHP().y / _playership.GetMaxCurrHP().x;
        _timelineBar.fillAmount =  _world.worldTime / startWorldTime;
        asteroidtext.text = _world.AsteroidDestroyCount.ToString();
    }
}
