using System;
using System.Diagnostics;
using UnityEngine;

public class Gun : MonoBehaviour
{


    public float damage = 10f;
    public float range = 100f;
    
    public Camera fpscamera;
    public ParticleSystem muzzleFlash;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButton("Fire1"))
        {
            muzzleFlash.Play();
            Shoot();
        }

    }

    void Shoot()
    {
        

        RaycastHit hit;
        if (Physics.Raycast(fpscamera.transform.position, fpscamera.transform.forward, out hit, range))
        {
            UnityEngine.Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }

    }
}