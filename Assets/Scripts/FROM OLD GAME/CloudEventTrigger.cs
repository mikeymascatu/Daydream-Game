using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CloudEventTrigger : MonoBehaviour
{
    public UnityEvent CloudAnim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CloudAnim == null)
        {
            Debug.Log("Triggered but nothing to activate"); 
        } else
        {
            Debug.Log("Triggered Clouds");
            CloudAnim.Invoke();
        }
    }


    

}
