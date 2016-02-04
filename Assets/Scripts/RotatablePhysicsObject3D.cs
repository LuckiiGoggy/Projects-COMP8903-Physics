using UnityEngine;
using System.Collections;

/// <summary>
/// Physics Object that can be rotated based on Keyboard inputs
/// </summary>
public class RotatablePhysicsObject3D : PhysicsObject {

    /// <summary>
    /// KeyCode to increase the angle of the rotateable object around the x axis
    /// </summary>
    public KeyCode m_IncreaseAngleX;
    /// <summary>
    /// KeyCode to decrease the angle of the rotateable object around the x axis
    /// </summary>
    public KeyCode m_DecreaseAngleX;
    /// <summary>
    /// KeyCode to increase the angle of the rotateable object around the y axis
    /// </summary>
    public KeyCode m_IncreaseAngleY;
    /// <summary>
    /// KeyCode to decrease the angle of the rotateable object around the y axis
    /// </summary>
    public KeyCode m_DecreaseAngleY;
    /// <summary>
    /// KeyCode to increase the angle of the rotateable object around the z axis
    /// </summary>
    public KeyCode m_IncreaseAngleZ;
    /// <summary>
    /// KeyCode to decrease the angle of the rotateable object around the z axis
    /// </summary>
    public KeyCode m_DecreaseAngleZ;
    /// <summary>
    /// Angle value increments per key press
    /// </summary>
    public float m_AngleIncrement;

    /// <summary>
    /// Current angles of the object
    /// </summary>
    public Vector3 m_Angles;

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


        GetComponent<Transform>().rotation = Quaternion.Euler(m_Angles);

        m_FacingVector = Quaternion.Euler(m_Angles) * m_InitialFacingVector; 

       
	}

    /// <summary>
    /// Resets the rotation and the angle 
    /// </summary>
    public void Reset()
    {
        m_FacingVector = m_InitialFacingVector;
        m_Angles = Vector3.zero;
    }

    /// <summary>
    /// Handle the Key Inputs that modifies the angle of the object
    /// </summary>
    private void HandleKeyInputs()
    {
        if (Input.GetKey(m_IncreaseAngleX)) m_Angles.x += m_AngleIncrement;
        if (Input.GetKey(m_DecreaseAngleX)) m_Angles.x -= m_AngleIncrement;
        if (Input.GetKey(m_IncreaseAngleY)) m_Angles.y += m_AngleIncrement;
        if (Input.GetKey(m_DecreaseAngleY)) m_Angles.y -= m_AngleIncrement;
        if (Input.GetKey(m_IncreaseAngleZ)) m_Angles.z += m_AngleIncrement;
        if (Input.GetKey(m_DecreaseAngleZ)) m_Angles.z -= m_AngleIncrement;
    }
}
