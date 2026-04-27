using UnityEngine;

public class PlayerStartingSpawn : PlayerSpawn
{
    public override void Awake()
    {
        // This needs to be in awake as the tile map is not already made on start.
        //GameManager.instance.playerStartingSpawnPoints.Add(this);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {

        GameManager.instance.playerStartingSpawnPoints.Add(this);
    }

    // Update is called once per frame
    public override void Update()
    {

    }

    public override void OnDestroy()
    {
        //remove from PlayerSpawn list
        GameManager.instance.playerStartingSpawnPoints.Remove(this);

    }
}
