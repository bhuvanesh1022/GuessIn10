using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TossControl : MonoBehaviour
{
    public GameObject panel;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(1.0f);
        panel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
