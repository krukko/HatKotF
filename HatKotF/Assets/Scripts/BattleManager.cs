//This script is looking over the general status of the battle, such as Win/Loss/Reset.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    public Player player = AddComponent(Player); Player();
    public Enemy enemy = new Enemy();
    private BattleButton battleButton;
    public EmotionList emotionList;

    public bool isBoss;

    public void Start()
    {
        player.ResetHP();
        enemy.ResetHP();
    }

    public void Win()
    {
        SceneManager.LoadScene("Overworld");
    }

    public void GameOver()
    {
        Debug.Log("Access GameOver");
        if (isBoss)
        {
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            battleButton.resetButton.SetActive(true);
            SceneManager.LoadScene("Overworld");
        }
    }

    public void BattleStatus()
    {
        if (battleButton.winGame == true)
        {
            Win();
        }
        else if (battleButton.loseGame == true)
        {
            GameOver();
        }
        else
        {
            Debug.Log("Continuing battle.");
        }
    }
    
    public void ResetButton()
    {
        Start();
    }
}
