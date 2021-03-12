using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BonusMainBehavior : MonoBehaviour
{   
    //Main abstract class for all bonuses
    //Drop le bonus down 
    private void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime * 3f);
    }

    //Add le bonus to Player on collision
    public abstract void AddBonus();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            AddBonus();
    }
}
