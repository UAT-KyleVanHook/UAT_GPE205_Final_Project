using UnityEngine;

public class PlayerPawn : Pawn
{
    private PawnShooter shooter;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {

        //Save tank to gamemanager
        GameManager.instance.playerPawn.Add(this);

        //Do what all pawns do
        base.Start();

        //get the shooter attached to the pawn
        shooter = gameObject.GetComponent<PawnShooter>();


    }

    public void OnDestroy()
    {

        //Remove tank from gamemanager
        GameManager.instance.playerPawn.Remove(this);

    }

    public override void Move(Vector3 directionToMove)
    {

        //Debug.Log("Moving!");
        mover.Move(directionToMove, moveSpeed);

    }
    public override void Rotate(Vector3 directionToRotate)
    {
        //might not need this
        mover.Rotate(directionToRotate, turnSpeed);
    }

    public override void RotateTowards(Vector3 position)
    {
        mover.RotateTowards(position, turnSpeed);
    }

    public override void Shoot()
    {
        //Debug.Log("Pew-Pew!");
        shooter.Shoot();
    }
}
