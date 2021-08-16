using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    // Events for view
    public event System.Action OnUpdate = () => { };
    public event System.Action OnStart = () => { };
    public event System.Action OnWheelieStart = () => { };
    public event System.Action OnWheelieEnd = () => { };
    public event System.Action OnDeth = () => { };

    // Vehicle body
    [SerializeField]
    Rigidbody2D vehicle;

    [SerializeField]
    WheelJoint2D[] driveWheels;

    /// <summary>
    /// Wheels that should be in the air for wheelie
    /// </summary>
    [SerializeField]
    Collider2D[] wheelieWheels;

    [SerializeField]
    Collider2D deadTrigget;

    bool isAccelerating = false;
    bool isBraking = false;

    bool wheelie = false;

    bool isDead = false;

    public bool IsDead
    {
        get => isDead;
        private set
        {
            if (isDead == value)
                return;
            isDead = value;
            if (isDead)
            {
                OnDeth();
            }
        }
    }

    public bool Wheelie
    {
        get => wheelie;
        private set
        {
            if (wheelie == value)
                return;
            wheelie = value;
            if (wheelie)
            {
                OnWheelieStart();
            }
            else
            {
                OnWheelieEnd();
            }
        }
    }

    public bool IsAccelerating
    {
        get => isAccelerating;
        set
        {
            if (isAccelerating == value)
                return;
            isAccelerating = value;
            if (isAccelerating)
            {
                ActivateDriveWheels();
            }
            else
            {
                StopDriveWheels();
            }
        }
    }

    public bool IsBraking
    {
        get => isBraking;
        set
        {
            if (isBraking == value)
                return;
            isBraking = value;
            if (isBraking)
            {
                BrakeDriveWheels();
            }
            else
            {
                UnbrakeDriveWheels();
            }
        }
    }

    public void Accelerate(bool accelerate)
    {
        IsAccelerating = accelerate;
    }

    public void SlowDown(bool brake)
    {
        IsBraking = brake;
    }

    // Start is called before the first frame update
    void Start()
    {
        OnStart();
    }

    // Update is called once per frame
    void Update()
    {
        OnUpdate();
        CheckWheelie();
        CheckDeadTrigger();
    }

    void ActivateDriveWheels()
    {
        foreach (WheelJoint2D wheel in driveWheels)
        {
            wheel.useMotor = true;
        }
    }

    void StopDriveWheels()
    {
        foreach (WheelJoint2D wheel in driveWheels)
        {
            wheel.useMotor = false;
        }
    }

    void CheckWheelie()
    {
        bool wheelie = true;
        foreach (Collider2D wheel in wheelieWheels)
        {
            if (wheel.IsTouchingLayers())
            {
                wheelie = false;
                break;
            }
        }
        foreach (WheelJoint2D wheel in driveWheels)
        {
            if (!wheel.GetComponent<Collider2D>().IsTouchingLayers())
            {
                wheelie = false;
                break;
            }
        }
        Wheelie = wheelie;
    }
    
    void CheckDeadTrigger()
    {
        if (deadTrigget.IsTouchingLayers())
        {
            IsDead = true;
        }
    }

    void BrakeDriveWheels()
    {
        foreach (WheelJoint2D wheel in driveWheels)
        {
            // Invert motor
            JointMotor2D motor = wheel.motor;
            motor.motorSpeed *= motor.motorSpeed >= 0 ? -1 : 1;
            wheel.motor = motor;

            wheel.useMotor = true;
        }
    }

    void UnbrakeDriveWheels()
    {
        foreach (WheelJoint2D wheel in driveWheels)
        {
            // Uninvert motor
            JointMotor2D motor = wheel.motor;
            motor.motorSpeed *= motor.motorSpeed < 0 ? -1 : 1;
            wheel.motor = motor;

            wheel.useMotor = false;
        }
    }
}
