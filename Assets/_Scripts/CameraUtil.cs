using UnityEngine;
using UnityEngine.InputSystem;

public class CameraUtil : MonoBehaviour
{
    private Camera _camera;
    
    private void Awake()
    {
        _camera = Camera.main;
    }
    
    public void ClampPlayerMovement()
    {
        Vector3 position = GameObject.Find("Player").transform.position; 
        
        float distance = GameObject.Find("Player").transform.position.z - _camera.transform.position.z;

        float leftBorder = _camera.ViewportToWorldPoint(new Vector3(0, 0, distance)).x + 0.45f; 
        float rightBorder = _camera.ViewportToWorldPoint(new Vector3(1, 0, distance)).x - 0.45f;
        
        position.x = Mathf.Clamp(position.x, leftBorder, rightBorder);
        GameObject.Find("Player").transform.position = position;
    }
}