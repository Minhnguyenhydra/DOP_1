using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkcupGames;

public class MyDebugger : MonoBehaviour
{
    public ChooseCorrectItemLevel choose;

    int count = 0;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            choose.ShowAnim(count);
            count++;
            if (count == 3) count = 0;
        }

    }
}
