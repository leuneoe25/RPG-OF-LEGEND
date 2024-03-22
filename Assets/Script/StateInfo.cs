using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateInfo : MonoBehaviour
{
    [SerializeField] private List<State> m_StateInfos = new List<State>();
    private Dictionary<string, State> states = new Dictionary<string, State>();
    void Start()
    {
        foreach (var stateInfo in m_StateInfos)
        {
            states.Add(stateInfo.Name, stateInfo);
        }
    }

    public State GetState(string name)
    {
        return states[name];
    }
}

[System.Serializable]
public class State
{
    public string Name;
    public Sprite image;
    public string explanation;
}