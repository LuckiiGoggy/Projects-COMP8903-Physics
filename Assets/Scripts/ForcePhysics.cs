using UnityEngine;
using System.Collections;

public class ForcePhysics : PhysicsObject {

    public Vector3 m_Force;
    public Vector3 m_ForceIndicatorOffset;
    
    public Transform m_ForceIndicator;
    public MovablePhysicsObject m_ForceTarget;

    public Timer m_Time;
    public float m_LengthTimeAlive;
    
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
       if(m_Time.m_CurrTime == m_LengthTimeAlive)
       {
           Destroy(this.gameObject);
       }
       else if(m_Time.m_CurrTime > m_LengthTimeAlive)
       {
           //m_ForceTarget.m_TotalForce = Vector3.zero;
           //float actualTime = (m_LengthTimeAlive - m_Time.m_CurrTime);
           
           //m_ForceTarget.m_Velocity = 0.5f * (m_Force/m_ForceTarget.TotalMass()) * (m_LengthTimeAlive - m_Time.m_CurrTime);
           Destroy(this.gameObject);
       }
       else{
           m_ForceIndicator.localPosition = m_ForceTarget.GetComponent<Transform>().localPosition + m_ForceIndicatorOffset;
        
           m_ForceTarget.ApplyForce(m_Force);
       
       }
	}
}
