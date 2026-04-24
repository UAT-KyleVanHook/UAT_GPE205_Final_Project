using UnityEngine;
[System.Serializable]

public abstract class Powerup 
{
    public float lifeSpan;

    //apply the powerup effect
    public abstract void Apply(Pawn target);

    //remove the powerup effect
    public abstract void Remove(Pawn target);
}
