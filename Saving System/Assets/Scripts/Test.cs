using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Test : MonoBehaviour
{
    /*
    	Variables
    */
    [SerializeField] private string testString = null;
    //---------------------------------
    /*
    	Public Methods
    */
    public string GetTestString() => testString;
    //public void Serialize() => FileHandler.SerializeData(this);
    private void Start()
    {
        //SaveLoadSystem.DeSerializeOverwrite("Test.dat", this);
    } 
    //---------------------------------
    /*
    	Private Methods
    */
        
    //---------------------------------

}
