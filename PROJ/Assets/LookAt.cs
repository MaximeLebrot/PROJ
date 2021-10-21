using UnityEngine;

public class LookAt : MonoBehaviour
{


    // Update is called once per frame
    void Update() {
        Quaternion direction = Quaternion.LookRotation((Camera.main.transform.position - transform.position), Vector3.up);
        
        Vector3 euler = direction.eulerAngles;

        euler.x = 0;
        
        transform.eulerAngles = euler;

    }
}
