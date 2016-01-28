using UnityEngine;
using System.Collections;

/// <summary>
/// Physics Object that can be rotated based on Keyboard inputs
/// </summary>
public class RotatablePhysicsObject : PhysicsObject {

    /// <summary>
    /// KeyCode to increase the angle of the rotateable object
    /// </summary>
    public KeyCode m_IncreaseAngle;
    /// <summary>
    /// KeyCode to decrease the angle of the rotateable object
    /// </summary>
    public KeyCode m_DecreaseAngle;
    /// <summary>
    /// Angle value increments per key press
    /// </summary>
    public float m_AngleIncrement;

    /// <summary>
    /// Current angle of the object
    /// </summary>
    public float m_Angle;

    /// <summary>
    /// The initial facing vector of the object
    /// </summary>
    public Vector3 m_InitialFacingVector;
    ///<summary>
    /// The direction that the object is currently facing
    /// </summary>
    public Vector3 m_FacingVector;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        HandleKeyInputs();


        GetComponent<Transform>().rotation = Quaternion.AngleAxis(m_Angle, Vector3.forward);
        m_FacingVector = Quaternion.AngleAxis(m_Angle, Vector3.forward) * m_InitialFacingVector; 

       
	}

    /// <summary>
    /// Resets the rotation and the angle 
    /// </summary>
    public void Reset()
    {
        m_FacingVector = m_InitialFacingVector;
        m_Angle = 0;
    }

    /// <summary>
    /// Handle the Key Inputs that modifies the angle of the object
    /// </summary>
    private void HandleKeyInputs()
    {
        if (Input.GetKey(m_IncreaseAngle)) m_Angle += m_AngleIncrement;
        if (Input.GetKey(m_DecreaseAngle)) m_Angle -= m_AngleIncrement;
    }
}
