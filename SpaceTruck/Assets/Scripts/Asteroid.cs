using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

    [SerializeField]
    private float _asteroidSpeed;
    [SerializeField]
    private float _randomSpeed;
    private float ScaleRandom = 1f;


    private void Start()
    {
        _randomSpeed = Random.Range(1, _asteroidSpeed);
        transform.localScale = Vector3.zero;
        ScaleRandom = Random.Range(ScaleRandom, 5f);
    }
    private void Update()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * (-_randomSpeed * World.Instance()._timeSpeed));

        if(transform.localScale.x != ScaleRandom)
        {
            float scaler = Mathf.Lerp(transform.localScale.x, ScaleRandom, Time.deltaTime);
            transform.localScale = Vector3.one * scaler;
        }
    }
}
