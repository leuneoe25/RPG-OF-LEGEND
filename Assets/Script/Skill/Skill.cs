using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ScriptableObject
{
    public abstract IEnumerator Execute(GameObject myObj);
    public abstract string CheckExcuteSkill();
    public abstract IEnumerator CheckExecute(GameObject myObj);
}
