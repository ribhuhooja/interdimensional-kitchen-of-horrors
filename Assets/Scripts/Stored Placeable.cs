using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Why this extra level of indirection? Because an object
// cannot easily store a reference to its own prefab
// If you try to assign an object's prefab to itself in the
// prefab editor, when that object is instantiated, the field will
// contain a reference to the INSTANCE, not the prefab
public class StoredPlaceable : MonoBehaviour
{
    public Placeable placeablePrefab;
}
