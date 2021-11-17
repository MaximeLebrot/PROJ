using NewCamera;

[System.Serializable]
public class OneSwitchCameraBehaviour : BaseCameraBehaviour
{
    public override void ManipulatePivotTarget(CustomInput input) {
        target.rotation = target.parent.rotation;
    }
}
