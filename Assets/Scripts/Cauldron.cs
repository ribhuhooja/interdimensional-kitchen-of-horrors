using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlaceableContainer))]
public class Cauldron : MonoBehaviour, IPlaceableContainerManager
{
    private Placeable placeable;
    private PlaceableContainer container;
    

    private void Awake()
    {
        placeable = GetComponent<Placeable>();
        container = GetComponent<PlaceableContainer>();
        container.IsAcceptingItems = true;
    }
    
    public void manageInsertion(StoredPlaceable placeable, int ID)
    {
    }

    public void manageRemoval(StoredPlaceable placeable, int ID)
    {
    }
}
