using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedObj : MonoBehaviour {

    [SerializeField]
    private int health;

    private void Start()
    {
        health = Random.Range(health, health * 10);
    }

    public void Damage(int value)
    {
        health -= value;
        if (health <= 0) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            Bullet b = other.gameObject.GetComponent<Bullet>();
            Damage(b.GetDamage());
            Destroy(other.gameObject);
        }
    }
}
