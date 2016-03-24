using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DragCannon : MonoBehaviour {

	public ProjectilePhysicsObject DragCannonBall;
	public KeyCode ActivateDrag;


	public Text t_W;
	public Text t_Cw;
	public Text t_Cd;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (ActivateDrag))
			ActivateDragGun ();
		DragProjectilePhysicsObject d = DragCannonBall.GetComponent<DragProjectilePhysicsObject> ();
		t_W.text = "Wind: " + d.m_W;
		t_Cw.text = "Cw: " + d.m_Cw;
		t_Cd.text = "Cd: " + d.m_Cd;
	}

	void ActivateDragGun(){
		GameObject.Destroy(GetComponent<ProjectileLauncher3D> ().m_ProjectileToLaunch);
		GetComponent<ProjectileLauncher3D> ().m_ProjectileToLaunch = DragCannonBall;
		GetComponent<ProjectileLauncher3D> ().Reset ();
	}
}
