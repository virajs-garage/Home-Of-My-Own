using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    public GameObject TransitionImage;

    public void Start()
    {
        if (PlayerPrefs.GetInt("Transition") == 1)
        {
           TransitionRecive();
        }
    }

    public void TransitionStart(string SceneName)
    {
        PlayerPrefs.SetInt("Transition", 1);
        LeanTween.scale(TransitionImage, new Vector3(50, 50, 50), .8f).setEaseInOutCirc().setOnComplete(() =>
        {
            SceneManager.LoadScene(SceneName);
        });
    }
    public void TransitionRecive()
    {
        PlayerPrefs.SetInt("Transition", 0);
        LeanTween.value(TransitionImage, (float val) => { TransitionImage.transform.localScale = new Vector3(val, val, val); }, 50, 0, .8f).setEaseInOutCirc().setOnComplete(() =>
        {
            if (PlayerPrefs.GetInt("DoTutorial") == 1)
            {
                PlayerPrefs.SetInt("DoTutorial", 0);
                GetComponentInParent<Board>().InitiateTutorial();
            }
        });
    }
}
