using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class MapController : MonoBehaviour
{

    public List<GameObject> terrainChunks;
    public GameObject player;
    public float checkerRadius;
    public LayerMask terrainMask;
    public GameObject currentChunk;
    //PlayerMovement pm;
    Vector3 playerLastPosition;

    [Header("Optimization")]
    public List<GameObject> spawnedChunks;
    GameObject latestChunk;
    public float maxOpDist; //Must be greater than the length and width of the tilemap
    float opDist;
    float optimizerCooldown;
    public float optimizerCooldownDur;
    // Start is called before the first frame update
    void Start()
    {
        //pm = FindObjectOfType<PlayerMovement>();
        playerLastPosition = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ChunkChecker();
        ChunkOptimizer();
    }

    void ChunkChecker()
    {
        if(!currentChunk)
        {
            return;

        }

        Vector3 moveDir = player.transform.position - playerLastPosition;
        playerLastPosition = player.transform.position;

        string directionName = GetDirectionName(moveDir);

        CheckAndSpawnChunk(directionName);

        // Check additional adjacent directions for diagonal chunks
        if (directionName.Contains("Up")) {
            CheckAndSpawnChunk("Up");
        }
        if (directionName.Contains("Down")) {
            CheckAndSpawnChunk("Down");
        }
        if (directionName.Contains("Left")) {
            CheckAndSpawnChunk("Left");
        }
        if (directionName.Contains("Right")) {
            CheckAndSpawnChunk("Right");
        }
    }

    void CheckAndSpawnChunk(string direction) {
        if (!Physics2D.OverlapCircle(currentChunk.transform.Find(direction).position, checkerRadius, terrainMask)) {
            SpawnChunk(currentChunk.transform.Find(direction).position);
        }
    }

    string GetDirectionName(Vector3 direction) {
        direction = direction.normalized;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) {
            //Moving horizontally more than vertically
            if (direction.y > 0.5f) {
                // Also moving upwards
                return direction.x > 0 ? "Right Up" : "Left Up";
            } else if (direction.y < -0.5f) {
                // Also moving downwards
                return direction.x > 0 ? "Right Down" : "Left Down";
            } else {
                // Moving straight horizontally
                return direction.x > 0 ? "Right" : "Left";
            }
        } else {
            if (direction.x > 0.5f) {
                return direction.y > 0 ? "Right Up" : "Right Down";
            } else if (direction.x < -0.5f) {
                return direction.y > 0 ? "Left Up" : "Left Down";
            } else {
                return direction.y > 0 ? "Up" : "Down";
            }
        }
    }

    void SpawnChunk(Vector3 spawnPosition)
    {
        int rand = Random.Range(0, terrainChunks.Count);
        latestChunk = Instantiate(terrainChunks[rand], spawnPosition, Quaternion.identity);
        spawnedChunks.Add(latestChunk);
    }
    void ChunkOptimizer()
    {
        optimizerCooldown -= Time.deltaTime;
        if (optimizerCooldown <= 0f)
        {
            optimizerCooldown = optimizerCooldownDur;
        }
        else
        {
            return;
        }
        foreach (GameObject chunk in spawnedChunks)
        {
            opDist = Vector3.Distance(player.transform.position, chunk.transform.position);
            if(opDist > maxOpDist)
            {
                chunk.SetActive(false);
            }
            else
            {
                chunk.SetActive(true);
            }
        }
    }
}
