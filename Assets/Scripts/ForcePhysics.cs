using UnityEngine;
using System.Collections;

public class ForcePhysics : PhysicsObject {

    public Vector3 m_Force;
    public Vector3 m_ForceIndicatorOffset;
    
    public Transform m_ForceIndicator;
    public MovablePhysicsObject m_ForceTarget;

    public Timer m_Time;
    public float m_LengthTimeAlive;
    
    
	public KeyCode m_TurnOnAngular;
	public bool m_IsAngularOn;
	public Vector3 m_AngularForcePosition;
	public Vector3 m_AngularForceIndicatorOffset;
	public Vector3 m_AngularAcceleration;

	public Vector3 m_Radial;

	// Use this for initialization
	void Start () {
		UpdatePosition ();
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		HandleKeyInputs ();

		if(m_Time.m_CurrTime >= m_LengthTimeAlive)
        {
			//Destroy(this.gameObject);
        }
		else if(!m_Time.m_IsStopped)
		{
			m_ForceTarget.ApplyForce(m_Force);
		}
		UpdatePosition ();
	}

	void HandleKeyInputs()
	{
		if (Input.GetKeyDown (m_TurnOnAngular)) {
			m_IsAngularOn = !m_IsAngularOn;
			//if(m_IsAngularOn)
				//m_ForceTarget.GetComponent<AngularPhysics> ().m_AngularAcceleration = m_AngularAcceleration;
			//else
				//m_ForceTarget.GetComponent<AngularPhysics> ().m_AngularAcceleration = Vector3.zero;
				
		}
	}

	void UpdatePosition()
	{
		if (m_IsAngularOn) {
			Vector3 offset = m_ForceTarget.transform.rotation * m_AngularForceIndicatorOffset;
			m_ForceIndicator.localPosition = m_ForceTarget.transform.localPosition + offset;
		} else {
			Vector3 offset = m_ForceTarget.transform.rotation * m_ForceIndicatorOffset;
			m_ForceIndicator.localPosition = m_ForceTarget.transform.localPosition + offset;
		}

	}
}
