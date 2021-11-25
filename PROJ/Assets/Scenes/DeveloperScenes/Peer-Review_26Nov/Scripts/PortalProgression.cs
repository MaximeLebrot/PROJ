using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalProgression : MonoBehaviour
{
    [SerializeField] private string portalToScene;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            SceneManager.LoadScene(portalToScene);
    }
}
