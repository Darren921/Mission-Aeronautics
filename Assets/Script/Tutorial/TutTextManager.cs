using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

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
    private float typeSpeed;
    private bool isTalking;
    void Awake()
    {
        contText.enabled = false;
        sentences = new Queue<string>();
        tut = FindObjectOfType<Tutorial>();

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
                EndDialogue();
                break;
            case 1:

               
                break;
            case 2:
               
                break;
            case 3:
                tut.block = true;
                isTalking = true;
                break;
            case 4:
                enemy.SetActive(true);
                enemy.GetComponent<Enemy>().enabled = true;
                enemy.GetComponent<TrainingDummy>().enabled = true;
                enemyHealthbar.SetActive(true);
                isTalking = true;
                break;
            case 5:
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
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
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
        if (sentences.Count == 3 || sentences.Count == 2 || sentences.Count == 1)
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

        if (isTalking == false && sentences.Count == 3)
        {
            typeSpeed = 0f;
            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }
        if (isTalking == false && sentences.Count == 2 && tut.powerUps)
        {
            tut.powerUps = true;
            tut.CheckIfTrue();
            enemy.GetComponent<TrainingDummy>().enemyState = "Idle";      
            enemyHealthbar.SetActive(false);
            powerUpSpawner.GetComponent<PowerUpSpawner>().enabled = true;
            isTalking = true;
            typeSpeed = 0f;
            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }
    }
}
