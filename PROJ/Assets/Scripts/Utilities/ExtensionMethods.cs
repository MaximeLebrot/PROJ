using UnityEngine;

public static class ExtensionMethods  {

    public static Vector3 Clamp(this Vector3 target, float min, float max) {
        
        float xClamped = Mathf.Clamp(target.x, min, max);
        float yClamped = Mathf.Clamp(target.y, min, max);
        float zClamped = Mathf.Clamp(target.z, min, max);

        return new Vector3(xClamped, yClamped, zClamped);
    }
    
}
