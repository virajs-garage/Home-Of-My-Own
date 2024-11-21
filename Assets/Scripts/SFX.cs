using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    public AudioClip[] SoundEffects;
    public GameObject SoundPrefab;

    public void PlaySound(int num)
    {
        var s = Instantiate(SoundPrefab);
        s.GetComponent<AudioSource>().clip = SoundEffects[num];
        s.GetComponent<AudioSource>().Play();
    }
}
