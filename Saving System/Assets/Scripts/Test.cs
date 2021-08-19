using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Test : Serializer<Test>
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
    public void Serialize() => FileHandler.SerializeData(this);
    public void DeSerialize()
    {
        var tmp = FileHandler.DeSerializeData<Test>();
        testString = tmp.GetTestString();
    } 
    //---------------------------------
    /*
    	Private Methods
    */
        
    //---------------------------------

}
