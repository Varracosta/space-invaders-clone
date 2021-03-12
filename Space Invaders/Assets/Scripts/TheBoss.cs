using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBoss : MonoBehaviour
{
    [SerializeField] private AudioClip _deathSFX;
    [SerializeField] private GameObject _explosion;

    private int _pointsPerKill = 1000;

    private float _maxLeft = -2f; //destination point on the left
    private float _maxRight = 23f; //destination point on the right
    private float _speed = 12f;
    private bool _moveToTheLeft;

    private void Start()
    {
        _moveToTheLeft = FindObjectOfType<WaveBehavior>().WhereTheBossSpawned();
    }
    private void Update()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<GameStats>().AddToScore(_pointsPerKill);
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(_deathSFX, Camera.main.transform.position, 0.2f);
        GameObject bang = Instantiate(_explosion, transform.position, Quaternion.identity);
        Destroy(bang, 1f);
    }

    //(here is a little bit awkward and illogical, but it actually works that way... )
    // If le boss spawns on the left, it must move to the right, and vice versa. 
    private void Move()
    {
        if (_moveToTheLeft)
        {
            transform.Translate(Vector2.left * Time.deltaTime * _speed);

            if (transform.position.x <= _maxLeft)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            transform.Translate(Vector2.right * Time.deltaTime * _speed);

            if (transform.position.x >= _maxRight)
            {
                Destroy(gameObject);
            }
        }
    }
}
