using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// 开始游戏界面代码
/// </summary>
public class startScene : MonoBehaviour
{
    public Button startbtn;
    public GameObject helpobj;
    public Button onhelpbtn;
    public Button yeshelpbtn;
    public Button quitbtn;
    // Start is called before the first frame update
    void Start()
    {
        startbtn.onClick.AddListener(() => SceneManager.LoadScene("GameScenes"));
        onhelpbtn.onClick.AddListener(() => helpobj.SetActive(false));
        yeshelpbtn.onClick.AddListener(() => helpobj.SetActive(true));
        quitbtn.onClick.AddListener(OnExitGame);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnExitGame();
        }
    }
    public void OnExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
