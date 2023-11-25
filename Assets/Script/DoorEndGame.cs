using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorEndGame : MonoBehaviour
{
    [SerializeField] Transform panelEndGame;
    private void EndGame()
    {
        Time.timeScale = 0;
        panelEndGame.gameObject.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
           EndGame();
        }
    }
    public void PlayAgain()
    {
        Time.timeScale = 1;
        FindObjectOfType<GameSession>().ResetGameSession();
    }

  
}
