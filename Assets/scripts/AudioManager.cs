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
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Slider uiVolumeSlider;
    [SerializeField] private GameObject pauseUIPanel;
    [SerializeField] private AudioSource audioSourcePrefab;
    private static AudioManager instance;
    private AudioSource lastAudioSource;

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
            masterVolumeSlider.value = 1f;
        }
        else
        {
            Debug.LogWarning("Master Volume Slider not assigned to AudioManager!");
        }

        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
            musicVolumeSlider.value = 1f;
        }
        else
        {
            Debug.LogWarning("Music Volume Slider not assigned to AudioManager!");
        }

        if (sfxVolumeSlider != null)
        {
            sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
            sfxVolumeSlider.value = 1f;
        }
        
        else
        {
            Debug.LogWarning("SFX Volume Slider not assigned to AudioManager!");
        }
        if (uiVolumeSlider != null)
        {
            uiVolumeSlider.onValueChanged.AddListener(OnUIVolumeChanged);
            uiVolumeSlider.value = 1f;
        }
    }

    private void OnMasterVolumeChanged(float value)
    {
        if (audioMixer != null)
        {
            audioMixer.SetFloat("VolumeMaster", value != 0 ? Mathf.Log10(value) * 20 : -80f);
        }
    }

    private void OnMusicVolumeChanged(float value)
    {
        if (audioMixer != null)
        {
            audioMixer.SetFloat("VolumeMusic", value != 0 ? Mathf.Log10(value) * 20 : -80f);
        }
    }

    private void OnSFXVolumeChanged(float value)
    {
        if (audioMixer != null)
        {
            audioMixer.SetFloat("VolumeSFX", value != 0 ? Mathf.Log10(value) * 20 : -80f);
        }
    }

    private void OnUIVolumeChanged(float value)
    {
        if (audioMixer != null)
        {
            audioMixer.SetFloat("VolumeUI", value != 0 ? Mathf.Log10(value) * 20 : -80f);
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

    public void SetLastAudioVolume(float volume)
    {
        if (lastAudioSource != null)
        {
            lastAudioSource.volume = Mathf.Clamp01(volume);
        }
    }

    public void PlaySoundAt(AudioClip clip, Vector3 position, string mixerOutput = "Master", bool isUI = false)
    {
        if (audioSourcePrefab == null)
        {
            Debug.LogError("AudioSource prefab not assigned to AudioManager!");
            return;
        }

        if (clip == null)
        {
            Debug.LogWarning("AudioClip is null!");
            return;
        }

        AudioSource audioSource = Instantiate(audioSourcePrefab, position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.spatialBlend = isUI ? 0f : 1f;

        if (audioMixer != null)
        {
            AudioMixerGroup mixerGroup = audioMixer.FindMatchingGroups(mixerOutput).Length > 0 
                ? audioMixer.FindMatchingGroups(mixerOutput)[0] 
                : null;
            
            if (mixerGroup != null)
            {
                audioSource.outputAudioMixerGroup = mixerGroup;
            }
            else
            {
                Debug.LogWarning("Mixer output '" + mixerOutput + "' not found in AudioMixer!");
            }
        }

        lastAudioSource = audioSource;
        audioSource.Play();
        Destroy(audioSource.gameObject, clip.length);
    }

    public void PlaySoundAtDistance(AudioClip clip, Vector3 position, float maxDistance, string mixerOutput = "Master", bool isUI = false)
    {
        if (audioSourcePrefab == null)
        {
            Debug.LogError("AudioSource prefab not assigned to AudioManager!");
            return;
        }

        if (clip == null)
        {
            Debug.LogWarning("AudioClip is null!");
            return;
        }

        AudioSource audioSource = Instantiate(audioSourcePrefab, position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.spatialBlend = isUI ? 0f : 1f;
        audioSource.maxDistance = maxDistance;

        if (audioMixer != null)
        {
            AudioMixerGroup mixerGroup = audioMixer.FindMatchingGroups(mixerOutput).Length > 0 
                ? audioMixer.FindMatchingGroups(mixerOutput)[0] 
                : null;
            
            if (mixerGroup != null)
            {
                audioSource.outputAudioMixerGroup = mixerGroup;
            }
            else
            {
                Debug.LogWarning("Mixer output '" + mixerOutput + "' not found in AudioMixer!");
            }
        }

        lastAudioSource = audioSource;
        audioSource.Play();
        Destroy(audioSource.gameObject, clip.length);
    }
}
