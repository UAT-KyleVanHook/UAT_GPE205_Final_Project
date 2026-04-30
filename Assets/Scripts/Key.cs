using UnityEngine;

public class Key : MonoBehaviour
{
    private Collider mCollider;
    public AudioClip audioClip;

    public GameObject spawnedAudioPlayer;
    public GameObject destroyObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mCollider = GetComponent<Collider>();
        mCollider.isTrigger = true;


    }

    public void Awake()
    {
        GameManager.instance.keys.Add(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {

        //if the collider of this object has the tag "Player", destroy the destroyObject, then this key.
        if (other.CompareTag("Player"))
        {
            
            Destroy(destroyObject);

            //make sure score clip isn't null
            if (audioClip != null && spawnedAudioPlayer != null)
            {
                //spawn an object to play an audioclip
                GameObject tempObject = Instantiate<GameObject>(spawnedAudioPlayer, transform.position, Quaternion.identity);

                PlayAudioClipBeforeDestroy TempAudioPlayer = tempObject.GetComponent<PlayAudioClipBeforeDestroy>();

                TempAudioPlayer.SetAudioClip(audioClip);
            }

            Destroy(gameObject);
        }

    }

    public void OnDestroy()
    {
        GameManager.instance.keys.Remove(this);
    }
}
