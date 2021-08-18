using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMono : SerializerMono<TestMono>
{
    /*
        Variables
    */
    [SerializeField] string testString = null;
    //---------------------------------------------------
    /*
        Public Methods
    */

    //---------------------------------------------------
    /*
        Private Methods
    */
    private void Start()
    {
        CreateFromJsonOverwrite(FileHandler.ReadFile("TestMono.json"), this);
        FileHandler.CreateFile(ToJson(this), "TestMono.json");
    }
    //---------------------------------------------------
}
