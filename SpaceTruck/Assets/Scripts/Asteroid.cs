using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

    [SerializeField]
    private float _asteroidSpeed;
    [SerializeField]
    private float _randomSpeed;

    private void Start()
    {
        _randomSpeed = Random.Range(1, _asteroidSpeed);
    }
    private void Update()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * -_randomSpeed);
    }
}
