using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    /*
    	Variables
    */
    [SerializeField] Test test;
    //---------------------------------
    /*
    	Public Methods
    */
    
    //---------------------------------
    /*
    	Private Methods
    */
    private void Start()
    {
        test.DeSerialize();
    }
    private void Update()
    {
        
    }    
    //---------------------------------

}
