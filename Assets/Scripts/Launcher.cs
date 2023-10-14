using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField] GameObject Launchiee;
    [SerializeField] float speed;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject bullet = Instantiate(Launchiee, transform.position, transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * speed, ForceMode.Impulse);
        }
    }
}
