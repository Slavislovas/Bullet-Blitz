using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 5f;
    public float spawnDistanceFromCamera = 10f;
    public Tilemap collidableTilemap;
    public int mapHeight;
    public int mapWidth;

    private float timeSinceLastSpawn = 0f;

    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnEnemy();
            timeSinceLastSpawn = 0f;
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPos = GetRandomSpawnPosition();

        // Adjust the spawn position to be outside the camera view
        while (IsInsideCameraView(spawnPos))
        {
             spawnPos += (spawnPos - Camera.main.transform.position).normalized * spawnDistanceFromCamera;
        }

        // Check if spawn position is on a collidable tile
        if (collidableTilemap.GetTile(collidableTilemap.WorldToCell(spawnPos)))
        {
            // If so, shift the spawn position until it's not colliding
            while (collidableTilemap.GetTile(collidableTilemap.WorldToCell(spawnPos)))
            {
                spawnPos += (spawnPos - Camera.main.transform.position).normalized * spawnDistanceFromCamera;
            }
        }

        // Spawn the enemy prefab at the adjusted spawn position
        if (IsInsideMapBounds(spawnPos))
        {
            spawnPos.z = 0;
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        }
    }

    private bool IsInsideMapBounds(Vector3 spawnPos)
    {
        return spawnPos.x > 0 && spawnPos.x < mapWidth && spawnPos.y > 0 && spawnPos.y < mapHeight;
    }

    private bool IsInsideCameraView(Vector3 position)
    {
        float cameraHeight = 2f * Camera.main.orthographicSize;
        float cameraWidth = cameraHeight * Camera.main.aspect;

        Vector3 cameraPos = Camera.main.transform.position;

        float distanceX = Mathf.Abs(cameraPos.x - position.x);
        float distanceY = Mathf.Abs(cameraPos.y - position.y);

        return distanceX < cameraWidth / 2f && distanceY < cameraHeight / 2f;
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float cameraHeight = 2f * Camera.main.orthographicSize;
        float cameraWidth = cameraHeight * Camera.main.aspect;

        Vector3 cameraPos = Camera.main.transform.position;

        // Calculate a random position on the edge of the camera view that is at least spawnDistanceFromCamera units away from the camera
        Vector3 randomEdgePos = new Vector3(
            cameraPos.x + Random.Range(-cameraWidth / 2f, cameraWidth / 2f),
            cameraPos.y + Random.Range(-cameraHeight / 2f, cameraHeight / 2f),
            0f);

        Vector3 towardsCamera = (cameraPos - randomEdgePos).normalized;

        // Calculate the minimum distance from the camera that the enemy can spawn
        float minDistanceFromCamera = spawnDistanceFromCamera + enemyPrefab.GetComponent<Renderer>().bounds.extents.magnitude;

        // Calculate the spawn position by moving it in the direction away from the camera
        Vector3 spawnPos = randomEdgePos + new Vector3(towardsCamera.x, towardsCamera.y, 0f) * minDistanceFromCamera;

        return spawnPos;
    }
}