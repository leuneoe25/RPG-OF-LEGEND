using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SteelTempest", menuName = "CharacterSkill/Yasuo/Q_SteelTempest", order = 0)]
public class SteelTempest : Skill
{
    private GameObject Knife;
    [SerializeField] private GameObject Storm;
    [SerializeField] private float CoolDown;
    [SerializeField] private float ExecuteTime;
    public override IEnumerator Execute(GameObject myObj)
    {
        CooldownManager.ins.AddCoolDown(SkillType.U, ExecuteTime, CoolDown, "SteelTempest");

        Debug.Log("SteelTempest");
        Knife = myObj.transform.GetChild(1).GetChild(0).gameObject;

        if (myObj.GetComponent<PlayerStateController>().GetNowState("ȸ����"))
        {
            GameObject g = Instantiate(Storm, myObj.transform.position, Quaternion.identity);

            g.transform.localScale = new Vector3(0, 0, 1);
            if (myObj.transform.localScale.x > 0)
            {
                g.transform.DOLocalMoveX(myObj.transform.position.x + 5f, 1f).OnComplete(() =>
                {
                    g.transform.DOScale(new Vector3(0, 0), 0.2f).OnComplete(() =>
                    {
                        Destroy(g);
                    });
                });
                g.transform.DOScale(new Vector3(1, 1), 1);
            }
            else
            {
                g.transform.DOLocalMoveX(myObj.transform.position.x - 5f, 2f).OnComplete(() =>
                {
                    g.transform.DOScale(new Vector3(0, 0), 0.2f).OnComplete(() =>
                    {
                        Destroy(g);
                    });
                });
                g.transform.DOScale(new Vector3(-1, 1), 1);
            }

            myObj.GetComponent<PlayerStateController>().RemoveState("ȸ����");
            EffectOff(myObj.transform.GetChild(2).gameObject,0.5f);
            //���ݽ� �Ͼ �̺�Ʈ(ex.������ ���)
            Knife.transform.GetChild(0).GetComponent<Details_SteelTempest>().attackEvent = null;
        }
        else
        {
            Knife.transform.GetChild(0).GetComponent<Details_SteelTempest>().attackEvent = () =>
            {

                if (!myObj.GetComponent<PlayerStateController>().GetNowState("��ø"))
                    myObj.GetComponent<PlayerStateController>().AddState("��ø");
                else
                {
                    myObj.GetComponent<PlayerStateController>().RemoveState("��ø");
                    myObj.GetComponent<PlayerStateController>().AddState("ȸ����");
                    EffectOn(myObj.transform.GetChild(2).gameObject, 0.5f);
                }

                Knife.transform.GetChild(0).GetComponent<Details_SteelTempest>().attackEvent = null;
            };
        }
        

        Knife.transform.DOScaleX(2, ExecuteTime/4).SetEase(Ease.OutQuint).OnComplete(() =>
        {
            Knife.transform.DOScaleX(1, (ExecuteTime/4)*3);
        });
        yield break;
    }

    private void EffectOn(GameObject g,float time)
    {
        g.SetActive(true);
        for(int i = 0; i< g.transform.childCount; i++)
        {
            g.transform.GetChild(i).GetComponent<SpriteRenderer>().DOFade(0.7372549f, time);
        }
    }
    private void EffectOff(GameObject g, float time)
    {
        
        for (int i = 0; i < g.transform.childCount; i++)
        {
            g.transform.GetChild(i).GetComponent<SpriteRenderer>().DOFade(0, time).OnComplete(() => { g.SetActive(false); });
        }
    }

    public override string CheckExcuteSkill()
    {
        return "SweepingBlade";
    }

    public override IEnumerator CheckExecute(GameObject myObj)
    {
        Debug.Log("EQ");
        CooldownManager.ins.AddCoolDown(SkillType.U, ExecuteTime, CoolDown, "SteelTempest");
        GameObject g = myObj.transform.GetChild(3).gameObject;

        if (myObj.GetComponent<PlayerStateController>().GetNowState("ȸ����"))
        {
            GameObject storm = Instantiate(Storm, myObj.transform.position, Quaternion.identity);

            storm.transform.localScale = new Vector3(0, 0, 1);
            storm.transform.DOScale(new Vector3(1, 1), 1).OnComplete(() =>
            {
                storm.transform.DOScale(new Vector3(0, 0), 0.2f).OnComplete(() =>
                {
                    Destroy(storm);
                });
            });

            myObj.GetComponent<PlayerStateController>().RemoveState("ȸ����");
            EffectOff(myObj.transform.GetChild(2).gameObject, 0.5f);
            //���ݽ� �Ͼ �̺�Ʈ(ex.������ ���)
            g.GetComponent<Details_SteelTempest>().attackEvent = null;
        }
        else
        {
            g.GetComponent<Details_SteelTempest>().attackEvent = () =>
            {

                if (!myObj.GetComponent<PlayerStateController>().GetNowState("��ø"))
                    myObj.GetComponent<PlayerStateController>().AddState("��ø");
                else
                {
                    myObj.GetComponent<PlayerStateController>().RemoveState("��ø");
                    myObj.GetComponent<PlayerStateController>().AddState("ȸ����");
                    EffectOn(myObj.transform.GetChild(2).gameObject, 0.5f);
                }
            };
        }


        
        g.SetActive(true);
        for (int i = 0; i < g.transform.childCount; i++)
        {
            g.transform.GetChild(i).GetComponent<SpriteRenderer>().DOFade(0.7372549f, ExecuteTime/2);
        }
        yield return new WaitForSeconds(ExecuteTime / 3);
        yield return new WaitForSeconds(ExecuteTime / 3);
        for (int i = 0; i < g.transform.childCount; i++)
        {
            g.transform.GetChild(i).GetComponent<SpriteRenderer>().DOFade(0, ExecuteTime / 2);
        }
        yield return new WaitForSeconds(ExecuteTime / 3);
        g.SetActive(false);

        yield break;
    }
}
