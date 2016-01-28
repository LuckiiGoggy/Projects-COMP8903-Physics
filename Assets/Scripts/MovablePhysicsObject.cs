using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// MovablePhysicsObject is a basic object that allows for movement.
/// It holds the information for:
/// - the minimum and maximum allowed positions of the object.
/// - the current position of the object
/// - how much the object moves every input
/// - mass of the object
/// - minimum mass of the object
/// - maximum mass of the object
/// - how much the mass changes every input
/// - the conversion ratio between meters and in-game units
/// - list of all attached objects
/// - center of mass
/// - moment of inertia
/// - velocity
/// - acceleration
/// - if drag is enabled
/// 
/// The KeyCodes for the movement and the modification of the mass 
/// can be specified on the Unity Editor.
/// </summary>
public class MovablePhysicsObject : PhysicsObject {

    /// <summary>
    /// Initial Position of the Movable Physics Object
    /// </summary>
    public Vector3 m_InitPosition;
    /// <summary>
    /// Minimum position of the Movable Physics Object
    /// </summary>
    public Vector2 m_MinLocation;
    /// <summary>
    /// Maximum position of the Movable Physics Object
    /// </summary>
    public Vector2 m_MaxLocation;
    /// <summary>
    /// The value of each movement key input
    /// </summary>
    public Vector2 m_MovementIncrements;
    /// <summary>
    /// The current position of the Movable Physics Object
    /// </summary>
    public Vector2 m_Position;

    /// <summary>
    /// The minimum mass of the Movable Physics Object
    /// </summary>
    public float m_MinMass;
    /// <summary>
    /// The maximum mass of the Movable Physics Object
    /// </summary>
    public float m_MaxMass;
    /// <summary>
    /// The current mass of the Movable Physice Object
    /// </summary>
    public float m_Mass;
    /// <summary>
    /// The value of each mass key input
    /// </summary>
    public float m_MassIncrements;

    /// <summary>
    /// The ratio between meters and in-game units
    /// </summary>
    public float m_MetersToUnits = 1F/100F;

    /// <summary>
    /// The specified keycode for the up movement of the Movable Physics Object
    /// </summary>
    public KeyCode m_UpMove;
    /// <summary>
    /// The specified keycode for the down movement of the Movable Physics Object
    /// </summary>
    public KeyCode m_DownMove;
    /// <summary>
    /// The specified keycode for the left movement of the Movable Physics Object
    /// </summary>
    public KeyCode m_LeftMove;
    /// <summary>
    /// The specified keycode for the right movement of the Movable Physics Object
    /// </summary>
    public KeyCode m_RightMove;

    /// <summary>
    /// The specified keycode for increasing the mass of the Movable Physics Object
    /// </summary>
    public KeyCode m_IncreaseMass;
    /// <summary>
    /// The specified keycode for decreasing the mass of the Movable Physics Object
    /// </summary>
    public KeyCode m_DecreaseMass;

    /// <summary>
    /// List of all Movable Physics Objects attached to the Movable Physics Object
    /// </summary>
    public List<MovablePhysicsObject> m_Attached;

    /// <summary>
    /// The center of mass of the Movable Physics Object and all of its attachments
    /// </summary>
    public Vector2 m_CenterOfMass;
    /// <summary>
    /// The moment of inertia of the Movable Physics Object with all of its attachments
    /// </summary>
    public float m_MOI;
    /// <summary>
    /// The transform object that visually represents the center of mass of the Movable Physics Object
    /// </summary>
    public Transform m_ComTarget;
    /// <summary>
    /// The current velocity of the Movable Physics Object
    /// </summary>
    public Vector3 m_Velocity;
    /// <summary>
    /// The current acceleration of the Movable Physics Object
    /// </summary>
    public Vector3 m_Acceleration;
    /// <summary>
    /// Specified wether the drag is applied or not applied on the calculations of velocity
    /// </summary>
    public bool m_IsDragOn;
    
    void Start () {
        Move(m_InitPosition * m_MetersToUnits);
    }

    /// <summary>
    /// Resets the position, acceleration, and velocity
    /// </summary>
    public void Reset()
    {
        m_Velocity = Vector3.zero;
        m_Acceleration = Vector3.zero;
        m_Position = Vector3.zero;
        transform.localPosition = Vector3.zero;

        Move(m_InitPosition * m_MetersToUnits);
    }
    
