using UnityEngine;

public class TopBarManager : MonoBehaviour, IPlayIconAnimation
{
    public void PlayIconAnimation(Animator animator)
    {
        animator.Play("IconAnimation");
    }
}
