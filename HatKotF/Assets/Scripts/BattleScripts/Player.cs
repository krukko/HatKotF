using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int maxHP;
    public int currentHP;
    int XP;

    public int attack;
    public int defense;
    public int weapon = 0;


    public Slider healthBar;

    private void Start()
    {
        this.currentHP = this.maxHP;
        this.healthBar.maxValue = this.maxHP;
    }

    public int GiveHP()
    {
        return this.currentHP;
    }

    public void SetCurrentHP(int amount)
    {
        currentHP += amount;
        UpdateHPBar();
    }

    public void ResetHP()
    {
        this.currentHP = 5;
    }

    public void GetXP(int amount)
    {
        XP += amount;
    }

    public bool AmIAlive()
    {
        if (this.currentHP <= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void UpdateHPBar()
    {
        healthBar.value = currentHP;
    }
}
