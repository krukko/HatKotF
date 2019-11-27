using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueShower : MonoBehaviour
{
    public OldEmotionList emotionList = new OldEmotionList();
    public Text dialogueText;

    public void Start()
    {
        emotionList.NewEmotions();
        emotionList.NewDialogue();
    }
}
