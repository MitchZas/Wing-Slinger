using UnityEngine;
using System.Collections;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	protected static T instance;


	private static T FindInstance(){
		return (T) FindObjectOfType(typeof(T));
	}

	public static T Instance{
		get {
			if(instance == null) {
				instance = FindInstance();

				if (instance == null){
					Debug.LogError("An instance of " + typeof(T) + 
						" is needed in the scene, but there is none.");
				}
			}

			return instance;
		}
	}

	public static T GetOrCreateInstance(){
		if (instance == null) {
			instance = FindInstance();

			if (instance == null) {
				instance = new GameObject ().AddComponent<T> ();
				instance.transform.name = instance.GetType ().ToString ();
			}
		}

		return instance;
	}
}
