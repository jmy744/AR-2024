using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizController : MonoBehaviour
{
    [Header("UI Elements")]
    public Text questionText;
    public Button[] answerButtons;
    public Text feedbackText;
    public Button nextButton;
    public Text scoreText;
    public GameObject questionPanel; // Single question panel
    public Button tryAgainButton; // Try Again button

    [Header("Quiz Data")]
    private string[][] questions = new string[][]
    {
        // Correct answer for Question 1 is "Nitrogen" (index 1)
        new string[] {"1. What is the most abundant gas in the Earth's atmosphere?", "Oxygen", "Nitrogen", "Carbon Dioxide", "Hydrogen", "2"},
        // Correct answer for Question 2 is "Oxygen" (index 1)
        new string[] {"2. What element does 'O' represent on the periodic table?", "Gold", "Oxygen", "Osmium", "Iron", "2"},
        new string[] {"3. What element has the chemical symbol 'Fe'?", "Iron", "Fluorine", "Fermium", "Francium", "1"}, 
        new string[] {"4. Which of the following is a noble gas?", "Oxygen", "Nitrogen", "Argon", "Hydrogen", "3"}, 
        new string[] {"5. What is the pH level of pure water at room temperature?", "7", "1", "14", "5", "1"}
    };
    private int currentQuestionIndex = 0;
    private int score = 0;

    void Start()
    {
        // Assign button listeners
        for (int i = 0; i < answerButtons.Length; i++)
        {
            int index = i;
            answerButtons[i].onClick.AddListener(() => CheckAnswer(index));
        }
        
        // Initialize the quiz
        nextButton.onClick.AddListener(NextQuestion);
        nextButton.gameObject.SetActive(false); // Hide the next button initially
        tryAgainButton.onClick.AddListener(TryAgain); // Add listener for try again button
        tryAgainButton.gameObject.SetActive(false); // Hide the try again button initially
        ShowQuestion();
    }

    void ShowQuestion()
    {
        string[] questionData = questions[currentQuestionIndex];
        questionText.text = questionData[0];
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<Text>().text = questionData[i + 1];
            answerButtons[i].interactable = true; // Enable buttons
        }
        feedbackText.text = "";
        questionPanel.SetActive(true); // Show the question panel
        scoreText.text = ""; // Clear score text initially
    }

    void CheckAnswer(int index)
    {
        string[] questionData = questions[currentQuestionIndex];
        int correctAnswerIndex = int.Parse(questionData[5]) - 1;
        if (index == correctAnswerIndex)
        {
            feedbackText.text = "Correct!";
            score++;
        }
        else
        {
            feedbackText.text = $"Wrong! The correct answer is {questionData[correctAnswerIndex + 1]}";
        }
        scoreText.text = $"Score: {score}/5"; // Show score after user selects an answer
        nextButton.gameObject.SetActive(true); // Show the next button
        
        // Disable answer buttons
        foreach (Button button in answerButtons)
        {
            button.interactable = false;
        }
    }

    void NextQuestion()
    {
        currentQuestionIndex++;
        if (currentQuestionIndex < questions.Length)
        {
            questionPanel.SetActive(false); // Hide the question panel
            Invoke("ShowNextQuestion", 1f); // Add a small delay before showing the next question
        }
        else
        {
            feedbackText.text = $"Quiz Finished!"; // Center the feedback text
            feedbackText.alignment = TextAnchor.MiddleCenter; // Center the text
            tryAgainButton.gameObject.SetActive(true); // Show the try again button
            nextButton.gameObject.SetActive(false); // Hide the next button at the end
        }
    }

    void ShowNextQuestion()
    {
        questionPanel.SetActive(true); // Show the question panel for the next question
        ShowQuestion();
        nextButton.gameObject.SetActive(false); // Hide the next button
    }

    void TryAgain()
    {
        currentQuestionIndex = 0;
        score = 0;
        feedbackText.text = "";
        scoreText.text = ""; // Clear score text initially
        tryAgainButton.gameObject.SetActive(false); // Hide the try again button
        ShowQuestion();
    }
}


