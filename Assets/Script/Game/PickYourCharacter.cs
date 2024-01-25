using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PickYourCharacter : MonoBehaviour
{
    Buttons UIButtons = new();
    int isPressed = 0;
    // Start is called before the first frame update
    void Start()
    {
        Button[] Characters = GetComponents<Button>();

        if (isPressed == 1)
        {

        }

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
