using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundGenerator : MonoBehaviour
{
    [SerializeField] Sprite[] BackGrounds;
    [SerializeField] Image backGroundInScene;
    // Start is called before the first frame update
    void Start()
    {
      int randBackground = Random.Range(0, BackGrounds.Length);
        this.GetComponent<Image>().sprite = BackGrounds[randBackground];
    }
}
