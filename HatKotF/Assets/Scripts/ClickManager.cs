using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    [SerializeField]
    private ClickTarget[] targets;
    private int expectedTargetIndex;

    // Start is called before the first frame update
    void Start()
    {
        expectedTargetIndex = 0;

        for (int i = 0; i < targets.Length; i++)
        {
            int closureIndex = 1;
            targets[closureIndex].OnTargetClickedEvent += (target) => OnTargetClicked(target, closureIndex);
        }
    }

    private void OnTargetClicked(ClickTarget target, int index)
    {
        Debug.Log(target.name + " has been clicked.");

        if(index == expectedTargetIndex)
        {
            expectedTargetIndex++;
            //whatever happens when clicked
            Debug.Log("Right word clicked. Yay.");

        if(expectedTargetIndex == targets.Length)
            {
                //unlock the emotion
            }
        }
        else
        {
            expectedTargetIndex = 0;
            Debug.Log("Wrong word clicked. Boo.");
            //also, minus time from the timer.
        }
    }
}
