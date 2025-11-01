using UnityEngine;

public class Pelota : SelectableObject
{
    private Rigidbody rb;
    private Vector3 initialPosition;
    [SerializeField] private AudioClip bounceSound;
    [SerializeField] private string mixerOutput = "SFX";
    [SerializeField] private float velocityThreshold = 2f;
    [SerializeField] private float minImpactVolume = 0.3f;
    [SerializeField] private float maxImpactVolume = 1f;
    [SerializeField] private float maxVelocityForVolume = 20f;
    private float bounceCooldown = 0f;
    private float bounceCooldownDuration = 0.2f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (bounceCooldown > 0f)
        {
            bounceCooldown -= Time.fixedDeltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (rb == null  || bounceCooldown > 0f)
            return;

        float impactVelocity = rb.linearVelocity.magnitude;

        if (impactVelocity > velocityThreshold)
        {
            PlayBounceSound(impactVelocity);
            bounceCooldown = bounceCooldownDuration;
        }
    }

    private void PlayBounceSound(float impactVelocity)
    {
        if (bounceSound != null)
        {
            float normalizedVelocity = Mathf.Clamp01(impactVelocity / maxVelocityForVolume);
            float volume = Mathf.Lerp(minImpactVolume, maxImpactVolume, normalizedVelocity);

            AudioManager.GetInstance().PlaySoundAtDistance(bounceSound, transform.position, 10f, mixerOutput, false);
            AudioManager.GetInstance().SetLastAudioVolume(volume);
        }
    }

    public override void Interactuar()
    {
        base.Interactuar();
        Debug.Log("Pelota interactuada");
    }

    public override void OnDragStart()
    {
        base.OnDragStart();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
        }
    }

    public override void OnDragEnd()
    {
        base.OnDragEnd();
        Debug.Log("Pelota soltada");
    }
}
