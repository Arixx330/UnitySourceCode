using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// //技能打中敌人后扣血
/// </summary>
public class magicArts : MonoBehaviour
{
    private AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = GameObject.Find("beInjured").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider cldOther)
    {
        if (cldOther.gameObject.tag == "Enemy")
        {
            audio.Play();
            Enemy enemy = cldOther.gameObject.GetComponent<Enemy>();
            enemy.InitHead();
        }
        
    }
}
