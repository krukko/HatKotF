//Switch from overworld to battle scene.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public bool toBoss; //check if boss battle.

    void OnTriggerEnter(Collider ChangeScene)
    {
        if(ChangeScene.gameObject.CompareTag("Player"))
        {
            if (toBoss)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("BossBattle");
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("BattleScene");
                
            }

            Destroy(ChangeScene);
        }
    }
}
