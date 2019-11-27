using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance;

    public bool happyUnlocked = false;
    public bool sadUnlocked = false;
    public bool angryUnlocked = false;
    public bool surprisedUnlocked = false;
    public bool fearfulUnlocked = false;
    public bool disgustUnlocked = false;

    public bool tier2Unlocked = false;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void UnlockTier() //for unlocking all tier2s at once when needed (for testing).
    {
        tier2Unlocked = true;
    }

    public void UnlockHappy()
    {
        happyUnlocked = true;
    }

    public void UnlockSad()
    {
        sadUnlocked = true;
    }

    public void UnlockAngry()
    {
        angryUnlocked = true;
    }

    public void UnlockSurprise()
    {
        surprisedUnlocked = true;
    }

    public void UnlockFear()
    {
        fearfulUnlocked = true;
    }

    public void UnlockDisgust()
    {
        disgustUnlocked = true;
    }
}