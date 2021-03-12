using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    //one array (to rule them all) to hold them all and spawn in acsending order
    public GameObject[] allWaves;
    private GameObject _currentWave;
    private Vector2 _waveSpawnPosition;
    private int _waveIndex = 0;

    private void Start()
    {
        _waveSpawnPosition = new Vector2(7f, 13f);
        SpawnNewWave();
    }
    public void SpawnNewWave()
    {
        StartCoroutine(SpawnWave());
    }
    private IEnumerator SpawnWave()
    {
        if(_waveIndex < allWaves.Length)
        {
            if (_currentWave != null)
                Destroy(_currentWave);

            yield return new WaitForSeconds(2);

            _currentWave = Instantiate(allWaves[_waveIndex], _waveSpawnPosition, Quaternion.identity);
            _waveIndex++;
        }
        else if(_waveIndex == allWaves.Length)
        {
            SceneLoader.LoadWinnerScene();
        }
    }

    public int GetWaveIndex() { return _waveIndex; }
}
