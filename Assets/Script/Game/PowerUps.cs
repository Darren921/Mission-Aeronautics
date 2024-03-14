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
     SpriteRenderer powerupRenderer;
    public enum PowerUpType
    {
        Health,
        Damage,
        Shield
    }
    private static string PowerUpTypeS;

     
    [SerializeField]PowerUpType powerUpType;
    private static string Power;

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
            if (Tutorial.tutFin == true)
            {
                RandomPowerup();
            }

            Vector3 topLeft = cam.ViewportToWorldPoint(new Vector3(0, 1, cam.nearClipPlane));
            Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
            Vector3 spawnPoint = Vector3.Lerp(topLeft, topRight, UnityEngine.Random.value);

            GameObject Powerup = Instantiate(powerup, spawnPoint, Quaternion.identity);

            print(PowerUpTypeS);

            if (Tutorial.tutFin == true)
            {
                switch (PowerUpTypeS)
                {
                    case "Health":
                        powerupRenderer = powerup.GetComponent<SpriteRenderer>();
                        print("Health");
                        powerUpType = PowerUpType.Health;
                        powerupRenderer.sprite = PowerUpIcons[0];
                        break;
                    case "Damage":
                        powerupRenderer = powerup.GetComponent<SpriteRenderer>();
                        print("Damage");
                        powerUpType = PowerUpType.Damage;
                        powerupRenderer.sprite = PowerUpIcons[1];
                        break;
                    case "Shield":
                        powerupRenderer = powerup.GetComponent<SpriteRenderer>();
                        print("Shield");
                        powerUpType = PowerUpType.Shield;
                        powerupRenderer.sprite = PowerUpIcons[2];
                        break;

                }
                yield return new WaitForSeconds(20);
                Destroy(Powerup);
            }
            else
            {
                switch (PowerUpTypeS)
                {
                    case "Health":
                        powerupRenderer = powerup.GetComponent<SpriteRenderer>();
                        print("Health");
                        powerUpType = PowerUpType.Health;
                        powerupRenderer.sprite = PowerUpIcons[0];
                        break;
                }
            }
        }



    }
    void Start()
    {
        StartCoroutine(SpawnPowerUp());
    }
   
   public string RandomPowerup()
    {
        if (Tutorial.tutFin == true)
        {
            var PowerUpList = Enum.GetNames(typeof(PowerUpType));
            PowerUpTypeS = PowerUpList[UnityEngine.Random.Range(0, PowerUpList.Length)];
        } 
        else
        {
            var PowerUp = PowerUpType.Health.ToString();
            PowerUpTypeS = PowerUp;
        }
        return PowerUpTypeS;
    } 

    public PowerUpType returnType()
    {
        return powerUpType;
    }

 
}
