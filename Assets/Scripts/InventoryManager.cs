using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public PlayerObjectThrower thrower;
    public RangeSelector range;
    public Ability[] abilities;
    public Knife[] knives;

    private int activeAbility = 0;
    private int activeKnife = 0;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            throwKnife();
        }
        if (Input.GetMouseButtonDown(1))
        {
            useAbility(false);
        }
        else if (Input.GetMouseButton(1))
        {
            useAbility(true);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            Knife.setHoldAbility(null);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (abilities.Length > 0)
            {
                activeAbility = activeAbility - 1;
                activeAbility = activeAbility < 0 ? abilities.Length - 1 : activeAbility;
            }
            print(abilities[activeAbility].getName() + " is active");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (abilities.Length > 0)
            {
                activeAbility = (activeAbility + 1) % abilities.Length;
            }
            print(abilities[activeAbility].getName() + " is active");
        }
    }

    public void useAbility(bool hold)
    {
        useAbility(activeAbility, hold);
    }
    public void useAbility(int abilityIdx, bool hold)
    {
        float test = range.getRange();
        if (abilityIdx >= 0 && abilityIdx < abilities.Length)
        {
            Ability ability = abilities[abilityIdx];
            if (!hold)
            {
                Knife.getKnives(Camera.main.ScreenToWorldPoint(Input.mousePosition), range.getRange()).ForEach
                (
                    k => k.applyAbility(ability)
                );
            }
            else
            {
                Knife.setHoldAbility(ability);
            }
        }
    }

    public void throwKnife()
    {
        throwKnife(activeKnife);
    }
    public void throwKnife(int knifeIdx)
    {
        if (knifeIdx >= 0 && knifeIdx < knives.Length)
        {
            Knife knifeSpot = knives[knifeIdx];
            // if (knifeSpot.Item2 > 0)
            // {
            thrower.throwKnife(knifeSpot.gameObject);
            // knifeSpot.Item2--;
            // }
        }
    }
}
