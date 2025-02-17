﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    public AudioSource buttonsound;

    [SerializeField]
    private UnityEvent method1;

    [SerializeField]
    private UnityEvent method2;

    [SerializeField]
    private float triggerDistance;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private bool oneTimeUse;

    [SerializeField]
    private bool triggerMethod2;

    [SerializeField]
    private float delayTriggerMethod2;

    [SerializeField]
    private bool requirePushNumber;

    [SerializeField]
    private int requiredPushNumber;

    [SerializeField]
    private UnityEvent[] triggerWithPush;

    [SerializeField]
    private bool multipleTimeUse;

    [SerializeField]
    private float cooldowenBetweenUses = 3.0f;

    private bool method1Done = false;

    private bool multipleTimeUseBlock = false;

    private int counter = 0;

    // Update is called once per frame
    void Update()
    {
   
        if (Vector3.Distance(transform.position, player.position) <= triggerDistance)
        {
          
            if (Input.GetKeyDown(KeyCode.F))
            {
                

                if (oneTimeUse)
                {
                    buttonsound.Play();
                    method1.Invoke();
                    method1Done = true;
                    if(!triggerMethod2)
                        Destroy(this.GetComponent<Button>());
                }

                else if(requirePushNumber)
                {
                    counter++;
                    triggerWithPush[counter - 1].Invoke();
                    if (counter == requiredPushNumber)
                    {
                        method1.Invoke();
                        method1Done = true;
                        Destroy(this.GetComponent<Button>());
                    }
                        
                }

                else if (multipleTimeUse && !multipleTimeUseBlock)
                {
                    buttonsound.Play();
                    method1.Invoke();
                    StartCoroutine(multipleTimeUseTimer());
                    multipleTimeUseBlock = true;

                }
            }
        }

        if (triggerMethod2 && method1Done)
        {

            StartCoroutine(corTriggerMethod2());
        
        }
    }

    IEnumerator corTriggerMethod2()
    {
        yield return new WaitForSeconds(delayTriggerMethod2);
        method2.Invoke();
        Destroy(this.GetComponent<Button>());
    }

    IEnumerator multipleTimeUseTimer()
    {
        yield return new WaitForSeconds(cooldowenBetweenUses);
        multipleTimeUseBlock = false;
    }
}
