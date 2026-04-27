using UnityEngine;

public class PickUpScore : Pickup
{
    public static int count;
    public PowerUpScore powerup;
    public AudioClip scoreClip;
    public GameObject spawnedAudioPlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {


        //increment static count
        count++;

        base.Start();

    }

    // Update is called once per frame
    public override void Update()
    {

    }

    public override void OnTriggerEnter(Collider other)
    {
        //Check if the other object has a PowerUpManagar;

        PowerUpManager otherManager = other.GetComponent<PowerUpManager>();

        if (otherManager != null)
        {
            //If yes, add this to the powerup manager
            otherManager.Add(powerup);

            //make sure score clip isn't null
            if (scoreClip != null && spawnedAudioPlayer != null)
            {
                //spawn an object to play an audioclip
                GameObject tempObject = Instantiate<GameObject>(spawnedAudioPlayer, transform.position, Quaternion.identity);

                PlayAudioClipBeforeDestroy TempAudioPlayer = tempObject.GetComponent<PlayAudioClipBeforeDestroy>();

                TempAudioPlayer.SetAudioClip(scoreClip);
            }


            //Destroy this object
            Destroy(this.gameObject);

        }

        base.OnTriggerEnter(other);

    }
}
