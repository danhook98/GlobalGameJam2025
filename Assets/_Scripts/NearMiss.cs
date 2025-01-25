using UnityEngine;

public class NearMiss : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Debug.Log("Near Miss!");
        }
    }
}
