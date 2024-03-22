using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Details_Storm : MonoBehaviour
{
    public float RoateSpeed;

    private void Update()
    {
        transform.eulerAngles += new Vector3 (0f, 1f, 0f) * RoateSpeed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyBody"))
        {
            Debug.Log("Attack");
            
            if(collision.gameObject.GetComponent<PlayerStateController>().GetNowState("공중"))
            {
                return;
            }
            else
            {
                collision.gameObject.GetComponent<PlayerStateController>().AddState("공중");
                StartCoroutine(floating(collision.gameObject));
            }

        }
    }
    IEnumerator floating(GameObject obj)
    {
        obj.GetComponent<Rigidbody2D>().gravityScale = 0;
        obj.transform.DOLocalMoveY(obj.transform.position.y + 2.5f, 2f).OnComplete(() =>
        {
            obj.GetComponent<Rigidbody2D>().gravityScale = 5;
        });
        yield return new WaitForSeconds(1f);
    }

}
