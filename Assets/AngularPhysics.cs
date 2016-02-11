using UnityEngine;
using System.Collections;

public class AngularPhysics : MonoBehaviour {

    public Vector3 m_InitalAngularVelocity;
    public Vector3 m_AngularAcceleration;
    public Vector3 m_CurrentAngularVelocity;

    public bool m_IsActive;

	// Use this for initialization
	void Start () {
        m_CurrentAngularVelocity = m_InitalAngularVelocity;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (m_IsActive) ApplyAngularVelocity();
	}

    void ApplyAngularVelocity()
    {
        Vector3 degAngularV = m_CurrentAngularVelocity * Mathf.Rad2Deg * Time.fixedDeltaTime;

        transform.Rotate(degAngularV);

        m_CurrentAngularVelocity = m_CurrentAngularVelocity + m_AngularAcceleration * Time.fixedDeltaTime;
    }

    public void Reset()
    {
        m_CurrentAngularVelocity = m_InitalAngularVelocity;
        m_IsActive = false;
        transform.rotation = Quaternion.identity;
        GetComponentInChildren<TrailRenderer>().Clear();
    }
}
