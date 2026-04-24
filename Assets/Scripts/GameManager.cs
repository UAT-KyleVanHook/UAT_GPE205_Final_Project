using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //public Level level;

    bool bGameOver = false;

    [Header("Lives")]
    public int startingPlayerLives;
    public int currentPlayer1Lives;

    [Header("Score")]
    [HideInInspector] int highScore;
    public int currentPlayer1Score;

    [Header("Player Objects")]
    //objects to track players, controllers and cameras
    public GameObject player1Object;
    public Controller player1Controller;
    public GameObject player1Camera;

    [Header("Prefabs")]
    public GameObject playerControllerPrefab;
    public GameObject playerPawnPrefab;
    public GameObject cameraPrefab;

    [Header("Input Action Prefabs")]
    public InputActionAsset player1InputActionsPrefab;

    [Header("Up-To-Date Lists")]
    public List<Controller_AI> ai;

    [Header("AI Enemies")]
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

    void Awake()
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

        //check that playerprefs has a highscore to track. If it doesn't make on.
        if (!PlayerPrefs.HasKey("HighScore"))
        {
            Debug.Log("Making HighScore");

            PlayerPrefs.SetInt("HighScore", 0);
            PlayerPrefs.Save();
        }

        highScore = PlayerPrefs.GetInt("HighScore");

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
        bGameOver = false;

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

    public void GameQuit()
    {
        Debug.Log("Game Quit! Bye!");
        Application.Quit();

    }

    public void DeactivateAllStates()
    {

        TitleScreenObject.SetActive(false);
        MainMenuScreenObject.SetActive(false);
        OptionsScreenObject.SetActive(false);
        CreditsScreenObject.SetActive(false);
        GameplayScreenObject.SetActive(false);
        GameOverScreenObject.SetActive(false);

        //Camera camera = Camera.main;

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {

        //Spawn player
        //SpawnPlayer();



    }



    //Player Spawning//
    public void SpawnPlayer1()
    {
        Vector3 playerSpawnPosition;

        /*
        //choose a spawnpoint from the list
        if (playerSpawnPoints.Count > 0)
        {
            Debug.Log("Spawn point was chosen!");

            Transform spawnPoint = playerSpawnPoints[Random.Range(0, playerSpawnPoints.Count)].transform;

            playerSpawnPosition = spawnPoint.position;
        }
        else
        {
            Debug.Log("Spawm point was not chosen!");
            playerSpawnPosition = Vector3.zero;
        }
        */

        //Spawn tank pawn (and store it in tanks)
        Pawn tempPlayerPawn = SpawnPawn(playerPawnPrefab);

        //Spawn a player controller (and store it in players)
        Controller tempPlayerController = SpawnPlayerController(playerControllerPrefab);

        //Have player possess pawn
        tempPlayerController.Possess(tempPlayerPawn);

        //set the player contoller as the main controller to remember
        player1Controller = tempPlayerController;

        //set the playerinput
        player1Controller.SetInputActions(player1InputActionsPrefab);

        //set the lives of the player on spawn
        player1Controller.lives = startingPlayerLives;

        //set the current lives of the player
        currentPlayer1Lives = player1Controller.lives;

        //set controller for healthcomp
        PlayerHealthComponent tempHealthComp = tempPlayerPawn.GetComponent<PlayerHealthComponent>();
        tempHealthComp.AssignController(player1Controller);

        //add Audio Listener to pawn
        tempPlayerPawn.AddComponent<AudioListener>();

        //spawn and instantiate camera object
        player1Camera = SpawnCamera(cameraPrefab);


        // move to spawnpoint
       // tempPlayerPawn.transform.position = playerSpawnPosition;

        //set the player to be used as a target for AI
        SetPlayer1Object(tempPlayerPawn.gameObject);

        //set camera target
        CameraFollow tempCamera = player1Camera.GetComponent<CameraFollow>();
        tempCamera.SetTarget(player1Object);


    }


    
    //set playerObject in gameManager
    public void SetPlayer1Object(GameObject target)
    {
        //Pawn tempPawn = target.GetComponent<Pawn>();    

        player1Object = target;
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



    //Enemy Spawning//

    //TO-DO: fix this when working on enemy AI

    //spawn enemies //

    public void SpawnEnemy()
    {

        Vector3 enemySpawnPosition;

        /*
        //choose a spawnpoint from the list
        if (enemySpawnPoints.Count > 0)
        {
            Debug.Log("Enemy Spawn point was chosen!");

            //get spawn point
            EnemySpawn spawnPoint;

            //check if this enemySpawnPoint has already spawned an object
            do
            {
                //set randomly selected spawn point to this enemyspawn variable
                spawnPoint = enemySpawnPoints[Random.Range(0, enemySpawnPoints.Count)];

            } while (spawnPoint.IsSpawnedEnemy() == true);

            //set this spanw points transform to a transform variable
            Transform spawnPointTransform = spawnPoint.transform;

            enemySpawnPosition = spawnPointTransform.position;

            //keep track of the index of the enemy 
            int enemyIndex = Random.Range(0, enemies.Count);

            //get a random enemy prefab from list using enemyIndex.
            GameObject tempEnemyObject = enemies[enemyIndex];

            //Spawn tank pawn (and store it in tanks)
            Pawn tempEnemyPawn = SpawnEnemyPawn(tempEnemyObject);

            //set the pawn tank as the object to be traceked whne spawned.
            spawnPoint.SetSpawnedEnemy(tempEnemyObject);

            //create temp enemy controller
            Controller_AI tempEnemyController;


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

   

    public Pawn SpawnEnemyPawn(GameObject prefab)
    {

        //Spawn tank pawn (and store it in tanks)
        GameObject tempPawnObject = Instantiate<GameObject>(prefab, Vector3.zero, Quaternion.identity);
        return tempPawnObject.GetComponent<Pawn>();

    }

    /*
    public Controller_AI SpawnEnemyController(GameObject prefab)
    {

        GameObject tempEnemy = Instantiate<GameObject>(prefab, Vector3.zero, Quaternion.identity);
        return tempEnemy.GetComponent<Controller_AI>();

    }

    */

}




