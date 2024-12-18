using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingPlatform : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            Debug.Log("µé¾î¿È");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            Debug.Log("³ª°£");
    }
}
