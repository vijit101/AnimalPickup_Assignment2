//using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameService : MonoSingletongeneric<GameService>
{
    [Header("This List is for the Designers to update using ScriptableObjects")]
    public List<AnimalScriptableObj> AnimalScriptableList = new List<AnimalScriptableObj>();
    List<AnimalScriptableObj> TempList = new List<AnimalScriptableObj>(); // need it to make sure duplicate animal do not spawn
    Animal myAnimal = new Animal();
    private void Start()
    {
        BoardSetupAnimalTiles(Board.Instance.boardSpawnPoints, AnimalScriptableList);
        // putting back the animals that i removed after baord setup 
        for (int i = 0; i < TempList.Count; i++)
        {
            AnimalScriptableList.Add(TempList[i]);
        }
    }


    public void BoardSetupAnimalTiles(List<Transform> Spawnpts, List<AnimalScriptableObj> AnimalScriptableList)
    {
        //int prevnos;
        for (int i = 0; i < Spawnpts.Count; i++)
        {

            if (i < Spawnpts.Count - 2)
            {
                int selectRandomScriptable = Random.Range(0, AnimalScriptableList.Count);
                GameObject anim = myAnimal.InstantiateAnimal(AnimalScriptableList[selectRandomScriptable]);
                Animal animalComponent = anim.GetComponent<Animal>();

                // for unique spawns I am deleting the animal form list and adding it to another list 
                if (AnimalScriptableList.Count > 1)
                {
                    TempList.Add(AnimalScriptableList[selectRandomScriptable]);
                    AnimalScriptableList.RemoveAt(selectRandomScriptable);
                }


                anim.transform.parent = transform;
                anim.transform.position = Spawnpts[i].position;
                animalComponent.BoardPosAnimal = Spawnpts[i].position;
                if (anim != null)
                {
                    Board.Instance.AllSpawnedAnimal.Add(anim);
                }
            }
            else
            {
                BoardSorter boardSorter = Instantiate(Board.Instance.boardsorterPrefab);
                int RandomNumber = Random.Range(1, BoardSorterTypeEnum.GetNames(typeof(BoardSorterTypeEnum)).Length);
                boardSorter.BoardSorterTypeEnum = (BoardSorterTypeEnum)RandomNumber;

                boardSorter.transform.position = Spawnpts[i].position;
                boardSorter.textComponent.text = boardSorter.BoardSorterTypeEnum.ToString();
            }
           
            // running this for last two spawn points 7 and 8 th one 
            
            
        }
    }


}
