using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShipPhysicsObjectText : MonoBehaviour {
	#region
	/// <summary>
	/// UI Text Labels 
	/// </summary>
	public Text MassLabel;
	public Text DepthLabel;
	public Text PercentageDragLabel;
	public Text TerminalVelocityLabel;
	public Text PositionLabel;
	public Text VelocityLabel;
	public Text AccelerationLabel;
	public Text TauLabel;

	#endregion

	public ShipPhysicsObject TargetObject;
	// Use this for initialization
	void Start () {
		if (TargetObject == null)
			TargetObject = GetComponent<ShipPhysicsObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (MassLabel != null)
			MassLabel.text = "Mass: " + TargetObject.Mass.ToString("F2") + " m";
		if (DepthLabel != null)
			DepthLabel.text = "Depth: " + (-TargetObject.Position.y).ToString("F2") + " m";
		if (PercentageDragLabel != null)
			PercentageDragLabel.text = "Drag: " + (TargetObject.DragCoefficient / ShipPhysicsObject.MaximumDragCoefficient).ToString("F2");
		if (TerminalVelocityLabel != null)
			TerminalVelocityLabel.text = "T/C: " + (TargetObject.Thrust / TargetObject.DragCoefficient).ToString("F2") + " m/s";
		if (PositionLabel != null)
			PositionLabel.text = "p: " + TargetObject.Position.x.ToString("F2") + " m";
		if (VelocityLabel != null)
			VelocityLabel.text = "v: " + TargetObject.Velocity.x.ToString("F2") + " m/s";
		if (AccelerationLabel != null)
			AccelerationLabel.text = "a: " + TargetObject.Acceleration.x.ToString("F2") + " m/s^2";
		if (TauLabel != null)
			TauLabel.text = "Tau: " + (TargetObject.Mass / TargetObject.DragCoefficient).ToString("F2") + " s";
	}
}
