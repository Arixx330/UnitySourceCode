using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class end : MonoBehaviour
{
    public Button quitbtn;//������ť����
    // Start is called before the first frame update
    void Start()
    {
        //���õ���ģʽ������˳�����
        quitbtn.onClick.AddListener(manage._instance.OnExitGame);
    }
   
}
