using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseUIPanel;
    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            OpenPauseUI();
        }
        else
        {
            ClosePauseUI();
        }
    }

    public void OpenPauseUI()
    {
        isPaused = true;

        if (pauseUIPanel != null)
        {
            pauseUIPanel.SetActive(true);
        }

        Time.timeScale = 0f;
        AudioManager.GetInstance().EnterUI();

        Debug.Log("Pause UI opened");
    }

    public void ClosePauseUI()
    {
        isPaused = false;

        if (pauseUIPanel != null)
        {
            pauseUIPanel.SetActive(false);
        }

        Time.timeScale = 1f;
        AudioManager.GetInstance().ExitUI();

        Debug.Log("Pause UI closed");
    }

    public bool IsPaused()
    {
        return isPaused;
    }
}
