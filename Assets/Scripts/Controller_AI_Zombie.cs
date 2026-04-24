using UnityEngine;

public class Controller_AI_Zombie : Controller_AI
{
    public override void Start()
    {

        GameManager.instance.ai.Add(this);

        //do the base of start
        base.Start();



        ChangeState(AIState.Idle);


    }

    public override void Update()
    {

       // if (target == null)
        //{
            //set enemy targets
            //target = GameManager.instance.player1Object;
        //}

        base.Update();


    }

    public override void MakeDecisions()
    {
        Debug.Log("Can Hear: " + CanHear(target));

       

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


                //if can hear target, and target is not blocked, turn towards noise
                if (CanHear(target) && IsTargetNotBlocked(target, 100))
                {

                    ChangeState(AIState.Turn);

                }

                
                //if can see target, then start chase AI State
                if (CanSee(target))
                {

                    ChangeState(AIState.Chase);

                }
                

            break;



            //turn state
            case AIState.Turn:

                
                TurnTowardsPoint(target);

                //if can see target, chase
                if (CanSee(target))
                {

                    ChangeState(AIState.Chase);

                }


                //if can't hear target, and can't see target, go to idle
                if (!CanHear(target) && !CanSee(target))
                {

                    ChangeState(AIState.Idle);

                }


            break;


            //chase state
            case AIState.Chase:

                //Chase target
                DoChase();//if can't see target, go to idle


                //if can't see target, go to idle
                if (!CanSee(target))
                {

                    ChangeState(AIState.Idle);

                }


                break;




            //get the player as a target
            case AIState.ChooseTarget:

                //TargetPlayerOne();

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
