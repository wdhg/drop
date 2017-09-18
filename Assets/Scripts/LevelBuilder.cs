using UnityEngine;

public class LevelBuilder : MonoBehaviour {

    public GameObject[] blocks;
    public float spawnRate;
    public Transform level;
    public PlayerController playerController;
    [Range(0f, 1f)]
    public float damagingPlatformChance;
    [Tooltip("In seconds")]
    public float maxDamagingSpawnRate;

    private float nextSpawnTime;
    private float nextDamagingSpawnTime;

    private void Start() {
        playerController.OnPlayerSpawn.AddListener(SpawnStartBlock);
        playerController.OnPlayerSpawn.AddListener(ResetDamagingSpawn);
        playerController.OnPlayerDeath.AddListener(DestroyLevel);
    }

    private void ResetDamagingSpawn() {
        nextDamagingSpawnTime = Time.time + maxDamagingSpawnRate;
    }

    private GameObject RandomBlock() {
        return blocks[Mathf.FloorToInt(Random.Range(0f, blocks.Length))];
    }

    private void Spawn() {
        GameObject newBlock = Instantiate(RandomBlock(), level);
        newBlock.transform.position = new Vector2(Random.Range(-Globals.maxXPos, Globals.maxXPos), -Globals.maxYPos);
        // Set it to damaging or not
        if(Time.time > nextDamagingSpawnTime && Random.value < damagingPlatformChance) {
            newBlock.GetComponent<PlatformController>().isDamaging = true;
            nextDamagingSpawnTime = Time.time + maxDamagingSpawnRate;
        } else {
            newBlock.GetComponent<PlatformController>().isDamaging = false;
        }
    }

    private void SpawnStartBlock() {
        GameObject newBlock = Instantiate(RandomBlock(), level);
        newBlock.transform.position = new Vector2(0f, -Globals.maxYPos);
        nextSpawnTime = Time.time + spawnRate;
    }

    private void Update() {
        if (playerController.alive && Time.time > nextSpawnTime) {
            Spawn();
            nextSpawnTime += spawnRate;
        }
    }

    private void DestroyLevel() {
        GameObject[] children = new GameObject[level.childCount];

        for (int i = 0; i < level.childCount; i++) {
            children[i] = level.GetChild(i).gameObject;
        }

        foreach (GameObject child in children) {
            Destroy(child);
        }
    }
}
