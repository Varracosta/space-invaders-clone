using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //Script to load different scenes

    [SerializeField] private AudioClip _gameOverSFX;

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadGameOverScene()
    {
        StartCoroutine(WaitAndLoad());
    }
    private IEnumerator WaitAndLoad() 
    {
        AudioSource.PlayClipAtPoint(_gameOverSFX, Camera.main.transform.position, 0.4f);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Game Over");
    }
    public static void LoadWinnerScene()
    {
        SceneManager.LoadScene(2);
    }
    public void QuitTheGame()
    {
        Application.Quit();
    }
}
