using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> Area;
    [SerializeField] private List<GameObject> EnemyList;
    private GameObject enemyParent;
    private int MaxEnemy = 3;
    private bool cooltime = false;
    void Start()
    {
        enemyParent = transform.GetChild(1).gameObject;
        Spawn();
        Spawn();
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        //if(enemyParent.transform.childCount < MaxEnemy)
        //{
        //    if(! cooltime)
        //    {
        //        StartCoroutine(SpawnCoolTime());
        //    }
        //}
    }
    IEnumerator SpawnCoolTime()
    {
        cooltime = true;
        yield return new WaitForSeconds(10f);
        Spawn();
        cooltime = false;
    }
    private void Spawn()
    {
        GameObject enemy = EnemyList[Random.Range(0, EnemyList.Count)];
        Vector3 Areapos = new Vector3(
            Random.Range(Area[0].position.x, Area[1].position.x),
            Random.Range(Area[0].position.y, Area[1].position.y), 0);
        Instantiate(enemy, Areapos,Quaternion.identity)/*.transform.parent = enemyParent.transform*/;
    }
}
