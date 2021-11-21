using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour {

	public GameObject[] bottomRooms;
	public GameObject[] topRooms;
	public GameObject[] leftRooms;
	public GameObject[] rightRooms;

	public GameObject closedRoom;

	public List<GameObject> rooms;

	//public float waitTime;
	//private bool spawnedBoss;
	public GameObject boss;
	public GameObject enemies;

    private void Start()
    {
        Invoke("SpawnEnemies", 2f);
    }

    void SpawnEnemies()
    {
        Instantiate(boss, rooms[rooms.Count - 1].transform.position, Quaternion.identity);

    }
}
