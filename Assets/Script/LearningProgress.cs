using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LearningProgress : MonoBehaviour
{
    public Slider progressBar;
    public Text progressText;

    private static LearningProgress instance;
    private int maxSceneIndex;
    private int currentProgressIndex;

    void Awake()
    {
        // Singleton pattern: Ensure only one instance exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeProgress();
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void InitializeProgress()
    {
        // Set the maximum scene index based on the total number of scenes in Build Settings
        maxSceneIndex = SceneManager.sceneCountInBuildSettings-1;

        // Load saved progress from PlayerPrefs, defaulting to 0 (first scene)
        currentProgressIndex = PlayerPrefs.GetInt("MaxVisitedSceneIndex", 0);

        // Ensure the loaded progress does not exceed the maximum scene index
        currentProgressIndex = Mathf.Clamp(currentProgressIndex, 0, maxSceneIndex);

        Debug.Log($"Initialized Progress: Current Scene Index: {SceneManager.GetActiveScene().buildIndex}, Max Saved Index: {currentProgressIndex}");
        UpdateProgressBar();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Update the maximum scene index visited if moving forward
        if (currentSceneIndex > currentProgressIndex)
        {
            currentProgressIndex = currentSceneIndex;
            SaveProgress();
        }

        // Update the progress bar UI
        UpdateProgressBar();
    }

    private void UpdateProgressBar()
    {
        // Calculate progress based on the highest visited scene index
        float progress = (float)currentProgressIndex / maxSceneIndex;

        // Clamp progress value between 0 and 1
        progress = Mathf.Clamp01(progress);

        // Update the UI elements
        if (progressBar != null)
        {
            progressBar.value = progress;
        }
        if (progressText != null)
        {
            progressText.text = Mathf.RoundToInt(progress * 100) + "%";
        }

        Debug.Log($"Progress Updated: Current Scene Index: {currentProgressIndex}, Progress: {progress * 100}%");
    }

    private void SaveProgress()
    {
        PlayerPrefs.SetInt("MaxVisitedSceneIndex", currentProgressIndex);
        PlayerPrefs.Save();
        Debug.Log($"Progress Saved: MaxVisitedSceneIndex = {currentProgressIndex}");
    }

    public void ResetProgress()
    {
        // Reset progress to the first scene and update PlayerPrefs
        currentProgressIndex = 0;
        SaveProgress();
        UpdateProgressBar();
        Debug.Log("Progress Reset");
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
