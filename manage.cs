using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����ģʽ
/// </summary>
public class manage
{
    private static manage instance;
    public static manage _instance
    {
        get
        {
            if (instance == null )
            {
                instance = new manage();
            }
            return instance;
        }
    }

    //��Ϸ�ܿ���
    public bool mainSwitch;


    //���ǵ��ƶ��ٶ�
    public float speed;


    public void OnExitGame()//����һ���˳���Ϸ�ķ���
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//�������unity��������
#else
        Application.Quit();//�����ڴ���ļ���
#endif
    }

    //��ɱ�����¼�
    public delegate void Money(float number);
    private event Money KillTheEnemy;
    //����¼�
    public void AddedFraction(Money value)
    {
        KillTheEnemy += value;
    }

    //�����¼�
    public void updateFraction(float number)
    {
        if (KillTheEnemy != null)
        {
            KillTheEnemy(number);
        }
    }
    //ɾ���¼�
    public void deleteFraction(Money value)
    {
        KillTheEnemy -= value;
    }


    //���ǿ�Ѫ�¼�
    public delegate void leadhpMoney(float Hp);
    private event leadhpMoney leadhpEvent;
    //������ǿ�Ѫ�¼�
    public void AddedLeadhp(leadhpMoney value)
    {
        leadhpEvent += value;
    }
    //�������ǿ�Ѫ�¼�
    public void updateLeadhp(float _hp)
    {
        if (leadhpEvent != null)
        {
            leadhpEvent(_hp);
        }
    }
    //ɾ�����ǿ�Ѫ�¼�
    public void deleteLeadhp(leadhpMoney number)
    {
        leadhpEvent -= number;
    }
}
