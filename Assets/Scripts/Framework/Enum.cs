using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nimbi.Framework
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
        Right,
		Up,
		Down
    }

    public enum TypeCollider
    {
        Player,
        Elephant
    }

	public enum TriggerType
	{
		Start,
		End
	}
}
