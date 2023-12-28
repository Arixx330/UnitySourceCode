using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 主角发射技能与技能的切换
/// </summary>
public class RayMagic : MonoBehaviour
{
   
    public GameObject FirePoint;//发射点
    //public Camera Cam;//摄像机
    public float MaxLength;
    public GameObject[] Prefabs;//播放的特效

    private Ray RayMouse;//射线
    private Vector3 direction;//位置
    private Quaternion rotation;


    private int Prefab;

    private float hSliderValue = 0.1f;
    private float fireCountdown = 0f;

    private float buttonSaver = 0f;

    public Animation camAnim;

    public Image _fill;
    float leadHp;
    void Start()
    {
        _fill = transform.Find("100710_albb_npc/Head_point/Canvas/hp/bar0").GetComponent<Image>();
        manage._instance.AddedLeadhp(InitHead);
    }

    public void CreatSkill()
    {
        //实例化特效               在发射点的位置                     发射点的旋转
        Instantiate(Prefabs[Prefab], FirePoint.transform.position, FirePoint.transform.rotation);
    }
    void Update()
    {
        //Single shoot
        if (Input.GetKeyDown(KeyCode.R))
        {
            camAnim.Play(camAnim.clip.name);//播放特效
        }

        //To change projectiles 换子弹
        if ((Input.GetKey(KeyCode.F)) && buttonSaver >= 0.4f)// left button
        {
            buttonSaver = 0f;
            Counter(-1);
        }
        if ((Input.GetKey(KeyCode.G)) && buttonSaver >= 0.4f)// right button
        {
            buttonSaver = 0f;
            Counter(+1);
        }
        buttonSaver += Time.deltaTime;
    }


    // To change prefabs (count - prefab number)
    void Counter(int count)
    {
        Prefab += count;
        if (Prefab > Prefabs.Length - 1)
        {
            Prefab = 0;
        }
        else if (Prefab < 0)
        {
            Prefab = Prefabs.Length - 1;
        }
    }

    private void AttackTarget()
    {
        // 触发攻击动作
        this.GetComponent<Animator>().SetTrigger("Attack");
    }
    //拿到当前血量
    public void InitHead(float number)
    {
        //   计算血量
       leadHp = number / 1;
        _fill.fillAmount -= leadHp;
        float i = _fill.fillAmount;
        Debug.Log(i + "血量"); 
    }
}
