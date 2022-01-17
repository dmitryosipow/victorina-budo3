using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ScoreSO : ScriptableObject
{
    public int correctAnswers;
    public int wrongAnswers;

    public void Reset()
    {
        correctAnswers = 0;
        wrongAnswers = 0;
    }

    public void CorrectAnswer()
    {
        correctAnswers++;
    }

    public void WrongAnswer()
    {
        wrongAnswers++;
    }
}