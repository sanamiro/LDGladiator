using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopItemButton : MonoBehaviour, ISelectHandler
{

    public ShopItem shopItem;

    public void OnSelect(BaseEventData eventData)
    {
        shopItem.onSelected();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
