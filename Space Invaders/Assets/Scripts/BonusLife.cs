using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusLife : BonusMainBehavior
{
    //Adding bonus life to Player and then destroying itself
    public override void AddBonus()
    {
        FindObjectOfType<Player>().AddBonusLife();
        Destroy(gameObject);
    }
}
