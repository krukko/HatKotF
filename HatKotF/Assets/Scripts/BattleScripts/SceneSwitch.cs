using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public bool foxFight;
    public bool catFight;
    public bool tonttuFight;
    public bool neitoFight;

    void ChooseScene(Collider ChangeScene)
    {
        if (ChangeScene.gameObject.CompareTag("Player"))
        {

            if (foxFight)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("BattleScene");
            }
            if (catFight)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("BattleScene");
            }
            if (tonttuFight)
            {
                Debug.Log("Ya done goofed.");
            }
        }
        
    }

}
