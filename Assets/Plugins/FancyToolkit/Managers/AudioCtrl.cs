using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioCtrl : MonoBehaviour
{
    public static AudioCtrl current;

    public AudioSource music;
    public AudioSource effects;

    private void Awake()
    {
        current = this;
    }

    public void Play(AudioClip clip, float volumeScale = 1f)
    {
        effects.PlayOneShot(clip, volumeScale);
    }


    [ContextMenu("Setup Audio")]
    void DoSomething()
    {
        music = gameObject.AddComponent<AudioSource>();
        effects = gameObject.AddComponent<AudioSource>();
        effects.playOnAwake = false;
    }
}
