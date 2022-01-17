using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Victorina : MonoBehaviour
{
    #region Variables
    
    public Image questionImage;

    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;

    public Image heart1;
    public Image heart2;
    public Image heart3;

    public TMP_Text questionText;

    public QuestionSO[] questions;
    public ScoreSO score;

    private int _currentQuestionIndex;
    private int _lives;
    private QuestionSO[] _temporaryQuestions;

    private const int VariantsTotal = 4;
    private const int HintRemoveTotal = 2;
    
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
        button1.GetComponentInChildren<TMP_Text>().text = currentQuestion.answer1;
        button2.GetComponentInChildren<TMP_Text>().text = currentQuestion.answer2;
        button3.GetComponentInChildren<TMP_Text>().text = currentQuestion.answer3;
        button4.GetComponentInChildren<TMP_Text>().text = currentQuestion.answer4;

        questionImage.sprite = currentQuestion.image;
        questionText.text = currentQuestion.question;

        button1.gameObject.SetActive(true);
        button2.gameObject.SetActive(true);
        button3.gameObject.SetActive(true);
        button4.gameObject.SetActive(true);
    }

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

    private void FinishGame()
    {
        ScenesManager.LoadScene("FinishScene");
    }

    private void UpdateLives()
    {
        switch (_lives)
        {

            case 2:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(true);
                heart3.gameObject.SetActive(false);
                break;
            case 1:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(false);
                heart3.gameObject.SetActive(false);
                break;
            case 0:
                heart1.gameObject.SetActive(false);
                heart2.gameObject.SetActive(false);
                heart3.gameObject.SetActive(false);
                break;
            case 3:
            default:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(true);
                heart3.gameObject.SetActive(true);
                break;
        }
    }

    private void HideVariant(int num)
    {
        switch (num)
        {

            case 1:
                button1.gameObject.SetActive(false);
                break;
            case 2:
                button2.gameObject.SetActive(false);
                break;
            case 3:
                button3.gameObject.SetActive(false);
                break;
            case 4:
                button4.gameObject.SetActive(false);
                break;
        }
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
