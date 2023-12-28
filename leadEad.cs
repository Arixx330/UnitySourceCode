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
    [Header("倒计时")]
    public Text downTineText;
    public float downTine;

    //击杀敌人的数量
    [Header("击杀敌人的数量")]
    public float total;
    public Text KillText;

    //音效
    public AudioSource audio;

    public Button button;
    public GameObject bgimg;
    // Start is called before the first frame update
    void Start()
    {
        KillText.text = total.ToString("0") + "/10";
        manage._instance.AddedFraction(KillEvent);
        button.onClick.AddListener(() => {
            manage._instance.mainSwitch = true;
            bgimg.SetActive(false);
        });
    }
    //计算杀死敌人的事件
    public void KillEvent(float Kill)
    {
        total += Kill;
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
            //失败
            SceneManager.LoadScene("lose");
        }
       


        //按下F健触发加速
        if (Input.GetKeyDown(KeyCode.E) && speedNumber > 0)
        {
            float numder = speedNumber -= 1; ;
            speedText.text = numder.ToString() + "个加速";
            manage._instance.speed = 10;
            speedtime = 5;
        }
        if (speedtime >= 0)
        {
            speedtime -= Time.deltaTime;
        }
        else
        {
            manage._instance.speed = 5;
            speedtime = 5;
        }
    }
    void OnTriggerEnter(Collider cldOther)
    {
        if (cldOther.gameObject.tag == "1")
        {
            audio.Play();
            Destroy(cldOther.gameObject);
            float numbers = number += 1;
            numberTest.text = numbers.ToString() + "/10";
        }
        if (cldOther.gameObject.tag == "2")
        {
            audio.Play();
            Destroy(cldOther.gameObject);
            float numder= speedNumber += 1; ;
            speedText.text = numder.ToString()+"个加速";
        }
        if (cldOther.gameObject.tag == "3")
        {
            audio.Play();
            Destroy(cldOther.gameObject);
            downTine += 10;
        }
    }
}
