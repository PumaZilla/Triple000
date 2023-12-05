using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRooms : MonoBehaviour
{
    // All the possible openings of a room
    public enum Direction { UP, RIGHT, BOTTOM, LEFT, CLOSED };

    // --- | TEMPLATE ROOMS | ---

    // The path/suffix to the room prefabs
    private const string ROOM_PATH = "Dungeon/Room ";

    // The room templates by direction
    private Dictionary<Direction, GameObject[]> templates = new Dictionary<Direction, GameObject[]>();

    // Get a random template room by direction
    public GameObject getTemplate(Direction direction) {
        // Get a random template room by direction
        int rnd = Random.Range(0, templates[direction].Length);
        return templates[direction][rnd];
    }

    // Load all the room templates from Resources/
    private void Awake() {
        templates = new Dictionary<Direction, GameObject[]>() {
            {Direction.UP, new GameObject[] {
                Resources.Load<GameObject>(ROOM_PATH + "B"),
                Resources.Load<GameObject>(ROOM_PATH + "UB"),
                Resources.Load<GameObject>(ROOM_PATH + "BL")
            } },
            {Direction.RIGHT, new GameObject[] {
                Resources.Load<GameObject>(ROOM_PATH + "L"),
                Resources.Load<GameObject>(ROOM_PATH + "RL"),
                Resources.Load<GameObject>(ROOM_PATH + "BL")
            } },
            {Direction.BOTTOM, new GameObject[] {
                Resources.Load<GameObject>(ROOM_PATH + "U"),
                Resources.Load<GameObject>(ROOM_PATH + "UR"),
                Resources.Load<GameObject>(ROOM_PATH + "UB")
            } },
            {Direction.LEFT, new GameObject[] {
                Resources.Load<GameObject>(ROOM_PATH + "R"),
                Resources.Load<GameObject>(ROOM_PATH + "UR"),
                Resources.Load<GameObject>(ROOM_PATH + "RL")
            } },
            {Direction.CLOSED, new GameObject[] {
                Resources.Load<GameObject>(ROOM_PATH + "0")
            } }
        };
    }
}
