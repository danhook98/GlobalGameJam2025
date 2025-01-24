using UnityEngine;
using UnityEngine.InputSystem;

public class CameraUtil : MonoBehaviour
{
    //private float _halfPlayerSizeX;
    private Camera _camera;
    [SerializeField] private PlayerController playerControllerScript;
    
    private void Awake()
    {
        _camera = Camera.main;
    }
    
    public void ClampPlayerMovement()
    {
        Vector3 position = GameObject.Find("Player").transform.position;  //transform.position;
        
        float distance = GameObject.Find("Player").transform.position.z - _camera.transform.position.z;

        float leftBorder = _camera.ViewportToWorldPoint(new Vector3(0, 0, distance)).x + playerControllerScript._halfPlayerSizeX;
        float rightBorder = _camera.ViewportToWorldPoint(new Vector3(1, 0, distance)).x - playerControllerScript._halfPlayerSizeX;
        
        position.x = Mathf.Clamp(position.x, leftBorder, rightBorder);
        GameObject.Find("Player").transform.position = position;
    }
}
