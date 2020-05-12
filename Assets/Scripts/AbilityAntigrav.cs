using UnityEngine;
using System.Collections.Generic;

public class AbilityAntigrav : Ability
{
    public static float antigravRandomNoise = 10f;
    public RangeSelector range;

    public override void activate(List<Knife> knives)
    {
        knives.ForEach(knife => enterAntiGrav(knife));
    }
    private void enterAntiGrav(Knife k)
    {
        k.state = Knife.States.AntiGrav;
        k.rb.AddForce(new Vector2(Random.Range(0f, antigravRandomNoise), Random.Range(0f, antigravRandomNoise)));
    }
}
