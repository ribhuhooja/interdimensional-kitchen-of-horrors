using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;


[CreateAssetMenu()]
public class InitialGameTiles : ScriptableObject
{
   public int numrows;
   public int numcols;
   
   public Placeable[] placeablePrefabs;
   public Vector2Int[] placeableLocations;
}
