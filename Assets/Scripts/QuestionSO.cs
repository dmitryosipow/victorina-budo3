using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class QuestionSO : ScriptableObject
{
    [Header("Answers")]
    public string answer1;
    public string answer2;
    public string answer3;
    public string answer4;

    [Header("Question")]
    public string question;
    
    public Sprite image;

    [Header("Number of the proper answer")]
    public int correctAnswer;
}