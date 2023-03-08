using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public int mapWidth = 50;
    public int mapHeight = 50;
    public float scale = 5f;
    public Tilemap tilemap;
    public Tilemap collidableTilemap;
    public Tile grassTile;
    public Tile wallTile;
    public float wallThreshold = 0.5f;
    public GameObject playerPrefab;

    void Start()
    {
        GenerateMap();
        SpawnPlayer();
    }

    void GenerateMap()
    {
        Random.InitState((int)Random.Range(-100000f, 100000f));
        float xOffset = Random.Range(-10000f, 10000f);
        float yOffset = Random.Range(-10000f, 10000f);
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                //float noiseValue = Mathf.PerlinNoise((float)x / scale, (float)y / scale);
                float noiseValue = Mathf.PerlinNoise((float)x / scale + xOffset, (float)y / scale + yOffset);
                Tile tile = noiseValue > wallThreshold ? wallTile : grassTile;
                tilemap.SetTile(new Vector3Int(x, y, 0), tile);
            }
        }
    }

    void SpawnPlayer()
    {
        // Create a grassy area for the player to spawn on
        int spawnAreaWidth = 10;
        int spawnAreaHeight = 10;
        Vector3Int spawnAreaOrigin = new Vector3Int(mapWidth / 2 - spawnAreaWidth / 2, mapHeight / 2 - spawnAreaHeight / 2, 0);
        for (int x = spawnAreaOrigin.x; x < spawnAreaOrigin.x + spawnAreaWidth; x++)
        {
            for (int y = spawnAreaOrigin.y; y < spawnAreaOrigin.y + spawnAreaHeight; y++)
            {
                Vector3Int tilePos = new Vector3Int(x, y, 0);
                tilemap.SetTile(tilePos, grassTile);
            }
        }

        // Set the player's spawn position to the center of the grassy area
        Vector3 center = tilemap.CellToWorld(spawnAreaOrigin + new Vector3Int(spawnAreaWidth / 2, spawnAreaHeight / 2, 0));

        // Instantiate the player prefab at the center of the grassy area
        GameObject player = Instantiate(playerPrefab, center, Quaternion.identity);
    }
}