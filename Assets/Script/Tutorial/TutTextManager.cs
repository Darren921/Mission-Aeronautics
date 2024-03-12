using System;
using System.Collections;
using System.Collections.Generic;
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
    private float typeSpeed;
    private bool isTalking;
     void Awake()
    {
        contText.enabled = false;
        sentences = new Queue<string>();
        tut = FindObjectOfType<Tutorial>();

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
                isTalking = true;
                enemyHealthbar.SetActive(true);
                enemy.SetActive(true);
                break;
            case 4:
               
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
        typeSpeed = 0.01f;
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
        contText.enabled = true;
        isTalking = false;
        if(sentences.Count == 3)
        {
            isTalking = true;
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
       
    }
}
