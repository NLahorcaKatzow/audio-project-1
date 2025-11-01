using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioMixerSnapshot pasillo;
    [SerializeField] private AudioMixerSnapshot pausa;
    [SerializeField] private float transitionDuration = 0.5f;
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private GameObject pauseUIPanel;
    private static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (audioMixer == null)
        {
            Debug.LogError("AudioMixer not assigned to AudioManager!");
        }
        if (pasillo == null)
        {
            Debug.LogError("Pasillo snapshot not assigned to AudioManager!");
        }
        if (pausa == null)
        {
            Debug.LogError("Pausa snapshot not assigned to AudioManager!");
        }

        if (masterVolumeSlider != null)
        {
            masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
            masterVolumeSlider.value = 0f;
        }
        else
        {
            Debug.LogWarning("Master Volume Slider not assigned to AudioManager!");
        }
    }

    private void OnMasterVolumeChanged(float value)
    {
        if (audioMixer != null)
        {
            audioMixer.SetFloat("VolumeMaster", Mathf.Log10(value) * 20);
        }
    }

    public void EnterUI()
    {
        if (pausa == null)
        {
            Debug.LogError("Pausa snapshot is not assigned!");
            return;
        }

        pausa.TransitionTo(transitionDuration);
        Debug.Log("Entering UI - Switched to Pausa snapshot");
    }

    public void ExitUI()
    {
        if (pasillo == null)
        {
            Debug.LogError("Pasillo snapshot is not assigned!");
            return;
        }

        pasillo.TransitionTo(transitionDuration);
        Debug.Log("Exiting UI - Switched to Pasillo snapshot");
    }
    
    public void TogglePauseUI()
    {
        pauseUIPanel.SetActive(!pauseUIPanel.activeSelf);
    }
    
    public bool IsPauseUIActive()
    {
        return pauseUIPanel.activeSelf;
    }

    public void SetTransitionDuration(float duration)
    {
        transitionDuration = duration;
    }

    public static AudioManager GetInstance()
    {
        return instance;
    }
}
