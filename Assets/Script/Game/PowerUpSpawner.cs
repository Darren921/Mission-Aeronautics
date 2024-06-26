using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static PowerUps;
using static UnityEngine.EventSystems.EventTrigger;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] internal Player player;
    [SerializeField] Camera cam;
    [SerializeField] GameObject powerup;
    Enemy enemy;
    [SerializeField] Sprite[] PowerUpIcons;
    SpriteRenderer powerupRenderer;
    Tutorial tut;
    private bool isSpawned;
    public enum PowerUpType
    {
        Health,
        Damage,
        Shield
    }
    private static string PowerUpTypeS;


    [SerializeField] PowerUpType powerUpType;
    private IEnumerator SpawnPowerUp()
    {
        if (isSpawned == false)
        {
            RandomPowerup();
            Vector3 topLeft = cam.ViewportToWorldPoint(new Vector3(0, 1, 13));
            Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, 13));
            Vector3 spawnPoint = Vector3.Lerp(topLeft, topRight, UnityEngine.Random.value);
            GameObject Powerup = Instantiate(powerup, spawnPoint, Quaternion.identity);

            isSpawned = true;
            print(PowerUpTypeS);


            switch (PowerUpTypeS)
            {

                case "Health":
                    powerupRenderer = powerup.GetComponent<SpriteRenderer>();
                    //print("Health");
                    powerUpType = PowerUpType.Health;
                    Powerup.GetComponentInChildren<SpriteRenderer>().sprite = PowerUpIcons[0];
                    break;
                case "Damage":
                    powerupRenderer = powerup.GetComponent<SpriteRenderer>();
                    //print("Damage");
                    powerUpType = PowerUpType.Damage;
                    Powerup.GetComponentInChildren<SpriteRenderer>().sprite = PowerUpIcons[1];
                    break;
                case "Shield":
                    powerupRenderer = powerup.GetComponent<SpriteRenderer>();
                    //print("Shield");
                    powerUpType = PowerUpType.Shield;
                    Powerup.GetComponentInChildren<SpriteRenderer>().sprite = PowerUpIcons[2];
                    break;

            }   
            yield return new WaitForSeconds(20);
            isSpawned = false;
            Destroy(Powerup);
        }





    }
    void Start()
    {
        if (Tutorial.tutFin == false)
        {
            tut = FindObjectOfType<Tutorial>();
            enemy = FindObjectOfType<TrainingDummy>();
        }

        StartCoroutine(SpawnPowerUp());
    }

    private void Update()
    {
        if (Tutorial.tutFin == false)
        {
            if (tut.powerUps == true &&  isSpawned == false)
            {
                StartCoroutine(SpawnPowerUp());
            }
        }
        else
        {
           if(isSpawned  == false)
            {
                StartCoroutine(SpawnPowerUp());
            }
        }
    }



    public string RandomPowerup()
    {
            var PowerUpList = Enum.GetNames(typeof(PowerUpType));
            PowerUpTypeS = PowerUpList[UnityEngine.Random.Range(0, PowerUpList.Length)];
      
        return PowerUpTypeS;
    }

    public PowerUpType returnType()
    {
        return powerUpType;
    }

}
