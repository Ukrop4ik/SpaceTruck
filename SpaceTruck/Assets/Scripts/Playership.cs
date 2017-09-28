using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playership : MonoBehaviour {

    [SerializeField]
    private int _maxHP;
    [SerializeField]
    private int _curHP;
    [SerializeField]
    private GameObject _collisionEffect;

    // Use this for initialization
    void Start () {
        _curHP = _maxHP;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Damage(int value)
    {
        _curHP -= value;
    }
    public Vector2 GetMaxCurrHP()
    {
        return new Vector2(_maxHP, _curHP);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Asteroid")
        {
            Asteroid asteroid = collision.gameObject.GetComponent<Asteroid>();
            Damage(asteroid.asteroiddamage);
            Instantiate(_collisionEffect, collision.contacts[0].point, Quaternion.identity);
        }
    }
}

