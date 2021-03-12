using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBehavior : MonoBehaviour
{
    [Header("Wave Info")]
    public static List<GameObject> waveOfEnemies;

    //borders:
    private const float MIN_X = 0.7f;            //left screen border
    private const float MAX_X = 20.5f;           //right screen border
    private float _positionY = 5f;                    //vertical start position of each wave
    private float _gameOverBottomBorder = 2f; 

    private float _moveTimer = 0.01f;
    private float _timeToMove = 0.005f;
    private bool _movingRight = true;
    private float _maximumSpeed = 0.01f;

    private float _shootTimer;
    private float _maxFiringSpeed = 2f;
    private float _minFiringSpeed = 0.3f;

    [Header("Boss Info")]
    public GameObject boss;
    private Vector2 _bossRightSpawnPosition;
    private Vector2 _bossLeftSpawnPosition;

    private bool _needToSpawnBossOnTheRight = true;
    private bool _moveTheBossToTheLeft;

    private float _bossSpawnTimer = 1f;
    private float _minSpawnTime = 5f;

    private bool _dropDown = true;
    private Enemy _enemy;
    private SceneLoader _sceneLoader;
    private WaveSpawner _waveSpawner;

    private void Start()
    {
        waveOfEnemies = new List<GameObject>();
        AddAllEnemies();
        _enemy = FindObjectOfType<Enemy>();
        _sceneLoader = FindObjectOfType<SceneLoader>();
        _waveSpawner = FindObjectOfType<WaveSpawner>();
        _shootTimer = Random.Range(0f, 2f);
        _bossRightSpawnPosition = new Vector2(22f, 9f);
        _bossLeftSpawnPosition = new Vector2(-1.5f, 9f);
    }

    private void Update()
    {
        if (_dropDown)  //new wave will drop down from its spawn position above the screen
        {
            transform.Translate(Vector2.down * Time.deltaTime * 10);

            if(transform.position.y <= _positionY)   //if a new wave reaches a specific vertical position, it will stop droping
            {
                _dropDown = false;
            }
        }
        else
        {
            Move();
            Shoot();
            CountingEnemies();
            SpawnBoss();
        }
    }
    //adding all enemies with corresponding tag to an array-wave
    private void AddAllEnemies()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            waveOfEnemies.Add(enemy);
        }
    }
    private void Move()
    {
        Vector2 moveRight = new Vector2(0.07f, 0);
        Vector2 moveLeft = new Vector2(-0.07f, 0);
        Vector2 moveDown = new Vector2(0, -0.5f);

        bool boundariesReached = false;

        _moveTimer -= Time.deltaTime;
        if (_moveTimer <= 0)
        {
            for (int i = 0; i < waveOfEnemies.Count; i++)
            {
                if (_movingRight)
                    waveOfEnemies[i].transform.Translate(moveRight); //moving the wave until either of boundaries are reached. Then 
                else                                                 //droping down and move in opposite way
                    waveOfEnemies[i].transform.Translate(moveLeft);

                if (waveOfEnemies[i].transform.position.x > MAX_X || waveOfEnemies[i].transform.position.x < MIN_X)
                    boundariesReached = true;
            }

            if (boundariesReached == true)
            {
                for (int i = 0; i < waveOfEnemies.Count; i++)
                {
                    waveOfEnemies[i].transform.Translate(moveDown);
                    if (waveOfEnemies[i].transform.position.y <= _gameOverBottomBorder)   //if the lowest reach is...reached, than its GameOver time
                        _sceneLoader.LoadGameOverScene();
                }
                _movingRight = !_movingRight; ;
            }

            _moveTimer = GetWaveMovementSpeed();
        }
    }
    private float GetWaveMovementSpeed()
    {
        float waveSpeed = waveOfEnemies.Count * _timeToMove;      //the fewer enemies are, the faster they move then

        if(waveSpeed < _maximumSpeed)            
        {
            return _maximumSpeed;      //but not so quick. If its turns out its very quick, then the wave will move with max limit speed
        }
        else
        {
            return waveSpeed;
        }
    }
    private void Shoot()
    {
        _shootTimer -= Time.deltaTime;
        if (_shootTimer <= 0)
        {
            int lastEnemyInAWave = waveOfEnemies.Count;

            Vector2 randomEnemyPosition = waveOfEnemies[Random.Range(0, lastEnemyInAWave)].transform.position;
            GameObject projectile = Instantiate(_enemy.GetLaser(), randomEnemyPosition, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, _enemy.GetLaserSpeed());
            AudioSource.PlayClipAtPoint(_enemy.GetLaserSound(), Camera.main.transform.position, 0.2f);

            _shootTimer = GetFiringSpeed();
        }
    }
    private float GetFiringSpeed()
    {
        float initialFiringSpeed = waveOfEnemies.Count * 0.15f;    //same as with speed: the fewer they are, the faster they shoot

        if (initialFiringSpeed > _maxFiringSpeed)
            initialFiringSpeed = _maxFiringSpeed;

        if (initialFiringSpeed < _minFiringSpeed)
            initialFiringSpeed = _minFiringSpeed;

        float finalFiringSpeed = Random.Range(_minFiringSpeed, initialFiringSpeed);
        return finalFiringSpeed;
    }
    private void CountingEnemies()
    {
        if(waveOfEnemies.Count == 0)
        {
            _waveSpawner.SpawnNewWave();   //if everyone in the wave is killed, then next wave is dropping down
        }
    }
    private void SpawnBoss()
    {
        _bossSpawnTimer -= Time.deltaTime;

        if(_bossSpawnTimer <= 0)
        {
            if (_needToSpawnBossOnTheRight)
            {
                Instantiate(boss, _bossRightSpawnPosition, Quaternion.identity);
                _needToSpawnBossOnTheRight = false; //as its already spawned on the right
                _moveTheBossToTheLeft = true;
                _bossSpawnTimer = _minSpawnTime;
            }
            else
            {
                Instantiate(boss, _bossLeftSpawnPosition, Quaternion.identity);
                _needToSpawnBossOnTheRight = true;
                _moveTheBossToTheLeft = false;
                _bossSpawnTimer = _minSpawnTime;
            }
        }
    }
    public bool WhereTheBossSpawned() { return _moveTheBossToTheLeft; }
}
