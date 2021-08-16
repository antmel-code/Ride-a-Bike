using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceView : FloatValueView
{
    [SerializeField]
    VehicleController vehicle;

    Vector3 vehicleStartPoint = Vector3.zero;

    private void Awake()
    {
        base.Awake();
        vehicle.OnUpdate += UpdateDistance;
        vehicle.OnStart += VehicleStart;
    }

    private void OnDestroy()
    {
        vehicle.OnUpdate -= UpdateDistance;
    }

    void UpdateDistance()
    {
        Value = Mathf.RoundToInt(vehicle.transform.position.x - vehicleStartPoint.x);
    }

    void VehicleStart()
    {
        vehicleStartPoint = vehicle.transform.position;
    }
}
