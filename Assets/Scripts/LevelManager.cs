using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelManager : MonoBehaviour
{
    public abstract void Hint();

    public abstract void Win();
}
