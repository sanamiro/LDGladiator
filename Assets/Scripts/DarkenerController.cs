using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DarkenerController : MonoBehaviour
{
    // Start is called before the first frame update

    public Image darkenerImage;
    public bool isGoingDark = false;
    public bool isGoingLight = false;
    public bool isGoingMiddle = false;
    public string currentScene = "";

    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if (isGoingDark)
            InDarkener();
        else if (isGoingLight)
            OutDarkener();
        else if (isGoingMiddle)
            MiddleDarkener();
    }

    public void InDarkener()
    {
        float currentGamma = darkenerImage.color.a;
        if (currentGamma >= 0.99f)
        {
            isGoingDark = false;
            if (currentScene == "battle")
                GameManager.OnWinStage();
            else if (currentScene == "shop")
                GameManager.LoadNextStage();
        }
        else
        {
            Color newColor = new Color(0, 0, 0, currentGamma + 0.025f);
            darkenerImage.color = newColor;
        }
    }

    public void OutDarkener()
    {
        float currentGamma = darkenerImage.color.a;

        if (currentGamma <= 0.01f)
        {
            isGoingLight = false;
        }
        else
        {
            Color newColor = new Color(0, 0, 0, currentGamma - 0.05f);
            darkenerImage.color = newColor;
        }
    }

    public void MiddleDarkener()
    {
        float currentGamma = darkenerImage.color.a;
        if (currentGamma >= 0.5f)
        {
            isGoingMiddle = false;
        }
        else
        {
            Color newColor = new Color(0, 0, 0, currentGamma + 0.025f);
            darkenerImage.color = newColor;
        }
    }

}
