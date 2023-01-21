using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Player p1;
    public GameObject Check;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Instantiate(Check, transform.position, Quaternion.identity);
            Debug.Log("Preso");
            p1.respawnPoint = gameObject.transform.position;
            p1.hit_check = true;
        }
    }
}