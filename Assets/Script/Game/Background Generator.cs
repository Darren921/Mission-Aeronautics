using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundGenerator : MonoBehaviour
{
    [SerializeField] Sprite[] BackGrounds;
    [SerializeField] GameObject backGroundInScene;

    private static LevelPick levelPick;

    private int level;

    // Start is called before the first frame update
    void Start()
    {
        levelPick = new LevelPick();

        level = levelPick.Level();
        
        int randBackground = Random.Range(0, BackGrounds.Length);
        this.GetComponent<SpriteRenderer>().sprite = BackGrounds[level];
    }
}
