using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "DungeonLevelPattern", menuName = "ScriptableObjects/Dungeon Level Pattern")]
public class DungeonLevelPattern : ScriptableObject
{
    [System.Serializable] public class RoomPattern
    {
        public int minSize;
        public int maxSize;
        public RuleTile floorTile;
        public RuleTile wallTile;
        public List<GameObject> decorations;
    }

    [System.Serializable] public class CorridorPattern
    {
        public int minSize;
        public int maxSize;
        public RuleTile floorTile;
        public RuleTile wallTile;
    }

    [System.Serializable]
    public class SpecialObject
    {
        public GameObject specialObject;
            
    }

    [SerializeField] public List<RoomPattern> rooms;
    [SerializeField] public List<CorridorPattern> corridors;
    [SerializeField] public GameObject chest;
    [SerializeField] public List<Tile> traps;
    [SerializeField] public List<SpecialObject> specialObjects;

}
