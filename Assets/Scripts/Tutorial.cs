using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public UI ui;
    public Board board;

    public TutorialSteps[] Steps;
    public int stepNumber;
    public int RolledNumber;
    public bool isTutorialComplete;
    public bool isTutorialStarted;
    public bool isCurrentlyTyping;

    public float typeSpeedInterval;

    public GameObject DialogueBoxObject;

    public TMP_Text DialogueText;

    public AudioClip[] typeEffectSounds;
    public GameObject soundEffectPrefab;

    [Serializable]
    public class TutorialSteps
    {
        public string Speech;
    }

    public void IncrementStep()
    {
        if (!isTutorialComplete)
        {
            if (isCurrentlyTyping)
            {
                StopAllCoroutines();
                DialogueText.text = Steps[stepNumber - 1].Speech;       
                isCurrentlyTyping = false;
            }
            else
            {
                isTutorialStarted = true;
                var step = Steps[stepNumber];

                switch (stepNumber)
                {
                    case 0: // Hello
                        ui.RollButton.interactable = false;
                        break;
                    case 1: // Tell player to roll
                        DialogueBoxObject.GetComponent<Button>().interactable = false;
                        ui.RollButton.interactable = true;
                        break;
                    case 2: // After roll
                        DialogueBoxObject.GetComponent<Button>().interactable = true;
                        step.Speech = $"Great job! You rolled a " + RolledNumber + "! this will be how many spaces you can move for your turn.";
                        break;
                    case 3: // Tell player to move
                        DialogueBoxObject.GetComponent<Button>().interactable = false;
                        board.currentPlayer.ShowArrows(board.SpawnPiece);
                        break;
                    case 4: // Good Job
                        DialogueBoxObject.GetComponent<Button>().interactable = true;

                        break;
                    case 5: // Move and Roll again
                        DialogueBoxObject.GetComponent<Button>().interactable = false;

                        break;
                    case 6: // Move and Roll again
                        DialogueBoxObject.GetComponent<Button>().interactable = false;

                        break;
                        case 7:
                        board.currentPlayer.ShowArrows(board.SpawnPiece);
                        break;
                    case 8:
                        DialogueBoxObject.GetComponent<Button>().interactable = true;
                        
                        break;
                    case 11:
                        DialogueBoxObject.GetComponent<Button>().interactable = false;
                        board.ui.OpenCard.transform.GetChild(4).GetComponent<Button>().interactable = true;
                        break;
                    case 12:
                        DialogueBoxObject.GetComponent<Button>().interactable = true;
                        break;
                    case 13:
                        DialogueBoxObject.GetComponent<Button>().interactable = true;
                        board.isEvenFunction(4);
                        DialogueBoxObject.SetActive(false);
                        break;
                    case 14:
                        DialogueBoxObject.GetComponent<Button>().interactable = false;
                        DialogueBoxObject.SetActive(true);
                        break;
                    case 15:
                        DialogueBoxObject.GetComponent<Button>().interactable = true;
                        break;
                    case 16:
                        DialogueBoxObject.GetComponent<Button>().interactable = false;
                        break;
                    case 19:
                        DialogueBoxObject.GetComponent<Button>().interactable = true;
                        break;

                }
                StopAllCoroutines();
                StartCoroutine(TypeSentence(step.Speech));
                stepNumber++;
                if (stepNumber == Steps.Length)
                {
                    Save.Instance.SaveValue("IsTutorialComplete", true);
                    isTutorialComplete = true;
                    isTutorialStarted = false;
                    OpenOrClose(false);
                    GetComponentInParent<Transition>().TransitionStart("MainMenu");
                }
            }
        }
    }
    IEnumerator TypeSentence(string sentence)
    {
        isCurrentlyTyping = true;
        List<GameObject> sfxs = new List<GameObject>();
        DialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            DialogueText.text += letter;

            var sfx = Instantiate(soundEffectPrefab);
            sfx.GetComponent<AudioSource>().clip = typeEffectSounds[UnityEngine.Random.Range(0, typeEffectSounds.Length)];
            sfx.GetComponent<AudioSource>().Play();
            sfxs.Add(sfx);

            yield return new WaitForSeconds(typeSpeedInterval);
        }

        foreach (GameObject g in sfxs)
        {
            Destroy(g);
        }
        isCurrentlyTyping = false;
    }
    public void OpenOrClose(bool open)
    {
        DialogueBoxObject.SetActive(open);
    }
}