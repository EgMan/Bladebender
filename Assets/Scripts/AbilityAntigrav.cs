using UnityEngine;
using System.Collections.Generic;

public class AbilityAntigrav : Ability
{
    public static float antigravRandomNoise = 10f;
    public RangeSelector range;
    public override string getName()
    {
        return "Levitate";
    }
    public override void activate(Knife knife)
    {
        knife.state = Knife.States.AntiGrav;
        knife.rb.AddForce(new Vector2(Random.Range(0f, antigravRandomNoise), Random.Range(0f, antigravRandomNoise)));
    }
}
