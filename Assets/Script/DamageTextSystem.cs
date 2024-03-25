using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DamageTextSystem : MonoBehaviour
{
    private static DamageTextSystem instance;
    public static DamageTextSystem Instance { get { return instance; } }
    public GameObject TextObj;
    public GameObject Canvers;
    private void Awake()
    {
        instance = this;
    }
    public void ShowDamage(int Damage,Vector2 pos,Color color)
    {
        TMP_Text text = Instantiate(TextObj, Canvers.transform).GetComponent<TMP_Text>();
        text.text = Damage.ToString();
        text.gameObject.transform.position = pos;
        text.color = color;

        text.transform.DOLocalMoveY(0.5f, 1f);
        text.DOFade(0, 1f);
    }
    
}
