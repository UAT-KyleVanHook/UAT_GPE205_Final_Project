using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;

    public Vector3 CameraOffset;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameManager.instance.playerObject;


    }

    // Update is called once per frame
    void Update()
    {


    }


    void LateUpdate()
    {


        //check if the GameplayScreenObject is set as active
        //if (GameManager.instance.GameplayScreenObject.activeSelf && target != null)
        //{

            // returns the camera offset from local space to world space and sets the cameras transfrom position.
            transform.position = target.transform.TransformPoint(CameraOffset);

            //look at the player target. 
            //could set this at a point in front of the tank pawn later.
            transform.LookAt(target.transform.position);

        //}


    }

    public void SetTarget(GameObject gameObject)
    {
        target = gameObject;
    }
}
