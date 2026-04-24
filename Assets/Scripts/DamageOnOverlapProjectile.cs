using UnityEngine;

public class DamageOnOverlapProjectile : DamageOnOverlap
{
    public override void Start()
    {
        
        base.Start();

    }

    // Update is called once per frame
    public override void Update()
    {

    }

    public override void OnTriggerEnter(Collider other)
    {
        //get other objects health componenet
        HealthComponent otherHealth = other.GetComponent<HealthComponent>();

        //if it has a healthComp
        if (otherHealth != null)
        {

            //initiate damage on healthComp
            otherHealth.TakeDamage(damageDone, owner);

            //Destroy projectile
            Destroy(gameObject);

        }

        //if the projectile collides with a wall
        if (other.CompareTag("Wall"))
        {
            //Destroy projectile
            Destroy(gameObject);
        }





    }
}
