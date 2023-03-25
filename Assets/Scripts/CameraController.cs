using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform playerTransform;
    public float smoothTime = 0.3f;
    public float maxSpeed = 10f;

    public Camera cameraComponent;
    private Vector3 cameraOffset;
    private float cameraHalfWidth;
    private float cameraHalfHeight;
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        MapGenerator mapGenerator = GameObject.FindGameObjectWithTag("MapGenerator").GetComponent<MapGenerator>();
        cameraHalfHeight = cameraComponent.orthographicSize;
        cameraHalfWidth = cameraHalfHeight * cameraComponent.aspect;

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // Calculate the min and max bounds of the camera
        minX = transform.position.x + cameraHalfWidth;
        maxX = transform.position.x + mapGenerator.mapWidth - cameraHalfWidth;
        minY = transform.position.y + cameraHalfHeight;
        maxY = transform.position.y + mapGenerator.mapHeight - cameraHalfHeight;

        // Calculate the initial offset between the camera and player
        cameraOffset = transform.position - playerTransform.position;
    }

    private void LateUpdate()
    {
        Vector3 playerPosition = playerTransform.transform.position;
        playerPosition.z = transform.position.z;

        // Clamp the camera position to the bounds of the map
        playerPosition.x = Mathf.Clamp(playerPosition.x, minX, maxX);
        playerPosition.y = Mathf.Clamp(playerPosition.y, minY, maxY);

        // Smoothly move the camera towards the player's position
        transform.position = Vector3.SmoothDamp(transform.position, playerPosition, ref velocity, smoothTime);
    }
}
