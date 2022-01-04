using UnityEngine;

public class Breadloaf : MonoBehaviour
{
    [SerializeField] private Breadcrumb[] breadcrumbs;
    [SerializeField] private Breadcrumb currentCrumb;
    [SerializeField] private Breadcrumb nextCrumb;
    [SerializeField] private AudioSource currentAudioSource;
    [SerializeField] private AudioSource nextAudioSource;
    private AudioSource[] audioSources;
    private bool CASplaying;

    private void Start()
    {
        audioSources = GetComponentsInChildren<AudioSource>();
        currentAudioSource = audioSources[0];
        nextAudioSource = audioSources[1];
        nextAudioSource.Play();
        breadcrumbs = GetComponentsInChildren<Breadcrumb>();
        foreach (Breadcrumb crumb in breadcrumbs)
            crumb.parent = this;
        nextCrumb = breadcrumbs[0];
        nextAudioSource.transform.position = nextCrumb.transform.position;
    }

    public void UpdateCrumbStep(Breadcrumb crumb)
    {
        currentCrumb = crumb;
        for (int i = 0; i < breadcrumbs.Length; i++)
        {
            if (breadcrumbs[i] == currentCrumb)
                nextCrumb = breadcrumbs[i + 1];
        }
        UpdateAudioSources();
    }

    private void UpdateAudioSources()
    {
        if (!CASplaying)
        {
            currentAudioSource.Play();
            CASplaying = true;
        }
        currentAudioSource.transform.position = currentCrumb.transform.position;
        nextAudioSource.transform.position = nextCrumb.transform.position;
    }

}
