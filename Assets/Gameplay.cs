using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    public static Gameplay Instance;

    public List<ParticleSystem> effects;

    private void Awake() {
        Instance = this;

        for (int i = 0; i < effects.Count; i++) {
            effects[i].Stop();
        }
    }

    public void PlayCongratulationEffect() {
        for (int i = 0; i < effects.Count; i++) {
            effects[i].Play();
        }
    }

    public void Hint() {
        FindObjectOfType<LevelManager>()?.Hint();
    }
}
