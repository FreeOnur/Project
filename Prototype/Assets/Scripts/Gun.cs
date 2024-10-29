using System;
using System.Diagnostics;
using UnityEngine;

public class Gun : MonoBehaviour
{


    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 30f;
    public float fireRate = 2f;
    
    public Camera fpscamera;
    public ParticleSystem muzzleFlash;
    // Datatype Gameobject instead of ParticleSystem because we need to instantiate the impact wherevere the bullet lands
    public GameObject impactEffect;


    private float nextTimeToFire = 0f;
    // Update is called once per frame
    void Update()
    {

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            // Time.time = current time
            nextTimeToFire = Time.time + 1f/fireRate;
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
            // acceses the rigidbody of the component which is hit
            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            // hit.point is the place where the raycast is ending so the landing point of the bullet
            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }

    }
}