﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    public BattleManager battleManager;
    public Timer timer;
    public GameManager gameManager;
    public EmotionList emotionList;

    [SerializeField]
    private ClickTarget[] targets;
    private int expectedTargetIndex;

    // Start is called before the first frame update
    void Start()
    {
        expectedTargetIndex = 0;

        for (int i = 0; i < targets.Length; i++)
        {
            int closureIndex = i;
            targets[closureIndex].OnTargetClickedEvent += (target) => OnTargetClicked(target, closureIndex);
        }
    }

    private void OnTargetClicked(ClickTarget target, int index)
    {
        if(index == expectedTargetIndex)
        {
            expectedTargetIndex++;
            //whatever happens when clicked

        if(expectedTargetIndex == targets.Length)       //after clicking all required words in correct order, unlock emotion.
            {
                battleManager.BackClicked();
                timer.timeLeft = timer.maxTime;
                gameManager.UnlockTier();

                

                if(emotionList.currentEmotionID == 1 && gameManager.happyUnlocked == false)
                {
                    gameManager.UnlockHappy();
                }
                if(emotionList.currentEmotionID == 2)
                {
                    gameManager.UnlockSad();
                }
                if (emotionList.currentEmotionID == 3)
                {
                    gameManager.UnlockAngry();
                }
                if (emotionList.currentEmotionID == 4)
                {
                    gameManager.UnlockSurprise();
                }
                if (emotionList.currentEmotionID == 5)
                {
                    gameManager.UnlockFear();
                }
                if (emotionList.currentEmotionID == 6)
                {
                    gameManager.UnlockDisgust();
                }
            }
        }
        else
        {
            expectedTargetIndex = 0;
            timer.timeLeft -= 1;
        }
    }
}
