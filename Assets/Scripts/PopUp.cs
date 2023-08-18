using UnityEngine;
using DG.Tweening;

//Simple class that listens to Interactable callbacks and shows a pop up accordingly
[RequireComponent(typeof(Interactable))]
public class PopUp : MonoBehaviour
{
    [SerializeField] CanvasGroup canvas;
    Interactable interactable;

    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        interactable.OnShow += ShowPopUp;
        interactable.OnHide += HidePopUp;
    }

    //Fades pop up in
    void ShowPopUp()
    {
        canvas.alpha = 0;
        canvas.DOFade(1, 0.5f).SetEase(Ease.InSine);
    }

    //Fades pop up alpha
    void HidePopUp()
    {
        canvas.alpha = 1;
        canvas.DOFade(0, 0.5f).SetEase(Ease.OutSine);
    }
}
