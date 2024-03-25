using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public delegate void attackEvent(Entity entity);
public class Details_SteelTempest : MonoBehaviour
{
    public attackEvent attackEvent;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyBody"))
        {
            if(attackEvent!= null)
            {
                attackEvent(collision.gameObject.GetComponent<Entity>());
            }
        }
    }
}
