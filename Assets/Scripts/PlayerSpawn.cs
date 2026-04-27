using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public virtual void Awake()
    {
        // This needs to be in awake as the tile map is not already made on start.
        GameManager.instance.playerSpawnPoints.Add(this);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {


    }

    // Update is called once per frame
    public virtual void Update()
    {

    }

    public virtual void OnDestroy()
    {
        //remove from PlayerSpawn list
        GameManager.instance.playerSpawnPoints.Remove(this);

    }
}
