using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using static PowerUps;

public class PowerUps : MonoBehaviour
{
    
    
    private static string Power;

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<Player>().IsCollecting() == true)
        {
            Destroy(gameObject);
            
        }
        else
        {
            return;
        }
    }
    private void OnTriggerStay2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<Player>().IsCollecting() == true)
        {
            Destroy(gameObject);

        }   
        else
        {
            return;
        }
    }


  
 
}
