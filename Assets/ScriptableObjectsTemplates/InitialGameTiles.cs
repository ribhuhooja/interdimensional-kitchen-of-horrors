using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;


[CreateAssetMenu()]
public class InitialGameTiles : ScriptableObject
{
   public int numrows;
   public int numcols;
   
   public Vector2Int playerSpawnLocation;
   public Vector2Int cauldronLocation;
}
