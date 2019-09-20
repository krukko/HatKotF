//This script should only include functions strictly related to the emotion buttons
//in the battle scene.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleButton : MonoBehaviour
{
    public int ID;
    public string name;
    internal object onClick;

    public Player player = new Player();
    public Enemy enemy = new Enemy();
    public EmotionList emotionList;
    public BattleManager battleManager;

    public GameObject resetButton;

    protected bool endGame;
    public bool winGame;
    public bool loseGame;

    public void Start()
    {
        resetButton.SetActive(false);
        emotionList.OnResetClick();

        endGame = false;
        winGame = false;
        loseGame = false;
    }

    public BattleButton(int ID, string name)
    {
        this.ID = ID;
        this.name = name;
    }

    public int giveButtonID()
    {
        return this.ID;
    }

    public string giveButtonName()
    {
        return this.name;
    }

    public void DoWhenClicked(BattleButton button)
    {
        int buttonNumber = button.giveButtonID();
        int comparableEmotionID = emotionList.currentEmotionID;

        int currentPlayerHP = player.GiveHP();
        int currentEnemyHP = enemy.GiveHP();

        if(buttonNumber == comparableEmotionID)
        {
            if(enemy.AmIAlive() == true)
            {
                enemy.SetCurrentHP(-1);

            }
            if (enemy.AmIAlive() == false)
            {
                winGame = true;
                endGame = true;
            }
            else
            {
                emotionList.GameRoundEmotions();
                battleManager.BattleStatus();
            }
        }
        else
        {
            player.SetCurrentHP(-1);
            if (player.AmIAlive() == false)
            {
                loseGame = true;
                endGame = true;
            }
            else
            {
                emotionList.GameRoundEmotions();
                battleManager.BattleStatus();
            }
        }
        emotionList.GameRoundEmotions();
        battleManager.BattleStatus();
    }

    public void ResetClick(Button button)
    {
        Start();
        emotionList.OnResetClick();
        battleManager.ResetButton();
    }
}
