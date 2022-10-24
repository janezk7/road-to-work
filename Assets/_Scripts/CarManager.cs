using PathCreation.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    [Header("Managers")]
    public RoadDriver RoadDriver;
    
    [Header("Fields")]
    public Transform ParentTransform;

    public void StartDriving()
    {
        //ParentTransform.localRotation = Quaternion.Euler(0, 0, 90f);
        RoadDriver.enabled = true;
        Debug.Log(gameObject.name + " driving!");
    }
}
