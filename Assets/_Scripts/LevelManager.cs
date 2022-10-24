using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField]
    private PathCreation.PathCreator PathCreator;

    [Header("Prefabs")]
    [SerializeField]
    private GameObject CarPrefab;

    [SerializeField]
    private int OppositeTrafficCount = 5;
    [SerializeField]
    private float AverageDelay = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RunTraffic());
    }

    IEnumerator RunTraffic()
    {
        for(int i = 0; i < OppositeTrafficCount; i++)
        {
            var car = Instantiate(CarPrefab);
            var carManager = car.GetComponent<CarManager>();
            carManager.RoadDriver.pathCreator = PathCreator;
            carManager.RoadDriver.speed = 8;
            carManager.RoadDriver.DriveOnRightSide = true;
            carManager.RoadDriver.DrivingOppositeWay = true;
            carManager.StartDriving();

            var stdDev = AverageDelay * 0.7f;
            yield return new WaitForSeconds(Random.Range(AverageDelay - stdDev, AverageDelay + stdDev));


        }
    }
}
