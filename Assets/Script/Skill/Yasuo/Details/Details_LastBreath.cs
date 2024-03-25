using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Details_LastBreath : MonoBehaviour
{
    public float Damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyBody"))
        {
            Debug.Log($"Attack {collision.gameObject.name}");
            //Damage
            collision.gameObject.GetComponent<Enemy>().GetDamage(Damage,
                transform.parent.transform.parent.gameObject);
        }
    }
}
