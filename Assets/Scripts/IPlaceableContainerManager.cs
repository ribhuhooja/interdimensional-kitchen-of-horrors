using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlaceableContainerManager
{
    public void manageInsertion(StoredPlaceable placeable, int ID);
    public void manageRemoval(StoredPlaceable placeable, int ID);
}
