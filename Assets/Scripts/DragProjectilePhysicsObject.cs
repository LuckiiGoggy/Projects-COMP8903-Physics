using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DragProjectilePhysicsObject : ProjectilePhysicsObject {



	public float m_Cd;
	public float m_Cw;
	public float m_G;

	public Vector3 m_InitVec;
	public Vector3 m_CurrVec;

	public float m_FancyT;

	public float m_W;

	public float m_Gamma;
	public float m_Alpha;

	public ProjectileLauncher3D cannon;

	// Use this for initialization
	void Start () {
		m_SelfMPO = GetComponent<MovablePhysicsObject>();
		m_TrajectoryIndicators = new List<Transform>();
		m_Explosion = GetComponent<AudioSource>();

		m_Cd = 0.2f;
		m_Cw = 0.1f;
		m_W = 10;
		m_FancyT = m_SelfMPO.m_Mass / m_Cd;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){
		m_Gamma = cannon.GetComponent<RotatablePhysicsObject3D> ().m_Angles.y;
		m_Alpha = cannon.GetComponent<RotatablePhysicsObject3D> ().m_Angles.x;


		if (m_InFlight) m_TrajectoryIndicators.Add(Instantiate(m_TrajectoryIndicator, transform.position, transform.rotation) as Transform);
		if (m_InFlight) {
			UpdatePosition ();
			UpdateVelocity ();
		}
		if (m_SelfMPO.m_Position.y < 0)
		{
			StopProjectile ();
		}

		if((m_SelfMPO.m_Position - m_Target.m_Position).magnitude < 2)
		{
			if(m_Explosion != null)
				m_Explosion.Play();
			StopProjectile ();
		}


	}


	void UpdatePosition(){
		float eChunk = (1 - Mathf.Exp (-Time.fixedDeltaTime / m_FancyT));

		Vector3 delta = new Vector3 ();

		delta.x = (m_CurrVec.x * m_FancyT * eChunk) + 
			(((m_Cw * m_W * Mathf.Cos (m_Gamma)) / m_Cd) * m_FancyT * eChunk) - 
			(((m_Cw * m_W * Mathf.Cos (m_Gamma)) / m_Cd) * Time.fixedDeltaTime);
		delta.y = (m_CurrVec.y * m_FancyT * eChunk) +
			((m_G * m_FancyT * m_FancyT * eChunk)) -
			(m_G * m_FancyT * Time.fixedDeltaTime);
		delta.z = (m_CurrVec.z * m_FancyT * eChunk) + 
			(((m_Cw * m_W * Mathf.Sin (m_Gamma)) / m_Cd) * m_FancyT * eChunk) - 
			(((m_Cw * m_W * Mathf.Sin (m_Gamma)) / m_Cd) * Time.fixedDeltaTime);

		m_SelfMPO.Move (delta * m_SelfMPO.m_MetersToUnits);
	}

	void UpdateVelocity(){
		float e = Mathf.Exp (-Time.fixedDeltaTime / m_FancyT);

		m_CurrVec.x = e * m_CurrVec.x + (e - 1) * (m_Cw * m_W * Mathf.Cos(m_Gamma))/m_Cd; 
		m_CurrVec.y = e * m_CurrVec.y + (e - 1) * m_G * m_FancyT;
		m_CurrVec.z = e * m_CurrVec.z + (e - 1) * (m_Cw * m_W * Mathf.Sin(m_Gamma))/m_Cd; 
	}

	public override void ApplyGravity (Vector3 vec)
	{
		m_G = -vec.y;
	}

	public override void ApplyVelocity(Vector3 vec)
	{
		m_InitVec = vec;
		m_CurrVec = vec;
	}
}
