using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TermsPanel : Base_UIPanel
{
    public Button backBtn;

    public override void OpenBehavior()
    {
        base.OpenBehavior();

        backBtn.onClick.RemoveAllListeners();
        backBtn.onClick.AddListener(OnBack);

    }
        void OnBack()
    {
        Base_UIPanel nextPanel = UIManager.instance.registerPanel;
        UIManager.instance.TriggerPanelTransition(nextPanel);
    }
}
