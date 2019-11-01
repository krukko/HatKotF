using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Enemy enemy = new Enemy();
    public Slider hpBar;

    public void Update()
    {
        hpBar.value = enemy.enemyCurrentHP;
    }
}
