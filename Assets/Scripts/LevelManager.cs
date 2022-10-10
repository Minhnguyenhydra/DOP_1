using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkcupGames;
using UnityEngine.SceneManagement;
using Spine.Unity;

public class LevelManager : MonoBehaviour
{
    public SkeletonAnimation animAfter;
    public SkeletonAnimation animBefore;
    public List<string> winAnims;

    public virtual void Hint() {

    }

    public virtual void Win() {

    }
}
