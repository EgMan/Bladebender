using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public PlayerObjectThrower thrower;
    public RangeSelector range;

    private uint activeAbility = 0;
    private uint activeKnife = 0;
    private List<Ability> abilities;
    public Knife[] knives;
    void Start()
    {
        abilities = new List<Ability>();
        abilities.Add(new AbilityAntigrav());

        // knives = new List<(Knife, int)>();
        // knives.Add((new KnifeMain(), 10));
    }

    //TODO move input bindings into dedicated input script
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            throwKnife();
        }
        if (Input.GetMouseButtonDown(1))
        {
            useAbility();
        }
    }
 
    public void useAbility()
    {
        useAbility(activeAbility);
    }
    public void useAbility(uint abilityIdx)
    {
        float test = range.getRange();
        if (abilityIdx < abilities.Count)
        {
            abilities[(int)activeAbility].activate(
                Knife.getKnives(Camera.main.ScreenToWorldPoint(Input.mousePosition), range.getRange())
                );
        }
    }

    public void throwKnife()
    {
        throwKnife(activeKnife);
    }
    public void throwKnife(uint knifeIdx)
    {
        if (knifeIdx < knives.Length)
        {
            Knife knifeSpot = knives[(int)knifeIdx];
            // if (knifeSpot.Item2 > 0)
            // {
                thrower.throwKnife(knifeSpot.gameObject);
                // knifeSpot.Item2--;
            // }
        }
    }
}
