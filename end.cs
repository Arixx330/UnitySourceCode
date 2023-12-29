using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class end : MonoBehaviour
{
    public Button quitbtn;//声明按钮变量
    // Start is called before the first frame update
    void Start()
    {
        //调用单例模式里面的退出方法
        quitbtn.onClick.AddListener(manage._instance.OnExitGame);
    }
   
}
