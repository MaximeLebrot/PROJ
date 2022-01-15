using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Game Camera/Transition Camera", fileName = "Transition Camera")]
public class TransitionCamera : GameCamera {
    
    private CancellationTokenSource cancellationTokenSource;
    
    public override void Initialize(Transform transform, Transform pivotTarget, Transform character) {
        base.Initialize(transform, pivotTarget, character);
        cancellationTokenSource = new CancellationTokenSource();
    }

    public override void ExecuteCameraBehaviour() {}
    
    
    public async Task PlayTransition<T>(CameraTransition<T> transition) where T : TransitionData {
        await transition.RunTransition(Transform, cancellationTokenSource.Token);
    }
    
}