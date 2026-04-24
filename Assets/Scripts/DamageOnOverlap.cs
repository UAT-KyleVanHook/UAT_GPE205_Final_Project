using UnityEngine;

public class DamageOnOverlap : MonoBehaviour
{
    //[HideInInspector]
    public Pawn owner;

    public float damageDone;
    protected Collider mCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        //get collider
        mCollider = GetComponent<Collider>();
        //set this collider as a trigger
        mCollider.isTrigger = true;

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
        if (otherHealth != null)
        {

            //initiate damage on healthComp
            otherHealth.TakeDamage(damageDone, owner);

        }


  




    }
}
