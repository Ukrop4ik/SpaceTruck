﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

    [SerializeField]
    private List<Transform> _GOpoints = new List<Transform>();
    [SerializeField]
    private List<Transform> _ENVpoints = new List<Transform>();
    [SerializeField]
    private List<GameObject> _asteroids = new List<GameObject>();
    [SerializeField]
    private int _maxAsterpodSpaun = 1;
    [SerializeField]
    private int _minAsterpodSpaun = 1;
    [SerializeField]
    private Transform _gospaunerroot;
    [SerializeField]
    private Transform _envspaunerroot;

    [SerializeField]
    private float _spawnPeriod_min;
    [SerializeField]
    private float _spawnPeriod_max;

    [SerializeField]
    private float _ENVspawnPeriod_min;
    [SerializeField]
    private float _ENVspawnPeriod_max;

    private float _multiper = 1;
    [SerializeField]
    [Range(0.1f, 1f)]
    private float _MultiperStep = 0.1f;

    private void Start()
    {
        for(int i =0; i < _gospaunerroot.childCount; i++)
        {
            _GOpoints.Add(_gospaunerroot.GetChild(i));
        }
        for (int i = 0; i < _envspaunerroot.childCount; i++)
        {
            _ENVpoints.Add(_envspaunerroot.GetChild(i));
        }

        StartCoroutine(GOSpawn());
        StartCoroutine(ENVSpawn());
    }

    private IEnumerator GOSpawn()
    {
        yield return new WaitForSeconds(Random.Range(_spawnPeriod_min, _spawnPeriod_max));
        if(_spawnPeriod_min < _spawnPeriod_max)
            _spawnPeriod_max -= _MultiperStep;
        int asteroidcount = Random.Range(_minAsterpodSpaun, _maxAsterpodSpaun);

        for(int i=0; i < asteroidcount; i++)
            Instantiate(_asteroids[0], _GOpoints[Random.Range(0, _GOpoints.Count)].position, Quaternion.identity);

        StartCoroutine(GOSpawn());
    }
    private IEnumerator ENVSpawn()
    {
        yield return new WaitForSeconds(Random.Range(_ENVspawnPeriod_min, _ENVspawnPeriod_max));
        if (_ENVspawnPeriod_min < _ENVspawnPeriod_max)
            _ENVspawnPeriod_max -= _MultiperStep;
        int asteroidcount = Random.Range(_minAsterpodSpaun, _maxAsterpodSpaun);

        for (int i = 0; i < asteroidcount; i++)
        {
            Transform point = _ENVpoints[Random.Range(0, _ENVpoints.Count)];
            Instantiate(_asteroids[0], point.position, point.rotation);
        }

        StartCoroutine(ENVSpawn());
    }
}
