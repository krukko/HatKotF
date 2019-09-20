using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Player player;
    public Slider healthBar;

    public void Update()
    {
        //player.currentHP -= damage;
        this.healthBar.value = player.currentHP;
        Debug.Log("Lost HP.");
    }

}
