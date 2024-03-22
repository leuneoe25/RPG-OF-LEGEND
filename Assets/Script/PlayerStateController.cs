using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStateController : MonoBehaviour
{
    public List<string> states = new List<string>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, LayerMask.GetMask("Ground"));
        //Debug.DrawRay(transform.position, new Vector2(0, -0.6f));

        //if(hit.collider != null)
        //{
        //    if(GetNowState("공중"))
        //    {
        //        RemoveState("공중");
        //    }
        //}
    }
    public void AddState(string name,float Time)
    {
        states.Add(name);
        StartCoroutine(Remove(name, Time));
    }
    private IEnumerator Remove(string name,float Time)
    {
        yield return new WaitForSeconds(Time);
        RemoveState(name);
    }

    public void RemoveState(string name)
    {
        if(states.Contains(name))
            states.Remove(name);
    }
    public bool GetNowState(string name)
    {
        return states.Contains(name);
    }
}

