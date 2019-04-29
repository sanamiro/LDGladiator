using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour
{
    public List<Stage> stages;

    private void Awake()
    {
        GameManager.stages = stages;
        GameManager.InitGame();
        SceneManager.LoadScene("GUI", LoadSceneMode.Additive);
    }

    public void RestartGame()
    {
        GameManager.RestartGame();
    }

}
