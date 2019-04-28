using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public List<Stage> stages;

    private void Awake()
    {
        GameManager.stages = stages;
        GameManager.InitGame();
    }

}
