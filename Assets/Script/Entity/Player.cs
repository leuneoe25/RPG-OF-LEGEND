using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Player : MonoBehaviour
{
    public CharacterStatus Status;
    public int Lv = 1;
    
    public Status GetStatus()
    {
        return Status.GetStatus(Lv);
    }
}
