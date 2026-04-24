using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public List<Powerup> powerups;
    private Pawn pawn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        //get the pawn this powerupmanager is working with
        pawn = GetComponent<Pawn>();

        //initialize the list of powerups
        powerups = new List<Powerup>();

    }

    // Update is called once per frame
    public void Update()
    {

        //update the powerup life spans
        UpdatePowerUpLifeSpans();

        //check for expired powers and remove them
        CheckForExpiredPowerUps();

    }

    public void UpdatePowerUpLifeSpans()
    {

        foreach (Powerup powerup in powerups)
        {

            powerup.lifeSpan -= Time.deltaTime;

        }

    }

    public void CheckForExpiredPowerUps()
    {
        //make a list called powerups we need to remove
        List<Powerup> powerupsToRemove = new List<Powerup>();

        foreach (Powerup powerup in powerups)
        {
            if (powerup.lifeSpan <= 0)
            {
                //add powerups to list
                powerupsToRemove.Add(powerup);
            }
        }

        //go through list and remove listed power ups
        // -- This way, you aren't iterating through the main list when you remove them
        foreach (Powerup powerup in powerupsToRemove)
        {
            Remove(powerup);
        }

    }

    public void Add(Powerup powerup)
    {
        //apply the powerups effects
        powerup.Apply(pawn);

        //check if the lifespan is larger than zero. 
        //If it is zero or negative, then this powerup is permant
        if (powerup.lifeSpan >= 0)
        {
            //add it to our list
            powerups.Add(powerup);
        }

    }

    public void Remove(Powerup powerup)
    {
        //remove powerup effects
        powerup.Remove(pawn);

        //remove it form our list
        powerups.Remove(powerup);
    }
}
