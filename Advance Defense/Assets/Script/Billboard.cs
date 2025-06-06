using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class Billboard : MonoBehaviour
{
    public GameObject HpBar;
    void Start()
    {
    }
 
    void Update()
    {
        // 오브젝트에 따른 HP Bar 위치 이동
        HpBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 0.8f, 0));
    }
}