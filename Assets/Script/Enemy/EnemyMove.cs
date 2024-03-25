using Cinemachine.Utility;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BehaviorState
{
    Idle,
    Move,
    Attack,
    CrowdControl
}


public class EnemyMove : MonoBehaviour
{
    public BehaviorState BehaviorState;

    private int LeftmoveDir;
    private GameObject Target = null;
    private Coroutine NextStateCoroutine;
    public GameObject Hammer;
    private bool isAttack = false;
    void Start()
    {
        StartCoroutine(ChangeRandomState(3));
    }
    private void Update()
    {
        switch (BehaviorState)
        {
            case BehaviorState.Idle:
                GameObject obj = CanSeePlayer();
                if (obj != null)
                {
                    if (Vector2.Distance(obj.transform.position, gameObject.transform.position) > 1.5f)
                    {
                        Target = obj;
                        ChangeState(BehaviorState.Move);
                    }
                    else
                    {
                        ChangeState(BehaviorState.Attack);
                    }
                }
                break;
            case BehaviorState.Move:
                obj = CanSeePlayer();
                if (obj != null)
                {
                    Target = obj;
                    if (Vector2.Distance(obj.transform.position, gameObject.transform.position) < 1.5f)
                    {
                        ChangeState(BehaviorState.Attack);
                        break;
                    }
                }
                Move();
                break;
            case BehaviorState.Attack:
                //Attack();
                if(!isAttack)
                {
                    if (Target != null)
                    {
                        if (Vector2.Distance(Target.transform.position, gameObject.transform.position) > 1.5f)
                        {
                            ChangeState(BehaviorState.Move);
                        }
                        else
                        {
                            ChangeState(BehaviorState.Attack);
                        }
                    }
                }
                break;
        }
    }
    private IEnumerator ChangeRandomState(float Time)
    {
        yield return new WaitForSeconds(Time);
        ChangeState((BehaviorState)Random.Range(0, 2));
    }
    private GameObject CanSeePlayer()
    {
        RaycastHit2D hit =
            Physics2D.Raycast(transform.position, new Vector2(transform.localScale.x, 0), 5f, LayerMask.GetMask("PlayerBody"));
        if (hit.collider != null)
            return hit.collider.gameObject;
        return null;
    }
    private bool isNotGround(int dir)
    {
        RaycastHit2D hit =
            Physics2D.Raycast(
                new Vector2(transform.position.x + dir, transform.position.y),
                Vector2.down, LayerMask.GetMask("Ground"));

        if(hit.collider != null) 
            return false;
        else 
            return true;
    }
    private void Move()
    {
        if (Target == null)
        {
            
            if (LeftmoveDir == 1)
            {
                if(isNotGround(-1))
                {
                    ChangeState(BehaviorState.Idle);
                }    
                transform.position += new Vector3(-1, 0) * Time.deltaTime;
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                if (isNotGround(1))
                {
                    ChangeState(BehaviorState.Idle);
                }
                transform.position += new Vector3(1, 0) * Time.deltaTime;
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else
        {
            if(Vector2.Distance(Target.transform.position, transform.position) > 5f)
            {
                Target = null;
                //¿©±â
                ChangeState(BehaviorState.Move);
                return;
            }    
            Vector2 dir = Target.transform.position - gameObject.transform.position;
            dir.Normalize();
            float x = 0;
            if (dir.x > 0)
                x = 1;
            else
                x = -1;
            transform.position += new Vector3(x, 0) * Time.deltaTime;
            transform.localScale = new Vector3(x, 1,1);
        }
    }
    private IEnumerator Attack()
    {
        isAttack = true;
        Hammer.GetComponent<Hammer>().attackEvent = (Entity entity) =>
        {
            entity.GetDamage(GetComponent<Enemy>().GetStatus().ADAtk);
            Hammer.GetComponent<Hammer>().attackEvent = null;
        };


        Hammer.transform.DORotate(new Vector3(0, 0, 75), 0.6f).OnComplete(() =>
        {
            Hammer.GetComponent<BoxCollider2D>().enabled = true;
            Hammer.transform.DORotate(new Vector3(0, 0, -20), 0.4f).OnComplete(() =>
            {
                Hammer.GetComponent<BoxCollider2D>().enabled = false;
            });
        });
        
        yield return new WaitForSeconds(1f);
        
        
        //Attakc Effect
        //Debug.Log("Enemy Attack");
        yield return new WaitForSeconds(1f);
        isAttack = false;
    }
    private void ChangeState(BehaviorState state)
    {
        if (NextStateCoroutine != null)
            StopCoroutine(NextStateCoroutine);

        switch (state)
        {
            case BehaviorState.Idle:
                NextStateCoroutine = StartCoroutine(ChangeRandomState(3));
                break;
            case BehaviorState.Move:
                LeftmoveDir = Random.Range(0, 2);
                NextStateCoroutine = StartCoroutine(ChangeRandomState(3));
                break;
            case BehaviorState.Attack:
                StartCoroutine(Attack());
                break;
        }
        BehaviorState = state;
    }
    public void CrowdControl(float time)
    {
        if (NextStateCoroutine != null)
            StopCoroutine(NextStateCoroutine);
        BehaviorState = BehaviorState.CrowdControl;

        NextStateCoroutine = StartCoroutine(ChangeRandomState(time));
    }
}
