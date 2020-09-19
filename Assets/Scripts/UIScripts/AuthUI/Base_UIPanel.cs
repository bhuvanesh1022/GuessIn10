﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Base_UIPanel : MonoBehaviour 
{
    public bool isOpen = false;

    public virtual void OpenBehavior()
    {
        if (!isOpen)
        {
            isOpen = true;
            gameObject.SetActive(true);
        }
    }

    public virtual void UpdateBehavior()
    {
        
    }

    public virtual void CloseBehavior()
    {
        if (isOpen)
        {
            isOpen = false;
            gameObject.SetActive(false);
        }
    }
}
