using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveNumber : MonoBehaviour
{
    //Displaying wave number
    private Text _currentWaveNumber;
    private WaveSpawner _waveSpawner;

    private void Start()
    {
        _currentWaveNumber = GetComponent<Text>();
        _waveSpawner = FindObjectOfType<WaveSpawner>();
    }

    private void Update()
    {
        _currentWaveNumber.text = _waveSpawner.GetWaveIndex().ToString();
    }
}
