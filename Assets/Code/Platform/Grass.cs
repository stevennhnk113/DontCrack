using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Grass : Platform
{
	public override PlatformType PlatformType()
	{
		return global::PlatformType.Grass;
	}
}