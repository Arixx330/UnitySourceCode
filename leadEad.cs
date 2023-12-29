using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// 主角拾取物体
/// </summary>
public class leadEad : MonoBehaviour
{

    //获取的加分食物
    [Header("加分食物")]
    public Text numberTest;//记录食物的数量
    float number; //食物的数量

    //获取的加速药水
    [Header("加速药水")]
    public Text speedText;
    float speedNumber;
    float speedtime;

    //倒计时
    public Text downTineText;
    [Header("倒计时时间")]
    public float downTine;

    //击杀敌人的数量
    [Header("击杀敌人的数量")]
    public float total;
    public Text KillText;


    [Header("开始界面时显示的介绍物体")]
    public Button button;
    public GameObject bgimg;

    private bool speedOff;
    [Header("吃到停止食物的时常")]
    float traptime = 2;

    //音效
    [Header("播放音效的物体")]
    //加载音效
    public AudioSource audio;
    AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        KillText.text = total.ToString("0") + "/10";
        manage._instance.AddedFraction(KillEvent);
        button.onClick.AddListener(() => {
            manage._instance.mainSwitch = true;//打开启动游戏开关
            bgimg.SetActive(false);//隐藏介绍故事界面
        });

        clip = audio.GetComponent<AudioClip>();
    }
    //计算杀死敌人的事件
    public void KillEvent(float Kill)
    {
        total += Kill;//1+1=2
        KillText.text = total.ToString("0") + "/10";
        if (total >= 6 && number >= 10)
        {
            //成功
            SceneManager.LoadScene("success");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //倒计时
        if (downTine >= 0 && manage._instance.mainSwitch == true)
        {
            //记录倒计时
            downTine -= Time.deltaTime;
            downTineText.text = "计时：" + downTine.ToString("0");
        }
        else
        {
            if (downTine <= 0)
            {
                //失败
                SceneManager.LoadScene("lose");
            }
        }
       


        //按下F健触发加速
        if (Input.GetKeyDown(KeyCode.E) && speedNumber > 0 && speedOff == false)
        {
            float numder = speedNumber -= 1; ;//加速的药水数量减一
            speedText.text = numder.ToString() + "个加速";
            manage._instance.speed = 10;
            speedtime = 5;
        }
        //加速计时开始
        if (speedtime >= 0)
        {
            speedtime -= Time.deltaTime;
        }
        else
        {
            manage._instance.speed = 5;
            speedtime = 5;
        }

        //静止情况的判断
        if (speedOff == true)
        {
            traptime -= Time.deltaTime;
            if (traptime <= 0)
            {
                manage._instance.speed = 5;
                traptime = 2;
                Debug.Log(manage._instance.speed);
                speedOff = false;
            }
        }
    }

    //触发器
    void OnTriggerEnter(Collider cldOther)
    {
        if (cldOther.gameObject.tag == "1")//碰到teg名为1就加1数量
        {
            clip = Resources.Load<AudioClip>("Eat(1)");//拿到食物的音效
            audio.clip = clip;//赋值给音效给播放声音的物体
            audio.Play();//播放音效
            Destroy(cldOther.gameObject);//删除碰到的物体
            float numbers = number += 1;//加一个食物
            numberTest.text = numbers.ToString() + "/10";//计算拾取食物的数量
        }
        if (cldOther.gameObject.tag == "2")//药水加速
        {
            clip = Resources.Load<AudioClip>("Quest Complete");
            audio.clip = clip;
            audio.Play();
            Destroy(cldOther.gameObject);
            float numder= speedNumber += 1; ;
            speedText.text = numder.ToString()+"个加速";
        }
        if (cldOther.gameObject.tag == "3")//蛋糕增加倒计时
        {
            clip = Resources.Load<AudioClip>("Eat(1)");
            audio.clip = clip;
            audio.Play();
            Destroy(cldOther.gameObject);
            downTine += 10;
        }
        if (cldOther.gameObject.tag == "4")//红色球静止
        {
            clip = Resources.Load<AudioClip>("Player Hit");
            audio.clip = clip;
            audio.Play();
            Destroy(cldOther.gameObject);
            manage._instance.speed = 0;
            speedOff = true;
            Debug.Log(manage._instance.speed);
        }
    }
}
