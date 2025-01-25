using UnityEngine;
using UnityEngine.InputSystem;

public class CameraUtil : MonoBehaviour
{
    private static Camera _camera;
    
    private void Awake()
    {
        _camera = Camera.main;
    }
    
    public static Vector3 ClampPlayerMovement(Transform playerTransform)
    {
        Vector3 position = playerTransform.position;
        
        float distance = position.z - _camera.transform.position.z;

        float leftBorder = _camera.ViewportToWorldPoint(new Vector3(0, 0, distance)).x + 0.45f; 
        float rightBorder = _camera.ViewportToWorldPoint(new Vector3(1, 0, distance)).x - 0.45f;
        
        position.x = Mathf.Clamp(position.x, leftBorder, rightBorder);

        return position; 
    }
}