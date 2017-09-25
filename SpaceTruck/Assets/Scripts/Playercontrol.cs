using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontrol : MonoBehaviour {
    [SerializeField]
    private Transform _playership;
    private Rigidbody _playerRiggidbody;
    private float zPoz = 0f;
    [SerializeField]
    private float HorizonalSpeed;
    [SerializeField]
    private float VerticalSpeed;
    private void Start()
    {
        _playerRiggidbody = _playership.gameObject.GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update () {

        zPoz += Input.GetAxis("Vertical") * VerticalSpeed;
        zPoz = Mathf.Clamp(zPoz, 0, 10f);

        _playerRiggidbody.AddForce(transform.right * Input.GetAxis("Horizontal") * HorizonalSpeed);
        _playership.position = new Vector3(transform.position.x, transform.position.y, zPoz);
		
	}
}
