using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoClick : MonoBehaviour
{
    public ClickManager clickManager;

    public void NoClicks()
    {
        clickManager.timer.timeLeft -= 1;
    }
}
