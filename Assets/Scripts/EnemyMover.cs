using UnityEngine;

public class EnemyMover : Mover
{
    
    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //get the pawn and rigidbody
        
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Move(Vector2 moveDirection, float moveSpeed)
    {

        //get the moveCector from the moveDirection
        Vector3 moveVector = new Vector3(moveDirection.x, 0, moveDirection.y);

        //transform direction of moveVector from world Space to localSpace
        moveVector = transform.TransformDirection(moveVector);


        //transform.position += moveVector * (pawn.moveSpeed * Time.deltaTime);

        //get the rigidbody and move that based on its position
        rb.MovePosition(rb.position + (moveVector * (moveSpeed * Time.deltaTime)));

        //get noise maker for pawn and reset
        //Noisemaker moveNoiseMaker = rb.gameObject.GetComponent<Noisemaker>();

        //moveNoiseMaker.ResetNoiseAmount();

    }

    public override void Rotate(Vector2 rotateDirection, float turnSpeed)
    {
        transform.Rotate(0, rotateDirection.x * (turnSpeed * Time.deltaTime), 0);

        //get noise maker for pawn and reset
        //Noisemaker moveNoiseMaker = rb.gameObject.GetComponent<Noisemaker>();

        //moveNoiseMaker.ResetNoiseAmount();
    }

    public override void RotateTowards(Vector3 position, float turnSpeed)
    {

        //find the vector to target
        Vector3 vectorToTarget = position - transform.position;

        //Find the quaternion (look instruction) on how to look down that vector
        Quaternion lookRotation = Quaternion.LookRotation(vectorToTarget);

        //Rotate just a little towards that new rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, turnSpeed * Time.deltaTime);

    }
}
