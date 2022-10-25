using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DragAndDropScene : LevelManager
{
    public bool win = false;

    public GameObject[] checkPosition;

    public GameObject[] objectToCheck;

    public string objectNameCheck;

    public AudioSource audioSource;
    public AudioClip wrongSFX;
    public AudioClip rightSFX;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(loadNormalAnims());
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject item in objectToCheck)
        {
            var distance = Vector2.Distance(checkPosition[0].transform.position, item.transform.position);
            if(distance < Constants.FIND_ITEM_RANGE)
            {
                if(item.name == objectNameCheck)
                {
                    item.gameObject.SetActive(false);
                    win = true;
                    if (audioSource.isPlaying) audioSource.Stop();
                    audioSource.PlayOneShot(rightSFX);
                }
                else
                {
                    if (audioSource.isPlaying) audioSource.Stop();
                    audioSource.PlayOneShot(wrongSFX);


                    
                }
            }
        }
        if (win)
        {
            Gameplay.Instance.Win(this);
        }
    }

    IEnumerator loadNormalAnims()
    {
        Spine.Animation normal = animBefore.Skeleton.Data.FindAnimation("normal");
        yield return new WaitForSeconds(normal.Duration);
        animBefore.AnimationName = "normal_idle";
        animBefore.loop = true;
    }

}
