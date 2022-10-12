using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkcupGames;

public class BackgroundSong : MonoBehaviour
{
    public AudioClip clip;

    private void Start() {
        AudioSystem.Instance.FadeBackgroundSong(clip);
    }
}
