using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType
{
    freedom,
    up,
    right,
    down,
    left,
    vertical
}

public class ObjectSize : MonoBehaviour
{
    public Vector3Int objectSize;
    public ObjectType objectType;

    [SerializeField] public int objectDirection;
    [SerializeField] public List<Sprite> objectSprites;

    public void SetDirection(Sprite directionSprite)
    {
        
    }
}
