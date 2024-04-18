using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.Arm;

[CreateAssetMenu(fileName ="New Animal",menuName = "Scriptables/Animal")]
public class AnimalScriptableObj : ScriptableObject
{
    public Sprite AnimalSprite;
    //public Animal AnimalView;
    public String AnimalName;
    public bool isflying;
    public bool isInsect;
    public bool isOmnivorous;
    public bool isLivesingroup;
    public bool isLayeggs;
    public bool isGameobjectInteractive;
}
