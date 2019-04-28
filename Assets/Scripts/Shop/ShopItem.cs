using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public enum ItemType
    {
        Sword,
        Armor,
        Sandal,
        Cape,
        TheWoodenSword,
        Heal
    }

    public ItemType Type;
    public int Level = 1;

    public void OnBuy(ShopState shopState)
    {
        switch (Type)
        {
            case ItemType.Sword:
                GameManager.PlayerEquipment.swordLevel = Level;
                break;
            case ItemType.Armor:
                GameManager.PlayerEquipment.armorLevel = Level;
                break;
            case ItemType.Sandal:
                GameManager.PlayerEquipment.sandalLevel = Level;
                break;
            case ItemType.Cape:
                GameManager.PlayerEquipment.capeLevel = Level;
                break;
            case ItemType.TheWoodenSword:
                //TODO You Won
                Debug.Log("You Won !");
                break;
            case ItemType.Heal:
                GameManager.HealPlayer(HealingItemStats.GetLifeRegeneration(Level));
                shopState.ConsumeHealingItem(Level);
                break;
        }
    }

    public void UpdateState(ShopState shopState)
    {
        switch (Type)
        {
            case ItemType.Sword:
                Available = Level > GameManager.PlayerEquipment.swordLevel;
                break;
            case ItemType.Armor:
                Available = Level > GameManager.PlayerEquipment.armorLevel;
                break;
            case ItemType.Sandal:
                Available = Level > GameManager.PlayerEquipment.sandalLevel;
                break;
            case ItemType.Cape:
                Available = Level > GameManager.PlayerEquipment.capeLevel;
                break;
            case ItemType.TheWoodenSword:
                Available = true;
                break;
            case ItemType.Heal:
                Available = shopState.IsHealingItemAvailable(Level);
                break;
        }
    }

    public int Price
    {
        get
        {
            switch(Type)
            {
                case ItemType.Sword: return EquipmentStats.GetSwordPrice(Level);
                case ItemType.Armor: return EquipmentStats.GetArmorPrice(Level);
                case ItemType.Sandal: return EquipmentStats.GetSandalPrice(Level);
                case ItemType.Cape: return EquipmentStats.GetCapePrice(Level);
                case ItemType.TheWoodenSword:
                    return 100;
                case ItemType.Heal: return HealingItemStats.GetPrice(Level);

                default: return 0;
            }
        }
    }

    public bool Available
    {
        set
        {
            gameObject.SetActive(value);
        }
    }

}