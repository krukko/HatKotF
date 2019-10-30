//This script should only include functions strictly related to the emotion buttons
//in the battle scene.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleButton : MonoBehaviour
{
    public int ID;
    public string name;
    internal object onClick;
    
    public EmotionList emotionList;
    public BattleManager battleManager;

    GameManager gameManager;

    public GameObject resetButton;

    protected bool endGame;
    public bool winGame;
    public bool loseGame;

    public void Start()
    {
        resetButton.SetActive(false);
        //emotionList.OnResetClick();

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

    public int TakeNDigits(int emotionID, int N)
    {
        //N = NofDigits;
        //emotionID = emotionList.currentEmotionID;

        if (emotionList.currentEmotionID <= N)
        {
            return (int)Mathf.FloorToInt((emotionList.currentEmotionID / Mathf.Pow(10, N - N)));
        }
        else
        {
            return emotionList.currentEmotionID;
        }
    }

    public void Attack(BattleButton button)
    {
        int buttonNumber = button.giveButtonID();
        int comparableEmotionID = emotionList.currentEmotionID;

        int currentPlayerHP = battleManager.player.GiveHP();
        int currentEnemyHP = battleManager.enemy.GiveHP();

        //TakeNDigits(emotionList.currentEmotionID, NofDigits);

        if(battleManager.battleTier == 1)
        {
            comparableEmotionID = TakeNDigits(comparableEmotionID, battleManager.battleTier);

            if (buttonNumber == comparableEmotionID)
            {
                if (battleManager.enemy.AmIAlive() == true)
                {
                    battleManager.DamageToEnemy();
                }
                if (battleManager.enemy.AmIAlive() == false)
                {
                    winGame = true;
                    endGame = true;
                    Win();
                }
                else
                {
                    emotionList.GameRoundEmotions();
                    battleManager.emotionButtons.SetActive(true);
                }
            }
            else
            {
                battleManager.DamageToPlayer();

                if (battleManager.player.AmIAlive() == false)
                {
                    loseGame = true;
                    endGame = true;
                    GameOver();
                }
                else
                {
                    emotionList.GameRoundEmotions();
                    //BattleStatus();
                }
                emotionList.GameRoundEmotions();
            }
        }
        else
        {

            int CompareTier1 = TakeNDigits(comparableEmotionID, 1); //the base emotion
            int compareButton1 = TakeNDigits(comparableEmotionID, buttonNumber); //the int for the first number of the button (to compare with the base emotion)
            comparableEmotionID = TakeNDigits(comparableEmotionID, battleManager.battleTier);

            if(compareButton1 == CompareTier1)
            {
                if(comparableEmotionID == buttonNumber)
                {
                     if(battleManager.enemy.AmIAlive() == true)
                    {
                        battleManager.DamageToEnemy();
                    }
                    if(battleManager.enemy.AmIAlive() == false)
                    {
                        winGame = true;
                        endGame = true;
                        Win();
                    }
                    else
                    {
                        emotionList.GameRoundEmotions();
                        battleManager.emotionButtons.SetActive(true);
                    }
                }

                if(comparableEmotionID != buttonNumber)
                {
                    battleManager.damageModifier = 0.75f;
                }
                else
                {
                    battleManager.DamageToPlayer();

                    if(battleManager.player.AmIAlive() == false)
                    {
                        loseGame = true;
                        endGame = true;
                        GameOver();
                    }
                    else
                    {
                        emotionList.GameRoundEmotions();
                    }
                }
            }
        }
        
    }

    public void ResetClick(Button button)
    {
        Start();
        emotionList.OnResetClick();
        battleManager.ResetButton();
    }

    public void Win()
    {
        //activate after adding the Overworld scene.
        //SceneManager.LoadScene("Overworld");
        resetButton.SetActive(true);
    }

    public void GameOver()
    {
        Debug.Log("Access GameOver");
        if (battleManager.isBoss)
        {
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            resetButton.SetActive(true);
            //SceneManager.LoadScene("Overworld"); //Activate this line after overworld has been added!
        }
    }

    public void OpenHappy()
    {
        //if (gameManager.happyUnlocked == true)
        //{
        //    battleManager.happyButtons.SetActive(true);
        //}
        //else
        //{

        //}
    }
}

