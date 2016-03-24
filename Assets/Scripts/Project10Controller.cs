using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Project10Controller : MonoBehaviour {


	public MovablePhysicsObject LeftObject;
	public MovablePhysicsObject RightObject;
	public Vector3 LeftObjectMinVelocity;
	public Vector3 LeftObjectMaxVelocity;
	public Vector3 RightObjectMinVelocity;
	public Vector3 RightObjectMaxVelocity;
	public Vector3 VelIncrVal;

	public Vector3 LeftObjectInitVelocity;
	public Vector3 RightObjectInitVelocity;

	public KeyCode LeftIncVel;
	public KeyCode LeftDecVel;
	public KeyCode RightIncVel;
	public KeyCode RightDecVel;

	public float CoeffE;
	public float MinCoeffE;
	public float MaxCoeffE;
	public float CoeffEIncrVal;

	public KeyCode IncCoeffE;
	public KeyCode DecCoeffE;

	public KeyCode StartMotion;
	public KeyCode StopMotion;
	public KeyCode Reset;

	public int CollisionCounts;

	public float DstBtwn;

	public Vector3 nHat;
	public float uIn;
	public float vIn;
	public Vector3 uFt;
	public Vector3 vFt;
	public Vector3 uFn;
	public Vector3 vFn;
	public Vector3 tHat;
	public Vector3 n;



	public Text t_MassLeft;
	public Text t_MassRight;
	public Text t_InitVelocityLeft;
	public Text t_InitVelocityRight;
	public Text t_FinalVelocityLeft;
	public Text t_FinalVelocityRight;
	public Text t_CoeffE;
	public Text t_J;
	public Text t_CollCount;
	public Text t_pi;
	public Text t_pf;
	public Text t_n;
	public Text t_KEi;
	public Text t_KEf;

	public float J;

	// Use this for initialization
	void Start () {

		LeftObject.m_Velocity = LeftObjectInitVelocity;
		RightObject.m_Velocity = RightObjectInitVelocity;
	}

	// Update is called once per frame
	void Update () {
		HandleCoeffKeys ();
		HandleLeftVelocityKeys ();
		HandleRightVelocityKeys ();
		HandleStartStopMotion ();
		HandleReset ();

		if (t_MassLeft)
			t_MassLeft.text = "Mass 1: " + LeftObject.m_Mass.ToString("F2");
		if (t_MassRight)
			t_MassRight.text = "Mass 2: " + RightObject.m_Mass.ToString("F2");
		if (t_CoeffE)
			t_CoeffE.text = "e: " + CoeffE.ToString("F2");
		if (t_InitVelocityLeft)
			t_InitVelocityLeft.text = "ui: " + LeftObjectInitVelocity.ToString("F2");
		if (t_InitVelocityRight)
			t_InitVelocityRight.text = "vi: " + RightObjectInitVelocity.ToString("F2");
		if (t_FinalVelocityLeft)
			t_FinalVelocityLeft.text = "uf: " + LeftObject.m_Velocity.ToString("F2");
		if (t_FinalVelocityRight)
			t_FinalVelocityRight.text = "vf: " + RightObject.m_Velocity.ToString("F2");
		if (t_J)
			t_J.text = "J: " + J.ToString("F2");
		if (t_CollCount)
			t_CollCount.text = "Collision Count: " + CollisionCounts.ToString("F2");
		if (t_pi)
			t_pi.text = "pi: " + (LeftObject.m_Mass * LeftObjectInitVelocity) + " + " + (RightObject.m_Mass * RightObjectInitVelocity) + "=" + (LeftObject.m_Mass * LeftObjectInitVelocity + RightObject.m_Mass * RightObjectInitVelocity).ToString("F2");
		if (t_pf)
			t_pf.text = "pf: " + (LeftObject.m_Mass * LeftObject.m_Velocity) + " + " + (RightObject.m_Mass * RightObject.m_Velocity) + "=" + (LeftObject.m_Mass * LeftObject.m_Velocity + RightObject.m_Mass * RightObject.m_Velocity).ToString("F2");

		if (t_n)
			t_n.text = "n: " + (RightObject.m_Position - LeftObject.m_Position);
		if(t_KEi)
			t_KEi.text = "KEi: " + ((0.5f * LeftObject.m_Mass * LeftObjectInitVelocity.sqrMagnitude) + (0.5f * RightObject.m_Mass * RightObjectInitVelocity.sqrMagnitude));
		if(t_KEf)
			t_KEf.text = "KEf: " + ((0.5f * LeftObject.m_Mass * LeftObject.m_Velocity.sqrMagnitude) + (0.5f * RightObject.m_Mass * RightObject.m_Velocity.sqrMagnitude));
	}

	void HandleCoeffKeys(){
		if (Input.GetKeyDown (IncCoeffE))
			CoeffE += CoeffEIncrVal;
		if (Input.GetKeyDown (DecCoeffE))
			CoeffE -= CoeffEIncrVal;

		CoeffE = BoundedVal.KeepInBounds (CoeffE, MinCoeffE, MaxCoeffE);
	}

	void HandleLeftVelocityKeys(){
		if (Input.GetKeyDown (LeftIncVel)) {
			//LeftObject.m_Velocity += VelIncrVal;
			LeftObjectInitVelocity += VelIncrVal;
		}
		if (Input.GetKeyDown (LeftDecVel)) {
			//LeftObject.m_Velocity -= VelIncrVal;
			LeftObjectInitVelocity -= VelIncrVal;
		}


		//LeftObject.m_Velocity = BoundedVal.KeepInBounds (LeftObject.m_Velocity, LeftObjectMinVelocity, LeftObjectMaxVelocity);
		LeftObjectInitVelocity = BoundedVal.KeepInBounds (LeftObjectInitVelocity, LeftObjectMinVelocity, LeftObjectMaxVelocity);
	}

	void HandleRightVelocityKeys(){
		if (Input.GetKeyDown (RightIncVel)) {
			//RightObject.m_Velocity += VelIncrVal;
			RightObjectInitVelocity += VelIncrVal;
		}
		if (Input.GetKeyDown (RightDecVel)) {
			//RightObject.m_Velocity -= VelIncrVal;
			RightObjectInitVelocity -= VelIncrVal;
		}

		//RightObject.m_Velocity = BoundedVal.KeepInBounds (RightObject.m_Velocity, RightObjectMinVelocity, RightObjectMaxVelocity);
		RightObjectInitVelocity = BoundedVal.KeepInBounds (RightObjectInitVelocity, RightObjectMinVelocity, RightObjectMaxVelocity);
	}

	void HandleStartStopMotion(){
		if (Input.GetKeyDown (StartMotion)) {
			LeftObject.m_IsActive = true;
			RightObject.m_IsActive = true;
			if (J == 0) {
				LeftObject.m_Velocity = LeftObjectInitVelocity;
				RightObject.m_Velocity = RightObjectInitVelocity;
			}
		}

		if (Input.GetKeyDown (StopMotion)) {
			LeftObject.m_IsActive = false;
			RightObject.m_IsActive = false;
		}
	}

	void HandleReset(){
		if (Input.GetKeyDown (Reset)) {
			LeftObject.Reset ();
			RightObject.Reset ();
			LeftObject.m_Velocity = LeftObjectInitVelocity;
			RightObject.m_Velocity = RightObjectInitVelocity;
			CollisionCounts = 0;
		}

	}

	void FixedUpdate(){
		if (CollisionCounts == 0) {
			DstBtwn = Mathf.Abs((LeftObject.m_Position - RightObject.m_Position).magnitude);

			if(DstBtwn < 40){
				LeftObject.m_Position.x -= 40 - DstBtwn;
			}


			if (DstBtwn <= 40) {
				Vector3 u = LeftObject.m_Velocity;
				Vector3 v = RightObject.m_Velocity;

				n = (RightObject.m_Position - LeftObject.m_Position);



				nHat = n.normalized;
				uIn = Vector3.Dot (u, nHat);
				vIn = Vector3.Dot (v, nHat);


				Vector3 vRn = (uIn - vIn) * nHat;

				Vector3 Jn = -vRn * (CoeffE + 1) * ((LeftObject.m_Mass * RightObject.m_Mass) / (LeftObject.m_Mass + RightObject.m_Mass));

				uFn = Jn / LeftObject.m_Mass + uIn * nHat;
				vFn = -Jn / RightObject.m_Mass + vIn * nHat;

				tHat = Vector3.Cross(Vector3.Cross(nHat,u),nHat).normalized;

				uFt = Vector3.Dot (u, tHat) * tHat;
				vFt = Vector3.Dot (v, tHat) * tHat;

				J = Jn.magnitude;



				LeftObject.m_Velocity = uFn + uFt;
				RightObject.m_Velocity = vFn + vFt;








				CollisionCounts += 1;
			}


		}




	}
}
