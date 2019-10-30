using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuddyButton : MonoBehaviour
{

    Player player;
    int defenseBoost;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Calm()
    {
        
    }

    public void Enrage()
    {

    }

    public void Defend()
    {
        defenseBoost = 1;
        player.defense += 1;
    }

    public void Help()
    {

    }
}
