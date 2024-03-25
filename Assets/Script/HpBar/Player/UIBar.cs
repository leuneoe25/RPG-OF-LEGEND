using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBar : MonoBehaviour
{
    public Slider slider;
    public TMP_Text text;

    public void ChangeValue(float m_value,string m_text)
    {
        slider.value = (m_value);
        text.text = m_text;
    }
}
