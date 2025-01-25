using UnityEngine;
using UnityEngine.InputSystem;

public class CameraUtil : MonoBehaviour
{
    public static Vector3 ClampPlayerMovement(Transform playerTransform, Camera camera)
    {
        Vector3 position = playerTransform.position;
        
        float distance = position.z - camera.transform.position.z;

        float leftBorder = camera.ViewportToWorldPoint(new Vector3(0, 0, distance)).x + 0.45f; 
        float rightBorder = camera.ViewportToWorldPoint(new Vector3(1, 0, distance)).x - 0.45f;
        
        position.x = Mathf.Clamp(position.x, leftBorder, rightBorder);

        return position; 
    }
}