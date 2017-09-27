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
    [SerializeField]
    private LeftJoystick lJoy;
    private Vector3 newpos;
    public LayerMask layerMask;
    private void Start()
    {
        _playerRiggidbody = _playership.gameObject.GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update () {

        if(Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1000f, layerMask))
            {

                newpos = hit.point;
            }
        }

        if(newpos.x != transform.position.x)
        {
            float x = Mathf.Lerp(transform.position.x, newpos.x, Time.deltaTime * HorizonalSpeed);
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }

        zPoz += Input.GetAxis("Vertical") * (VerticalSpeed);
        zPoz += lJoy.GetInputDirection().y * (VerticalSpeed);
        zPoz = Mathf.Clamp(zPoz, 0, 10f);

        _playerRiggidbody.AddForce(transform.right * Input.GetAxis("Horizontal") * HorizonalSpeed);
        _playerRiggidbody.AddForce(transform.right * lJoy.GetInputDirection().x * HorizonalSpeed);
        _playership.position = new Vector3(transform.position.x, transform.position.y, zPoz);
    }
}
