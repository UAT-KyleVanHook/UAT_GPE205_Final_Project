using UnityEngine;

public class Controller_AI_Ghost : Controller_AI
{
    public override void Start()
    {

        GameManager.instance.ai.Add(this);

        //get damage on overlap

        DamageOnOverlap tempdamOnOverlap = pawn.GetComponentInChildren<DamageOnOverlap>();

        if (tempdamOnOverlap != null)
        {
            tempdamOnOverlap.owner = pawn;
        }

        //do the base of start
        base.Start();

        if (!IsHasTarget())
        {
            ChangeState(AIState.ChooseTarget);

        }
        else
        {
            ChangeState(AIState.Idle);
        }




    }

    public override void Update()
    {

        base.Update();

        //Debug.Log("Is Target Not Blocked: " + IsTargetNotBlocked(target, visionDistance));
        //Debug.Log("Zombie can see target: " + CanSee(target));
        //Debug.Log("Zombie can hear target: " + CanHear(target));

        /*
        Debug.Log("Current State: " + currentState);
        Debug.Log("Is Target Not Blocked: " + IsTargetNotBlocked(target, 100));

        Debug.Log("Zombie has target: " + IsHasTarget());
        Debug.Log("Zombie can see target: " + CanSee(target));
        Debug.Log("Zombie can hear target: " + CanHear(target));
        */
    }

    public override void MakeDecisions()
    {




        //if the pawn we are attached to is null, destroy this controller
        if (pawn == null)
        {
            Destroy(gameObject);
        }



        switch (currentState)
        {
            //idle state
            case AIState.Idle:

                //do nothing
                DoIdle();

                if (!IsHasTarget())
                {
                    ChangeState(AIState.ChooseTarget);
                }

                if (target != null)
                {
                    //if can hear target, and target is not blocked, turn towards noise
                    if (CanHear(target) && IsTargetNotBlocked(target, visionDistance))
                    {

                        ChangeState(AIState.Turn);

                    }
                }



                if (target != null)
                {
                    //if can see target, then start chase AI State
                    if (CanSee(target) && IsTargetNotBlocked(target, visionDistance))
                    {

                        ChangeState(AIState.Chase);

                    }
                }


                break;



            //turn state
            case AIState.Turn:

                if (target != null)
                {
                    if (CanHear(target) && IsTargetNotBlocked(target, visionDistance))
                    {
                        TurnTowardsPoint(target);
                    }
                }


                if (target != null)
                {
                    //if can see target, chase
                    if (CanSee(target) && IsTargetNotBlocked(target, visionDistance))
                    {

                        ChangeState(AIState.Chase);

                    }
                }



                if (target != null)
                {
                    //if can't hear target, and can't see target, go to idle
                    if (!CanHear(target) && !CanSee(target))
                    {

                        ChangeState(AIState.Idle);

                    }
                }



                break;


            //chase state
            case AIState.Chase:

                if (target != null)
                {
                    //Chase target
                    if (CanSee(target) && IsTargetNotBlocked(target, visionDistance))
                    {
                        DoChase();//if can't see target, go to idle
                    }
                }





                if (target != null)
                {
                    //if can't see target, go to idle
                    if (!CanSee(target))
                    {

                        ChangeState(AIState.Idle);

                    }
                }

                break;




            //get the player as a target
            case AIState.ChooseTarget:

                TargetPlayerOne();

                ChangeState(AIState.Idle);

                break;

        }


    }

    public void OnDestroy()
    {

        //Remove tank from gamemanager
        GameManager.instance.ai.Remove(this);

    }
}
