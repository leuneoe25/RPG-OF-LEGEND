using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BloomingButton : MonoBehaviour
{
    [SerializeField] private AudioClip MouseOverSound;
    [SerializeField] private float Scale = 1.08f;
    [SerializeField] private float ScaleDuration = 0.3f;

    private EventTrigger _eventTrigger;
    private Vector3 _orgScale;
    private CanvasGroup _canvasGroup;
    private void Start()
    {
        Init();
    }
    public void InitScale()
    {
        transform.localScale = _orgScale;
    }
    public void Init()
    {

        _orgScale = transform.localScale;

        _eventTrigger = GetComponent<EventTrigger>();

        EventTrigger.Entry enter = new EventTrigger.Entry();
        enter.eventID = EventTriggerType.PointerEnter;
        enter.callback.AddListener((eventData) => { MouseOver(); });

        EventTrigger.Entry exit = new EventTrigger.Entry();
        exit.eventID = EventTriggerType.PointerExit;
        exit.callback.AddListener((eventData) => { MouseExit(); });

        _eventTrigger.triggers.Add(enter);
        _eventTrigger.triggers.Add(exit);

        //if (MouseOverSound == null)
        //    MouseOverSound = PreloadingManager.Settings.ButtonMouseOverSound;
    }

    public void MouseOver()
    {
        //if (!_button.interactable)
        //    return;

        OnMouseOverSound();
        //SoundManager.ins.PlaySound(SoundManager.SoundType.effect, "EnterMouse");
        Vector3 newScale = _orgScale * Scale;
        transform.DOKill();
        transform.DOScale(newScale, ScaleDuration);
        //OnMouseEnterAction?.Invoke(this);
    }

    public void MouseExit()
    {
        //if (!_button.interactable)
        //    return;

        OnMouseExitSound();
        transform.DOKill();
        transform.DOScale(_orgScale, ScaleDuration);
        //OnMouseExitAction?.Invoke(this);
    }

    private void OnMouseOverSound()
    {
        if (MouseOverSound == null)
            return;

        //SoundManager.ins.PlaySFX(MouseOverSound?.name);
    }
    private void OnMouseExitSound()
    {

    }
    
}
