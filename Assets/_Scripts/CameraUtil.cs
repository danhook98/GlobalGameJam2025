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

    public static float GetScreenBottomY(Camera camera)
    {
        float position = camera.ViewportToWorldPoint(new Vector2(0, 0)).y;
        return position;
    }

    public static float GetScreenLeftX(Camera camera)
    {
        float position = camera.ViewportToWorldPoint(new Vector2(0, 0)).x;
        return position;
    }

    public static float GetScreenRightX(Camera camera)
    {
        float position = camera.ViewportToWorldPoint(new Vector2(1, 0)).x;
        return position;
    }
}