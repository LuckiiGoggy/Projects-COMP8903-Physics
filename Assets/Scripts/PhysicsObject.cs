using UnityEngine;
using System.Collections;

public class PhysicsObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public virtual T GetVariable<T>(string varName)
    {
        return (T)GetType().GetField(varName).GetValue(this);
    }
}
