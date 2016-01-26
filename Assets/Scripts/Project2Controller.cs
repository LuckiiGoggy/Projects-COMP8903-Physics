using UnityEngine;
using System.Collections;

/// <summary>
/// Controls the game based on the specifications of Project2.
/// </summary>
public class Project2Controller : MonoBehaviour {

    /// <summary>
    /// KeyCode value to toggle Drag 
    /// </summary>
    public KeyCode m_DragSwitch;
    /// <summary>
    /// KeyCode value to reset the simulation
    /// </summary>
    public KeyCode m_Reset;
    /// <summary>
    /// KeyCode to play or pause the simulation
    /// </summary>
    public KeyCode m_PlayPause;

    /// <summary>
    /// Target Movable Physics Object
    /// </summary>
    public MovablePhysicsObject target;
    /// <summary>
    /// Target Timer for this simulation
    /// </summary>
    public Timer timer;

	// Use this for initialization
	void Start () {
        target = GetComponent<MovablePhysicsObject>();
        Time.timeScale = 0;

        Reset();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(m_DragSwitch)) target.m_IsDragOn = !target.m_IsDragOn;

        if (Input.GetKeyDown(m_Reset)) Reset();

        if(Input.GetKeyDown(m_PlayPause))
        {
            if (Time.timeScale == 0) Time.timeScale = 1;
            else if (Time.timeScale == 1) Time.timeScale = 0;
        }

        if (timer.m_CurrTime == timer.m_EndTime)
        {
            Time.timeScale = 0;
        }
    }

    /// <summary>
    /// Reset the project to the initial states of the target and the timer.
    /// </summary>
    private void Reset()
    {
        target.Move((target.m_InitPosition * target.m_MetersToUnits) - target.GetComponent<Transform>().localPosition);

        target.m_Acceleration = Vector3.zero;
        target.m_IsDragOn = false;
        target.m_Velocity = new Vector3(100, 0, 0);
        target.m_Acceleration = new Vector3(-10, 0, 0);
        timer.m_CurrTime = timer.m_StartTime;
    }
}
