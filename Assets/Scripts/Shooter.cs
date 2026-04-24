using UnityEngine;

public abstract class Shooter : MonoBehaviour
{

    //location of firepoint for shooting
    public Transform muzzleLocation;

    public abstract void Shoot();

    public abstract void Shoot(float shootForce);

}
