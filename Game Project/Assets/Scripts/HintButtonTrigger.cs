﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HintButtonTrigger : MonoBehaviour
{

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float hintTriggerDistance;

    [SerializeField]
    private SOHintManager hintManager;

    [SerializeField]
    private string buttonHint;

    [SerializeField]
    private UnityEvent triggerAfterAction;

    [SerializeField]
    private bool withCooldown = false;

    [SerializeField]
    private float cooldown = 3.5f;

    private bool fPressed = false;

    void Update()
    {
        if (Vector3.Distance(this.transform.position, player.transform.position) <= hintTriggerDistance)
        {
            if (!fPressed)
            {
                hintManager.command = buttonHint;
                hintManager.timerCommand = 0.1f;
            }

            if (Input.GetKeyDown(KeyCode.F) && !fPressed)
            {
                
                triggerAfterAction.Invoke();
                fPressed = true;

                if (withCooldown)
                {
                    StartCoroutine(corTriggerMethod2());
                }
            }

        }
    }

    IEnumerator corTriggerMethod2()
    {
        yield return new WaitForSeconds(cooldown);
        resetHint();



    }

    public void resetHint()
    {
        fPressed = false;
    }

    public void destroyScript()
    {
        this.GetComponent<HintButtonTrigger>().enabled = false;
    }
}
