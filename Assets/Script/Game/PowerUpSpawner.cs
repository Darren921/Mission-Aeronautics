using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] GameObject powerup;
    [SerializeField] Player player;
    private void Update()
    {
    }
    private IEnumerator SpawnPowerUp()
    {
       while (player != null)
        {
            print("active");
            Vector3 topLeft = cam.ViewportToWorldPoint(new Vector3(0, 1, cam.nearClipPlane));
            Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
            Vector3 spawnPoint = Vector3.Lerp(topLeft, topRight, Random.value);
            GameObject Powerup = Instantiate(powerup, spawnPoint, Quaternion.identity);
            yield return new WaitForSeconds(10);
            Destroy(Powerup);
        }
          
     
      
    }

    // Update is called once per frame
    void Start()
    {
       
            StartCoroutine(SpawnPowerUp());
        

    }
}
