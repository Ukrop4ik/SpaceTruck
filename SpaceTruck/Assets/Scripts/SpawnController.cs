using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

    [SerializeField]
    private List<Transform> _points = new List<Transform>();
    [SerializeField]
    private List<GameObject> _asteroids = new List<GameObject>();
    [SerializeField]
    private int _maxAsterpodSpaun = 1;
    [SerializeField]
    private int _minAsterpodSpaun = 1;

    [SerializeField]
    private float _spawnPeriod_min;
    [SerializeField]
    private float _spawnPeriod_max;

    private void Start()
    {
        for(int i =0; i < transform.childCount; i++)
        {
            _points.Add(transform.GetChild(i));
        }

        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(Random.Range(_spawnPeriod_min, _spawnPeriod_max));

        int asteroidcount = Random.Range(_minAsterpodSpaun, _maxAsterpodSpaun);

        for(int i=0; i < asteroidcount; i++)
            Instantiate(_asteroids[0], _points[Random.Range(0, _points.Count)].position, Quaternion.identity);

        StartCoroutine(Spawn());
    }
}
