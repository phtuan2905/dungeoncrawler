using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ProceduralDungeonGenerate : MonoBehaviour
{
    [SerializeField] Tilemap groundTilemap;
    [SerializeField] Tile groundTile;

    private void Awake()
    {
        
        DrawRoom(Vector3Int.zero, Vector3Int.one * 4);
        Vector3Int roomCenter = (Vector3Int.zero + Vector3Int.one * 4) / 2;
        DrawCorridor(roomCenter + Vector3Int.up * (Vector3Int.one * 4 / 2), Vector3Int.up, 1, true);
        DrawCorridor(roomCenter + Vector3Int.right * (Vector3Int.one * 4 / 2), Vector3Int.right, 1, true);
        DrawCorridor(roomCenter + Vector3Int.down * (Vector3Int.one * 4 / 2), Vector3Int.down, 1, true);
        DrawCorridor(roomCenter + Vector3Int.left * (Vector3Int.one * 4 / 2), Vector3Int.left, 1, true);
        

        /*
        DrawRoom(Vector3Int.zero, Vector3Int.one * 5);
        Vector3Int roomCenter = (Vector3Int.zero + Vector3Int.one * 5) / 2;
        DrawCorridor(roomCenter + Vector3Int.up * (Vector3Int.one * 5 / 2), Vector3Int.up, 1, false);
        DrawCorridor(roomCenter + Vector3Int.right * (Vector3Int.one * 5 / 2), Vector3Int.right, 1, false);
        DrawCorridor(roomCenter + Vector3Int.down * (Vector3Int.one * 5 / 2), Vector3Int.down, 1, false);
        DrawCorridor(roomCenter + Vector3Int.left * (Vector3Int.one * 5 / 2), Vector3Int.left, 1, false);
        */
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

    void DrawCorridor(Vector3Int startPoint, Vector3Int direction, int lenght, bool isEven)
    {
        int j = 0;
        if ((direction == Vector3Int.right || direction == Vector3Int.up) && isEven) j = 1;
        for (int i = 1 - j; i <= lenght - j; i++)
        {
            groundTilemap.SetTile(startPoint + (direction * i), groundTile);
        }
    }
}
