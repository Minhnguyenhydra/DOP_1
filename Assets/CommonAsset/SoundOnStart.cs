using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DarkcupGames{
public class SoundOnStart : MonoBehaviour
{
    public AudioClip clip;

    private void OnEnable() {
        if (AudioSystem.Instance) {
            AudioSystem.Instance.PlaySound(clip);
        }
    }
}
}