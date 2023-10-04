using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class accessabilitySound : MonoBehaviour
{
    public AudioSource mySound;
    public AudioClip hoverSoundEffect;

    public void HoverSound() {
        mySound.PlayOneShot (hoverSoundEffect);
    }
}
