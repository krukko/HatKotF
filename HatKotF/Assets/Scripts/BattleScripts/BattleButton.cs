//This script should only include functions strictly related to the emotion buttons
//in the battle scene.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class BattleButton : MonoBehaviour
{
    public int ID;
    public string name;
    internal object onClick;
    
    public EmotionListCopy emotionList;
    public BattleManager battleManager;

    public GameManager gameManager;

    public GameObject resetButton;
    public GameObject tier1Menu;
    public GameObject tier2; //place the appropriate tier2 button.

    protected bool endGame;
    public bool winGame;
    public bool loseGame;
    bool tier2Menu;

    public int currentEmotionID;

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

    //for calculating the base emotion in tier2
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
        currentEmotionID = comparableEmotionID;

        int currentPlayerHP = battleManager.player.GiveHP();
        int currentEnemyHP = battleManager.enemy.GiveHP();

        //TakeNDigits(emotionList.currentEmotionID, NofDigits);

        if (gameManager.tier2Unlocked && !tier2Menu)
        {
            tier2.SetActive(true);
            tier2Menu = true;
            tier1Menu.SetActive(false);
        }

        if (!gameManager.tier2Unlocked && !tier2Menu) //battleManager.battleTier == 1
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
                    battleManager.Win();
                }
                else
                {
                    emotionList.GameRoundEmotions();
                    //battleManager.emotionButtons.SetActive(true);
                    battleManager.BackClicked();
                }
            }
            else
            {
                battleManager.DamageToPlayer();

                if (battleManager.player.AmIAlive() == false)
                {
                    loseGame = true;
                    endGame = true;
                    battleManager.GameOver();
                }
                else
                {
                    emotionList.GameRoundEmotions();
                    //BattleStatus();
                    battleManager.BackClicked();
                }
                emotionList.GameRoundEmotions();
            }
        }
        //else
        //{

        //    int CompareTier1 = TakeNDigits(comparableEmotionID, 1); //the base emotion
        //    int compareButton1 = TakeNDigits(comparableEmotionID, buttonNumber); //the int for the first number of the button (to compare with the base emotion)
        //    comparableEmotionID = TakeNDigits(comparableEmotionID, battleManager.battleTier);

        //    if (compareButton1 == CompareTier1)
        //    {
        //        if (comparableEmotionID == buttonNumber)
        //        {
        //            if (battleManager.enemy.AmIAlive() == true)
        //            {
        //                battleManager.DamageToEnemy();
        //            }
        //            if (battleManager.enemy.AmIAlive() == false)
        //            {
        //                winGame = true;
        //                endGame = true;
        //                battleManager.Win();
        //            }
        //            else
        //            {
        //                emotionList.GameRoundEmotions();
        //                battleManager.emotionButtons.SetActive(true);
        //            }
        //        }

        //        if (comparableEmotionID != buttonNumber)
        //        {
        //            battleManager.damageModifier = 0.75f;
        //        }
        //        else
        //        {
        //            battleManager.DamageToPlayer();

        //            if (battleManager.player.AmIAlive() == false)
        //            {
        //                loseGame = true;
        //                endGame = true;
        //                battleManager.GameOver();
        //            }
        //            else
        //            {
        //                emotionList.GameRoundEmotions();
        //            }
        //        }
        //    }
        //}
        
    }

    public void ResetClick(Button button)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("ForestEdge");
    }
}

