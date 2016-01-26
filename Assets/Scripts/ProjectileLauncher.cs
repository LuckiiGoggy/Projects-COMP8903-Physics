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
public class ProjectileLauncher : MonoBehaviour {

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

    public KeyCode m_IncreaseInitialVelocityMagnitude;
    public KeyCode m_DecreaseInitialVelocityMagnitude;
    public KeyCode m_LaunchProjectile;

    public bool m_HasLaunched = false;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetKeyDown(m_IncreaseInitialVelocityMagnitude))
            m_ProjectileInitialVelocityMagnitude += m_ProjectileInitialVelocityMagnitudeIncrement;
        if (Input.GetKeyDown(m_DecreaseInitialVelocityMagnitude))
            m_ProjectileInitialVelocityMagnitude -= m_ProjectileInitialVelocityMagnitudeIncrement;
        if (Input.GetKeyDown(m_LaunchProjectile) && !m_HasLaunched)
            LaunchProjectile();    
	}

    private void LaunchProjectile()
    {
        RotatablePhysicsObject rotObj = GetComponent<RotatablePhysicsObject>();
        Vector3 trajectory = rotObj == null ? Vector3.right : rotObj.m_FacingVector.normalized;


        m_HasLaunched = true;

        m_ProjectileToLaunch.GetComponent<MovablePhysicsObject>().m_Velocity = trajectory * m_ProjectileInitialVelocityMagnitude;
        m_ProjectileToLaunch.GetComponent<MovablePhysicsObject>().m_Acceleration = new Vector3(0, -9.81f, 0);

    }
}
