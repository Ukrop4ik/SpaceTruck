using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    public GameObject bullet;
    bool isLastShoot = false;
    [SerializeField]
    float shootrate = 0.2f;
    public bool isShooting = true;
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

    private IEnumerator Shooting()
    {

        yield return new WaitForSeconds(shootrate);

        GameObject bulletObj = Instantiate(bullet, transform.position, Quaternion.identity);
        bulletObj.GetComponent<Rigidbody>().AddForce(bulletObj.transform.forward * 10f, ForceMode.Impulse);

        isLastShoot = false;
    }
}
