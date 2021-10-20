using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Camera Transitions/Behaviour Transition", fileName = "Behaviour Transition")]
public class BehaviourTransition : CameraBehaviour {
    
    [SerializeField] private float closestDistance;
    
    private Vector3 fromPosition;
    private Vector3 toPosition;
    private Transform transform;
    
    public void AssignFromAndTo(Transform whoToMove, Vector3 from, Vector3 to) {
        transform = whoToMove;
        fromPosition = from;
        toPosition = to;
    }
    
    public override async Task BehaveAsync() {

        float currentDistance = Vector3.Distance(fromPosition, toPosition);

        while (closestDistance < currentDistance) {
            transform.position = Vector3.SmoothDamp(transform.position, toPosition, ref fromPosition, .1f, 10, Time.deltaTime);
            
            currentDistance = Vector3.Distance(transform.position, toPosition);
            await Task.Yield();
        }
    }
    
    
    
}
