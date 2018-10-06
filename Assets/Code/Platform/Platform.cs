using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
	public virtual PlatformType PlatformType()
	{
		return global::PlatformType.Base;
	}
}

public enum PlatformType
{
	Base,
	Grass,
	HorizontalMovingGrass
}
