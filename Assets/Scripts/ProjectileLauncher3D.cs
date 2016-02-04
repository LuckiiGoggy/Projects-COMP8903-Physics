using UnityEngine;
using System.Collections;

/// <summary>
/// Projectile Launcher launches a projectile.
/// 
/// Spawns a specified ProjectilePhysicsObject.
/// Sets the Projectile Physics Object velocity to a specified
/// power and trajectory.
/// 
/// Assumes the object is a RotatablePhysicsObject if not, uses 
/// a pre-set angle.
/// </summary>
public class ProjectileLauncher3D : PhysicsObject {

    ///<summary>
    /// Pre-set angle of the ProjectileLauncher if it is not a RotatablePhysicsObject
    /// </summary>
    public Vector3 m_PreSetAngle;

    /// <summary>
    /// ProjectPhysicsObject to launch fromt he Projectile Launcher
    /// </summary>
    public ProjectilePhysicsObject m_ProjectileToLaunch;

    /// <summary>
    /// The initial velocity of the projectile to be launched
    /// </summary>
    public float m_ProjectileInitialVelocityMagnitude;

    ///<summary>
    /// The minimum possible Initial Velocity Magnitude.
    /// </summary>
    public float m_MinimumProjectileInitialVelocityMagnitude;

    ///<summary>
    /// The maximum possible Initial Velocity Magnitude.
    /// </summary>
    public float m_MaximumProjectileInitialVelocityMagnitude;

    ///<summary>
    /// The Initial Velocity Magnitude to modify.
    /// </summary>
    public float m_ProjectileInitialVelocityMagnitudeIncrement;

    /// <summary>
    /// Keyboard Key for increasing the velocity of the projectile when launched
    /// </summary>
    public KeyCode m_IncreaseInitialVelocityMagnitude;
    /// <summary>
    /// Keyboard Key for decreasing the velocity of the projectile when launched
    /// </summary>
    public KeyCode m_DecreaseInitialVelocityMagnitude;
    /// <summary>
    /// Keyboard Key for firing the projectile
    /// </summary>
    public KeyCode m_LaunchProjectile;

    /// <summary>
    /// Boolean to track if the projectile has launched its one and only projectile.
    /// </summary>
    public bool m_HasLaunched = false;

    /// <summary>
    /// The timer that shows how long the projectile has been moving.
    /// </summary>
    public Timer m_Timer;

    /// <summary>
    /// The target that the ProjectLauncher is aiming at.
    /// </summary>
    public MovablePhysicsObject m_Target;

    /// <summary>
    /// The correct low gun angle to make the shot.
    /// </summary>
    public float m_CorrectGunAngleLow;

    /// <summary>
    /// The correct high gun angle to make the shot.
    /// </summary>
    public float m_CorrectGunAngleHigh;

    /// <summary>
    /// The range between the gun and the target.
    /// </summary>
    public float m_Range;

    /// <summary>
    /// Keyboard Key to reset the projectile
    /// </summary>
    public KeyCode m_ResetKey;

	// Use this for initialization
	void Start () {

	}
	
    /// <summary>
    /// Resets the launcher and its projectile and as well the time
    /// </summary>
    public void Reset()
    {
        m_HasLaunched = false;
        m_ProjectileToLaunch.Reset();
        m_Timer.Reset();
        m_Timer.m_IsStopped = true;
        Time.timeScale = 1;
    }

    public void Update()
    {
        if (Input.GetKey(m_ResetKey)) Reset();
    }

	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetKeyDown(m_IncreaseInitialVelocityMagnitude))
            m_ProjectileInitialVelocityMagnitude = Mathf.Min(m_ProjectileInitialVelocityMagnitude + m_ProjectileInitialVelocityMagnitudeIncrement, m_MaximumProjectileInitialVelocityMagnitude);
        if (Input.GetKeyDown(m_DecreaseInitialVelocityMagnitude))
            m_ProjectileInitialVelocityMagnitude = Mathf.Max(m_ProjectileInitialVelocityMagnitude - m_ProjectileInitialVelocityMagnitudeIncrement, m_MinimumProjectileInitialVelocityMagnitude);
        if (Input.GetKeyDown(m_LaunchProjectile) && !m_HasLaunched)
            LaunchProjectile();

        m_Range = (m_Target.GetComponent<MovablePhysicsObject>().m_Position - GetComponent<MovablePhysicsObject>().m_Position).magnitude;

        m_CorrectGunAngleLow = Mathf.Asin((9.81f * m_Range) / (m_ProjectileInitialVelocityMagnitude * m_ProjectileInitialVelocityMagnitude)) / 2 * Mathf.Rad2Deg;
        m_CorrectGunAngleHigh = 360 + (Mathf.Asin((9.81f * m_Range) / (m_ProjectileInitialVelocityMagnitude * m_ProjectileInitialVelocityMagnitude)) / 2 * Mathf.Rad2Deg);
    }

    /// <summary>
    /// Launches the projectile of the launcher.
    /// It is currently set up so that only one projectile will be launched from the launcher.
    /// </summary>
    private void LaunchProjectile()
    {
        RotatablePhysicsObject3D rotObj = GetComponent<RotatablePhysicsObject3D>();
        Vector3 trajectory = rotObj == null ? Vector3.right : rotObj.m_FacingVector.normalized;

        m_Timer.m_IsStopped = false;
        m_HasLaunched = true;

        m_ProjectileToLaunch.GetComponent<MovablePhysicsObject>().m_Velocity = trajectory * m_ProjectileInitialVelocityMagnitude;
        m_ProjectileToLaunch.GetComponent<MovablePhysicsObject>().m_Acceleration = new Vector3(0, -9.81f, 0);
        m_ProjectileToLaunch.m_InFlight = true;

    }
}
