using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioMixerSnapshot pasillo;
    [SerializeField] private AudioMixerSnapshot pausa;
    [SerializeField] private float transitionDuration = 0.5f;

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

    public void SetTransitionDuration(float duration)
    {
        transitionDuration = duration;
    }

    public static AudioManager GetInstance()
    {
        return instance;
    }
}
