using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    VehicleController vehicle;

    bool LeftTap
    {
        get
        {
            bool res = false;
            foreach (Touch touch in Input.touches)
            {
                if (touch.position.x < Screen.width / 2)
                {
                    res = true;
                    break;
                }
            }
            return res;
        }
    }

    bool RightTap
    {
        get
        {
            bool res = false;
            foreach(Touch touch in Input.touches)
            {
                if (touch.position.x > Screen.width / 2)
                {
                    res = true;
                    break;
                }
            }
            return res;
        }
    }

    private void Awake()
    {
        vehicle.OnDeth += Restart;
    }

    private void OnDestroy()
    {
        vehicle.OnDeth -= Restart;
    }

    // Update is called once per frame
    void Update()
    {
        if (LeftTap)
        {
            vehicle.SlowDown(true);
        }
        else
        {
            vehicle.SlowDown(false);
        }

        if (RightTap)
        {
            vehicle.Accelerate(true);
        }
        else
        {
            vehicle.Accelerate(false);
        }

        if (LeftTap && RightTap)
        {
            vehicle.Accelerate(false);
            vehicle.SlowDown(false);
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
