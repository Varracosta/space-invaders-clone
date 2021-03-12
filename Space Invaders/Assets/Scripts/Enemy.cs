using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Info")]
    [SerializeField] private float _health = 100;
    [SerializeField] private int _scorePointsPerKill = 20;
    [SerializeField] private AudioClip _deathSFX;
    [SerializeField] [Range(0, 1)] private float _deathSoundVolume = 0.2f;
    [SerializeField] private GameObject _explosion;
    [SerializeField] private AudioClip _hitSFX;

    [Header("Projectile")]
    [SerializeField] private GameObject _laser;
    [SerializeField] private AudioClip _laserSFX;
    private float _laserSpeed = -10f;

    [Header("Misc")]
    [SerializeField] private GameObject _lifeBonus;
    [SerializeField] private GameObject _healthBonus;

    //what is a chance to spawn each bonus
    private float _bonusLifeSpawnChance = 1;
    private float _bonusHealthSpawnChance = 3;

    //how often bonuses will spawnn after each killed enemy
    private float _randomSpawn;

    private void Start()
    {
        _randomSpawn = Random.Range(0, 30);
    }

    //Getting info about enemy 
    public GameObject GetLaser() { return _laser; }
    public float GetLaserSpeed() { return _laserSpeed; }
    public AudioClip GetLaserSound() { return _laserSFX; }
    public AudioClip GetDeathSound() { return _deathSFX; }

    //Getting hit, substracting health and either playing "death sound" on dying or "hit sound" when...hit
    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        DamageDealer damageDealer = otherObject.gameObject.GetComponent<DamageDealer>();
        _health -= damageDealer.GetDamage();
        damageDealer.Hit();

        if(_health <= 0)
        {
            Die();
        }
        else
        {
            AudioSource.PlayClipAtPoint(_hitSFX, Camera.main.transform.position, 0.5f);
        }
    }

    //here is the process of dying: adding score, desctroying object and substracting itself from wave of enemies array,
    //instantiating an explosion with soundeffect, and spawn health/life bonuses 
    private void Die()
    {
        FindObjectOfType<GameStats>().AddToScore(_scorePointsPerKill);
        Destroy(gameObject);
        WaveBehavior.waveOfEnemies.Remove(gameObject);
        GameObject bang = Instantiate(_explosion, transform.position, Quaternion.identity);
        Destroy(bang, 1f);
        AudioSource.PlayClipAtPoint(_deathSFX, Camera.main.transform.position, _deathSoundVolume);
        SpawnBonuses();
    }

    //Spawning health and life bonuses after destroying enemies
    private void SpawnBonuses()
    {
        if (_randomSpawn == _bonusLifeSpawnChance)
            Instantiate(_lifeBonus, transform.position, Quaternion.identity);
        else if (_randomSpawn == _bonusHealthSpawnChance)
            Instantiate(_healthBonus, transform.position, Quaternion.identity);
    }
}
