using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectCharacter : MonoBehaviour
{
    [SerializeField] private Button CharacterSlot;
    [SerializeField] private GameObject CharacterInfo;
    [SerializeField] private GameObject CharacterSelect;
    [SerializeField] private Button CharacterButton;
    [SerializeField] private Button SelectButton;
    void Start()
    {
        CharacterSlot.onClick.AddListener(() =>
        {
            OpneCharacterSelect();
        });
        CharacterButton.onClick.AddListener(() =>
        {
            SelectButton.interactable = true;
            OpneCharacterInfo();
        });
        SelectButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("ExplanationScene");
        });
    }
    private void OpneCharacterSelect()
    {
        CharacterSelect.GetComponent<RectTransform>().DOScaleY(1, 0.3f);
    }
    private void OpneCharacterInfo()
    {
        CharacterInfo.GetComponent<RectTransform>().DOAnchorPosX(0, 0.3f);
        CharacterInfo.GetComponent<CanvasGroup>().DOFade(1, 0.3f);
    }
}
