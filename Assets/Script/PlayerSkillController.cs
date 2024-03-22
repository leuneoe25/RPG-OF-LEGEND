using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerSkillController : MonoBehaviour
{
    [SerializeField] private Skill[] PlayerSkills = new Skill[4];
    public bool IsExecuteSkill = false;
    int index = -1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        index = InputKey();
        if (index != -1)
        {
            if (CooldownManager.ins.GetSkillExecutte(PlayerSkills[index].name))
            {
                return;
            }

            if (PlayerSkills[index].CheckExcuteSkill() != null)
            {
                if (CooldownManager.ins.CheckExecuteSkill(PlayerSkills[index].CheckExcuteSkill()))
                {
                    Debug.Log(PlayerSkills[index].name);
                    //StopAllCoroutines();
                    StartCoroutine(PlayerSkills[index].CheckExecute(gameObject));
                    return;
                }
            }

                
            StartCoroutine(PlayerSkills[index].Execute(gameObject));
            IsExecuteSkill = true;
        }
    }
    private int InputKey()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            return 0;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            return 1;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            return 2;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            return 3;
        }
        return -1;
    }
}
