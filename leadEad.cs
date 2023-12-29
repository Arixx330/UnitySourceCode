using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// ����ʰȡ����
/// </summary>
public class leadEad : MonoBehaviour
{

    //��ȡ�ļӷ�ʳ��
    [Header("�ӷ�ʳ��")]
    public Text numberTest;//��¼ʳ�������
    float number; //ʳ�������

    //��ȡ�ļ���ҩˮ
    [Header("����ҩˮ")]
    public Text speedText;
    float speedNumber;
    float speedtime;

    //����ʱ
    public Text downTineText;
    [Header("����ʱʱ��")]
    public float downTine;

    //��ɱ���˵�����
    [Header("��ɱ���˵�����")]
    public float total;
    public Text KillText;


    [Header("��ʼ����ʱ��ʾ�Ľ�������")]
    public Button button;
    public GameObject bgimg;

    private bool speedOff;
    [Header("�Ե�ֹͣʳ���ʱ��")]
    float traptime = 2;

    //��Ч
    [Header("������Ч������")]
    //������Ч
    public AudioSource audio;
    AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        KillText.text = total.ToString("0") + "/10";
        manage._instance.AddedFraction(KillEvent);
        button.onClick.AddListener(() => {
            manage._instance.mainSwitch = true;//��������Ϸ����
            bgimg.SetActive(false);//���ؽ��ܹ��½���
        });

        clip = audio.GetComponent<AudioClip>();
    }
    //����ɱ�����˵��¼�
    public void KillEvent(float Kill)
    {
        total += Kill;//1+1=2
        KillText.text = total.ToString("0") + "/10";
        if (total >= 6 && number >= 10)
        {
            //�ɹ�
            SceneManager.LoadScene("success");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //����ʱ
        if (downTine >= 0 && manage._instance.mainSwitch == true)
        {
            //��¼����ʱ
            downTine -= Time.deltaTime;
            downTineText.text = "��ʱ��" + downTine.ToString("0");
        }
        else
        {
            if (downTine <= 0)
            {
                //ʧ��
                SceneManager.LoadScene("lose");
            }
        }
       


        //����F����������
        if (Input.GetKeyDown(KeyCode.E) && speedNumber > 0 && speedOff == false)
        {
            float numder = speedNumber -= 1; ;//���ٵ�ҩˮ������һ
            speedText.text = numder.ToString() + "������";
            manage._instance.speed = 10;
            speedtime = 5;
        }
        //���ټ�ʱ��ʼ
        if (speedtime >= 0)
        {
            speedtime -= Time.deltaTime;
        }
        else
        {
            manage._instance.speed = 5;
            speedtime = 5;
        }

        //��ֹ������ж�
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

    //������
    void OnTriggerEnter(Collider cldOther)
    {
        if (cldOther.gameObject.tag == "1")//����teg��Ϊ1�ͼ�1����
        {
            clip = Resources.Load<AudioClip>("Eat(1)");//�õ�ʳ�����Ч
            audio.clip = clip;//��ֵ����Ч����������������
            audio.Play();//������Ч
            Destroy(cldOther.gameObject);//ɾ������������
            float numbers = number += 1;//��һ��ʳ��
            numberTest.text = numbers.ToString() + "/10";//����ʰȡʳ�������
        }
        if (cldOther.gameObject.tag == "2")//ҩˮ����
        {
            clip = Resources.Load<AudioClip>("Quest Complete");
            audio.clip = clip;
            audio.Play();
            Destroy(cldOther.gameObject);
            float numder= speedNumber += 1; ;
            speedText.text = numder.ToString()+"������";
        }
        if (cldOther.gameObject.tag == "3")//�������ӵ���ʱ
        {
            clip = Resources.Load<AudioClip>("Eat(1)");
            audio.clip = clip;
            audio.Play();
            Destroy(cldOther.gameObject);
            downTine += 10;
        }
        if (cldOther.gameObject.tag == "4")//��ɫ��ֹ
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
