using UnityEngine;

[System.Serializable]
public class PowerUpScore : Powerup
{
    public int scoreAmount;

   

    public override void Apply(Pawn target)
    {

        Controller tempController = target.GetController();


        //if controller isn't null, add score to controller amount
        if (tempController != null)
        {
            tempController.AddToScore(scoreAmount);
        }


    }

    public override void Remove(Pawn target)
    {
        //TODO: Nothing. We don't do anything when removing a healing powerup 
    }

}
