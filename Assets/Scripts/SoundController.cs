using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioClip[] SoundEffects;
    public GameObject SoundPrefab;


    public void PlaySound(int num, float waitTime)
    {
        StartCoroutine(WaitToPlay(num, waitTime));
    }
    public void DefaultPlaySound(int num)
    {
        var s = Instantiate(SoundPrefab, Camera.main.transform);
        s.GetComponent<AudioSource>().clip = SoundEffects[num];
        s.GetComponent<AudioSource>().Play();
    }

    IEnumerator WaitToPlay(int num, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        var s = Instantiate(SoundPrefab, Camera.main.transform);
        s.GetComponent<AudioSource>().clip = SoundEffects[num];
        s.GetComponent<AudioSource>().Play();
    }
}
