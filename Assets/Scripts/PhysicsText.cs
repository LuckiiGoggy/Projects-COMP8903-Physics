using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

/// <summary>
/// Physics Test is a Component that is to be added to a UI Text object and sets the text
/// of the UI Text to the specified variable value from the MovablePhysicsObject.
/// It assumses that the object it is added to contains a Text object that can be modified.
/// 
/// It requires that:
/// - a target MovablePhysicsObject is specified
/// - the name of the value to show is specified
/// - the desired format of the the text is specified
///     - "`" defines where the desired value will be printed on
///     - Only the first instance is replaced
///
/// It is possible to specify the string formatting that the user requires.
/// </summary>
public class PhysicsText : MonoBehaviour {

    /// <summary>
    /// Defines the Escape Character where the value is to be insterted in the desired format.
    /// </summary>
    private const string c_EscapeCharacter = "`";

    /// <summary>
    /// The target MovablePhysicsObject that the Text will pull data from.
    /// </summary>
    public PhysicsObject m_TargetObject;

    /// <summary>
    /// The targeted variable that needs to be pulled from the MovablePhysicsObject
    /// </summary>
    public string m_TargetVar;

    /// <summary>
    /// The desired format of printing the data together with any other string required for the text.
    /// </summary>
    public string m_DesiredPrint; // ` gets replaced by the next string on the list, if it exists
    
    /// <summary>
    /// The specified string format that the value should use for its ToString function.
    /// </summary>
    public string m_SpecifiedStringFormat = "";

	/// <summary>
    /// Every update the text is updated with the desired information in the desired format from 
    /// the desired MovablePhysicsObject.
    /// </summary>
	void Update () {
        GetComponent<Text>().text = GetText();
	}

    /// <summary>
    /// Gets the formatted text with the proper information from the targeted MovablePhysicsObject.
    /// </summary>
    /// <returns>The formatted text with the proper information from the targeted MovablePhysicsObject</returns>
    private string GetText()
    {
        string result = m_DesiredPrint;
        

        var target = m_TargetObject.GetType().GetField(m_TargetVar).GetValue(m_TargetObject);

        switch (target.GetType().ToString())
        {
            case "UnityEngine.Vector3":
                result = ReplaceFirst(result, c_EscapeCharacter, (m_TargetObject.GetVariable<Vector3>(m_TargetVar)).ToString(m_SpecifiedStringFormat));
                break;
            case "UnityEngine.Vector2":
                result = ReplaceFirst(result, c_EscapeCharacter, (m_TargetObject.GetVariable<Vector2>(m_TargetVar)).ToString(m_SpecifiedStringFormat));
                break;
            case "System.Single":
                result = ReplaceFirst(result, c_EscapeCharacter, (m_TargetObject.GetVariable<float>(m_TargetVar)).ToString(m_SpecifiedStringFormat));
                break;

            default:
                break;
        }

        return result;
    }


    /// <summary>
    /// Replaces the first instance of a specified keyword in a text with another string.
    /// </summary>
    /// <param name="text">Specified string to search through.</param>
    /// <param name="keyword">Keyword to be replaced.</param>
    /// <param name="replace">String to replace keyword.</param>
    /// <returns>Modified string</returns>
    private string ReplaceFirst(string text, string keyword, string replace)
    {
        int ndex = text.IndexOf(keyword);

        return ndex < 0 ? text : text.Substring(0, ndex) + replace + text.Substring(ndex + keyword.Length);
    }

}
