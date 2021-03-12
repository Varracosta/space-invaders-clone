using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private AudioClip _deathSFX;
    [SerializeField] private AudioClip _hitSFX;
    [SerializeField] private GameObject _explosion;
    [SerializeField] private AudioClip _bonusSFX;
    private float _deathSoundVolume = 0.7f;
    private const int MAX_HEALTH = 300;
    private int _currentHealth;
    private float _shipSpeed = 10f;

    [Header("Projectile")]
    [SerializeField] private GameObject _projectile;
    [SerializeField] private AudioClip _projectileSFX;
    private float _projectileSoundVolume = 0.5f;
    private float _blastSpeed = 15f;

    //values of screen boundaries, between them player's ship can move 
    private const float MIN_X = 0.7f;
    private const float MAX_X = 20.5f;

    //instead of destroying player, its just "hiddens" offsreen
    private Vector2 _startPosition;
    private Vector2 _offscreenPosition;

    private LivesManager _livesManager;
    private SceneLoader _sceneLoader;

    private void Start()
    {
        _currentHealth = MAX_HEALTH;
        _livesManager = FindObjectOfType<LivesManager>();
        _sceneLoader = FindObjectOfType<SceneLoader>();
        _startPosition = new Vector2(10f, 1f);
        _offscreenPosition = new Vector2(-20f, -20f);
    }

    private void Update()
    {
        MovingTheShip();
        Firing();
    }
    public int GetHealth() { return _currentHealth; }
    private void MovingTheShip()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * _shipSpeed;
        float newXPosition = transform.position.x + deltaX;
        Vector2 shipsNewPosition = new Vector2(newXPosition, transform.position.y);
        shipsNewPosition.x = Mathf.Clamp(newXPosition, MIN_X, MAX_X);
        transform.position = shipsNewPosition;
    }
    private void Firing()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject blast = Instantiate(_projectile, transform.position, Quaternion.identity);
            blast.GetComponent<Rigidbody2D>().velocity = new Vector2(0, _blastSpeed);
            AudioSource.PlayClipAtPoint(_projectileSFX, Camera.main.transform.position, _projectileSoundVolume);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        _currentHealth -= damageDealer.GetDamage();
        damageDealer.Hit();

        if(_currentHealth <= 0)
        {
            _currentHealth = 0;
            Dying();
        }
        else
        {
            AudioSource.PlayClipAtPoint(_hitSFX, Camera.main.transform.position, 0.5f);
        }
    }
    private void Dying()
    {
        GameObject bang = Instantiate(_explosion, transform.position, Quaternion.identity);
        Destroy(bang, 1f);
        AudioSource.PlayClipAtPoint(_deathSFX, Camera.main.transform.position, _deathSoundVolume);

        _livesManager.numOfLives--;
        _livesManager.DisplayLives(_livesManager.numOfLives);
        if (_livesManager.numOfLives == 0)
        {
            Destroy(gameObject);
            _sceneLoader.LoadGameOverScene();
        }
        else
        {
            StartCoroutine(Respawning());
        }
    }

    IEnumerator Respawning()
    {
        transform.position = _offscreenPosition;
        yield return new WaitForSeconds(2f);
        _currentHealth = MAX_HEALTH;
        transform.position = _startPosition;
    }

    public void AddBonusLife()
    {
        _livesManager.AddLife();
        AudioSource.PlayClipAtPoint(_bonusSFX, Camera.main.transform.position, 0.5f);
    }
    public void AddBonusHealth()
    {
        if (_currentHealth == MAX_HEALTH)
            FindObjectOfType<GameStats>().AddToScore(50);
        else
            _currentHealth += 100;
        AudioSource.PlayClipAtPoint(_bonusSFX, Camera.main.transform.position, 0.5f);
    }
}
