using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectilePhysicsObject : PhysicsObject {

    /// <summary>
    /// Boolean to tell if the projectile is still in flight
    /// </summary>
    public bool m_InFlight;

    /// <summary>
    /// The object to be spawned to show the trajectory of the projectile
    /// </summary>
    public Transform m_TrajectoryIndicator;

    /// <summary>
    /// The projectile's target
    /// </summary>
    public MovablePhysicsObject m_Target;

    /// <summary>
    /// Maximum bounds for the projectile to keep flying
    /// </summary>
    public Vector3 m_MaximumBounds;
    /// <summary>
    /// Minimum bounds for the projectile to keep flying
    /// </summary>
    public Vector3 m_MinimumBounds;

    /// <summary>
    /// A List of gameobjects that work as the trajectory of the projectile
    /// </summary>
    public List<Transform> m_TrajectoryIndicators;

    /// <summary>
    /// The Movable Physics Object of this GameObject
    /// </summary>
    private MovablePhysicsObject m_SelfMPO;

	// Use this for initialization
	void Start () {
        m_SelfMPO = GetComponent<MovablePhysicsObject>();
        m_TrajectoryIndicators = new List<Transform>();
        
    }

    /// <summary>
    /// Resets the position of the projectile
    /// Destroys the trajectory objects
    /// </summary>
    public void Reset()
    {
        m_SelfMPO.Reset();
        foreach(Transform obj in m_TrajectoryIndicators)
        {
            Destroy(obj.gameObject);
        }
        m_InFlight = false;
        m_TrajectoryIndicators = new List<Transform>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (m_InFlight) m_TrajectoryIndicators.Add(Instantiate(m_TrajectoryIndicator, transform.position, transform.rotation) as Transform);


        if(m_SelfMPO.m_Position.x > m_MaximumBounds.x || m_SelfMPO.m_Position.y > m_MaximumBounds.y)
        {
            Time.timeScale = 0;
        }
        if (m_SelfMPO.m_Position.x < m_MinimumBounds.x || m_SelfMPO.m_Position.y < m_MinimumBounds.y)
        {
            Time.timeScale = 0;
        }

        if((m_SelfMPO.m_Position - m_Target.m_Position).magnitude < 1)
        {
            Time.timeScale = 0;
        }
    }
}
