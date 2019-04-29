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

    public static GuiController Gui;
    public static List<Stage> stages;

    private static PlayerController player;
    private static MouseManager mouseManager;
    private static EnemySpawner spawner;
    private static DarkenerController darkener;
    private static GameObject gameOverView;

    // Player info
    private static float playerHealth = MaxHealth;
    private static int playerMoney = 0;
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

    public static void RestartGame()
    {
        currentStage = -1;
        SceneManager.LoadScene("BattleArena");
        AudioManager.instance.Play("ig music arena");
        AudioManager.instance.Play("ig crowd ambiance");
        AudioManager.instance.Stop("menu music");
    }

    public static void InitGame()
    {
        if (currentStage == -1)
        {
            playerMoney = 0;
            playerHealth = MaxHealth;
            playerEquipment = new EquipmentInfo();
            currentStage = 0;
            InitStage();
            player.Health = MaxHealth;
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

            AudioManager.instance.Stop("menu music");
            SceneManager.LoadScene("BattleArena");
            AudioManager.instance.LoadArenaFromShop();
        }
    }

    private static void InitStage()
    {
        player = UnityEngine.Object.FindObjectOfType<PlayerController>();
        mouseManager = UnityEngine.Object.FindObjectOfType<MouseManager>();
        spawner = UnityEngine.Object.FindObjectOfType<EnemySpawner>();
        darkener = UnityEngine.Object.FindObjectOfType<DarkenerController>();
        gameOverView = GameObject.Find("GameOverView");
        gameOverView.SetActive(false);
        waveCount = stages[currentStage].Waves.Count;
        bonusMoney = 10;
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
            // Call on win stage
            darkener.currentScene = "battle";
            darkener.isGoingDark = true;
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

    public static void OnWinStage()
    {
        playerHealth = player.Health;
        for (int j = 1; j <= currentStage; j++)
        {
            bonusMoney += bonusMoney / 4;
        }
        for (int i = 0; i < 5 - (int)Mathf.Floor(PlayerHealth / 20); i++)
        {
            bonusMoney += bonusMoney / 2;
        }
        playerMoney += bonusMoney;
        playerEquipment = player.Equipment;


        //Load Marchant scene
        SceneManager.LoadScene("Shop");
        AudioManager.instance.LoadShopFromArena();
    }

    public static void UpdatePlayerInfo()
    {
        playerHealth = player.Health;
        if (Gui != null)
        {
            Gui.UpdatePlayerInfo();
        }
    }

    public static void OnPlayerDie()
    {
        darkener.isGoingMiddle = true;
        mouseManager.enabled = false;
        
        foreach (CharacterController controller in UnityEngine.Object.FindObjectsOfType<CharacterController>())
        {
            controller.enabled = false;
        }

        gameOverView.SetActive(true);
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