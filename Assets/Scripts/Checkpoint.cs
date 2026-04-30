using TMPro;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Collider mCollider;

    public AudioClip audioClip;

    public GameObject spawnedAudioPlayer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mCollider = GetComponent<Collider>();
        mCollider.isTrigger = true;
    }

    public void Awake()
    {
        GameManager.instance.checkpoints.Add(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {

        //if the collider of this object has the tag "Player", go to win screen.
        if (other.CompareTag("Player"))
        {
            GameManager.instance.currentPlayerCheckpoint = transform;

            //make sure score clip isn't null
            if (audioClip != null && spawnedAudioPlayer != null)
            {
                //spawn an object to play an audioclip
                GameObject tempObject = Instantiate<GameObject>(spawnedAudioPlayer, transform.position, Quaternion.identity);

                PlayAudioClipBeforeDestroy TempAudioPlayer = tempObject.GetComponent<PlayAudioClipBeforeDestroy>();

                TempAudioPlayer.SetAudioClip(audioClip);
            }

        }

    }

    /*
    //can't figure out how to get this to work correctly
    public void SpawnText(Color color, float size, Collider other)
    {
        if (textPrefab == null)
        {
            Debug.LogError("Text Prefab not assigned!");
            return;
        }

        Debug.Log("Made checkpoint");

        // Instantiate the text object
        TextMeshPro tempTExt = Instantiate(textPrefab, other.transform.position, Quaternion.identity);

        // Configure text properties
        tempTExt.text = "CheckPoint!";
        tempTExt.color = color;
        tempTExt.fontSize = size;
        
        // Make it face the camera
        tempTExt.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
        //tempTExt.transform.position = other.transform.position;
        
        // Destroy after lifetime
        Destroy(tempTExt.gameObject, lifetime);
    }
    */


    public void OnDestroy()
    {
        GameManager.instance.checkpoints.Remove(this);
    }
}
