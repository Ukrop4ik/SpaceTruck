using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    public GameObject bullet;
    bool isLastShoot = false;
    [SerializeField]
    float shootrate = 0.2f;
    public bool isShooting = true;
    [SerializeField]
    private Transform firepoint;
    [SerializeField]
    private float speed;
    public bool isCanShoot = true;
    [SerializeField]
    private int _damage;
    // Update is called once per frame
    void Update () {

        if(isShooting)
        {
            if (!isLastShoot)
            {
                StartCoroutine(Shooting());
                isLastShoot = true;
            }
        }

        if(Input.GetKey(KeyCode.Space))
        {
            if (!isLastShoot)
            {
                StartCoroutine(Shooting());
                isLastShoot = true;
            }
        }
		
	}

    public void SetDamage(float value)
    {
        _damage = (int)(_damage * value);
    }

    private IEnumerator Shooting()
    {

        yield return new WaitForSeconds(shootrate);

        if (isCanShoot)
        {
            GameObject bulletObj = Instantiate(bullet, firepoint.position, firepoint.rotation);
            bulletObj.GetComponent<Rigidbody>().AddForce(bulletObj.transform.forward * speed, ForceMode.Impulse);
            Bullet bull = bulletObj.GetComponent<Bullet>();
            bull.SetDamage(_damage);
        }
        isLastShoot = false;
    }
}
