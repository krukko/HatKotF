//This script should only have material related straight to the emotion list and emotion
//generation during battle.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmotionList : MonoBehaviour
{
    public List<Emotion> emotions = new List<Emotion>();

    public Emotion happy;
    public Emotion sad;
    public Emotion angry;
    public Emotion fearful;
    public Emotion surprised;
    public Emotion disgusted;

    public Sprite happySprite;
    public Sprite sadSprite;
    public Sprite angrySprite;
    public Sprite fearfulSprite;
    public Sprite surprisedSprite;
    public Sprite disgustedSprite;

    public GameObject shownSprite;

    public Text dialogueText;
    public Text faceText;

    public int currentEmotionID;

    BattleManager battleManager;

    void Start()
    {
        NewEmotions();
        NewDialogue();

        GameRoundEmotions();
    }

    public void GameRoundEmotions()
    {
            Emotion randomEmotion = EmotionRandomiser();
            currentEmotionID = GiveID(randomEmotion);

            string printLine = GiveDialogue(randomEmotion);
            string printFace = GiveExpression(randomEmotion);
            string printNro = GiveID(randomEmotion).ToString();

        dialogueText.text = printLine;
        //faceText.text = printFace;

        Sprite showThisSprite = GiveFace(randomEmotion);
            shownSprite.GetComponent<SpriteRenderer>().sprite = showThisSprite;
    }

    //Create the emotions
    public void NewEmotions()
    {
        happy = new Emotion("happy", 0, ":)", happySprite);
        emotions.Add(happy);

        sad = new Emotion ("sad", 1, ":(", sadSprite);
        emotions.Add(sad);

        angry = new Emotion ("angry", 2, ">:(", angrySprite);
        emotions.Add(angry);

        fearful = new Emotion ("fearful", 3, ":{", fearfulSprite);
        emotions.Add(fearful);

        surprised = new Emotion("surprised", 4, ":O", surprisedSprite);
        emotions.Add(surprised);

        disgusted = new Emotion("disgusted", 5, "XP", disgustedSprite);
        emotions.Add(disgusted);
        //Debug.Log("Emotions created and added to list.");
    }

    //Give the emotions dialogue options
    public void NewDialogue()
    {
        happy.AddDialogue("This works now.");
        happy.AddDialogue("My code works!");

        sad.AddDialogue("My code doesn't work...");
        sad.AddDialogue("I don't have any more cookies.");

        angry.AddDialogue("My code doesn't work!!!!");
        angry.AddDialogue("RAAAAAAAHHHHHHH!!!!");

        fearful.AddDialogue("What if the code doesn't work?!");
        fearful.AddDialogue("Did I leave the stove on?");

        surprised.AddDialogue("It's Tuesday already?!");
        surprised.AddDialogue("Someone is playing this?!");

        disgusted.AddDialogue("This code is subpar!");
        disgusted.AddDialogue("What an ugly screen.");
    }

    //Get a dialogue line for chosen emotion
    public string GiveDialogue(Emotion emotion)
    {
        string givenDialogue = emotion.GiveLine();
        return givenDialogue;
    }

    //Get a face for chosen emotion
    public string GiveExpression(Emotion emotion)
    {
        string givenExpression = emotion.GiveFace();
        return givenExpression;
    }

    public Sprite GiveFace(Emotion emotion)
    {
        Sprite givenFace = emotion.ReturnSprite();
        return givenFace;
    }

    //Get ID number for chosen emotion
    public int GiveID(Emotion emotion)
    {
        int number = emotion.GiveID();
        return number;
        
    }

    //Get choose a random emotion
    public Emotion EmotionRandomiser()
    {
        Emotion chosenEmotion = emotions[Random.Range(0, emotions.Count)];
        return chosenEmotion;
    }

    public void OnResetClick()
    {
        Start();
    }
}
