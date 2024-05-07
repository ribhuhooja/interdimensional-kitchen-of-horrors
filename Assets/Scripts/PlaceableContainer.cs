using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A component that must be attached to anything that can hold placeable
// objects inside itself
[RequireComponent(typeof(Placeable))]
public class PlaceableContainer : MonoBehaviour
{
    // a dictionary with an ID integer and a Placeable
    private Dictionary<int, StoredPlaceable> storedPlaceables = new();
    private int currentID = 0;
    private IPlaceableContainerManager placeableContainerManager; // something that manages contained items

    private Placeable selfPlaceable;
    
    public bool IsAcceptingItems { get; set; }

    private void Awake()
    {
        placeableContainerManager = GetComponent<IPlaceableContainerManager>();
        selfPlaceable = GetComponent<Placeable>();
    }

    public void AddPlaceableFromBoard(Placeable placeable)
    {
        // we need to instantiate the fake "storage" object because if we want to
        // store two separate placeables of the same type, then we want to avoid any
        // problems caused by having the same prefab twice in the list
        StoredPlaceable prefabForStorage = Instantiate(placeable.PrefabForStorage(), transform);
        storedPlaceables.Add(currentID, prefabForStorage);
        selfPlaceable.ground.DestroyPlaceable(placeable);
        
        placeableContainerManager.manageInsertion(prefabForStorage, currentID);
        currentID++;
    }
}
