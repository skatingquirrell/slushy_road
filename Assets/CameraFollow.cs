using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float xOffset = 0f; // Horizontal offset between the player and camera

    private float leftViewBound; // Left boundary of the game view
    private float rightViewBound; // Right boundary of the game view

    private float leftBound; // Left boundary of the ground
    private float rightBound; // Right boundary of the ground

    private void Start()
    {
        // Calculate the left and right boundaries of the game view
        Camera mainCamera = Camera.main;
        float halfWidth = mainCamera.orthographicSize * mainCamera.aspect;
        leftViewBound = halfWidth - mainCamera.transform.position.x;
        rightViewBound = mainCamera.transform.position.x + halfWidth;

        // Find the Ground object and get its BoxCollider component
        GameObject groundObject = GameObject.Find("LevelColliders/LeftRoadBarrierCollider");
        BoxCollider groundCollider = groundObject.GetComponent<BoxCollider>();

        // Calculate the left and right boundaries based on the ground's bounds
        leftBound = groundCollider.bounds.min.x;
        rightBound = groundCollider.bounds.max.x;
    }

    private void LateUpdate()
    {
        // Move the camera horizontally to follow the player's movement if they go out of the game view
        if (player.position.x < leftViewBound || player.position.x > rightViewBound ||
            player.position.x < leftBound || player.position.x > rightBound)
        {
            transform.position = new Vector3(player.position.x + xOffset, transform.position.y, transform.position.z);
        }
    }

    private void OnDrawGizmos()
    {
        // Draw the left and right bounds as red lines in the scene view
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(leftViewBound, transform.position.y, transform.position.z), new Vector3(leftViewBound, transform.position.y + 10f, transform.position.z));
        Gizmos.DrawLine(new Vector3(rightViewBound, transform.position.y, transform.position.z), new Vector3(rightViewBound, transform.position.y + 10f, transform.position.z));
    }
}
