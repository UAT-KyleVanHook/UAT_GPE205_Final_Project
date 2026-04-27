using UnityEngine;

public class DamageOnOverlap : MonoBehaviour
{
    //[HideInInspector]
    public Pawn owner;

    public float damageDone;
    protected Collider mCollider;

    [Header("Damage Per Second")]
    public float damageRate; // how many shots per second we can fire
    private float nextDamageTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        //get collider
        mCollider = GetComponent<Collider>();
        //set this collider as a trigger
        mCollider.isTrigger = true;

        nextDamageTime = Time.time;

    }

    // Update is called once per frame
    public virtual void Update()
    {

    }

    public virtual void OnTriggerEnter(Collider other)
    {
        //get other objects health componenet
        HealthComponent otherHealth = other.GetComponent<HealthComponent>();
        
        //if it has a healthComp
        if (otherHealth != null && other.CompareTag("Player"))
        {


                //initiate damage on healthComp
                otherHealth.TakeDamage(damageDone, owner);

                //update nextShootTime to when the player can shoot again
                nextDamageTime = Time.time + (1 / damageRate); // Invert our fireRate to turn (shots/sec to seconds/shot)

            
            

        }


    }

    public void OnTriggerStay(Collider other)
    {

        //get other objects health componenet
        HealthComponent otherHealth = other.GetComponent<HealthComponent>();

        //if it has a healthComp
        if (otherHealth != null && other.CompareTag("Player"))
        {


            if (Time.time >= nextDamageTime)
            {

                //initiate damage on healthComp
                otherHealth.TakeDamage(damageDone, owner);

                //update nextShootTime to when the player can shoot again
                nextDamageTime = Time.time + (1 / damageRate); // Invert our fireRate to turn (shots/sec to seconds/shot)

            }




        }
    }

    //reset time when player exits trigger
    public void OnTriggerExit(Collider other)
    {


        //check that the leaving object is a player object
        if (other.CompareTag("Player"))
        {
            nextDamageTime = Time.time + (1 / damageRate); // Invert our fireRate to turn (shots/sec to seconds/shot)
        }
        

    

    }
}
