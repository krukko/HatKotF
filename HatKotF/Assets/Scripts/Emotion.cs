using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emotion : MonoBehaviour
{
    //Creates a new emotion. You can give it a name, an ID number, a list of dialogue options and a face (as a string).
    string name;
    int ID;
    List<string> dialogue = new List<string>();
    string face;
    Sprite expression;

    public Emotion(string newName, int newID, string newFace, Sprite sprite)
    {
        this.name = newName;
        this.ID = newID;
        this.face = newFace;
        this.expression = sprite;
    }

    public void AddDialogue(string newDialogue)
    {
        dialogue.Add(newDialogue);
    }

    public string GiveName()
    {
        return name;
    }

    public int GiveID()
    {
        return ID;
    }

    public string GiveFace()
    {
        return face;
    }

    public Sprite ReturnSprite()
    {
        return expression;
    }

    public string GiveLine()
    {
        string givenLine = dialogue[Random.Range(0, dialogue.Count)];
        return givenLine;
    }
}
