using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Details_WindWall : MonoBehaviour
{
    public float holdingTime = 0f;
    void Start()
    {
        Invoke("Des", holdingTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("projectile"))
        {
            Destroy(collision.gameObject);
        }
    }
    private void Des()
    {
        gameObject.transform.DOScale(Vector3.zero, 1).OnComplete(() =>
        {
            Destroy(gameObject);
        });
        
    }
}
