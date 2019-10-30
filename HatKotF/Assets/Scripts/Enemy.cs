using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int enemyMaxHP;
    public int enemyCurrentHP;
    public int ExpValue;

    public int attack;
    public int defense;
    public int armor;

    public Slider healthBar;

    private void Start()
    {
        this.enemyCurrentHP = this.enemyMaxHP;
        this.healthBar.maxValue = this.enemyMaxHP;
    }

    public int GiveHP()
    {
        return this.enemyCurrentHP;
    }

    public void SetCurrentHP(int amount)
    {
        enemyCurrentHP += amount;
        updateHPBar();
    }

    public void ResetHP()
    {
        this.enemyCurrentHP = 5;
    }

    public bool AmIAlive()
    {
        if (this.enemyCurrentHP <= 0) {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void updateHPBar()
    {
        healthBar.value = enemyCurrentHP;
    }
}
