using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField]
    private int damageValue;

	// Use this for initialization
	void Start () {
		
	}
	
    public int GetDamage()
    {
        return damageValue;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
