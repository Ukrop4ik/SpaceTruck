using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Playership : MonoBehaviour {

    [SerializeField]
    private int _maxHP;
    [SerializeField]
    private int _curHP;
    [SerializeField]
    private GameObject _collisionEffect;
    [SerializeField]
    private List<WeaponsInLevel> weapons = new List<WeaponsInLevel>();
    [SerializeField]
    private int _weaponupgradeLEVEL;
    [SerializeField]
    private int _weaponupgradeseparator;
    public int _weaponLVL;
    [SerializeField]
    private List<Shoot> weaponsstat = new List<Shoot>();
    public void SetWeaponLevel(int lvl, int separator)
    {
        _weaponupgradeLEVEL = lvl;
        _weaponupgradeseparator = separator;
    }
    // Use this for initialization


    public void SetWeaponDamage(float value)
    {
        foreach(Shoot s in weaponsstat)
        {
            s.SetDamage(value);
        }
    }
    void Start () {
        _curHP = _maxHP;

        //if (_weaponLVL == 0) _weaponLVL = 1;
        //if (_weaponLVL > weapons.Count) _weaponLVL = weapons.Count;

        //_weaponLVL = _weaponupgradeLEVEL / _weaponupgradeseparator;

       // SelectWeapon(_weaponLVL - 1);
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Damage(int value)
    {
        _curHP -= value;
        if(_curHP <= 0)
        {
            SceneManager.LoadScene(1);
        }
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
        if(collision.gameObject.tag == "Bullet")
        {
            Bullet b = collision.gameObject.GetComponent<Bullet>();
            Damage(b.GetDamage());
            Destroy(collision.gameObject);
        }
    }

    public void SelectWeapon()
    {

        if (_weaponupgradeseparator == 0) _weaponupgradeseparator = 10;
        _weaponLVL = _weaponupgradeLEVEL / _weaponupgradeseparator;
        if (_weaponLVL == 0) _weaponLVL = 1;
        if (_weaponLVL > weapons.Count) _weaponLVL = weapons.Count;

        foreach (GameObject go in weapons[_weaponLVL-1].weapons)
        {
            go.SetActive(true);
        }
       
    }

    [System.Serializable]
    public class WeaponsInLevel
    {
        public List<GameObject> weapons;

        public WeaponsInLevel(List<GameObject> weapons)
        {
            this.weapons = weapons;
        }
    }
}

