using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class WheelieView : MonoBehaviour
{
    [SerializeField]
    VehicleController vehicle;

    Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
        vehicle.OnWheelieStart += WheelieStart;
        vehicle.OnWheelieEnd += WheeleEnd;
    }

    private void OnDestroy()
    {
        vehicle.OnWheelieStart -= WheelieStart;
        vehicle.OnWheelieEnd -= WheeleEnd;
    }

    void WheelieStart()
    {
        text.enabled = true;
    }

    void WheeleEnd()
    {
        text.enabled = false;
    }
}
