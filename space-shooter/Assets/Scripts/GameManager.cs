using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool _gameOver;

    private void Update()
    {
        if (_gameOver && Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Level Restarted");
            SceneManager.LoadScene("Game");
        }
    }

    public void GameOver()
    {
        _gameOver = true;
    }
}
