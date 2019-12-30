using UnityEngine;
using Random = UnityEngine.Random;


public class CloudsProducer : MonoBehaviour
{
    public Cloud[] cloudsPrefab;
    public GameObject Spawns;
    public int maxClouds = 40;
    public float spawnDelay = 5;
    public static int cloudsCount;
    
    private float timer;
    
    void Update() {
        timer += Time.deltaTime;
        if (cloudsCount < maxClouds && timer >= spawnDelay) {
            timer = 0;
            Transform spawns = Spawns.GetComponentInChildren<Transform>();
            int spawnCount = spawns.childCount;
            int idx = Random.Range(0, spawnCount);
            Transform spawn = spawns.GetChild(idx);
            SpawnCloud(spawn);
        }
    }

    private void SpawnCloud(Transform spawn) {
        Cloud prefab = cloudsPrefab[Random.Range(0, cloudsPrefab.Length)];
        var cloud = Instantiate(prefab, spawn.position, Quaternion.Euler(0, 90, 0));
        float scale = (float) Random.Range(4, 8) / 10;
        float XScale = cloud.transform.localScale.x * scale;
        float YScale = cloud.transform.localScale.y * scale;
        float ZScale = cloud.transform.localScale.z * scale;
        cloud.transform.localScale = new Vector3(XScale, YScale, ZScale);
        cloudsCount++;
    }
}