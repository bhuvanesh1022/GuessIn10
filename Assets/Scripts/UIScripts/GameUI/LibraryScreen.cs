using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LibraryScreen : GameUI_Screen
{
    public ButtonFeature animalKingdom;

    public override void OpenBehavior()
    {
        base.OpenBehavior();
    }

    public void OnClickAnimalKingdom()
    {
        GameUI_Screen screenToOpen = GameScreenManager.instance.screens[1];
        GameScreenManager.instance.TriggerScreenTranstion(screenToOpen);
    }
}
