using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinishScene : MonoBehaviour
{
    public TMP_Text correctAnswersTotal;
    public TMP_Text wrongAnswersTotal;

    public ScoreSO score;
    
    private void Start()
    {
        correctAnswersTotal.text = score.correctAnswers.ToString();
        wrongAnswersTotal.text = score.wrongAnswers.ToString();
    }
}
