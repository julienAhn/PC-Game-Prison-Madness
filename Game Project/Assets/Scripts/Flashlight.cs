﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField]
    private AudioSource click;

    [SerializeField]
    private int weaponNR;

    [SerializeField]
    private SOWeaponManager weaponManager;

    private int batteryLvl;

    private bool flashlightStatus = false;

    private float tickTimer;

    [SerializeField]
    private float maxTime;

    void Start()
    {
        tickTimer = maxTime;
        weaponManager.batteryLevel = 4;
    }

    void Update()
    {
        GetComponentInChildren<Light>().enabled = flashlightStatus;
        batteryLvl = weaponManager.batteryLevel;
        int sWeapon = weaponManager.selectedWeapon;

        if (sWeapon == weaponNR)
        {
            this.GetComponent<MeshRenderer>().enabled = true;
            this.GetComponentsInChildren<MeshRenderer>()[2].enabled = true;
            this.GetComponentsInChildren<MeshRenderer>()[1].enabled = true;
            this.GetComponent<BoxCollider>().enabled = true;
            this.GetComponentsInChildren<BoxCollider>()[2].enabled = true;
            this.GetComponentsInChildren<BoxCollider>()[1].enabled = true;
        }
        else
        {
            this.GetComponent<MeshRenderer>().enabled = false;
            this.GetComponentsInChildren<MeshRenderer>()[2].enabled = false;
            this.GetComponentsInChildren<MeshRenderer>()[1].enabled = false;
            this.GetComponent<BoxCollider>().enabled = false;
            this.GetComponentsInChildren<BoxCollider>()[2].enabled = false;
            this.GetComponentsInChildren<BoxCollider>()[1].enabled = false;
        }

        if (flashlightStatus)
        {
            tickTimer -= Time.deltaTime;

            if (tickTimer <= 0)
                weaponManager.batteryLevel = 0;
            else if (tickTimer <= (maxTime * 0.25f))
                weaponManager.batteryLevel = 1;
            else if (tickTimer <= (maxTime * 0.5f))
                weaponManager.batteryLevel = 2;
            else if (tickTimer <= (maxTime * 0.75f))
                weaponManager.batteryLevel = 3;


        }

        if (batteryLvl != 0 && Input.GetButtonDown("Fire1") && sWeapon == weaponNR)
        {
            click.Play();
            ChangeState();
        }

        if (batteryLvl == 0 || sWeapon != weaponNR)
        {
            flashlightStatus = false;
        }


    }

    void ChangeState()
    {
        flashlightStatus = !flashlightStatus;
    }
}
