using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawner
{
    void InstantiateObjectAtCell(GameObject gameObject, Cell cell);
}


