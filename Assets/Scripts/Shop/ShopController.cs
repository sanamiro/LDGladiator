using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{

    public Text ItemName;
    public Text ItemInfo;

    public List<ShopItem> items;

    private ShopState state = new ShopState();

    private void Start()
    {
        state.OnStagePassed();
        items.ForEach(item => item.UpdateState(state));
    }

}