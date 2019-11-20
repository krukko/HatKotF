using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public bool happyUnlocked = false;
    public bool sadUnlocked = false;
    public bool angryUnlocked = false;
    public bool fearfulUnlocked = false;
    public bool disgustUnlocked = false;

    public bool tier2Unlocked = false;
    
    public void UnlockTier()
    {
        tier2Unlocked = true;
    }
}