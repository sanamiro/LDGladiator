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
    public TextMeshProUGUI ItemPrice;
    public Button BuyButton;
    public Button QuitButton;
    public DarkenerController Darkener;
    public SelectOnInput selector;
    public ScrollRect scroller;

    public List<ShopItem> items;

    private ShopState state = new ShopState();
    private ShopItem selectedItem;

    private void Start()
    {
        items.ForEach(item => item.UpdateState(this));
        UpdatePlayerData();
        Darkener.isGoingLight = true;
        Select(items[0]);
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
        ItemPrice.text = item.Price.ToString();

        if (selectedItem != null)
        {
            selectedItem.Selected = true;
            selector.eventSystem.SetSelectedGameObject(item.Button.gameObject);
            BuyButton.interactable = selectedItem.Price <= GameManager.PlayerMoney;
        }
        else BuyButton.interactable = false;
    }

    public void Buy()
    {
        if (selectedItem != null && selectedItem.Available && selectedItem.Price <= GameManager.PlayerMoney)
        {
            GameManager.SpendMoney(selectedItem.Price);
            AudioManager.instance.Play("ig merchant buy item");
            selectedItem.OnBuy();
            items.ForEach(item => item.UpdateState(this));
            UpdatePlayerData();
            if (!selectedItem.Available)
                Select(items[0]);
            /*selector.enabled = false;
            selector.enabled = true;*/
        }
        else if(selectedItem.Price > GameManager.PlayerMoney)
        {
            AudioManager.instance.Play("ig merchant not enought currency");
        }
    }

    public void QuitShop()
    {
        Darkener.currentScene = "shop";
        Darkener.isGoingDark = true;
    }

    public ShopState State { get => state; }
}