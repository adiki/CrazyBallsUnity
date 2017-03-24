using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Hit {

	public Ball collider;
	public DateTime time;

	public Hit(Ball collider, DateTime time)
	{
		this.time = time;
		this.collider = collider;
	}

}
