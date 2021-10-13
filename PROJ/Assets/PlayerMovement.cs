using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] private float speed;
    
    private void Update() {
        Vector3 direction = UnityEngine.Camera.main.transform.right * Input.GetAxis("Horizontal") + UnityEngine.Camera.main.transform.forward * Input.GetAxis("Vertical");

        transform.Translate( direction * (speed * Time.deltaTime));

    }
}
