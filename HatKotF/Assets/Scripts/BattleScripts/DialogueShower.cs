using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueShower : MonoBehaviour
{
    public EmotionListCopy emotionList = new EmotionListCopy();
    public Text dialogueText;

    public void Start()
    {
        emotionList.NewEmotions();
        emotionList.NewDialogue();
    }
}
