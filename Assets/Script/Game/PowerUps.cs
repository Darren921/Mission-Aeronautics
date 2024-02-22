using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        print(collision.gameObject.GetComponent<Player>().IsCollecting());
        print(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<Player>().IsCollecting() == true)
        { 
            Destroy(gameObject);
        }
    }

}
