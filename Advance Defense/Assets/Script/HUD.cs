using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    PlayerMovement playerMovement;
    public enum Infotype { Exp, Level, Kill, Time, Health } // 유니티 인스펙터 열거형
    public Infotype type;

    Text myText;
    Slider mySlider;

    void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
        GameObject playerObj = GameObject.Find("Player");
        playerMovement = playerObj.GetComponent<PlayerMovement>();
    }

    void LateUpdate()
    {
        switch (type)
        {
            case Infotype.Exp:
                float curExp = GameManager.instance.exp;
                float maxExp = GameManager.instance.nextExp[GameManager.instance.level];
                mySlider.value = curExp / maxExp;
                break;
            case Infotype.Level:
                myText.text = string.Format("Lv.{0:F0}", GameManager.instance.level);
                break;
            case Infotype.Kill:

                break;
            case Infotype.Time:

                break;
            case Infotype.Health:
                float curHealth = playerMovement.PlayerHp;
                float maxHealth = playerMovement.MaxPlayerHp;
                mySlider.value = curHealth / maxHealth;
                break;
        } 
    }
}
