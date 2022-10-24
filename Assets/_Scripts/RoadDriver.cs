using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class RoadDriver : MonoBehaviour
{
    public PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;
    public float speed = 5;
    public bool DriveOnRightSide = true;
    public bool DrivingOppositeWay = false;
    
    private float distanceTravelled;

    // Start is called before the first frame update
    void Start()
    {
        if (pathCreator != null)
            pathCreator.pathUpdated += OnPathChanged;
    }

    // Update is called once per frame
    void Update()
    {
        if (pathCreator is null)
            return;

        distanceTravelled += speed * Time.deltaTime;

        var mappedDistance = distanceTravelled;
        if (DrivingOppositeWay)
        {
            if (distanceTravelled > pathCreator.path.length)
                distanceTravelled = 0;
            mappedDistance = pathCreator.path.length - distanceTravelled;
        }

        transform.position = pathCreator.path.GetPointAtDistance(mappedDistance, endOfPathInstruction);
        var pathRot = pathCreator.path.GetRotationAtDistance(mappedDistance, endOfPathInstruction);
        var yRotCorrection = DrivingOppositeWay ? 180.0f : 0;
        transform.rotation = Quaternion.Euler(
            pathRot.eulerAngles.x, 
            pathRot.eulerAngles.y + yRotCorrection, 
            transform.rotation.z
        );

        // Drive on right side of the road
        if(DriveOnRightSide)
        {
            var xOffset = 1.2f;
            xOffset *= DrivingOppositeWay ? -1 : 1;
            transform.position += new Vector3(xOffset, 0, 0);
        }
    }

    void OnPathChanged()
    {
        distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
    }
}
