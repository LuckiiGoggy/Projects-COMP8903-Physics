using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GunText3D : MonoBehaviour {

    public ProjectileLauncher3D m_Gun;

    public TextType m_TextType;

    public enum TextType { Velocity, Range, CorrectGunAngleAlpha, CorrectGunAngleGamma };

    /// <summary>
    /// Defines the Escape Character where the value is to be insterted in the desired format.
    /// </summary>
    private const string c_EscapeCharacter = "`";

    /// <summary>
    /// The desired format of printing the data together with any other string required for the text.
    /// </summary>
    public string m_DesiredPrint; // ` gets replaced by the next string on the list, if it exists

    /// <summary>
    /// The specified string format that the value should use for its ToString function.
    /// </summary>
    public string m_SpecifiedStringFormat = "";

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        string result = m_DesiredPrint;
        switch (m_TextType)
        {
            case TextType.Velocity:
                result = ReplaceFirst(result, c_EscapeCharacter, m_Gun.m_ProjectileInitialVelocityMagnitude.ToString(m_SpecifiedStringFormat));
                break;

            case TextType.Range:
                result = ReplaceFirst(result, c_EscapeCharacter, m_Gun.m_Range.ToString(m_SpecifiedStringFormat));
                break;

            case TextType.CorrectGunAngleAlpha:
                result = ReplaceFirst(result, c_EscapeCharacter, m_Gun.m_CorrectGunAngleAlpha.ToString(m_SpecifiedStringFormat));
                break;

            case TextType.CorrectGunAngleGamma:
                result = ReplaceFirst(result, c_EscapeCharacter, m_Gun.m_CorrentGunAngleGamma.ToString(m_SpecifiedStringFormat));
                break;

            default:
                break;
        }

        GetComponent<Text>().text = result;
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
