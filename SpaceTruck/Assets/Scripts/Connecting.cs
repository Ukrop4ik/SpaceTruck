using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Connecting : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GooglePlayGames.PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool succes) => { if (succes) Debug.Log("Login"); else Debug.Log("Falsre"); SceneManager.LoadScene(1); });
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
