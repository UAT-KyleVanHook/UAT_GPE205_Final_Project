using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Level level;

    bool bGameOver = false;

   

    [Header("Lives")]
    public int startingPlayerLives;
    public int currentPlayerLives;

    [Header("Score")]
    [HideInInspector] int highScore;
    public int currentPlayerScore;

    [Header("Player Objects")]
    //objects to track players, controllers and cameras
    public GameObject playerObject;
    public Controller playerController;
    public GameObject playerCamera;
    public Transform currentPlayerCheckpoint;

    [Header("Prefabs")]
    public GameObject playerControllerPrefab;
    public GameObject playerPawnPrefab;
    public GameObject cameraPrefab;

    [Header("Input Action Prefabs")]
    public InputActionAsset playerInputActionsPrefab;

    [Header("Up-To-Date Lists")]
    public List<Controller_AI> ai;
    public List<PlayerStartingSpawn> playerStartingSpawnPoints;
    public List<PlayerSpawn> playerSpawnPoints;
    public List<PlayerPawn> playerPawn;
    public List<PlayerController> playerControllers;
    public List<EnemySpawn> enemySpawnPoints;
    public List<Pickup> pickUps;
    public List<GameWin> endGoals;
    public List<Checkpoint> checkpoints;
    public List<Key> keys;

    // public GameObject playerStartingSpawnPoint;

    [Header("AI Enemies")]
    public int enemyInitialSpawnAmount;
    //public int SpawnedEnemies;
    public List<Pawn> enemyPawns;

    [Header("Spawnable Enemies")]
    public List<GameObject> enemyPrefabs;

    [Header("UI-Menu's")]
    public GameObject TitleScreenObject;
    public GameObject MainMenuScreenObject;
    public GameObject OptionsScreenObject;
    public GameObject CreditsScreenObject;
    public GameObject GameplayScreenObject;
    public GameObject GameOverScreenObject;
    public GameObject WinScreenObject;

    public void Awake()
    {
        //check if there is an instance of the GameManager.
        //If there isn't one, make a new instance and tell the game to not destroy on load.
        //If there is one, destroy the one currently alive.
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


        //Clear our up to date list objects (not just memory locations, but actual lists)
        enemyPawns = new List<Pawn>();
        playerControllers = new List<PlayerController>();
        ai = new List<Controller_AI>();
        playerStartingSpawnPoints = new List<PlayerStartingSpawn>();
        playerSpawnPoints = new List<PlayerSpawn>();
        playerPawn = new List<PlayerPawn>();
        enemySpawnPoints = new List<EnemySpawn>();
        endGoals = new List<GameWin>();
        pickUps = new List<Pickup>();
        checkpoints = new List<Checkpoint>();
        keys = new List<Key>();


        //check that playerprefs has a highscore to track. If it doesn't make one.
        if (!PlayerPrefs.HasKey("HighScore"))
        {
            Debug.Log("Making HighScore");

            PlayerPrefs.SetInt("HighScore", 0);
            PlayerPrefs.Save();
        }

        highScore = PlayerPrefs.GetInt("HighScore");


        ActivateTitleScreen();
    }

    public void ActivateTitleScreen()
    {
        //playerCamera.SetActive(false);

        DeactivateAllStates();
        TitleScreenObject.SetActive(true);
    }

    public void ActivateMainMenuScreen()
    {
        DeactivateAllStates();
        MainMenuScreenObject.SetActive(true);
    }

    public void ActivateOptionsScreen()
    {
        DeactivateAllStates();
        OptionsScreenObject.SetActive(true);
    }

    public void ActivateCreditsScreen()
    {
        DeactivateAllStates();
        CreditsScreenObject.SetActive(true);
    }

    public void ActivateGameplayScreen()
    {
        //bGameOver = false;

        DeactivateAllStates();
        GameplayScreenObject.SetActive(true);

        //clear previous level
        //start game
        StartGame();

    }

    public void ActivateGameOverScreen()
    {
        DeactivateAllStates();
        GameOverScreenObject.SetActive(true);
    }

    public void ActivateWinScreen()
    {
        DeactivateAllStates();
        WinScreenObject.SetActive(true);
    }

    public void GameQuit()
    {
        Debug.Log("Game Quit! Bye!");
        Application.Quit();

    }

    //game was won
    public void GameWin()
    {


        if (currentPlayerScore > highScore)
        {

            bGameOver = true;

            Debug.Log("Game Won!!!");

            Debug.Log("Setting Hi-Score!");

            PlayerPrefs.SetInt("HighScore", currentPlayerScore);
            PlayerPrefs.Save();

        }

        //DeactivateAllStates();

        ResetMap();

        ActivateWinScreen();
    }

    public void DeactivateAllStates()
    {

        TitleScreenObject.SetActive(false);
        MainMenuScreenObject.SetActive(false);
        OptionsScreenObject.SetActive(false);
        CreditsScreenObject.SetActive(false);
        GameplayScreenObject.SetActive(false);
        GameOverScreenObject.SetActive(false);
        WinScreenObject.SetActive(false);

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
            
        currentPlayerLives = startingPlayerLives;

        //StartGame();

    }

    // Update is called once per frame
    void Update()
    {


        //set score and lives, if gameplay screen is active
        if (GameplayScreenObject.activeSelf)
        {
            if (playerController != null)
            {

                currentPlayerLives = playerController.lives;
                currentPlayerScore = playerController.currentScore;

                Canvas canvas1 = playerCamera.GetComponentInChildren<Canvas>();
                UScoreManager player1ScoreManger = canvas1.GetComponent<UScoreManager>();
                player1ScoreManger.SetLivesValue(currentPlayerLives);
                player1ScoreManger.SetScoreValue(currentPlayerScore);

            }
        }




        //if the playerLives if less than or equal to zero, and the game over bool is false, then display "Game OVer!" and flip bool to true.
        if (bGameOver == false && currentPlayerLives <= 0 && GameplayScreenObject.activeSelf)
        {
            // game over bool is true
            bGameOver = true;

                Debug.Log("Game Over!!!");


                if (currentPlayerScore > highScore)
                {

                    Debug.Log("Setting Hi-Score!");

                    PlayerPrefs.SetInt("HighScore", currentPlayerScore);
                    PlayerPrefs.Save();

                }


                //reset all varaibles for the game
                ResetMap();

                //set gameover screen
                ActivateGameOverScreen();

            


        }



    }

    public void StartGame()
    {

        //Do everything to start game

        //generate map
        level.mapGenerator.GenerateMap();

        

        //Spawn player
        SpawnPlayer();

        if(enemyInitialSpawnAmount > 0)
        {
            for (int i = 0; i < enemyInitialSpawnAmount; i++)
            {
                SpawnEnemy();
            }
        }



    }



    //Player Spawning//
    public void SpawnPlayer()
    {
        Vector3 playerSpawnPosition;

        Debug.Log(playerStartingSpawnPoints[0]);

        /*
        if (playerStartingSpawnPoint != null)
        {
            Debug.Log("Spawn point was chosen!");

            currentPlayerCheckpoint = playerStartingSpawnPoint.transform;

            playerSpawnPosition = currentPlayerCheckpoint.position;
        }
        else
        {
            Debug.Log("Spawm point was not chosen!");

            playerSpawnPosition = Vector3.zero;
        }
        */

        //choose a spawn point from the list of Player Starting Spawn points
        //Also set the transform of the current checkpoint
        if (playerStartingSpawnPoints.Count > 0)
        {
            Debug.Log("Spawn point was chosen!");
            currentPlayerCheckpoint = playerStartingSpawnPoints[0].transform;
            playerSpawnPosition = currentPlayerCheckpoint.position;
        }
        else
        {
            Debug.Log("Spawm point was not chosen!");
            playerSpawnPosition = Vector3.zero;
        }


            


        //Spawn  pawn (and store it in tanks)
        Pawn tempPlayerPawn = SpawnPawn(playerPawnPrefab);

        //Spawn a player controller (and store it in players)
        Controller tempPlayerController = SpawnPlayerController(playerControllerPrefab);

        //Have player possess pawn
        tempPlayerController.Possess(tempPlayerPawn);

        //set the player contoller as the main controller to remember
        playerController = tempPlayerController;

        //set the playerinput
        playerController.SetInputActions(playerInputActionsPrefab);

        //set the lives of the player on spawn
        playerController.lives = startingPlayerLives;

        //set the current lives of the player
        currentPlayerLives = playerController.lives;

        //set controller for healthcomp
        PlayerHealthComponent tempHealthComp = tempPlayerPawn.GetComponent<PlayerHealthComponent>();
        tempHealthComp.AssignController(playerController);

        //add Audio Listener to pawn
        tempPlayerPawn.AddComponent<AudioListener>();

        //spawn and instantiate camera object
        playerCamera = SpawnCamera(cameraPrefab);


        // move to spawnpoint
        tempPlayerPawn.transform.position = playerSpawnPosition;

        //set the player to be used as a target for AI
        SetPlayerObject(tempPlayerPawn.gameObject);

        //set camera target
        CameraFollow tempCamera = playerCamera.GetComponent<CameraFollow>();
        tempCamera.SetTarget(playerObject);

        //set the camera of the pawn mover
        PawnMover tempPawnMover = tempPlayerPawn.GetComponent<PawnMover>();

        tempPawnMover.camera = playerCamera.GetComponent<Camera>();

    }


    
    //set playerObject in gameManager
    public void SetPlayerObject(GameObject target)
    {
        //Pawn tempPawn = target.GetComponent<Pawn>();    

        playerObject = target;

    }

    public GameObject SpawnCamera(GameObject prefab)
    {
        GameObject tempCameraObject = Instantiate<GameObject>(prefab, Vector3.zero, Quaternion.identity);
        return tempCameraObject;
    }

    //set the target for the playerCamera to the player pawn.
    //public void SetCameraTarget(GameObject target)
    //{
    // playerCamera = Camera.main;
    //} 

    public Pawn SpawnPawn(GameObject prefab)
    {

        //Spawn tank pawn (and store it in tanks)
        GameObject tempTankObject = Instantiate<GameObject>(prefab, Vector3.zero, Quaternion.identity);
        return tempTankObject.GetComponent<Pawn>();

    }

    public Controller SpawnPlayerController(GameObject prefab)
    {

        GameObject tempPlayer = Instantiate<GameObject>(prefab, Vector3.zero, Quaternion.identity);
        return tempPlayer.GetComponent<Controller>();

    }


    //respawn without destroying.
    // one cvall is found in player health component
    public void RespawnPlayer()
    {
        //player has died, move prefab and deincrement lives
        currentPlayerLives = playerController.lives;

        PlayerHealthComponent tempHealthComp = playerObject.GetComponent<PlayerHealthComponent>();

        tempHealthComp.Heal(tempHealthComp.maxHealth);

        // move to checkpoint
        playerObject.transform.position = currentPlayerCheckpoint.position;

    }





    //Enemy Spawning//

    //TO-DO: fix this when working on enemy AI

    //spawn enemies //

    public void SpawnEnemy()
    {

        Vector3 enemySpawnPosition;

        Debug.Log(enemySpawnPoints.Count);

        //choose a spawnpoint from the list
        if (enemySpawnPoints.Count > 0)
        {
            

            //get spawn point
            EnemySpawn spawnPoint;

            //check if this enemySpawnPoint has already spawned an object


            //set randomly selected spawn point to this enemyspawn variable
            spawnPoint = enemySpawnPoints[Random.Range(0, enemySpawnPoints.Count)];

            Debug.Log("Enemy Spawn point was chosen!");

            //set this spanw points transform to a transform variable
            Transform spawnPointTransform = spawnPoint.transform;

            enemySpawnPosition = spawnPointTransform.position;

            //keep track of the index of the enemy 
            int enemyIndex = Random.Range(0, enemyPrefabs.Count);

            //get a random enemy prefab from list using enemyIndex.
            GameObject tempEnemyObject = enemyPrefabs[enemyIndex];

            //Spawn tank pawn (and store it in tanks)
            Pawn tempEnemyPawn = SpawnEnemyPawn(tempEnemyObject);

            //set the pawn tank as the object to be traceked whne spawned.
            spawnPoint.SetSpawnedEnemy(tempEnemyObject);

            // move to spawnpoint
            tempEnemyPawn.transform.position = enemySpawnPosition;

           // SpawnedEnemies++;

            //create temp enemy controller
            //Controller_AI tempEnemyController;


            /*
            //switch case to figure out which controller to assign to the spawned tank
            // MAKE SURE THAT THE ENEMY LIST AND THE AI CONTROLLER ARE IN THE SAME ORDER!!!
            switch (enemyIndex)
            {
                //flee AI
                case 0:

                    //set controller for the enemy tank
                    tempEnemyController = SpawnEnemyController(enemiesAIController[enemyIndex]);

                    //Have player possess pawn
                    tempEnemyController.Possess(tempEnemyTankPawn);

                    break;

                //Kamikaze AI
                case 1:

                    //set controller for the enemy tank
                    tempEnemyController = SpawnEnemyController(enemiesAIController[enemyIndex]);

                    //Have player possess pawn
                    tempEnemyController.Possess(tempEnemyTankPawn);

                    break;

                //Pursuer AI
                case 2:

                    //set controller for the enemy tank
                    tempEnemyController = SpawnEnemyController(enemiesAIController[enemyIndex]);

                    //Have player possess pawn
                    tempEnemyController.Possess(tempEnemyTankPawn);

                    break;

                //Sentry AI
                case 3:

                    //set controller for the enemy tank
                    tempEnemyController = SpawnEnemyController(enemiesAIController[enemyIndex]);

                    //Have player possess pawn
                    tempEnemyController.Possess(tempEnemyTankPawn);

                    break;



            }
            



            // move to spawnpoint
            tempEnemyPawn.transform.position = enemySpawnPosition;
        }
        else
        {
            Debug.Log("Enemy Spawn point was not chosen!");
            enemySpawnPosition = Vector3.zero;


        }

        //get a random enemy prfab from list
        //GameObject tempEnemyObject = enemies[Random.Range(0, enemies.Count)];

        //Spawn tank pawn (and store it in tanks)
        //Pawn tempEnemyTankPawn = SpawnEnemyTank(tempEnemyObject);

        //spawnPoint.SetSpawnedEnemy()

        // move to spawnpoint
        //tempEnemyTankPawn.transform.position = enemySpawnPosition;

        */

        }

    }

   

    public Pawn SpawnEnemyPawn(GameObject prefab)
    {

        //Spawn tank pawn (and store it in tanks)
        GameObject tempPawnObject = Instantiate<GameObject>(prefab, Vector3.zero, Quaternion.identity);
        return tempPawnObject.GetComponent<Pawn>();

    }


    //Reset the map
    public void ResetMap()
    {

        Debug.Log("Reset Game.");

        bGameOver = false;

        //reset player trackers
        if (GameplayScreenObject.activeSelf)
        {
            Destroy(playerObject.gameObject);
            Destroy(playerController.gameObject);
            Destroy(playerCamera.gameObject);

            playerObject = null;
            playerController = null;
            playerCamera = null;
            currentPlayerCheckpoint = null;
        }


        //reset lives and score
        currentPlayerScore = 0;
        currentPlayerLives = 1;



        //go through each list and destroy the object
        foreach (PlayerPawn tempPawn in playerPawn)
        {
            Destroy(tempPawn);

        }

        foreach (Pawn tempPawn in enemyPawns)
        {
            Destroy(tempPawn);
            
        }

        foreach (Controller_AI tempAI in ai)
        {
            Destroy(tempAI);

        }

        foreach (PlayerController tempController in playerControllers)
        {
            Destroy(tempController);

        }

        foreach (PlayerSpawn tempSpawnPoint in playerSpawnPoints)
        {
            Destroy(tempSpawnPoint);

        }

        foreach (PlayerStartingSpawn tempSpawnPoint in playerStartingSpawnPoints)
        {
            Destroy(tempSpawnPoint);

        }

        foreach (EnemySpawn tempSpawnPoint in enemySpawnPoints)
        {
            Destroy(tempSpawnPoint);

        }

        foreach (Pickup tempPickup in pickUps)
        {
            Destroy(tempPickup);

        }

        foreach (GameWin tempGoal in endGoals)
        {
            Destroy(tempGoal);
        }

        foreach (Checkpoint tempCP in checkpoints)
        {
            Destroy(tempCP);
        }

        foreach (Key tempKey in keys)
        {
            Destroy(tempKey);
        }



        //remove currnetly spawned map
        if (GameplayScreenObject.activeSelf)
        {
            level.mapGenerator.RemoveMap();
        }




        PickUpHealth.count = 0;
        PickUpScore.count = 0;


    }

}




