using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PawnMover : Mover
{

    private Rigidbody rb;
    
    public new Camera camera;

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
        //moveVector = transform.TransformDirection(moveVector);

        moveVector = Quaternion.Euler(0, camera.gameObject.transform.eulerAngles.y, 0) * moveVector;


        //get the rigidbody and move that based on its position
        rb.MovePosition(rb.position + (moveVector * (moveSpeed * Time.deltaTime)));



    }

    public override void Rotate(Vector2 rotateDirection, float turnSpeed)
    {
        //get raycast of the mouse
        Ray ray = camera.ScreenPointToRay(rotateDirection);

        //this works

        
        //check if raycast hits anything
        if(Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 300f))
        {
            //get point of hit
            var target = hitInfo.point;

            //make it so the y value is the current objects y value.This way the object doesn't freak out and rotate towards the Y-value.
            target.y = transform.position.y;


            //Debug.Log(target);

            // look towards mouse direction
            transform.LookAt(target);
            

        }
        




        //var rotation = Quaternion.LookRotation(rotateDirection);


        //transform.Rotate(0, rotateDirection.x * (turnSpeed * Time.deltaTime), 0);

    }

    public override void RotateTowards(Vector3 position, float turnSpeed)
    {

        //not used in player

    }

}
