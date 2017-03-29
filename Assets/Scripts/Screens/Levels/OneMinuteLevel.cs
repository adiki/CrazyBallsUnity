﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneMinuteLevel : Level
{
	protected override int gameTime () 
	{
		return 60;
	}

	protected override int pointsForStar1 ()
	{
		return 5;
	}

	protected override int pointsForStar2 ()
	{
		return 10;
	}

	protected override int pointsForStar3 ()
	{
		return 15;
	}
}
