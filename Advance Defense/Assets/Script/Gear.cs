using UnityEngine;

public class Gear : MonoBehaviour
{
    public ItemData.ItemType type;
    public float rate;

    public GameObject player;

    PlayerMovement playerMovement;

    public void Init(ItemData data)
    {
        name = "Gear" + data.itemId;
        player = GameManager.instance.player;
        playerMovement = player.GetComponent<PlayerMovement>();
        transform.parent = GameManager.instance.player.transform;
        transform.localPosition = Vector3.zero;

        type = data.itemType;
        rate = data.damages[0];
        ApplyGear();
    }

    public void LevelUp(float rate)
    {
        this.rate = rate;
        ApplyGear();
    }

    void ApplyGear()
    {
        switch (type)
        {
            case ItemData.ItemType.Speed:
                SpeedUp();
                break;
            case ItemData.ItemType.AtkDmg:
                PowerUp();
                break;
        }
    }

    void SpeedUp()
    {
        float speed = 1;
        playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.Playerspeed = speed + speed * rate;
    }

    void PowerUp()
    {
        float power = 1;
        playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.Power = power + power * rate;
    }
}
