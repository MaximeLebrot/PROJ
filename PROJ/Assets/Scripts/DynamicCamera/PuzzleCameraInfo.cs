using UnityEngine;

public class PuzzleCameraInfo : MonoBehaviour {

    [SerializeField] private Transform cameraPosition;

    public delegate void StartPuzzle(Transform cameraPosition);

    public static event StartPuzzle PuzzleInit;


    private void OnTriggerStay(Collider other) {
        if(other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
            PuzzleInit?.Invoke(cameraPosition);
    }
}
