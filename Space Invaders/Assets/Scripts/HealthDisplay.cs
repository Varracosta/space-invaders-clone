using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    //Displaying players current health
    private Player _player;
    private Text _healthText;

    private void Start()
    {
        _healthText = GetComponent<Text>();
        _player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        _healthText.text = _player.GetHealth().ToString(); 
    }
}
