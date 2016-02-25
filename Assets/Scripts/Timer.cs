using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Timer modifies a UI Text object based on the current time with a specified
/// start time and end time. Pauses the 
/// </summary>
public class Timer : MonoBehaviour {

    /// <summary>
    /// The label to prepend to the time print out
    /// </summary>
    public string m_Label;
    /// <summary>
    /// Start time of the timer
    /// </summary>
    public float m_StartTime;
    /// <summary>
    /// Current time of the timer
    /// </summary>
    public float m_CurrTime;
    /// <summary>
    /// End time of the timer
    /// </summary>
    public float m_EndTime;

    /// <summary>
    /// Defines if the timer counts up or counts down
    /// </summary>
    public bool m_IsCountDown;

    /// <summary>
    /// Defines if the time is stopped.
    /// </summary>
    public bool m_IsStopped;

	/// <summary>
	/// KeyCode for starting the time
	/// </summary>
	public KeyCode m_StartTimer;

	// Use this for initialization
	void Start () {
        Reset();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Input.GetKeyDown(m_StartTimer)) m_IsStopped = false;
        if (m_IsStopped) return;

        if(m_IsCountDown)
        {
            if (m_CurrTime > m_EndTime)
            {
                m_CurrTime -= Time.fixedDeltaTime;

                m_CurrTime = m_CurrTime < m_EndTime ? m_EndTime : m_CurrTime;

                GetComponent<Text>().text = m_Label + m_CurrTime.ToString("F1") + "s";

            }
            else
            {
                Time.timeScale = 0;
            }
        }
        else
        {
            if (m_CurrTime < m_EndTime)
            {
                m_CurrTime += Time.fixedDeltaTime;

                m_CurrTime = m_CurrTime > m_EndTime ? m_EndTime : m_CurrTime;

                GetComponent<Text>().text = m_Label + m_CurrTime.ToString("F1") + "s";
            }
            else
            {

                Time.timeScale = 0;
            }
        }

	}

    /// <summary>
    /// Resets the timer.
    /// </summary>
    public void Reset()
    {
        m_CurrTime = m_StartTime;
		GetComponent<Text>().text = m_Label + m_CurrTime.ToString("F1") + "s";
    }
}
