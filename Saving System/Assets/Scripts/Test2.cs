using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Test2 : MonoBehaviour
{
    /*
        Variables
    */
    [SerializeField] private int number;
    [SerializeField] private string text;
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
        SaveLoadSystem.SerializeMonoBehaviour(this, "Test2.dat");
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SaveLoadSystem.DeSerializeOverwrite("Test2.dat", this);
    }
    //---------------------------------------------------
}
