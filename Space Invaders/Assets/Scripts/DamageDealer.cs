using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    //unified script for all projectiles
    [SerializeField] private int _damage = 100;

    //inflicts damage and destroys itself after hitting an object
    public int GetDamage() { return _damage; }
    public void Hit()
    {
        Destroy(gameObject);
    }
}
