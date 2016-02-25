using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShipPhysicsObject : MonoBehaviour {

	#region
	/// <summary>
	/// Boat Information
	/// </summary>
	public float Mass;
	public float Length;
	public float Width;
	public float MaximumMass;
	public float MinimumMass;
	#endregion

	#region
	/// <summary>
	/// Boat Physical Status
	/// </summary>
	public float Thrust;
	public Vector3 Position; //x-position, depth, y-position(from overhead)
	public Vector3 Velocity;
	public Vector3 Acceleration;
	public float Bouyancy;
	public float DragCoefficient;
	#endregion

	public Timer Time;
	public float lastTime;
	public float deltaTime;
	/// <summary>
	/// The maximum drag coefficient.
	/// </summary>
	public const float MaximumDragCoefficient = 10000000.0f;

	public KeyCode IncreaseMass;
	public KeyCode DecreaseMass;
	public float MassIncrement;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!Time.m_IsStopped) {
			deltaTime = Time.m_CurrTime - lastTime;
			UpdateBouyancy ();
			UpdateDragCoefficient ();
			UpdatePosition ();
			UpdateVelocity ();
			UpdateAcceleration ();
			lastTime = Time.m_CurrTime;
		}
	}

	void Update(){
		HandleKeyInputs ();
	}

	void HandleKeyInputs (){
		Mass = Input.GetKeyDown (IncreaseMass) && Mass + MassIncrement <= MaximumMass ? Mass + MassIncrement : Mass;
		Mass = Input.GetKeyDown (DecreaseMass) && Mass - MassIncrement >= MinimumMass ? Mass - MassIncrement : Mass;
	}

	void UpdateBouyancy(){
		Bouyancy = Length * Width;
	}

	void UpdateDragCoefficient(){
		DragCoefficient = MaximumDragCoefficient * Mass / MaximumMass;
	}

	void UpdatePosition(){
		Position.x = Position.x + Thrust / DragCoefficient * deltaTime + (Thrust - DragCoefficient * Velocity.x)/DragCoefficient * Mass/DragCoefficient * (Mathf.Exp(-DragCoefficient*deltaTime/Mass)-1);
		Position.y = -(Mass / (Length * Width * Bouyancy));

		transform.localPosition = Position / 100;
	}

	void UpdateVelocity(){
		Velocity.x = 1 / DragCoefficient * (Thrust - Mathf.Exp (-DragCoefficient * deltaTime / Mass) * (Thrust - DragCoefficient * Velocity.x));
	}

	void UpdateAcceleration(){
		Acceleration.x = (Thrust - DragCoefficient * Velocity.x) / Mass;
	}
}
