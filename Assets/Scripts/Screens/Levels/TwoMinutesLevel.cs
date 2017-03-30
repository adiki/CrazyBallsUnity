using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoMinutesLevel : Level
{
	protected override int gameTime () 
	{
		return 120;
	}

	protected override int pointsForStar1 ()
	{
		return 10;
	}

	protected override int pointsForStar2 ()
	{
		return 15;
	}

	protected override int pointsForStar3 ()
	{
		return 20;
	}
}
