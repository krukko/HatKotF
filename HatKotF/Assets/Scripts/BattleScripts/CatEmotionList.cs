//Emotionlist for the cat.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatEmotionList : MonoBehaviour
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
        shownSprite.GetComponent<Image>().sprite = showThisSprite;
        //shownSprite.GetComponent<SpriteRenderer>().sprite = showThisSprite;
    }

    //Create the emotions
    public void NewEmotions()
    {
        happy = new Emotion("happy", 1, ":)", happySprite);
        emotions.Add(happy);

        sad = new Emotion("sad", 2, ":(", sadSprite);
        emotions.Add(sad);

        angry = new Emotion("angry", 3, ">:(", angrySprite);
        emotions.Add(angry);

        fearful = new Emotion("fearful", 4, ":{", fearfulSprite);
        emotions.Add(fearful);

        surprised = new Emotion("surprised", 5, ":O", surprisedSprite);
        emotions.Add(surprised);

        disgusted = new Emotion("disgusted", 6, "XP", disgustedSprite);
        emotions.Add(disgusted);
        //Debug.Log("Emotions created and added to list.");
    }

    //Give the emotions dialogue options
    public void NewDialogue()
    {
        happy.AddDialogue("Yup, I’m a witch’s familiar. The real deal.");
        happy.AddDialogue("I’m just runnin’ this treasure errand for an old friend.");

        sad.AddDialogue("I can’t help ya with the treasure nor the curse, kid.");
        sad.AddDialogue("No one has managed to find that treasure yet, so don’t feel too bad.");

        angry.AddDialogue("I didn’t run off! You’re the one who stopped followin’ me!");
        angry.AddDialogue("Don’t blame me! Ya were told not to look back!");

        fearful.AddDialogue("My master doesn’t actually know I’m here.");
        fearful.AddDialogue("Cats like me don’t have many other places to go.");

        surprised.AddDialogue("Ya heard voices on the field? But there was nobody there.");
        surprised.AddDialogue("Ya got cursed? Well, that’s what you get for not listenin’ to directions.");

        disgusted.AddDialogue("Humans don’t usually treat my kind that well.");
        disgusted.AddDialogue("Oh I’m bad luck? The bad luck is sittin’ right next to ya!");
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
