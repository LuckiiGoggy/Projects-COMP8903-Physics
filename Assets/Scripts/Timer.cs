using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Timer modifies a UI Text object based on the current time with a specified
/// start time and end time. Pauses the 
/// </summary>
public class Timer : MonoBehaviour {

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

	// Use this for initialization
	void Start () {
        Reset();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (m_CurrTime > m_EndTime)
        {
            m_CurrTime -= Time.fixedDeltaTime;

            m_CurrTime = m_CurrTime < m_EndTime ? m_EndTime : m_CurrTime;

            GetComponent<Text>().text = m_CurrTime.ToString("F1") + "s";
        }

	}

    /// <summary>
    /// Resets the timer.
    /// </summary>
    public void Reset()
    {
        m_CurrTime = m_StartTime;
    }
}
