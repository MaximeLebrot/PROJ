using UnityEngine;

[CreateAssetMenu(menuName = "Dynamic Camera/Camera Behaviour/Transition", fileName = "CameraTransition")]
public class CameraTransition : ScriptableObject {

    [SerializeField] private AnimationCurve path;
    
}
