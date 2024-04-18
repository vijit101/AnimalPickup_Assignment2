using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoSingletongeneric<Board>
{
    public List<Transform> boardSpawnPoints;
    [HideInInspector]
    public List<GameObject> AllSpawnedAnimal = new List<GameObject>();
    public BoardSorter boardsorterPrefab;
    // to work on baord configurations later hence created a Class for board

}