    void FixedUpdate () {
        //create a clean movement vector
        Vector3 moveVec = Vector3.zero;

        //add movements specified by keyboard presses
        moveVec = HandleMovementKeyInputs(moveVec);

        //apply vector movements
        moveVec = ApplyVelocity(moveVec);

        //apply movement vector to the object
        Move(moveVec);

        //apply changes in mass
        HandleMassKeyInputs();

        //update center of mass
        UpdateCOMTarget();
        //update momentum of inertia
        m_MOI = TotalMOI();
    }
       
    /// <summary>
    /// Handle key inputs and modify the movement vector accordingly.
    /// </summary>
    /// <param name="moveVec">current movement vector</param>
    /// <returns>modified movement vector</returns>
    private Vector3 HandleMovementKeyInputs(Vector3 moveVec)
    {
        if (Input.GetKey(m_LeftMove) && (transform.localPosition.x / m_MetersToUnits - m_MovementIncrements.x >= m_MinLocation.x))
            moveVec.x -= m_MovementIncrements.x * m_MetersToUnits;

        if (Input.GetKey(m_RightMove) && (transform.localPosition.x / m_MetersToUnits + m_MovementIncrements.x <= m_MaxLocation.x))
            moveVec.x += m_MovementIncrements.x * m_MetersToUnits;

        if (Input.GetKey(m_UpMove) && (transform.localPosition.y / m_MetersToUnits + m_MovementIncrements.y <= m_MaxLocation.y))
            moveVec.y += m_MovementIncrements.y * m_MetersToUnits;

        if (Input.GetKey(m_DownMove) && (transform.localPosition.y / m_MetersToUnits - m_MovementIncrements.y >= m_MinLocation.y))
            moveVec.y -= m_MovementIncrements.y * m_MetersToUnits;

        return moveVec;
    }

    /// <summary>
    /// Modify the object's mass based on the keyboard inputs.
    /// </summary>
    private void HandleMassKeyInputs()
    {
        if (Input.GetKey(m_IncreaseMass)) ModifyMass(m_MassIncrements);
        if (Input.GetKey(m_DecreaseMass)) ModifyMass(-m_MassIncrements);
    }

    /// <summary>
    /// Apply the velocity of the MovablePhysicsObject to its movement vector.
    /// </summary>
    /// <param name="moveVec">current movement vector</param>
    /// <returns>the new movement vector</returns>
    private Vector3 ApplyVelocity(Vector3 moveVec)
    {
        if (m_IsDragOn)
        {
            moveVec += new Vector3(
                Mathf.Log(1F + 0.001F * m_Velocity.x * Time.fixedDeltaTime * m_MetersToUnits)/(0.001F),
                Mathf.Log(1F + 0.001F * m_Velocity.y * Time.fixedDeltaTime * m_MetersToUnits)/(0.001F),
                Mathf.Log(1F + 0.001F * m_Velocity.z * Time.fixedDeltaTime * m_MetersToUnits)/(0.001F));
            
            m_Velocity = new Vector3(
                m_Velocity.x / (1F + (0.001F) * m_Velocity.x * Time.fixedDeltaTime),
                m_Velocity.y / (1F + (0.001F) * m_Velocity.y * Time.fixedDeltaTime),
                m_Velocity.z / (1F + (0.001F) * m_Velocity.z * Time.fixedDeltaTime));
        }
        else
        {
            moveVec += m_Velocity * Time.fixedDeltaTime * m_MetersToUnits;
            m_Velocity += m_Acceleration * Time.fixedDeltaTime;
        }

        return moveVec;
    }

    /// <summary>
    /// Modifies the mass of the object.
    /// </summary>
    /// <param name="deltaMass"></param>
    private void ModifyMass(float deltaMass)
    {
        float newMass = m_Mass + deltaMass;
        if (newMass >= m_MinMass && newMass <= m_MaxMass) m_Mass = newMass;
    }

    /// <summary>
    /// Moves this MovablePhysicsObjects.
    /// </summary>
    /// <param name="moveVec">The movement vector that the object should move with.</param>
    public void Move(Vector3 moveVec)
    {
        GetComponent<Transform>().Translate(moveVec);
        MoveAttached(moveVec);
        m_Position = transform.localPosition / m_MetersToUnits;
    }

