using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectedButton : MonoBehaviour, IPlayIconAnimation
{
    [SerializeField] private Image selectedButtonIcon;
    [SerializeField] private TextMeshProUGUI selectedButtonText;

    //Set Icon and Text for the Button Menu
    public void SetIcon(Sprite sprite, string text)
    {
        selectedButtonIcon.sprite = sprite;
        selectedButtonText.text = text;
    }

    //Play Icon Animation when Icon Clicked
    public void PlayIconAnimation(Animator animator)
    {
        Debug.Log("IconClicked");
        animator.Play("SelectedButton_IconJiggle");
    }
}
