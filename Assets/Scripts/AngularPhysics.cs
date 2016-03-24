using UnityEngine;
using System.Collections;

public class AngularPhysics : PhysicsObject {

    public Vector3 m_InitalAngularVelocity;
    public Vector3 m_AngularAcceleration;
    public Vector3 m_CurrentAngularVelocity;
    public Vector3 m_AngularDisplacement;

    public Vector3 m_PointVelocity;
    public Vector3 m_PointAcceleration;

	public Vector3 m_Radial;

    public MovablePhysicsObject m_Object;

	public Transform m_Force;

    public Vector3 m_Position;

    public bool m_IsActive;

	public Vector3 m_Torque;

	public Timer m_Time;

	// Use this for initialization
	void Start () {
        m_CurrentAngularVelocity = m_InitalAngularVelocity;

    }
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (m_Force != null)
			m_Radial = m_Force.transform.localPosition / m_Object.m_MetersToUnits - m_Object.m_CenterOfMass;
		else
			m_Radial = Vector3.zero;


		if(m_Force != null)
			m_Torque = Vector3.Cross (m_Radial, m_Force.GetComponent<ForcePhysics> ().m_Force);



		m_Position = (m_Object.transform.rotation * Vector3.right) * 100f;

		if (m_IsActive && !m_Time.m_IsStopped) ApplyAngularVelocity();


		if (m_Object.m_MOI != 0 && m_Time.m_CurrTime <= 2.65)
			m_AngularAcceleration = m_Torque / m_Object.m_MOI;
		else
			m_AngularAcceleration = Vector3.zero;

		m_PointVelocity = m_Object.m_Velocity + Vector3.Cross(m_CurrentAngularVelocity, m_Radial * 100f);
		m_PointAcceleration = m_Object.m_Acceleration + Vector3.Cross(m_AngularAcceleration, m_Radial * 100f) + Vector3.Cross(m_CurrentAngularVelocity, Vector3.Cross(m_CurrentAngularVelocity, m_Radial * 100f));

    }

    void ApplyAngularVelocity()
    {
		//Debug.Log ("A");
		Vector3 degAngularV = m_CurrentAngularVelocity * Mathf.Rad2Deg * Time.fixedDeltaTime + 0.5f * m_AngularAcceleration * Mathf.Rad2Deg * Time.fixedDeltaTime * Time.fixedDeltaTime;

		m_Object.RotateObject(degAngularV);

		m_AngularDisplacement += m_CurrentAngularVelocity * Time.fixedDeltaTime + 0.5f * m_AngularAcceleration * Time.fixedDeltaTime * Time.fixedDeltaTime;



		m_CurrentAngularVelocity = m_CurrentAngularVelocity + m_AngularAcceleration * Time.fixedDeltaTime;

    }

    public void Reset()
    {
        m_CurrentAngularVelocity = m_InitalAngularVelocity;
        m_IsActive = false;
        transform.rotation = Quaternion.identity;
        GetComponentInChildren<TrailRenderer>().Clear();
        m_AngularDisplacement = Vector3.zero;
    }
}
