using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Controller
{
    public InputActionAsset inputActions;

    [Header("Noisemaker")]
    private Noisemaker moveNoiseMaker;
    public float walkNoiseAmount;
    public float shootNoiseAmount;

    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {

        //enable input actions
        inputActions.Enable();

        //add this to the list of players
        //GameManager.instance.players.Add(this);

        moveNoiseMaker = pawn.GetComponent<Noisemaker>();

        rb = pawn.GetComponent<Rigidbody>();
    }

    public void OnDestroy()
    {

        //remove this from list of players
        //GameManager.instance.players.Remove(this);

    }

    // Update is called once per frame
    public override void Update()
    {
        Debug.Log("Player is moving: " + inputActions["Move"].IsPressed());

        //do what our parent class does in its function
        base.Update();
    }

    public override void MakeDecisions()
    {

        Vector2 movementVector = inputActions["Move"].ReadValue<Vector2>();
        Vector2 mousePosition = inputActions["Look"].ReadValue<Vector2>();

        float x = movementVector.x;
        float z = movementVector.y;

        //check that the pawn is not null and that the Move input is pressed.
        if (pawn != null&& inputActions["Move"].IsPressed())
        {
            //pass movement vector from inputAction into the pawns move and rotate functions
            pawn.Move(new Vector3(x, z));

            //the player is making sound
            moveNoiseMaker.SetNoiseVolume(walkNoiseAmount);
            

            //pawn.Rotate(new Vector2(movementVector.x, 0));


        }
        else 
        {
            moveNoiseMaker.ResetNoiseAmount();
        }



        //Debug.Log(mousePosition);

        if (pawn != null)
        {
            //Debug.Log(mousePosition);
            pawn.Rotate(new Vector2(mousePosition.x, mousePosition.y));
        }




        if (inputActions["Shoot"].IsPressed())
        {
            if (pawn != null && inputActions["Shoot"].IsPressed())
            {
                //TankShooter shooter = pawn.GetComponent<TankShooter>();

                //was "pawn.Shoot()", but had to change to to issue with AI enemies also firing.
                //shooter.Shoot();

                Debug.Log("Shooting!");

                pawn.Shoot();

                moveNoiseMaker.SetNoiseVolume(shootNoiseAmount);

            }
            else
            {
                moveNoiseMaker.ResetNoiseAmount();
            }

        }




    }

    //set the specified input actions for this controller
    public override void SetInputActions(InputActionAsset prefabInputActions)
    {

        inputActions = prefabInputActions;

    }
}
