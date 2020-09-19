using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScreenManager : MonoBehaviour
{
    public static GameScreenManager instance;

    public Transform _holder;

    public List<GameObject> _screens;

    //[HideInInspector]
    public List<GameUI_Screen> screens;

    GameUI_Screen currentpanel;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        foreach (var screen in _screens) screens.Add(Instantiate(screen, _holder).GetComponent<GameUI_Screen>());

    }

    // Start is called before the first frame update
    void Start()
    {
        TriggerEnableScreen(screens[0]);
    }

    public void TriggerScreenTranstion(GameUI_Screen gameUI_Screen)
    {

    }

    public void TriggerEnableScreen(GameUI_Screen gameUI_Screen)
    {

    }

    public void TriggerDisableScreen(GameUI_Screen gameUI_Screen)
    {

    }
}
