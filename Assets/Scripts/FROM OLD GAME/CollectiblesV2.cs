using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesV2 : MonoBehaviour {

    public int Collectables = 0;


    [SerializeField] 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("item"))
        {
            Destroy(collision.gameObject);
            Collectables++;
            Debug.Log("Wood: " + Collectables);
        }

    }
}
