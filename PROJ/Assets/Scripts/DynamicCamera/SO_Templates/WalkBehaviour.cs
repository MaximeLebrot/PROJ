using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Camera Behaviours/Walk Behaviour", fileName = "Walk Behaviour")]
public class WalkBehaviour : MovementBehaviour {

    private void CastWhisker() {

        Debug.DrawRay(followTarget.position, transform.position- followTarget.position, Color.green);

        Vector3 direction1 = transform.rotation * new Vector3(Mathf.Cos(25 * Mathf.Deg2Rad), 0, Mathf.Sin(25 * Mathf.Deg2Rad));
        Vector3 direction2 = transform.rotation * new Vector3(Mathf.Cos(45 * Mathf.Deg2Rad), 0, Mathf.Sin(45 * Mathf.Deg2Rad));

        Debug.DrawRay(followTarget.position, (transform.position- followTarget.position) + direction1, Color.red);
        Debug.DrawRay(followTarget.position, (transform.position- followTarget.position) + direction2, Color.yellow);
    }
}
