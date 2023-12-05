using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private DungeonRooms rooms;                                 // The object with all the rooms templates and variables
    private bool spawned = false;                               // Check if this spawn point has already spawned a room.
    private Vector3 offset = new Vector3(2, 0, 0);              // The offset of the spawn point
    [SerializeField] private bool isTheOrigin;                  // This spawn point is the origin of the dungeon
    [SerializeField] private DungeonRooms.Direction direction;  // This spawn points to a room in the following direction

    // --- | SPAWN A ROOM | ---

    private void Start() {
        // Get the room templates object
        rooms = GameObject.FindGameObjectWithTag("Dungeon").GetComponent<DungeonRooms>();
        // If it is the origin, it has already spawned a room.
        if(isTheOrigin) spawned = true;
        else Invoke("Spawn", 0.1f);
        // Destroy this spawn point after 4 seconds in order to optimize the game
        Destroy(gameObject, 4f);
    }

    private void Spawn() {
        // Check if this spawn point has already spawned a room or if it is the origin of the dungeon
        if(spawned) return;
        // Spawn the template room
        Instantiate(rooms.getTemplate(direction), transform.position - offset, Quaternion.identity);
        spawned = true;
    }

    // --- | EVENTS | ---

    private void OnTriggerEnter2D(Collider2D other) {
        // Check if another spawn point is colliding with this one
        if(other.CompareTag("RoomSpawn")) {
            // If this spawn point is the origin of the dungeon, destroy other spawn points
            if(isTheOrigin) Destroy(other.gameObject);
            else
            {
                // Check if the other spawn point or this one has already spawned a room
                if(!other.GetComponent<SpawnPoint>().spawned && !spawned) {
                    // Create a closed room and destroy this spawn point
                    Instantiate(rooms.getTemplate(DungeonRooms.Direction.CLOSED), transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
                // This spawn has spawned a room
                spawned = true;
            }
        }
    }
}
