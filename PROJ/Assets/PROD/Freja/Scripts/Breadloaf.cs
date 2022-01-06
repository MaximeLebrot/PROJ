using UnityEngine;

public class Breadloaf : MonoBehaviour
{
    [SerializeField] private Breadcrumb[] breadcrumbs;
    [SerializeField] private Breadcrumb currentCrumb;
    [SerializeField] private Breadcrumb nextCrumb;
    [SerializeField] private FMOD.Studio.EventInstance currentAudioSource;
    [SerializeField] private FMOD.Studio.EventInstance nextAudioSource;
    private bool CASplaying;

    private FMOD.Studio.EventInstance Breadcrumbs;

    private void OnEnable()
    {
        breadcrumbs = GetComponentsInChildren<Breadcrumb>();
        foreach (Breadcrumb crumb in breadcrumbs)
            crumb.parent = this;
        nextCrumb = breadcrumbs[0];
        nextAudioSource = FMODUnity.RuntimeManager.CreateInstance("event:/Blind/NextSound");
        nextAudioSource.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(nextCrumb.transform.position));
        nextAudioSource.start();
        CASplaying = false;

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
            currentAudioSource = FMODUnity.RuntimeManager.CreateInstance("event:/Blind/CurrentSound");
            currentAudioSource.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
            currentAudioSource.start();
            currentAudioSource.release();
            CASplaying = true;
        }
        currentAudioSource.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(currentCrumb.transform.position));
        nextAudioSource.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(nextCrumb.transform.position));
    }

    public void EndAudioSources()
    {
        currentAudioSource.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        currentAudioSource.release();
        nextAudioSource.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        nextAudioSource.release();
    }

}
