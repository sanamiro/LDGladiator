using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class GameManager
{
    private static PlayerController player;

    // Player info
    private static float playerHealth;
    private static int playerMoney;
    private static EquipmentInfo playerEquipment;

    // Wave info
    private static int waveCount;
    private static int currentWave;
    private static int bonusMoney;

    private static int killCount;
    private static int killsNeededForNextWave;

    public static void InitStage(PlayerController player, int waveCount)
    {
        GameManager.player = player;
        GameManager.waveCount = waveCount;
        bonusMoney = 0;
        killCount = 0;
        currentWave = 0;
        LoadWave();
    }

    public static void OnEnemyKilled()
    {
        killCount++;
        if (killCount >= killsNeededForNextWave)
        {
            currentWave++;
            LoadWave();
        }
    }

    private static void LoadWave()
    {
        if (currentWave >= waveCount)
        {
            OnWinStage();
        }
        else
        {
            //TODO Load new wave data

            //Start wave
        }
    }

    private static void OnWinStage()
    {
        playerHealth = player.Health;
        //playerEquipment = player.Equipment;

        //TODO Load Marchant scene
    }

}