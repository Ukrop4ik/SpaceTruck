using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOT : MonoBehaviour {

    private Vector2 XLimit;
    [SerializeField]
    private Vector3 _movepos;

    [SerializeField]
    private int _maxHP;
    [SerializeField]
    private int _curHP;
    [SerializeField]
    private GameObject _collisionEffect;
    [SerializeField]
    private float changepositiontime;

    private void Start()
    {
        XLimit = new Vector2(-7f, 7f);
        _curHP = _maxHP;

        InvokeRepeating("ChangePosition", 0 , 5f);
    }

    private void Update()
    {
        if(transform.position != _movepos)
        {
            transform.position = Vector3.Lerp(transform.position, _movepos, Time.deltaTime);
        }
    }

    public void Move()
    {

    }

    public void Damage(int value)
    {
        _curHP -= value;
        if(_curHP <= 0)
         Destroy(gameObject);
    }
    public Vector2 GetMaxCurrHP()
    {
        return new Vector2(_maxHP, _curHP);
    }

    private void ChangePosition()
    {
        _movepos = SpawnController.Instance().GetRandomBotPoint();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Bullet b = collision.gameObject.GetComponent<Bullet>();
            Damage(b.GetDamage());
            Destroy(collision.gameObject);
        }
    }
}
