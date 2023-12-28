using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ���Ƿ��似���뼼�ܵ��л�
/// </summary>
public class RayMagic : MonoBehaviour
{
   
    public GameObject FirePoint;//�����
    //public Camera Cam;//�����
    public float MaxLength;
    public GameObject[] Prefabs;//���ŵ���Ч

    private Ray RayMouse;//����
    private Vector3 direction;//λ��
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
        //ʵ������Ч               �ڷ�����λ��                     ��������ת
        Instantiate(Prefabs[Prefab], FirePoint.transform.position, FirePoint.transform.rotation);
    }
    void Update()
    {
        //Single shoot
        if (Input.GetKeyDown(KeyCode.R))
        {
            camAnim.Play(camAnim.clip.name);//������Ч
        }

        //To change projectiles ���ӵ�
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
        // ������������
        this.GetComponent<Animator>().SetTrigger("Attack");
    }
    //�õ���ǰѪ��
    public void InitHead(float number)
    {
        //   ����Ѫ��
       leadHp = number / 1;
        _fill.fillAmount -= leadHp;
        float i = _fill.fillAmount;
        Debug.Log(i + "Ѫ��"); 
    }
}
