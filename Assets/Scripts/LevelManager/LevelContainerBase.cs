using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelContainerBase : MonoBehaviour
{
	public static LevelContainerBase Instance;

	private void Awake ()
	{
		Instance = this;
	}
}
