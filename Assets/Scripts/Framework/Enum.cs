using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotsOfTowers.Framework
{
	public enum State
	{
		Active,
		Inactive,
		Deleted,
		Invisible,
		Charging
	}

    public enum Direction
    {
        Forward,
        Backward,
        Left,
        Right
    }

    public enum TypeCollider
    {
        Player,
        Elephant
    }
}
