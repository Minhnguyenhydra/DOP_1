using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragInAndChoose : LevelManager
{
    public bool win;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (win)
        {
            Gameplay.Instance.Win(this);
        }
    }
}
