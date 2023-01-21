using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUp : MonoBehaviour {

    private static LifeUp instance;
    private int valoreUp = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            LifeUpManager.getInstance().AddLife(valoreUp);
        }
    }

}
