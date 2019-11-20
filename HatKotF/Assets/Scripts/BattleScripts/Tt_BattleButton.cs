//Battle Button for tier2 emotions.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tt_BattleButton : MonoBehaviour
{
    public int ID;
    public string name;
    internal object onClick;

    public EmotionList emotionList;
    public BattleManager battleManager;

    protected bool endGame;
    public bool winGame;
    public bool loseGame;

    public int currentEmotionID;

    public void Attack(Tt_BattleButton button)
    {
        int buttonNumber = button.giveButtonID();
        int comparableEmotionID = emotionList.currentEmotionID;
        currentEmotionID = comparableEmotionID;

        int currentPlayerHP = battleManager.player.GiveHP();
        int currentEnemyHP = battleManager.enemy.GiveHP();
        int CompareTier1 = TakeNDigits(comparableEmotionID, 1); //the base emotion

        int compareButton1 = TakeNDigits(comparableEmotionID, buttonNumber); //the int for the first number of the button (to compare with the base emotion)
        comparableEmotionID = TakeNDigits(comparableEmotionID, battleManager.battleTier);

        if (compareButton1 == CompareTier1)
        {
            if (comparableEmotionID == buttonNumber)
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
                    battleManager.BackClicked();
                }
            }

            if (comparableEmotionID != buttonNumber)
            {
                battleManager.damageModifier = 0.75f;
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
                    battleManager.BackClicked();
                }
            }
        }
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

    public Tt_BattleButton(int ID, string name)
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
}
