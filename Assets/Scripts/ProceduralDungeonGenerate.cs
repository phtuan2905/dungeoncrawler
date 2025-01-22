using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ProceduralDungeonGenerate : MonoBehaviour
{
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tile groundTile;

    [SerializeField] private List<Vector3Int> ways;
    [SerializeField] private List<Vector3Int> currentWays;
    [SerializeField] private Vector3Int roomCenter;
    [SerializeField] private Vector3Int roomSize;
    [SerializeField] private int wayIndex;

    [SerializeField] private int roomTotal;
    [SerializeField] private List<Room> rooms;
    [SerializeField] private int roomIndex;

    private void Awake()
    {         
        ways.Add(Vector3Int.up);
        ways.Add(Vector3Int.right);
        ways.Add(Vector3Int.down);
        ways.Add(Vector3Int.left);
        roomCenter = (Vector3Int.zero + new Vector3Int(5, 5, 0)) / 2;
        roomSize = new Vector3Int(5, 5, 0);
        DrawRoom(Vector3Int.zero, roomSize);

        Room baseRoom = Instantiate(new Room());
        baseRoom.roomCenter = roomCenter;
        baseRoom.roomSize = roomSize;
        baseRoom.roomDirections = ways;
        rooms.Add(baseRoom);

        roomIndex = 0;
        while (roomTotal > 0)
        {
            int roomNumber = Random.Range(1, 3);
            for (int i = 0; i < roomNumber; i++)
            {
                GenerateDungeon(rooms[roomIndex]);
                roomIndex++;
                roomTotal--;
                if (roomTotal == 0) 
                {
                    break;
                }
            }
        }
    }

    void GenerateDungeon(Room parentRoom)
    {
        int directionIndex = Random.Range(0, parentRoom.roomDirections.Count);
        Vector3Int wayStartPoint = parentRoom.roomCenter + parentRoom.roomDirections[directionIndex] * parentRoom.roomSize / 2;
        DrawCorridor(wayStartPoint, parentRoom.roomDirections[directionIndex], 3);
        Vector3Int wayEndPoint = parentRoom.roomCenter + parentRoom.roomDirections[directionIndex] * parentRoom.roomSize / 2 + parentRoom.roomDirections[directionIndex] + parentRoom.roomDirections[directionIndex] * 3;
        Room newRoom = new Room();
        newRoom.roomSize = new Vector3Int(5, 5, 0);
        newRoom.roomCenter = wayEndPoint + parentRoom.roomDirections[directionIndex] * newRoom.roomSize / 2;
        newRoom.parentRoom = parentRoom;
        DrawRoom(newRoom.roomCenter - newRoom.roomSize / 2, newRoom.roomSize);
        rooms.Add(newRoom);
        parentRoom.roomDirections.Remove(parentRoom.roomDirections[directionIndex] * -1);
    }

    void DrawRoom(Vector3Int startPoint, Vector3Int roomSize)
    {
        for (int x = 0; x < roomSize.x; x++)
        {
            for (int y = 0; y < roomSize.y; y++)
            {
                groundTilemap.SetTile(startPoint + new Vector3Int(x, y, 0), groundTile);
            }
        }
    }

    void DrawCorridor(Vector3Int startPoint, Vector3Int direction, int lenght)
    {
        for (int i = 1; i <= lenght; i++)
        {
            groundTilemap.SetTile(startPoint + (direction * i), groundTile);
        }
    }
}

class Room
{
    public Room() { }

    public Vector3Int roomCenter;
    public Vector3Int roomSize;
    public Room parentRoom;
    public List<Vector3Int> roomDirections;

}
