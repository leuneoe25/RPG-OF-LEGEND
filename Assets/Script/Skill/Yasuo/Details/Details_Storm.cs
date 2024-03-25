using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Details_Storm : MonoBehaviour
{
    public GameObject MyObj;
    public float RoateSpeed;
    public float Damage;

    private void Update()
    {
        transform.eulerAngles += new Vector3 (0f, 1f, 0f) * RoateSpeed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyBody"))
        {
            //Damage
            collision.gameObject.GetComponent<Enemy>().GetDamage(Damage,MyObj);
            
            if(collision.gameObject.GetComponent<PlayerStateController>().GetNowState("����"))
            {
                return;
            }
            else
            {
                collision.gameObject.GetComponent<PlayerStateController>().AddState("����",1.5f);
                Debug.Log("ADD STATE ����");
                StartCoroutine(floating(collision.gameObject));
            }

        }
    }
    IEnumerator floating(GameObject obj)
    {
        obj.transform.DOKill();
        obj.GetComponent<EnemyMove>().CrowdControl(2f);
        obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        obj.GetComponent<Rigidbody2D>().gravityScale = 0;
        obj.transform.DOLocalMoveY(obj.transform.position.y + 2.5f, 2f).OnComplete(() =>
        {
            if(obj != null)
                obj.GetComponent<Rigidbody2D>().gravityScale = 5;
        });
        yield return new WaitForSeconds(1f);
    }

}
