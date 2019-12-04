using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewSceneSwitch : MonoBehaviour
{
    public bool foxFight;
    public bool catFight;
    public bool tonttuFight;
    public bool neitoFight;

    void OnTriggerEnter(Collider ChangeScene)
    {
        Debug.Log("I'm functional!");
        if (ChangeScene.gameObject.CompareTag("Player"))
        {

            if (foxFight)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("BattleScene");
            }
            if (catFight)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("CatBattleScene");
            }
            if (tonttuFight)
            {
                Debug.Log("Ya done goofed.");
            }
        }
    }
}
