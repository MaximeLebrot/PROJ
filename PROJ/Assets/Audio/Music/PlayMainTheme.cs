using UnityEngine;

public class PlayMainTheme : MonoBehaviour
{
    [SerializeField] private AudioClip theme;
    [SerializeField] private AudioSource source;
    private void Awake()
    {
        if (source == null)
            source = GetComponent<AudioSource>();
    }

    public void PlayTheme()
    {
        source.PlayOneShot(theme);
    }
}
