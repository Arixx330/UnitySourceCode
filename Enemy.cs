using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
/// <summary>
/// ���˹���
/// </summary>
public class Enemy : MonoBehaviour
{
    public float speed;//ɢ��ʱ���ٶ�
    public float waitTime;//�ȴ�ʱ��

    public float distance;//�ƶ��ķ�Χ

    public float enemyHp;//����Ѫ��

    public float hurtDistance;//�ӵ��ڵ��˴ﵽһ������ʱ���˿�Ѫ


    //Ѱ·���
    NavMeshAgent navMeshAgent;
    //���ǵ�λ��
    private Transform player;

    //����Ѫ��
    //public Slider slider;
    public Animator animator;
    private Vector3 tempPoint;

    public Image _fill;

    private float max = 1;
    private float cur_hp;

    //����Ѫ��
    float leadHp;

    float times = 2;
    private void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        StartCoroutine(Move());
        //slider.value = 1.0f;
        //��ʼ��ֵ
        navMeshAgent.speed = speed;
        player = GameObject.Find("Player").transform;

        //navMeshAgent.areaMask = 1;
        _fill = transform.Find("Head_point/Canvas/hp/bar0").GetComponent<Image>();
    }
    
    public void InitHead()
    {
        //    //�õ���ǰѪ��
        enemyHp -= Random.Range(0.2f, 0.6f);
        _fill.fillAmount = cur_hp = enemyHp / max;
    }

    private void Update()
    {
        if (manage._instance.mainSwitch == true)
        {

            //�ж�Ѫ��
            if (enemyHp <= 0)
            {
                navMeshAgent.enabled = false;
                Destroy(this.gameObject);
                manage._instance.updateFraction(1);
            }
            if (enemyHp > 0)
            {
                //�������ߵ����ǵ�λ��
                navMeshAgent.SetDestination(player.position);
                float m03 = Vector3.Distance(this.transform.position, player.position);//����
                if (m03 > 3f)
                {
                    animator.SetBool("enemyAttack", false);//�ƶ�����
                }
                else
                {
                    //����������
                    tempPoint = new Vector3(player.position.x, transform.position.y, player.position.z);
                    float tempAngle = Vector3.Angle(transform.forward, tempPoint - transform.position);//�����Ŀ��ļн�
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(tempPoint - transform.position), 0.2f);
                    //���Ź�������
                    animator.SetBool("enemyAttack", true);
                   
                    if (times > 0)
                    {
                        times -= Time.deltaTime;
                    }
                    else
                    {
                        //���ǿ�Ѫ
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

            //�����
            Vector3 movev3 = new Vector3(Random.Range(this.transform.position.x - distance, this.transform.position.x + distance), this.transform.position.y, Random.Range(this.transform.position.z - distance, this.transform.position.z + distance));
            navMeshAgent.SetDestination(movev3);
            waitTime = Random.Range(5, 15);
        }
    }
}
