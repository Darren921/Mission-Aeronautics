using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutTextManager : MonoBehaviour
{
    private Queue<string> sentences;
    [SerializeField] private TextMeshProUGUI tutText;
    [SerializeField] private TextMeshProUGUI contText;
    [SerializeField] private Animator animator;
    private Tutorial tut;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject enemyHealthbar;
    [SerializeField] private GameObject powerUpSpawner;
    [SerializeField] private GameObject[] Barriers;
     Player player;
    [SerializeField] GameObject skipTut;
    [SerializeField] GameObject finishTut;
    private float typeSpeed;
    [SerializeField] private Health _health;
    private bool isTalking;
    CinemachineFramingTransposer cam;
    Camera mainCam;
    CinemachineTargetGroup TargetGroup;
    
    void Awake()
    {
        contText.enabled = false;
        sentences = new Queue<string>();
        tut = FindObjectOfType<Tutorial>();
        player = FindObjectOfType<Player>();
        cam = FindObjectOfType<CinemachineFramingTransposer>();
        TargetGroup = FindObjectOfType<CinemachineTargetGroup>();
        mainCam = FindObjectOfType<Camera>();
    }

    public bool IsTalking
    {
        get { return isTalking; }
        set { isTalking = value; }
    }
    internal void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        switch (sentences.Count)
        {
            case 0:
                skipTut.SetActive(false);
                finishTut.SetActive(true);
                EndDialogue();
                break;
            case 1:
                break;
            case 2:
                _health.GetCombo = 0;
                for (int i = 0; i < Barriers.Length; i++)
                {
                    Barriers[i].SetActive(false);
                }
                tut.battleStart = true;
                tut.CheckIfTrue();
                enemy.SetActive(true);
                enemy.GetComponent<Enemy>().enabled = true;
                enemy.GetComponent<TrainingDummy>().enabled = true;
                enemyHealthbar.SetActive(true);
                TargetGroup.RemoveMember(GameObject.Find("Camera placeholder").transform);
                TargetGroup.AddMember(enemy.transform, 0.9f, 4);
                mainCam.GetComponent<CinemachineBrain>().enabled = true;
                cam.m_ScreenX = 0.46f;
                typeSpeed = 0f;
                isTalking = true;
                break;
            case 3:
                contText.GetComponent<RectTransform>().anchoredPosition = new Vector2(-38.421f, 50f);
                tut.powerUps = true;
                tut.CheckIfTrue();
                powerUpSpawner.GetComponent<PowerUpSpawner>().enabled = true;

                break;
            case 4:
                tut.block = true;   
                tut.CheckIfTrue();
                break;
            case 5:
                _health.GetCombo = 5;
                tut.specialAtk = true;
                tut.CheckIfTrue();
                break;
            case 6:
                tut.basicAtk = true;
                tut.CheckIfTrue();
                break;
        }
        typeSpeed = 0.05f;
        isTalking = true;
        if (sentences.Count > 0)
        {
            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }
    }

    private void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
    }

    IEnumerator TypeSentence(string sentence)
    {
        tutText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            tutText.text += letter;
            yield return new WaitForSeconds(typeSpeed);

        }
        if (sentences.Count == 1)
        {
            isTalking = true;
        }
        else
        {
            contText.enabled = true;
            isTalking = false;
        }

    }

    
   

    public void nextSentence()
    {
        typeSpeed = 0f;

        if (!isTalking)
        {
            contText.enabled = false;
            DisplayNextSentence();
        }
    }

    private void Update()
    {
        print(sentences.Count);

     
    
        if (isTalking == false && sentences.Count == 1 && tut.battleComplete == true)
        {
            
            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
            enemy.SetActive(false);
            enemyHealthbar.SetActive(false);
            TargetGroup.RemoveMember(enemy.transform);
            TargetGroup.AddMember(GameObject.Find("Camera placeholder").transform, 0.9f, 4);


        }
    }
}
