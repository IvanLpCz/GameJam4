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
		Invoke("SpawnEnemies",2f);
    }

	void SpawnEnemies()
    {
		Instantiate(boss, rooms[rooms.Count - 1].transform.position, Quaternion.identity);

		for (int i = 0; i < rooms.Count; i++)
        {
			Instantiate(enemies, rooms[i].transform.position, Quaternion.identity);
        }

    }

    /*void Update(){

		if(waitTime <= 0 && spawnedBoss == false){
			for (int i = 0; i < rooms.Count; i++) {
				if(i == rooms.Count-1){
					Instantiate(boss, rooms[i].transform.position, Quaternion.identity);
					spawnedBoss = true;
				}
			}
		} else {
			waitTime -= Time.deltaTime;
		}
	}*/
}
