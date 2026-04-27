using UnityEngine;

public class PickUpHealth : Pickup
{
    public static int count;
    public PowerUpHealth powerup;

    public AudioClip eatClip;
    public GameObject spawnedAudioPlayer;
    public override void Start()
    {
        //increment static count
        count++;

        base.Start();

    }

    public override void OnTriggerEnter(Collider other)
    {
        //Check if the other object has a PowerUpManagar;

        PowerUpManager otherManager = other.GetComponent<PowerUpManager>();

        HealthComponent otherHealthComp = other.GetComponent<HealthComponent>();

        //check that the health comp is true
        if(otherHealthComp != null && otherHealthComp.currentHealth < otherHealthComp.maxHealth)
        {

            if (otherManager != null)
            {
                //If yes, add this to the powerup manager
                otherManager.Add(powerup);


                //make sure score clip isn't null
                if (eatClip != null && spawnedAudioPlayer != null)
                {
                    //spawn an object to play an audioclip
                    GameObject tempObject = Instantiate<GameObject>(spawnedAudioPlayer, transform.position, Quaternion.identity);

                    PlayAudioClipBeforeDestroy TempAudioPlayer = tempObject.GetComponent<PlayAudioClipBeforeDestroy>();

                    TempAudioPlayer.SetAudioClip(eatClip);
                }


                //Destroy this object
                Destroy(this.gameObject);

            }

            base.OnTriggerEnter(other);

        }


    }

    public override void OnDestroy()
    {
        //deincremnt count.
        count--;

        base.OnDestroy();
    }
}
