using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Victorina : MonoBehaviour
{
    #region Variables
    
    private const int VariantsTotal = 4;
    private const int HintRemoveTotal = 2;

    public Image questionImage;

    public GameObject[] buttonObjects;
    public GameObject[] livesObjects;
    public TextMeshProUGUI[] buttonTexts;

    public TextMeshProUGUI questionText;

    public QuestionSO[] questions;
    public ScoreSO score;

    private int _currentQuestionIndex;
    private int _lives;
    private QuestionSO[] _temporaryQuestions;

    #endregion

    # region Private methods

    // Start is called before the first frame update
    private void Start()
    {
        _currentQuestionIndex = 0;
        _lives = 3;
        score.Reset();

        _temporaryQuestions = new QuestionSO[questions.Length];
        questions.CopyTo(_temporaryQuestions, 0);
        ShuffleArray(_temporaryQuestions);

        SetQuestion();
    }

    private void SetQuestion(int questionIndex = -1)
    {
        int index = questionIndex != -1 ? questionIndex : _currentQuestionIndex;
        QuestionSO currentQuestion = _temporaryQuestions[index];
        buttonTexts[0].text = currentQuestion.answer1;
        buttonTexts[1].text = currentQuestion.answer2;
        buttonTexts[2].text = currentQuestion.answer3;
        buttonTexts[3].text = currentQuestion.answer4;

        foreach (var button in buttonObjects)
        {
            button.SetActive(true);
        }

        questionImage.sprite = currentQuestion.image;
        questionText.text = currentQuestion.question;
    }

    private void FinishGame()
    {
        ScenesManager.LoadScene("FinishScene");
    }

    private void UpdateLives()
    {
        for (int i = 0; i < livesObjects.Length; i++)
        {
            livesObjects[i].SetActive(i <= _lives - 1);
        }
    }

    private void HideVariant(int num)
    {
        buttonObjects[num - 1].SetActive(false);
    }

    private void ShuffleArray(QuestionSO[] inputArray)
    {
        for (int i = inputArray.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);

            (inputArray[i], inputArray[randomIndex]) = (inputArray[randomIndex], inputArray[i]);
        }
    }

    #endregion

    #region Public methods

    public void SetAnswer(int answerIndex)
    {
        if (_temporaryQuestions[_currentQuestionIndex].correctAnswer == answerIndex)
        {
            score.CorrectAnswer();
        }
        else
        {
            score.WrongAnswer();
            _lives--;
            UpdateLives();
        }

        _currentQuestionIndex++;

        if (_currentQuestionIndex > _temporaryQuestions.Length - 1)
        {
            FinishGame();
        }
        else
        {
            SetQuestion();
        }
    }

    public void RemoveWrong()
    {
        List<int> nonRepeatedIndeces = new List<int>();

        for (int i = 1; i <= VariantsTotal; i++)
        {
            if (i != _temporaryQuestions[_currentQuestionIndex].correctAnswer)
            {
                nonRepeatedIndeces.Add(i);
            }
        }

        for (int i = 0; i < HintRemoveTotal; i++)
        {
            int randomIndex = Random.Range(0, nonRepeatedIndeces.Count);
            HideVariant(nonRepeatedIndeces[randomIndex]);
            nonRepeatedIndeces.RemoveAt(randomIndex);
        }
    }

    #endregion
}