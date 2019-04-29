using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiController : MonoBehaviour
{
    public Image LifeBar;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Gui = this;
        UpdatePlayerInfo();
    }

    public void UpdatePlayerInfo()
    {
        LifeBar.fillAmount = GameManager.PlayerHealth / GameManager.MaxHealth;
    }
}
