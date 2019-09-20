using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueShower : MonoBehaviour
{
    public EmotionList emotionList = new EmotionList();
    public Text dialogueText;

    public void Start()
    {
        emotionList.NewEmotions();
        emotionList.NewDialogue();
    }
}
