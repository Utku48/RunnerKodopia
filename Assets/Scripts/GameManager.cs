using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> roads = new List<GameObject>();
    [SerializeField] private Transform player;
    [SerializeField] Transform carSpawn;

    private float previousPlayerZ;
    [SerializeField] private int roadCount = 5;
    [SerializeField] private float roadLength = 3.2f;

    private float nextRoadZ;


    void Start()
    {

        nextRoadZ = player.position.z;

        for (int i = 0; i < roadCount; i++)
        {
            CreateRoad();
        }
    }

    void Update()
    {

        if (player.position.z > nextRoadZ - (roadLength * roadCount))
        {
            CreateRoad();
        }
    }

    private void FixedUpdate()
    {
        //Oyuncunun z pozisyonundaki değişimini hesapla
        float deltaZ = player.transform.position.z - previousPlayerZ;

        //Spawner pozisyonunu yukarıdaki değişim kadar değiştir
        carSpawn.position += new Vector3(0, 0, deltaZ);

        previousPlayerZ = player.position.z;
    }

    void CreateRoad()
    {
        Vector3 spawnPosition = new Vector3(0f, 0f, nextRoadZ);

        GameObject selectedRoad = roads[Random.Range(0, roads.Count)];

        Instantiate(selectedRoad, spawnPosition, Quaternion.identity);

        nextRoadZ += roadLength;
    }
}