using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomness
{

	public static int Sign ()
	{
		return Random.value < .5 ? 1 : -1;
	}
}
