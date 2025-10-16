using System;
using UnityEngine;

public class OptionButton : MonoBehaviour
{
    private bool currentSwitch = true;
    //Switch Button On or Off in Settings
    private void Start()
    {
        UiManager.Instance.OnSettingsOpen += RefreshOptionButton;
    }

    private void RefreshOptionButton(object sender, EventArgs e)
    {
        if (GetComponent<Animator>())
        {
            GetComponent<Animator>().SetBool("ON", currentSwitch);
        }
    }

    public void Switch()
    {
        currentSwitch = !GetComponent<Animator>().GetBool("ON");
        GetComponent<Animator>().SetBool("ON", currentSwitch);
    }
}
