using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

public enum SkillType
{
    U, I, O, P
}
public class CooldownManager : MonoBehaviour
{
    private static CooldownManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public static CooldownManager ins { get { return instance; } }

    public List<string> ExecuteSkill = new List<string>();
    public List<string> coldownSkill = new List<string>();
    [Header("Skill Icon")]
    [SerializeField] private Image U_Image;
    [SerializeField] private Image U_CoolTime_Image;
    [SerializeField] private Text U_Text;
    [SerializeField] private Image I_Image;
    [SerializeField] private Image I_CoolTime_Image;
    [SerializeField] private Text I_Text;
    [SerializeField] private Image O_Image;
    [SerializeField] private Image O_CoolTime_Image;
    [SerializeField] private Text O_Text;
    [SerializeField] private Image P_Image;
    [SerializeField] private Image P_CoolTime_Image;
    [SerializeField] private Text P_Text;

    [Header("Skill Execute")]
    [SerializeField] private GameObject barObj;
    [SerializeField] private Image bar;
    [SerializeField] private Text bar_skillname;

    [SerializeField] private PlayerSkillController skillController; 
    void Start()
    {
        
    }
    public bool CheckExecuteSkill(string skillName)
    {
        if (ExecuteSkill.Count > 0)
            return ExecuteSkill.Contains(skillName);
        return false;
    }
    public bool GetSkillExecutte(string name)
    {
        if(ExecuteSkill.Count > 0)
        {
            if (ExecuteSkill.Contains(name))
                return true;
            else
                return coldownSkill.Contains(name);
        }
        return coldownSkill.Contains(name);

        
    }
    public void AddCoolDown(SkillType skillType, float ExecuteTime, float Cooldown,string Skillname)
    {
        ExecuteSkill.Add(Skillname);
        
        switch (skillType)
        {
            case SkillType.U:
                
                StartCoroutine(AddExecuteTime(U_Image, U_CoolTime_Image, U_Text, ExecuteTime, Cooldown, Skillname));
                break;
            case SkillType.I:
                StartCoroutine(AddExecuteTime(I_Image, I_CoolTime_Image, I_Text, ExecuteTime, Cooldown, Skillname));
                break;
            case SkillType.O:
                StartCoroutine(AddExecuteTime(O_Image, O_CoolTime_Image, O_Text, ExecuteTime, Cooldown, Skillname));
                break;
            case SkillType.P:
                StartCoroutine(AddExecuteTime(P_Image, P_CoolTime_Image, P_Text, ExecuteTime, Cooldown, Skillname));
                break;
        }
    }
    IEnumerator AddExecuteTime(Image ui,Image CoolTime,Text text, float ExecuteTime, float Cooldown,string Skillname)
    {
        

        barObj.SetActive(true);
        bar.fillAmount = 0;
        bar_skillname.text = Skillname;
        bar.DOFillAmount(1, ExecuteTime);

        yield return new WaitForSeconds(ExecuteTime);
        coldownSkill.Add(Skillname);
        ExecuteSkill.Remove(Skillname);
        skillController.IsExecuteSkill = false;
        barObj.SetActive(false);

        ui.gameObject.SetActive(false);

        CoolTime.fillAmount = 0;
        StartCoroutine(CoolDownText(text, Cooldown));
        CoolTime.DOFillAmount(1, Cooldown).OnComplete(() =>
        {
            ui.gameObject.SetActive(true);
            coldownSkill.Remove(Skillname);
        });

    }
    IEnumerator CoolDownText(Text text, float Cooldown)
    {

        double now = Math.Round(Cooldown, 1);
        int c = (int)(now * 10);
        for (int i = 0; i < c; i++)
        {
            if(now >= 1)
                text.text = now.ToString("#");
            else
                text.text = "0"+now.ToString("#.#");
            now -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        text.text = "";
    }
}
