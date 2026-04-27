using UnityEngine;

public class PlayerStartingSpawn : MonoBehaviour
{
    public void Awake()
    {
        // This needs to be in awake as the tile map is not already made on start.
        GameManager.instance.playerStartingSpawnPoints.Add(this);
        //GameManager.instance.playerStartingSpawnPoint = this.gameObject;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {

        //GameManager.instance.playerStartingSpawnPoints.Add(this);
        //GameManager.instance.playerStartingSpawnPoint = this.gameObject;
    }

    // Update is called once per frame
    public void Update()
    {

    }

    public void OnDestroy()
    {
        //remove from PlayerSpawn list
        GameManager.instance.playerStartingSpawnPoints.Remove(this);

    }
}
