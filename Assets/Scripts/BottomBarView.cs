using UnityEngine;
using UnityEngine.UI;

public class BottomBarView : MonoBehaviour
{
    [SerializeField] private GameObject selectingButtonPrefab;
    private GameObject selectingButton;
    private GameObject button;

    //When the pointer enter an Icon
    public void PointerEnter(GameObject button)
    {
        Debug.Log("Pointer Enter");
        if (selectingButton != null)
        {
            Destroy(selectingButton);
        }

        if (this.button != null)
        {
            this.button.SetActive(true);

        }
        selectingButton = Instantiate(selectingButtonPrefab, transform);
        selectingButton.transform.SetSiblingIndex(button.transform.GetSiblingIndex());
        selectingButton.GetComponent<SelectedButton>()
            .SetIcon(button.GetComponent<Image>().sprite, button.GetComponent<BottomBarButton>().name);
        this.button = button;
        button.SetActive(false);
        Debug.Log($"ContentActivated");
    }
    
    //Player selecting no menu
    public void PointerExit()
    {
        //Debug.Log("Pointer Exit");
        Destroy(selectingButton);
        if (button != null)
        {
            button.SetActive(true);
            button = null;
        }
        Debug.Log($"Closed");

    }
    
    //Pointer Hover Lockers
    public void PointerLocked(Transform target)
    {
        target.GetComponent<Animator>().Play("Locked_TurnAround");
        PointerExit();
    }
}
