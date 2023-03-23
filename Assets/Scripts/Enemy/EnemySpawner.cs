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
        spawnPos.z = 0;
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }

    private Vector3 GetRandomSpawnPosition()
    {

        float cameraHeight = 2f * Camera.main.orthographicSize;
        float cameraWidth = cameraHeight * Camera.main.aspect;

        Vector3 cameraPos = Camera.main.transform.position;

        // Calculate a random position on the edge of the camera view
        Vector3 randomEdgePos = new Vector3(
            cameraPos.x + Random.Range(-cameraWidth / 2f, cameraWidth / 2f),
            cameraPos.y + Random.Range(-cameraHeight / 2f, cameraHeight / 2f),
            0f);

        // Calculate the direction towards the camera
        Vector3 towardsCamera = (cameraPos - randomEdgePos).normalized;

        // Calculate the spawn position by moving it in the direction away from the camera
        Vector3 spawnPos = randomEdgePos + new Vector3(towardsCamera.x, towardsCamera.y, 0f) * spawnDistanceFromCamera;
        spawnPos.z = 0;

        return spawnPos;
    }
}