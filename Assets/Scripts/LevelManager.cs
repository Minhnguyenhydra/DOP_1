using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkcupGames;
using UnityEngine.SceneManagement;
using Spine.Unity;

public class LevelManager : MonoBehaviour
{
    public int indexTxtQuest;
    public SkeletonAnimation animAfter;
    public SkeletonAnimation animBefore;
    public List<string> winAnims;
    public bool loopAnimation = true;
    //public Transform guidePosition;

    public virtual void Hint() {
        Gameplay.Instance.Hint();
    }

    public virtual void Win() {

    }

    public virtual Vector3 GetGuidePosition() {
        return Vector3.zero;
    }
}
