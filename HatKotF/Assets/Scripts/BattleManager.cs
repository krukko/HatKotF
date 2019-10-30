//This script is looking over the general status of the battle, such as Win/Loss/Reset/Damage,
//as well as looking after the choice buttons. Buttons might be moved to their own manager later on.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    public Player player = new Player();
    public Enemy enemy = new Enemy();
    BattleButton battleButton; //this is the reference to the BattleButton script in the scene
    public EmotionList emotionList;
    public GameManager gameManager;
    public IdentifyWords identify;
    public Timer timer;

    //Buttonlists
    public GameObject emotionButtons; //reference to the parent object under which the emotion buttons are
    public GameObject companionActions; //reference to companion buttons
    public GameObject menuButtons; //all buttons that are open when the battle starts ("main menu")
    public GameObject identifyMenu; //opens the identify-mini game.

    //Buttonlists for Tier 2 emotions in emotionwheel
    public GameObject happyButtons;
    public GameObject fearButtons;
    public GameObject sadButtons;
    public GameObject surpriseButtons;
    public GameObject angryButtons;
    public GameObject disgustButtons;

    public float damageModifier = 1.0f;
    public bool isBoss;
    public int battleTier = 1;

    public void Start()
    {
        player.ResetHP();
        enemy.ResetHP();

        battleButton = GetComponent<BattleButton>();

        //if (battleTier == 1)
        //{
        //    battleButton.NofDigits = 1;
        //}
        //else
        //{
        //    battleButton.NofDigits = 2;
        //}

    }

    public void Win()
    {
        SceneManager.LoadScene("Overworld");
    }

    public void GameOver()
    {
        Debug.Log("Access GameOver");
        if (isBoss)
        {
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            battleButton.resetButton.SetActive(true);
            SceneManager.LoadScene("Overworld");
        }
    }

    public void DamageToPlayer()
    {
        int damage = ((enemy.attack) / (player.defense + enemy.armor) + 1);
        player.SetCurrentHP(-damage);
    }

    public void DamageToEnemy()
    {
        int damage = ((int)damageModifier * ((player.attack + player.weapon) / (enemy.defense + enemy.armor)) + 1);
        enemy.SetCurrentHP(-damage);
    }

    public void FightClicked()
    {
        emotionButtons.SetActive(true);
        menuButtons.SetActive(false);

    }

    public void PalClicked()
    {
        menuButtons.SetActive(false);
        companionActions.SetActive(true);
    }

    public void BackClicked()
    {
        happyButtons.SetActive(false);
        sadButtons.SetActive(false);
        angryButtons.SetActive(false);
        fearButtons.SetActive(false);
        surpriseButtons.SetActive(false);
        disgustButtons.SetActive(false);
        emotionButtons.SetActive(false);
        companionActions.SetActive(false);
        identifyMenu.SetActive(false);
        menuButtons.SetActive(true);
    }

    public void RunButton()
    {
        if(isBoss == true)
        {
            Debug.Log("Escape attempted and miserably failed.");
        }
        else
        {
            //add the load scene when it's been added to the game.
        }
    }

    public void IdentifyButton()
    {
        happyButtons.SetActive(false);
        sadButtons.SetActive(false);
        angryButtons.SetActive(false);
        fearButtons.SetActive(false);
        surpriseButtons.SetActive(false);
        disgustButtons.SetActive(false);
        emotionButtons.SetActive(false);
        companionActions.SetActive(false);
        menuButtons.SetActive(false);
        identifyMenu.SetActive(true);

        identify.SpawnWords();
        timer.active = true;
        
    }

    public void UnlockHappy()
    {
        
    }

    //public void BattleStatus()
    //{
    //    if(battleButton == null)
    //    {
    //        Debug.Log("BattleButton be null. :I");
    //    }
    //    if (battleButton.winGame == true)
    //    {
    //        Win();
    //    }
    //    else if (battleButton.loseGame == true)
    //    {
    //        GameOver();
    //    }
    //    else
    //    {
    //        Debug.Log("Continuing battle.");
    //    }
    //}
    
    public void ResetButton()
    {
        Start();
    }
}