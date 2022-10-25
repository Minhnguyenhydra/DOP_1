using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnWrong : MonoBehaviour
{
    public float timeOff;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        StartCoroutine(offDisplay());
    }

    IEnumerator offDisplay()
    {
        yield return new WaitForSeconds(timeOff);
        gameObject.SetActive(false);
    }
}
