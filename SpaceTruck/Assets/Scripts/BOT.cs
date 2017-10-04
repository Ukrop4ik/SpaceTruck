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
    private SpawnController.BotPosiionPoint _myPoint;
    private Shoot _shoot;

    private void Start()
    {
        XLimit = new Vector2(-7f, 7f);
        _curHP = _maxHP;

        InvokeRepeating("ChangePosition", 0 , 5f);
        _shoot = gameObject.GetComponent<Shoot>();
        World.Instance().botinspace.Add(gameObject);
    }

    public void Create(int HP)
    {
        _maxHP = HP;
        _curHP = _maxHP;
    }

    private void Update()
    {
        if(transform.position != _movepos)
        {
            transform.position = Vector3.Lerp(transform.position, _movepos, Time.deltaTime);
        }

        if (!_shoot) return;
        _shoot.isCanShoot = _myPoint._point_position.y == 0;
        
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

        _myPoint = SpawnController.Instance().GetRandomBotPoint();

        if (_myPoint._bot == this) return;

        _myPoint.isActive = false;
        _myPoint._bot = null;

        if(!_myPoint.isActive)
        {
            _movepos = _myPoint._point_position;
            _myPoint._bot = this;
            _myPoint.isActive = true;
        }
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

    private void OnDestroy()
    {
        World.Instance().botinspace.Remove(gameObject);
    }
}
