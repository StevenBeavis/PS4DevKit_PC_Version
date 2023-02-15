using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PS4;
using System;

public class Shooting : MonoBehaviour
{
    public float damage;
    public float range = 100f;

    public Camera cam;

    public float countdown;
    float shootDelay = 1f;

    public ParticleSystem smoke;

    bool hasShot;

    private void Start()
    {
        countdown = shootDelay;

        hasShot = false;
    }
    void Update()
    {
        if (Math.Abs(Input.GetAxis("joystick1_right_trigger")) > 0.001f && !hasShot)
        {
            smoke.Play();
            Shoot();
        }

        if (hasShot)
        {
            countdown -= Time.deltaTime;
        }

        if (countdown <= 0)
        {
            smoke.Stop();
            hasShot = false;

            countdown = shootDelay;
        }
    }

    void Shoot()
    {
        hasShot = true;

        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            Debug.Log("shooting");

            Debug.Log(hit.transform.name);

            EnemyController enemy = hit.transform.GetComponent<EnemyController>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }

    }
}
