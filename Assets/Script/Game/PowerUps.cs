using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using static PowerUps;

public class PowerUps : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] GameObject powerup;
    [SerializeField] Player player;
    [SerializeField] Sprite[] PowerUpIcons;
    [SerializeField]
    SpriteRenderer powerupRenderer;
    public enum PowerUpType
    {
        Health,
        Damage,
        Shield
    }
    private string PowerUpTypeS;

     
    [SerializeField]PowerUpType powerUpType;
    
    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        print(collision.gameObject.GetComponent<Player>().IsCollecting());
        print(collision.gameObject.tag);
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
        print(collision.gameObject.GetComponent<Player>().IsCollecting());
        print(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<Player>().IsCollecting() == true)
        {
            Destroy(gameObject);

        }
        else
        {
            return;
        }
    }


    private IEnumerator SpawnPowerUp()
    {
         
        while (player != null)
        {
            
            Vector3 topLeft = cam.ViewportToWorldPoint(new Vector3(0, 1, cam.nearClipPlane));
            Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
            Vector3 spawnPoint = Vector3.Lerp(topLeft, topRight, UnityEngine.Random.value);
            GameObject Powerup = Instantiate(powerup, spawnPoint, Quaternion.identity);
            powerupRenderer = powerup.GetComponent<SpriteRenderer>();

            RandomPowerup();
            switch (PowerUpTypeS)
            {
                case "Health":
                    powerUpType = PowerUpType.Health;
                    powerupRenderer.sprite = PowerUpIcons[0];
                    break;
                case "Damage":
                    powerUpType = PowerUpType.Damage;
                    powerupRenderer.sprite = PowerUpIcons[1];
                    break;
                case "Shield":
                    powerUpType = PowerUpType.Shield;
                    powerupRenderer.sprite = PowerUpIcons[2];
                    break;
                default: 
                    break;
            }
            yield return new WaitForSeconds(10);
            Destroy(Powerup);
        }



    }
    void Start()
    {
        PowerUpTypeS = "";
        StartCoroutine(SpawnPowerUp());
    }
   
   public void   RandomPowerup()
    {
        var PowerUpList = Enum.GetNames(typeof(PowerUpType));
        PowerUpTypeS = PowerUpList[UnityEngine.Random.Range(0, PowerUpList.Length)];
        print(PowerUpTypeS);
        
    } 

   
}
