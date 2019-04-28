using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    public TextMeshProUGUI PlayerMoney;
    public Image LifeBar;

    // Item Description
    public TextMeshProUGUI ItemName;
    public TextMeshProUGUI ItemInfo;
    public Button BuyButton;
    public Button QuitButton;
    public DarkenerController Darkener;
    public SelectOnInput selector;

    public List<ShopItem> items;

    private ShopState state = new ShopState();
    private ShopItem selectedItem;

    private void Start()
    {
        state.OnStagePassed();
        items.ForEach(item => item.UpdateState(this));
        BuyButton.interactable = false;
        UpdatePlayerData();
        Darkener.isGoingLight = true;
    }

    public void UpdatePlayerData()
    {
        PlayerMoney.text = GameManager.PlayerMoney.ToString();
        LifeBar.fillAmount = GameManager.PlayerHealth / GameManager.MaxHealth;
    }

    public void Select(ShopItem item)
    {
        if (selectedItem != null) selectedItem.Selected = false;
        selectedItem = item;

        ItemName.text = item.ItemName;
        ItemInfo.text = item.ItemDesc;

        if (selectedItem != null)
        {
            selectedItem.Selected = true;
            BuyButton.interactable = selectedItem.Price <= GameManager.PlayerMoney;
        }
        else BuyButton.interactable = false;
    }

    public void Buy()
    {
        if (selectedItem != null && selectedItem.Available && selectedItem.Price <= GameManager.PlayerMoney)
        {
            GameManager.SpendMoney(selectedItem.Price);
            selectedItem.OnBuy();
            items.ForEach(item => item.UpdateState(this));
            UpdatePlayerData();
            BuyButton.interactable = false;
            items[0].Selected = true;
            selector.enabled = false;
            selector.enabled = true;
        }
    }

    public void QuitShop()
    {
        Darkener.currentScene = "shop";
        Darkener.isGoingDark = true;
    }

    public ShopState State { get => state; }
}