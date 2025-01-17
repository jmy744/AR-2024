using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DynamicQuizManager : MonoBehaviour
{
    public Text questionText; // Text field for displaying the question
    public Button[] answerButtons; // Array of answer buttons
    public Text feedbackText; // Text for feedback (Correct/Incorrect)
    public Text scoreText; // Text to display the score

    private int currentQuestionIndex = 0; // Track the current question index
    private int score = 0; // Track the user's score

    // A list to store the questions and their answers
    private List<Question> questions = new List<Question>();

    // Start is called before the first frame update
    void Start()
    {
        // Initialize questions
        questions.Add(new Question("What is the capital of France?", "Paris", new string[] { "London", "Berlin", "Rome" }));
        questions.Add(new Question("What is 2 + 2?", "4", new string[] { "3", "4", "5", "6" }));
        questions.Add(new Question("Which animal is known as the king of the jungle?", "Lion", new string[] { "Tiger", "Lion", "Elephant", "Giraffe" }));

        // Show the first question
        ShowQuestion();
    }

    // Method to display the current question
    void ShowQuestion()
    {
        if (currentQuestionIndex < questions.Count)
        {
            Question currentQuestion = questions[currentQuestionIndex];
            questionText.text = currentQuestion.question;

            // Shuffle and assign answers to buttons
            List<string> answersList = new List<string>(currentQuestion.answers);
            answersList.Add(currentQuestion.correctAnswer);
            answersList = ShuffleList(answersList); // Shuffle answers

            // Assign the answers to buttons
            for (int i = 0; i < answerButtons.Length; i++)
            {
                answerButtons[i].GetComponentInChildren<Text>().text = answersList[i];
                string answer = answersList[i]; // Capture the answer string in the correct scope
                answerButtons[i].onClick.RemoveAllListeners();
                answerButtons[i].onClick.AddListener(() => CheckAnswer(answer));
            }

            feedbackText.text = ""; // Clear previous feedback
        }
        else
        {
            feedbackText.text = "Quiz Complete! Final Score: " + score;
            scoreText.text = "Score: " + score;
        }
    }

    // Method to check if the selected answer is correct
    void CheckAnswer(string selectedAnswer)
    {
        Question currentQuestion = questions[currentQuestionIndex];

        if (selectedAnswer == currentQuestion.correctAnswer)
        {
            feedbackText.text = "Correct!";
            score++; // Increase score for correct answer
        }
        else
        {
            feedbackText.text = "Incorrect! The correct answer is: " + currentQuestion.correctAnswer;
        }

        // Update score text
        scoreText.text = "Score: " + score;

        // Move to the next question
        currentQuestionIndex++;
        Invoke("ShowQuestion", 1f); // Wait 1 second before showing the next question
    }

    // Shuffle list (Fisher-Yates algorithm)
    List<T> ShuffleList<T>(List<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
        return list;
    }
}

// Class to store question data
[System.Serializable]
public class Question
{
    public string question;
    public string correctAnswer;
    public string[] answers;

    public Question(string q, string correct, string[] ans)
    {
        question = q;
        correctAnswer = correct;
        answers = ans;
    }
}
