using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 生成敌人
/// </summary>
public class EnemyCloner : MonoBehaviour
{
    public List<GameObject> points;
    public GameObject enemy;
    [Header("敌人生成的时间间隔")]
    public float waitTime;
    public GameObject enemyClone;
    bool off;

    private void Start()
    {
       
    }
    void Update()
    {
        if (manage._instance.mainSwitch == true && off == false)
        {
            StartCoroutine(Clone());
            off = true;
        }
    }
    IEnumerator Clone()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            GameObject e = Instantiate(enemy.gameObject, points[Random.Range(0, points.Count - 1)].transform.position, Quaternion.identity);
            e.transform.SetParent(enemyClone.transform);
            waitTime = Random.Range(8f, 11f);
        }
    }
}
