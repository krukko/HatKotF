using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float maxTime;
    public float timeLeft;
    public bool active = false;

    public BattleManager battleManager;

    public Slider timeSlider;
    
    void Start()
    {
        timeSlider.value = maxTime;
        timeLeft = maxTime;
    }
    
    void Update()
    {
        if(active)
        {
            timeLeft -= Time.deltaTime;
            timeSlider.value = timeLeft;

            if (timeLeft <= 0)
            {
                TimeUp();
            }
        }
    }

    public void TimeUp()
    {
        timeSlider.value = maxTime;
        timeLeft = maxTime;
        active = false;

        battleManager.BackClicked();
        battleManager.DamageToPlayer();
        //add all the other stuff depending on stuff.
    }
}
