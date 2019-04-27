using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{
    public static List<Stage> stages;

    private static PlayerController player;
    private static EnemySpawner spawner;

    // Player info
    private static float playerHealth;
    private static int playerMoney;
    private static EquipmentInfo playerEquipment;

    // Stage Info
    private static int currentStage = -1;

    // Wave info
    private static int waveCount;
    private static int currentWave;
    private static int bonusMoney;

    private static int killCount;
    private static int killsNeededForNextWave;

    public static void InitFirstStage()
    {
        if (currentStage == -1)
        {
            currentStage = 0;
            InitStage();
        }
    }

    public static void LoadNextStage()
    {
        currentStage++;

        if (currentStage >= stages.Count)
        {
            //TODO Fin ?
        }
        else
        {
            //Open Arena Scene
            
            SceneManager.LoadScene("BattleArena");
            InitStage();

            //Reset Player Attributes
            player.Health = playerHealth;
            //player.Equipment = playerEquipment;
        }
    }

    private static void InitStage()
    {
        player = UnityEngine.Object.FindObjectOfType<PlayerController>();
        spawner = UnityEngine.Object.FindObjectOfType<EnemySpawner>();
        waveCount = stages[currentStage].Waves.Count;
        bonusMoney = 0;
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
        killCount = 0;
        if (currentWave >= waveCount)
        {
            OnWinStage();
        }
        else
        {
            //Load new wave data

            List<EnemyData> enemiesToSpawn = stages[currentStage].Waves[currentWave].Enemies;

            killsNeededForNextWave = 0;
            foreach (EnemyData enemy in enemiesToSpawn)
            {
                killsNeededForNextWave += enemy.Count;

                //Spawn enemy
                for (int i = 0; i < enemy.Count; i++)
                {
                    spawner.SpawnEnemy(spawner.transform, enemy.EnemyType);
                }
            }
        }
    }

    private static void OnWinStage()
    {
        playerHealth = player.Health;
        //playerEquipment = player.Equipment;

        //Load Marchant scene
        SceneManager.LoadScene("Store");
    }

}