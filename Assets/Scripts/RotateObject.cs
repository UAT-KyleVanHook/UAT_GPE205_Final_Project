using UnityEngine;
using UnityEngine.InputSystem;

public class RotateObject : MonoBehaviour
{

    public InputActionAsset inputActions;

    public float turnSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        

        Vector2 movementVector = inputActions["Move"].ReadValue<Vector2>();

        //if the movement vector of the x is less than 0, turn left
        if (movementVector.x < 0 && inputActions["Move"].IsInProgress())
        {
            transform.Rotate(Vector3.up,  movementVector.x * (turnSpeed * Time.deltaTime));
        }
        //if the movement vector of the x is more than 0, turn right
        else if (movementVector.x > 0 && inputActions["Move"].IsInProgress())
        {
            transform.Rotate(Vector3.up, movementVector.x * (turnSpeed * Time.deltaTime));
        }
        //else, don't move at all
        else
        {
            //transform.Rotate(Vector3.up, 0 * (turnSpeed * Time.deltaTime));
        }


    }


}
