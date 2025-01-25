using UnityEngine;
using UnityEngine.InputSystem;

public class CameraUtil : MonoBehaviour
{
    public static Vector2 ClampPlayerMovement(Transform playerTransform, Camera camera)
    {
        Vector2 position = playerTransform.position;

        float leftBorder = camera.ViewportToWorldPoint(new Vector2(0, 0)).x + 0.45f; 
        float rightBorder = camera.ViewportToWorldPoint(new Vector2(1, 0)).x - 0.45f;
        
        position.x = Mathf.Clamp(position.x, leftBorder, rightBorder);

        return position; 
    }
}