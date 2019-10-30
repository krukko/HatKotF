using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float maxTime;
    float timeLeft;
    public bool active = false;

    public BattleManager battleManager;

    public Slider timeSlider;

    // Start is called before the first frame update
    void Start()
    {
        timeSlider.value = maxTime;
        timeLeft = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(active)
        {
            //Debug.Log("Active is true.");
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
        //add all the other stuff depending on stuff.
    }
}
