using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// TotalMassText modifies a UI Text object to specify 
/// the total mass of a collection of Movable Physics Object
/// for Project 1
/// </summary>
public class TotalMassText : MonoBehaviour {

    /// <summary>
    /// List of MovablePhysicsObject to pull masses from
    /// </summary>
    public List<MovablePhysicsObject> m_Objects; 
	
	// Update is called once per frame
	void FixedUpdate () {
        string text = "Car + Tank + Driver: ";

        if (m_Objects.Count >= 3)
            text += "(" + m_Objects[0].m_Mass + " + " + m_Objects[1].m_Mass + " + " + m_Objects[2].m_Mass + " ) kg ";


        GetComponent<Text>().text = text;
	}
}
