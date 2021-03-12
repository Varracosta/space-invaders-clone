using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusHealth : BonusMainBehavior
{
    //Adding bonus health to Player and then destroying itself
    public override void AddBonus()
    {
        FindObjectOfType<Player>().AddBonusHealth();
        Destroy(gameObject);
    }
}
