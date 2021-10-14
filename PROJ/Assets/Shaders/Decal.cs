using UnityEngine;
using UnityEditor;

public class Decal : MonoBehaviour
{
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        //For testing only
        Handles.color = Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position + transform.forward * transform.localScale.z / 2, new Vector3(0, transform.localScale.y, transform.localScale.x));
        Gizmos.DrawWireCube(transform.position - transform.forward * transform.localScale.z / 2, new Vector3(0, transform.localScale.y, transform.localScale.x));
        Handles.DrawLine(transform.position, transform.position + transform.forward, 1);
        Handles.ConeHandleCap(0, transform.position + transform.forward, Quaternion.LookRotation(transform.forward, Vector3.up), .2f, EventType.Repaint);
    }
#endif
}
