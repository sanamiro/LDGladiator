using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{
    public static readonly float MaxHealth = 100;

    public static List<Stage> stages;

    private static PlayerController player;
    private static EnemySpawner spawner;

    // Player info
    private static float playerHealth = MaxHealth;
    private static int playerMoney = 20;
    private static EquipmentInfo playerEquipment = new EquipmentInfo()
    { //Default value
        swordLevel = 0,
        armorLevel = 0,
        sandalLevel = 0,
        capeLevel = 0
    };

    // Stage Info
    private static int currentStage = -1;

    // Wave info
    private static int waveCount;
    private static int currentWave;
    private static int bonusMoney;

    private static int killCount;
    private static int killsNeededForNextWave;

    public static void InitGame()
    {
        if (currentStage == -1)
        {
            currentStage = 0;
            InitStage();
        }
        else
        {
            InitStage();

            //Reset Player Attributes
            player.Health = playerHealth;
            player.Equipment = playerEquipment;
        }
    }

    public static void LoadNextStage()
    {
        currentStage++;

        if (currentStage >= stages.Count)
        {
            //TODO Fin ?
            Debug.Log("No more stages");
        }
        else
        {
            //Open Arena Scene
            SceneManager.LoadScene("EnemyScene"); //TODO Change to "BattleArena"
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
        playerEquipment = player.Equipment;

        //Load Marchant scene
        SceneManager.LoadScene("Shop");
    }

    // Shop
    public static void HealPlayer(float regeneration)
    {
        playerHealth += regeneration;
        if (playerHealth > MaxHealth)
        {
            playerHealth = MaxHealth;
        }
    }

    public static void SpendMoney(int amount)
    {
        playerMoney -= amount;
    }

    public static float PlayerHealth { get => playerHealth; }
    public static EquipmentInfo PlayerEquipment { get => playerEquipment; }
    public static int PlayerMoney { get => playerMoney; }
}