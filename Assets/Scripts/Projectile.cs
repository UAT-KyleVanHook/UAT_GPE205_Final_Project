using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifespan;
    public AudioClip hitClip;

    protected Collider mCollider;


    public void Start()
    {
        mCollider = GetComponent<Collider>();
        mCollider.isTrigger = true;


        //destroy this object after a set time.
        Destroy(gameObject, lifespan);
    }

    public void OnTriggerEnter(Collider other)
    {
        //AudioSource.PlayClipAtPoint(hitClip, transform.position);

        //change this, spawn a new audio source object
        if (hitClip != null)
        {
            AudioSource.PlayClipAtPoint(hitClip, transform.position);
        }

    }
}
