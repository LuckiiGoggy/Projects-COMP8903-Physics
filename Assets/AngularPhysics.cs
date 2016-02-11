using UnityEngine;
using System.Collections;

public class AngularPhysics : PhysicsObject {

    public Vector3 m_InitalAngularVelocity;
    public Vector3 m_AngularAcceleration;
    public Vector3 m_CurrentAngularVelocity;
    public Vector3 m_AngularDisplacement;

    public Vector3 m_PointVelocity;
    public Vector3 m_PointAcceleration;

    public MovablePhysicsObject m_Object;
    public Transform m_Point;

    public Vector3 m_Position;

    public bool m_IsActive;

	// Use this for initialization
	void Start () {
        m_CurrentAngularVelocity = m_InitalAngularVelocity;

    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Debug.Log(transform.rotation * Vector3.right * .2f);

        Vector3 R = transform.rotation * Vector3.right * .2f;

        m_Position = (transform.rotation * Vector3.right * .2f + m_Object.GetComponent<Transform>().localPosition) * 100f;

        if (m_IsActive) ApplyAngularVelocity();

        m_PointVelocity = m_Object.m_Velocity + Vector3.Cross(m_CurrentAngularVelocity, R * 100f);
        m_PointAcceleration = m_Object.m_Acceleration + Vector3.Cross(m_AngularAcceleration, R * 100f) + Vector3.Cross(m_CurrentAngularVelocity, Vector3.Cross(m_CurrentAngularVelocity, R * 100f));

    }

    void ApplyAngularVelocity()
    {
        Vector3 degAngularV = m_CurrentAngularVelocity * Mathf.Rad2Deg * Time.fixedDeltaTime;

        transform.Rotate(degAngularV);

        m_AngularDisplacement += m_CurrentAngularVelocity * Time.fixedDeltaTime;

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
