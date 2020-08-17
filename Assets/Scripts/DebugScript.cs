using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScript : MonoBehaviour
{
    int n;

    public void OnClickBtn()
    {
        n++;
        Debug.Log("Button clicked " + n + " times.");
    }
}
