using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Explanation : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Button NextButton;
    [SerializeField] private List<Sprite> pageList;
    private int page = 0;
    void Start()
    {
        NextButton.onClick.AddListener(() =>
        {
            if(page == 0)
            {
                page++;
                image.sprite = pageList[page];
                NextButton.transform.GetChild(0).GetComponent<TMP_Text>().text = "게임 시작";
                return;
            }
            else
            {
                SceneManager.LoadScene("MainScene");
            }
        });
    }
}
