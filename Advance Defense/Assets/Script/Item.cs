using UnityEngine;
using UnityEngine.UI;
public class Item : MonoBehaviour
{
    public ItemData data;
    public int level;
    public Gear gear;

    Text icon;
    Text textLevel;

    void Awake()
    {
        icon = GetComponentsInChildren<Text>()[1];

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
    }

    void LateUpdate()
    {
        textLevel.text = "Lv." + (level);
    }

    public void OnClick()
    {
        switch (data.itemType)
        {
            case ItemData.ItemType.Speed:
            case ItemData.ItemType.AtkDmg:
                if (level == 0)
                {
                    GameObject newGear = new GameObject();
                    gear = newGear.AddComponent<Gear>();
                    gear.Init(data);
                }
                else
                {
                    float nextRate = data.damages[level];
                    gear.LevelUp(nextRate);
                }
                level++;
                break;
            case ItemData.ItemType.Heal:
                GameManager.instance.player.GetComponent<PlayerMovement>().Heal(5);
                break;
        }


        if (level == data.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
