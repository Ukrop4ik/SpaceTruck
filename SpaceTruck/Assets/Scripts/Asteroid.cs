using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

    [SerializeField]
    private float _asteroidSpeed;
    [SerializeField]
    private float _randomSpeed;
    private float ScaleRandom = 1f;
    [SerializeField]
    private GameObject _model;
    private float _randomrotation;
    private int _randomdirection;
    private Rigidbody _rigg;
    public int asteroiddamage;
    [SerializeField]
    private GameObject _destroyeffect;

    private void Start()
    {
        _rigg = GetComponent<Rigidbody>();
        _randomSpeed = Random.Range(1, _asteroidSpeed);
        transform.localScale = Vector3.zero;
        ScaleRandom = Random.Range(ScaleRandom, 5f);
        _randomrotation = GetRandom(1, 2);
        _randomdirection = Random.Range(1, 4);
        asteroiddamage = asteroiddamage * (int)_randomSpeed;
    }
    private void Update()
    {
        _rigg.AddForce(transform.forward * (-_randomSpeed * World.Instance()._timeSpeed));

        if(_randomdirection > 1)
        {
            _model.transform.Rotate(_model.transform.forward * _randomrotation);
        }
        else if(_randomdirection > 2)
        {
            _model.transform.Rotate(_model.transform.up * _randomrotation);
        }
        else
        {
            _model.transform.Rotate(_model.transform.right * _randomrotation);
        }

        if(transform.localScale.x != ScaleRandom)
        {
            float scaler = Mathf.Lerp(transform.localScale.x, ScaleRandom, Time.deltaTime);
            transform.localScale = Vector3.one * scaler;
        }
    }

    private float GetRandom(float min, float max)
    {
        return Random.Range(min, max);
    }

    private void OnDestroy()
    {
        Instantiate(_destroyeffect, transform.position, Quaternion.identity);
        World.Instance().AsteroidDestroyCount++;
    }
}
