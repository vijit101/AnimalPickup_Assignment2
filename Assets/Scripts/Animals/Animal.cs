using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Reflection;
using Unity.VisualScripting.FullSerializer.Internal;

public  class Animal : MonoBehaviour
{


    GameObject SelectedGmo;
    Vector2 campos;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        BoardSorter sorter = collision.gameObject.GetComponent<BoardSorter>();
        if (sorter != null) {
            SpriteRenderer SorterRenderer = sorter.gameObject.GetComponent<SpriteRenderer>();
            if ((bool)this[sorter.textComponent.text] == true)
            {
                SorterRenderer.color = Color.green;
                StartCoroutine(holdOnForaFewSec(2));
                Destroy(gameObject);
            }
            else
            {
                SorterRenderer.color = Color.red;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        BoardSorter sorter = collision.gameObject.GetComponent<BoardSorter>();
        if (sorter != null)
        {
            sorter.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
    public Sprite AnimalSprite;
    public String AnimalName;
    public bool isflying;
    public bool isInsect;
    public bool isOmnivorous;
    public bool isLivesingroup;
    public bool isLayeggs;
    public bool isGameobjectInteractive;
    public Vector3 BoardPosAnimal;

    
    public void SetValuesFromScriptable(AnimalScriptableObj AnimalScriptable)
    {
        AnimalSprite = AnimalScriptable.AnimalSprite;
        AnimalName = AnimalScriptable.AnimalName;
        isflying = AnimalScriptable.isflying;
        isInsect = AnimalScriptable.isInsect;
        isOmnivorous = AnimalScriptable.isOmnivorous;
        isLivesingroup = AnimalScriptable.isLivesingroup;
        isLayeggs = AnimalScriptable.isLayeggs;
        isGameobjectInteractive = AnimalScriptable.isGameobjectInteractive;
    }

    public GameObject InstantiateAnimal(AnimalScriptableObj animalScriptable)
    {
        
        GameObject InstantedGameObj = new GameObject();       
        Animal AnimalComponent =  InstantedGameObj.AddComponent<Animal>();
        AnimalComponent.SetValuesFromScriptable(animalScriptable);             
        InstantedGameObj.name = animalScriptable.name;
        InstantedGameObj.transform.localScale = new Vector3(.2f, .2f, .2f);
        InstantedGameObj.AddComponent<BoxCollider2D>();
        //InstantedGameObj.AddComponent<BoxCollider>();
        Rigidbody2D rgbd =  InstantedGameObj.AddComponent<Rigidbody2D>();
        rgbd.gravityScale = 0;
        //Debug.Log(AnimalComponent["isflying"].ToString());
        if (AnimalComponent.AnimalSprite != null ) 
        { 
            SpriteRenderer spriteRenderer =  AnimalComponent.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = animalScriptable.AnimalSprite;
        }
        return InstantedGameObj;
    }

    private void OnMouseDown()
    {
        if ( this.isGameobjectInteractive ) {

            
            SelectedGmo = this.gameObject;
            
        }
        

    }

    private void Update()
    {
        campos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if ( campos != null && SelectedGmo != null) {
            SelectedGmo.transform.position = campos;
        }

        if(Input.GetMouseButtonDown(1) && SelectedGmo != null)
        {

            // to set animal to its board position remove the next line to make it move freely or add 
            // a variable isfreemove in animal scriptable to check if and move freely or not 
            SelectedGmo.transform.position = SelectedGmo.GetComponent<Animal>().BoardPosAnimal;
            SelectedGmo.transform.rotation = Quaternion.identity;
            SelectedGmo = null;
        }
    }


    


    //public object this[string name]
    //{
    //    get
    //    {
    //        var properties = typeof(Animal)
    //                .GetProperties(BindingFlags.Public | BindingFlags.Instance);

    //        foreach (var property in properties)
    //        {
    //            if (property.Name == name && property.CanRead)
    //                return property.GetValue(this, null);
    //        }

    //        throw new ArgumentException("Can't find property");

    //    }
    //    set
    //    {
    //        return;
    //    }
    //}

    public object this[string propertyName]
    {
        get
        {
            // probably faster without reflection:
            // like:  return Properties.Settings.Default.PropertyValues[propertyName] 
            // instead of the following
            Type myType = typeof(Animal);// base.DeclaredField.Name
            
            FieldInfo[] myPropInfo = myType.GetDeclaredFields();
            for ( int i = 0; i < myPropInfo.Length; i++)
            {
                if (myPropInfo[i].Name == propertyName)
                {
                    
                    return myPropInfo[i].GetValue(this);
                }
                
            }
            //PropertyInfo myPropInfo = myType.GetProperty(propertyName);
            //return myPropInfo.GetValue(this, null);
            throw new ArgumentException("Can't find property");
            return null;
            

        }

        set
        {
            Type myType = typeof(Animal);
            PropertyInfo myPropInfo = myType.GetProperty(propertyName);
            myPropInfo.SetValue(this, value, null);
        }
    }

    public IEnumerator holdOnForaFewSec(int time)
    {
        yield return new WaitForSeconds(time);
    }
}


