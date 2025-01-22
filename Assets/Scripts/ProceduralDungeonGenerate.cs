using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ProceduralDungeonGenerate : MonoBehaviour
{
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tile groundTile;

    [SerializeField] private Vector3Int currentRoomCenter;
    [SerializeField] private Vector3Int corridorEndPoint;
    [SerializeField] private List<Vector3Int> ways;

    private void Awake()
    {         
        ways.Add(Vector3Int.up);
        ways.Add(Vector3Int.right);
        ways.Add(Vector3Int.down);
        ways.Add(Vector3Int.left);
        DrawRoom(Vector3Int.zero, new Vector3Int(5, 5, 0));
        Vector3Int roomCenter = (Vector3Int.zero + new Vector3Int(5, 5, 0)) / 2;
        Vector3Int roomSize = new Vector3Int(5, 5, 0);
        currentRoomCenter = roomCenter;

        List<Vector3Int> currentWays = ways;
        for (int i = 0; i < 3; i++)
        {
            int wayIndex = Random.Range(0, currentWays.Count);
            DrawCorridor(currentRoomCenter + currentWays[wayIndex] * roomSize / 2, currentWays[wayIndex], 3);
            corridorEndPoint = (currentRoomCenter + currentWays[wayIndex] * roomSize / 2 + currentWays[wayIndex] + currentWays[wayIndex] * 3);
            roomSize = new Vector3Int(5, 5, 0);
            roomCenter = (roomCenter + currentWays[wayIndex] * roomSize + currentWays[wayIndex] * 1 + currentWays[wayIndex]) + roomSize * currentWays[wayIndex] / 2;
            DrawRoom(roomCenter - roomSize / 2, roomSize);
            Vector3Int way = currentWays[wayIndex];
            currentWays = ways;
            currentWays.Remove(way);
            currentRoomCenter = roomCenter;
        }

        Vector3Int room1Size = new Vector3Int(7, 3, 0);
        Vector3Int room1Center = (roomCenter + Vector3Int.up * new Vector3Int(9, 5, 0) / 2 + Vector3Int.up * 1 + Vector3Int.up) + room1Size * Vector3Int.up / 2;
        Vector3Int room1StartPoint = room1Center - room1Size / 2;
        DrawRoom(room1StartPoint, room1Size);
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
