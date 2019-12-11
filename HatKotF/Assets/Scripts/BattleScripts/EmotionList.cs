using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmotionList : MonoBehaviour
{
    public List<Emotion> emotions = new List<Emotion>();
    public DialogueTrigger dialogueTrigger;

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

    public bool foxFight;
    public bool catFight;
    public bool tonttuFight;
    public bool neitoFight;

    void Start()
    {
        if(!foxFight)
        {
            NewEmotions();
            ChooseDialogue();

            GameRoundEmotions();
        }
        else
        {
            dialogueTrigger.TriggerDialogue();
            Debug.Log("Starting dialogue...");
        }
    }

    public void ChooseDialogue()
    {
        if (foxFight)
        {
            FoxNewDialogue();
        }
        if (catFight)
        {
            CatNewDialogue();
        }
        if (tonttuFight)
        {
            TonttuNewDialogue();
        }
        if (neitoFight)
        {
            NeitoNewDialogue();
        }
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
        happy = new Emotion("happy", 1, ":)", happySprite, false);
        emotions.Add(happy);

        sad = new Emotion("sad", 2, ":(", sadSprite, false);
        emotions.Add(sad);  

        fearful = new Emotion("fearful", 4, ":{", fearfulSprite, false);
        emotions.Add(fearful);

        surprised = new Emotion("surprised", 5, ":O", surprisedSprite, false);
        emotions.Add(surprised);

        if (!neitoFight)
        {
            angry = new Emotion("angry", 3, ">:(", angrySprite, false);
            emotions.Add(angry);
        }

        if (!tonttuFight)
        {
            disgusted = new Emotion("disgusted", 6, "XP", disgustedSprite, false);
            emotions.Add(disgusted);
        }

        //Debug.Log("Emotions created and added to list.");
    }

    //Give the emotions dialogue options
    public void FoxNewDialogue()
    {
        happy.AddDialogue("Don't worry. With my guidance, we'll get you home in no time.");
        happy.AddDialogue("I'm the most cunning creature in this entire forest. Feel free to admire me.");

        sad.AddDialogue("It's a shame you lost that treasure…");
        sad.AddDialogue("Boy, what have I gotten myself into...");

        angry.AddDialogue("When I finally catch the Rabbit, that fluffy cheater will get what's coming to him.");
        angry.AddDialogue("Hey, I know perfectly well where we're going!");

        fearful.AddDialogue("Say, you haven't seen any bears around here, have you?");
        fearful.AddDialogue("There are some pretty shady places between this forest and the King's palace...");

        surprised.AddDialogue("A black cat? You've seen one?");
        surprised.AddDialogue("Did you just try to pet me?!");

        disgusted.AddDialogue("Did you know rowanberries taste terrible?");
        disgusted.AddDialogue("I hope that daft Owl isn't anywhere nearby.");
    }


    public void CatNewDialogue()
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

    public void TonttuNewDialogue()
    {
        happy.AddDialogue("I’m so glad at least you are safe.");
        happy.AddDialogue("I loved your family's sauna a lot...");

        sad.AddDialogue("What will become of you now?");
        sad.AddDialogue("sssssssss");

        angry.AddDialogue("I tried to warn everyone. But I was too late!");
        angry.AddDialogue("aaaaaaaaaaa");

        fearful.AddDialogue("I couldn’t do anything to help anyone…");
        fearful.AddDialogue("Don’t go back home now, Hilja! Promise you won’t!");

        surprised.AddDialogue("Little Hilja? Here all alone?");
        surprised.AddDialogue("Only your farmhand has seen me before.");
    }

    public void NeitoNewDialogue()
    {
        happy.AddDialogue("I’m sure the King will be happy to help you.");
        happy.AddDialogue("I'm a forest maiden. We decorate the woods for Mistress Mielikki.");

        sad.AddDialogue("So you have been forest covered? Poor child.");
        sad.AddDialogue("Children get lost here all the time, but forest covering is another thing.");

        fearful.AddDialogue("I saw an upset gnome running past. I hope everything is okay.");
        fearful.AddDialogue("Stay safe on your travels. The forest is a dangerous place.");

        surprised.AddDialogue("You’re travelling with Kettu? Willingly?");
        surprised.AddDialogue("What a polite little human!");

        disgusted.AddDialogue("I hope Kettu is not up to his old tricks again.");
        disgusted.AddDialogue("Kettu tends to trick people just for fun. Be careful.");
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

    //add the tier2 bool returning.
    public bool GiveTier(Emotion emotion)
    {
        bool newTier = emotion.GiveBool();
        return newTier;
    }

    public void OnResetClick()
    {
        Start();
    }
}
