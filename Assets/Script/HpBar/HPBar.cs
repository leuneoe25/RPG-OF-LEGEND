using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    public GameObject _Bar;
    public SpriteRenderer Barsprite;
    private GameObject _target;
    public TMP_Text LvText;
    public void Init(Color color, int Lv,GameObject Target = null)
    {
        _target = Target;
        Barsprite.color = color;
        LvText.text = $"Lv.{Lv}";
    }
    public void Update()
    {
        if(_target != null)
        {
            transform.position = new Vector3(_target.transform.position.x, _target.transform.position.y+1,0);
        }

    }
    public void ChageValue(float value,int Lv = 0)
    {
        if(Lv != 0)
            LvText.text = $"Lv.{Lv}";
        _Bar.transform.DOKill();
        _Bar.transform.DOScaleX(value, 0.2f);
        //_Bar.transform.localScale = new Vector3(value,1,1);
    }
}
