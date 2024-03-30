using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class qngfade : MonoBehaviour
{
    private float debounce = 0;
    void Update()
    {
        debounce += Time.deltaTime;

        if (debounce > 3)
        {
            SceneManager.LoadScene("TitleScreen");
        }
    }
}
