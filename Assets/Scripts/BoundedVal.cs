using System;
using UnityEngine;







/// <summary>
/// Contains helper methods for managing vector3s with min and max
/// </summary>
public class BoundedVal
{
	public static Vector3 KeepInBounds(Vector3 val, Vector3 min, Vector3 max){
		val.x = val.x > max.x ? max.x : val.x;
		val.x = val.x < min.x ? min.x : val.x;
		val.y = val.y > max.y ? max.y : val.y;
		val.y = val.y < min.y ? min.y : val.y;
		val.z = val.z > max.z ? max.z : val.y;
		val.z = val.z < min.z ? min.z : val.y;	
	
		return val;
	}

	public static float KeepInBounds(float val, float min, float max){
		val = val > max ? max : val;
		val = val < min ? min : val;

		return val;
	}

}



