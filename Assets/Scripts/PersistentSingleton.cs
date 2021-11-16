using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentSingleton : MonoBehaviour
{
	public static PersistentSingleton singleton;

    void Awake()
    {
		//if i am NOT the first one, this is a reload of the scene; self-destroy
        if (singleton)
		{
			Destroy(this.gameObject);
			return;
		}

		//if i survived the above, i AM the first one, so establish myself on the throne :)
		singleton = this;
		DontDestroyOnLoad(this.gameObject);
    }

}