    /// <summary>
    /// Moves all the attached MovablePhysicsObjects that this MovablePhysicsObjects has.
    /// </summary>
    /// <param name="moveVec">The movement vector that the objects should move with.</param>
    private void MoveAttached(Vector3 moveVec)
    {
        if(m_Attached.Count > 0)
        {
            foreach(MovablePhysicsObject obj in m_Attached)
            {
                obj.GetComponent<Transform>().Translate(moveVec);
                obj.m_MinLocation.x += moveVec.x / obj.m_MetersToUnits;
                obj.m_MinLocation.y += moveVec.y / obj.m_MetersToUnits;
                obj.m_MaxLocation.x += moveVec.x / obj.m_MetersToUnits;
                obj.m_MaxLocation.y += moveVec.y / obj.m_MetersToUnits;
            }
        }
    }

    /// <summary>
    /// Updates the position of the COMTarget uf ut exists.
    /// </summary>
    private void UpdateCOMTarget()
    {
        if (m_ComTarget == null) return;

        m_CenterOfMass = new Vector2();
        m_CenterOfMass.x = SumMX() / TotalMass();
        m_CenterOfMass.y = SumMY() / TotalMass();

        m_ComTarget.localPosition = m_CenterOfMass * m_MetersToUnits;
    }

    /// <summary>
    /// The total mass * x position of the MovablePhysicsObject.
    /// </summary>
    /// <returns>total mass * x position of the MovablePhysicsObject</returns>
    private float SumMX()
    {
        float sum = 0;

        foreach (MovablePhysicsObject obj in GetAllOBjects())
        {
            sum += obj.m_Mass * obj.GetComponent<Transform>().localPosition.x / obj.m_MetersToUnits;
        }

        return sum;
    }

    /// <summary>
    /// The total mass * y position of the MovablePhysicsObject.
    /// </summary>
    /// <returns>total mass * y position of the MovablePhysicsObject</returns>
    private float SumMY()
    {
        float sum = 0;

        foreach (MovablePhysicsObject obj in GetAllOBjects())
        {
            sum += obj.m_Mass * obj.GetComponent<Transform>().localPosition.y / obj.m_MetersToUnits;
        }

        return sum;
    }

    /// <summary>
    /// The total mass of the MovablePhysicsObject.
    /// </summary>
    /// <returns>total mass of the MovablePhysicsObject</returns>
    private float TotalMass()
    {
        float sum = 0;

        foreach (MovablePhysicsObject obj in GetAllOBjects()) sum += obj.m_Mass;
        
        return sum;
    }

    /// <summary>
    /// Calculates the Total Moment of Inertia of a MovablePhysicsObject
    /// </summary>
    /// <returns>Total Moment of Inertia of a MovablePhysicsObject</returns>
    private float TotalMOI()
    {
        float sum = 0;

        foreach (MovablePhysicsObject obj in GetAllOBjects())
        {
            float L = (obj.GetComponent<SpriteRenderer>().bounds.size.x) / obj.m_MetersToUnits;
            float W = (obj.GetComponent<SpriteRenderer>().bounds.size.y) / obj.m_MetersToUnits;
            sum += (1F / 12F) * obj.m_Mass * (L * L + W * W) + obj.MH2();
        }

        return sum;
    }

    /// <summary>
    /// Calculates the result of Mass * Height * Height of the MovablePhysicsObject.
    /// </summary>
    /// <returns>result of Mass * Height * Height of the MovablePhysicsObject</returns>
    private float MH2()
    {
        float result = 0;
        Vector3 com = new Vector3(m_CenterOfMass.x, m_CenterOfMass.y, 0);
        float h = (com - GetComponent<Transform>().localPosition / m_MetersToUnits).magnitude;
        result = m_Mass * h * h;

        return result;
    }

    /// <summary>
    /// Creates a list of all objects attached to the this MovablePhysicsObject and as well as itself.
    /// </summary>
    /// <returns>list of all objects attached to the this MovablePhysicsObject and as well as itself</returns>
    private List<MovablePhysicsObject> GetAllOBjects()
    {
        List<MovablePhysicsObject> objects = new List<MovablePhysicsObject>();
        objects.Add(this);
        objects.AddRange(m_Attached);

        return objects;
    }
}
