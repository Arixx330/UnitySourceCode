using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
/// <summary>
/// 敌人攻击
/// </summary>
public class Enemy : MonoBehaviour
{
    public float speed;//散步时的速度
    public float waitTime;//等待时间

    public float distance;//移动的范围

    public float enemyHp;//敌人血量

    public float hurtDistance;//子弹于敌人达到一定距离时敌人扣血


    //寻路组件
    NavMeshAgent navMeshAgent;
    //主角的位置
    private Transform player;

    //怪物血条
    //public Slider slider;
    public Animator animator;
    private Vector3 tempPoint;

    public Image _fill;

    private float max = 1;
    private float cur_hp;

    //主角血量
    float leadHp;

    float times = 2;
    private void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        StartCoroutine(Move());
        //slider.value = 1.0f;
        //初始化值
        navMeshAgent.speed = speed;
        player = GameObject.Find("Player").transform;

        //navMeshAgent.areaMask = 1;
        _fill = transform.Find("Head_point/Canvas/hp/bar0").GetComponent<Image>();
    }
    
    public void InitHead()
    {
        //    //拿到当前血量
        enemyHp -= Random.Range(0.2f, 0.6f);
        _fill.fillAmount = cur_hp = enemyHp / max;
    }

    private void Update()
    {
        if (manage._instance.mainSwitch == true)
        {

            //判断血量
            if (enemyHp <= 0)
            {
                navMeshAgent.enabled = false;
                Destroy(this.gameObject);
                manage._instance.updateFraction(1);
            }
            if (enemyHp > 0)
            {
                //找主角走到主角的位置
                navMeshAgent.SetDestination(player.position);
                float m03 = Vector3.Distance(this.transform.position, player.position);//距离
                if (m03 > 3f)
                {
                    animator.SetBool("enemyAttack", false);//移动动作
                }
                else
                {
                    //需面向主角
                    tempPoint = new Vector3(player.position.x, transform.position.y, player.position.z);
                    float tempAngle = Vector3.Angle(transform.forward, tempPoint - transform.position);//自身和目标的夹角
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(tempPoint - transform.position), 0.2f);
                    //播放攻击动作
                    animator.SetBool("enemyAttack", true);
                   
                    if (times > 0)
                    {
                        times -= Time.deltaTime;
                    }
                    else
                    {
                        //主角扣血
                        leadHp = Random.Range(0.05f, 0.1f);
                        manage._instance.updateLeadhp(leadHp);
                        times = 2;
                    }
                  
                }
            }
        }


    }

    IEnumerator Move()
    {
        while (true)
        {
            yield return null;
            yield return new WaitForSeconds(waitTime);

            //随机点
            Vector3 movev3 = new Vector3(Random.Range(this.transform.position.x - distance, this.transform.position.x + distance), this.transform.position.y, Random.Range(this.transform.position.z - distance, this.transform.position.z + distance));
            navMeshAgent.SetDestination(movev3);
            waitTime = Random.Range(5, 15);
        }
    }
}
