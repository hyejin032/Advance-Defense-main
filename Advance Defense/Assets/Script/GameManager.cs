using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("# Game Control")]
    public static GameManager instance;
    public GameObject menu;
    public GameObject Expbar;
    public GameObject HPbar;
    public Text Win;
    public string thisScene;
    [Header("# Player Info")]
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 선택: 씬 전환 후에도 유지하고 싶을 때
        }
        else
        {
            Destroy(gameObject); // 중복 방지
        }
    }
    void Start()
    {
        Time.timeScale = 0f;
        Expbar.SetActive(false);
        HPbar.SetActive(false);
    }
    void Update()
    {

    }
    public void Deactivate()
    {
        if (menu != null)
        {
            menu.SetActive(false);
            Time.timeScale = 1f;
        }
        if (Expbar != null)
        {
            Expbar.SetActive(true);
        }
        if (HPbar != null)
        {
            HPbar.SetActive(true);
        }
    }

    public void GetExp()
    {
        exp++;
        if (exp == nextExp[level])
        {
            level++;
            exp = 0;

        }
    }
    public void OnBaseDestroyed()
    {
        if (Win != null)
        {
            Win.gameObject.SetActive(true);
        }
        Time.timeScale = 0f;
    }
}
