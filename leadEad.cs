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
    [Header("����ʱ")]
    public Text downTineText;
    public float downTine;

    //��ɱ���˵�����
    [Header("��ɱ���˵�����")]
    public float total;
    public Text KillText;

    //��Ч
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
    //����ɱ�����˵��¼�
    public void KillEvent(float Kill)
    {
        total += Kill;
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
            //ʧ��
            SceneManager.LoadScene("lose");
        }
       


        //����F����������
        if (Input.GetKeyDown(KeyCode.E) && speedNumber > 0)
        {
            float numder = speedNumber -= 1; ;
            speedText.text = numder.ToString() + "������";
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
            speedText.text = numder.ToString()+"������";
        }
        if (cldOther.gameObject.tag == "3")
        {
            audio.Play();
            Destroy(cldOther.gameObject);
            downTine += 10;
        }
    }
}
