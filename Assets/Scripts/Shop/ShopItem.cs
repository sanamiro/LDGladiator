using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public ItemType Type;
    public int Level = 1;

    private ShopController controller;
    private Button button;

    private bool available = true;
    public Transform winScreen;
    private void Awake()
    {
        button = GetComponentInChildren<Button>();
    }

    private void Start()
    {
        Transform priceObj = transform.Find("PRICE/Value");
        if (priceObj != null)
        {
            TextMeshProUGUI priceText = priceObj.GetComponent<TextMeshProUGUI>();
            if (priceText != null)
            {
                priceText.text = Price.ToString();
            }
        }

        Transform nameObj = transform.Find("ItemName");
        if (nameObj != null)
        {
            TextMeshProUGUI nameText = nameObj.GetComponent<TextMeshProUGUI>();
            if (nameText != null)
            {
                nameText.text = ItemName;
            }
        }
    }

    public void UpdateState(ShopController controller)
    {
        this.controller = controller;
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
                Available = controller.State.IsHealingItemAvailable(Level);
                break;
        }
    }

    public void OnClicked()
    {
        controller.Select(this);
    }

    public void OnBuy()
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
                winScreen.gameObject.SetActive(true);
                Debug.Log("You Won !");
                break;
            case ItemType.Heal:
                GameManager.HealPlayer(HealingItemStats.GetLifeRegeneration(Level));
                controller.State.ConsumeHealingItem(Level);
                controller.UpdatePlayerData();
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
                case ItemType.TheWoodenSword: return EquipmentStats.WoodenSwordPrice;
                case ItemType.Heal: return HealingItemStats.GetPrice(Level);

                default: return 0;
            }
        }
    }

    public string ItemName
    {
        get
        {
            switch (Type)
            {
                case ItemType.Sword: return EquipmentStats.GetSwordName(Level);
                case ItemType.Armor: return EquipmentStats.GetArmorName(Level);
                case ItemType.Sandal: return EquipmentStats.GetSandalName(Level);
                case ItemType.Cape: return EquipmentStats.GetCapeName(Level);
                case ItemType.TheWoodenSword: return EquipmentStats.WoodenSwordName;
                case ItemType.Heal: return HealingItemStats.GetName(Level);

                default: return "";
            }
        }
    }

    public string ItemDesc
    {
        get
        {
            switch (Type)
            {
                case ItemType.Sword: return EquipmentStats.GetSwordDesc(Level);
                case ItemType.Armor: return EquipmentStats.GetArmorDesc(Level);
                case ItemType.Sandal: return EquipmentStats.GetSandalDesc(Level);
                case ItemType.Cape: return EquipmentStats.GetCapeDesc(Level);
                case ItemType.TheWoodenSword: return EquipmentStats.WoodenSwordDesc;
                case ItemType.Heal: return HealingItemStats.GetDesc(Level);

                default: return "";
            }
        }
    }

    public bool Selected
    {
        set
        {

        }
    }

    public void onSelected()
    {
        switch (transform.GetSiblingIndex())
        {
            case 0:
            case 1:
            case 2:
            case 3:
                controller.scroller.verticalNormalizedPosition = 1;
                break;

            case 4:
            case 5:
            case 6:
            case 7:
                controller.scroller.verticalNormalizedPosition = 0.5f;
                break;

            case 8:
            case 9:
            case 10:
            case 11:
                controller.scroller.verticalNormalizedPosition = 0;
                break;

            default:
                break;
        }
    }

    public bool Available
    {
        get => available;
        set
        {
            available = value;
            button.interactable = value;
        }
    }

    public Button Button { get => button; }
}