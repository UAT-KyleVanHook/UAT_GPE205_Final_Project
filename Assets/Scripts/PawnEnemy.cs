using UnityEngine;

public class PawnEnemy : Pawn
{
    //private TankShooter shooter;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {

        //Save tank to GameManager
        GameManager.instance.enemyPawns.Add(this);

        //Do what all pawns do
        base.Start();

        //get the shooter attached to the pawn
        //shooter = gameObject.GetComponent<TankShooter>();


    }

    public void OnDestroy()
    {

        //Remove enemy from GameManager
        GameManager.instance.enemyPawns.Remove(this);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Move(Vector3 directionToMove)
    {

        //Debug.Log("Moving!");
        mover.Move(directionToMove, moveSpeed);

        //set noise amount
        //noisemaker.SetNoiseVolume(5);

    }
    public override void Rotate(Vector3 directionToRotate)
    {
        //Debug.Log("Rotating!");
        mover.Rotate(directionToRotate, turnSpeed);


        //set noise amount
        //noisemaker.SetNoiseVolume(5);
    }

    public override void RotateTowards(Vector3 position)
    {
        mover.RotateTowards(position, turnSpeed);
    }

    public override void Shoot()
    {

        //Debug.Log("Pew-Pew!");
        //shooter.Shoot();

        //set noise amount
        //noisemaker.SetNoiseVolume(20);
    }
}
