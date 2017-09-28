using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerOBJ : MonoBehaviour {
    [SerializeField]
    private float _destroyertime;


	// Use this for initialization
	void Start () {
        Destroy(gameObject, _destroyertime);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Destroy()
    {
       
    }
}
