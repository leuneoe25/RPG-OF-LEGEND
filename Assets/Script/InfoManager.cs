using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoManager : MonoBehaviour
{
    private static InfoManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);

            stateInfo = GetComponent<StateInfo>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public static InfoManager ins { get { return instance; } }

    public StateInfo stateInfo;
}
