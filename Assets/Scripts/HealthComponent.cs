using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public float currentHealth;
    //[HideInInspector]
    public float maxHealth;

    public AudioClip destructionClip;
  

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {



    }

    public virtual void Awake()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void TakeDamage(float amount, Pawn source)
    {

        currentHealth = currentHealth - amount;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            Die(source);
        }

    }

    public virtual void Heal(float amount)
    {

        currentHealth += amount;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        //if (currentHealth <= 0)
        //{
        // Die(source);
        //}

    }

    public virtual void IncreaseMaxHealth(float amount)
    {
        //increae max heatlh by the amount
        maxHealth += amount;

        //set the current health to the new max health
        currentHealth = maxHealth;
    }

    public virtual void Die(Pawn source)
    {

        Controller sourceController = source.GetController();

        //check that the source that desrtoyed this object has a controller
        if (sourceController != null)
        {
            Pawn tempPawn = gameObject.GetComponent<Pawn>();

            //check that the destroyed object has a pawn.
            if (tempPawn != null)
            {
                Controller tempController = tempPawn.GetController();

                //check that destroyed object has a controller. Then get the scoreAmount form the controller
                if (tempController != null)
                {
                    sourceController.AddToScore(tempController.scoreAmount);
                }

            }


        }

        Debug.Log(gameObject.name + " has moved on to a better place.");
        Destroy(gameObject);

    }
}
