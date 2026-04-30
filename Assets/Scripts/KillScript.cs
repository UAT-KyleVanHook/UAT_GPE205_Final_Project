using UnityEngine;

public class KillScript : MonoBehaviour
{
    private Collider mCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mCollider = GetComponent<Collider>();
        mCollider.isTrigger = true;
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {

        HealthComponent health = other.GetComponent<HealthComponent>();

        if (health != null)
        {
            health.Die(null);
        }


    }

}
