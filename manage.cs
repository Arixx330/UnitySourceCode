using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 单例模式
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

    //游戏总开关
    public bool mainSwitch;


    //主角的移动速度
    public float speed;


    public void OnExitGame()//定义一个退出游戏的方法
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//如果是在unity编译器中
#else
        Application.Quit();//否则在打包文件中
#endif
    }

    //击杀敌人事件
    public delegate void Money(float number);
    private event Money KillTheEnemy;
    //添加事件
    public void AddedFraction(Money value)
    {
        KillTheEnemy += value;
    }

    //更新事件
    public void updateFraction(float number)
    {
        if (KillTheEnemy != null)
        {
            KillTheEnemy(number);
        }
    }
    //删除事件
    public void deleteFraction(Money value)
    {
        KillTheEnemy -= value;
    }


    //主角扣血事件
    public delegate void leadhpMoney(float Hp);
    private event leadhpMoney leadhpEvent;
    //添加主角扣血事件
    public void AddedLeadhp(leadhpMoney value)
    {
        leadhpEvent += value;
    }
    //更新主角扣血事件
    public void updateLeadhp(float _hp)
    {
        if (leadhpEvent != null)
        {
            leadhpEvent(_hp);
        }
    }
    //删除主角扣血事件
    public void deleteLeadhp(leadhpMoney number)
    {
        leadhpEvent -= number;
    }
}
