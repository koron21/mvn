using System.Collections;
using UnityEngine;

[System.Serializable]
public class TimeString
{
	[Range(0.0f, 10.0f)]
	public float T;
	public string S;
}

public class HelloWorld : MonoBehaviour
{
	public TimeString[] TS;
	private int pos;

	// Use this for initialization
	void Start ()
	{
		pos = 0;
		StartCoroutine(WaitAndPrint());
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	private IEnumerator WaitAndPrint()
	{
		while (true)
		{
			if (TS == null)
				yield return new WaitForSeconds(0.5f);

			float time = TS[pos].T;
			string message = TS[pos].S;

			guiText.text = message;
			pos ++;

			if (pos >= TS.Length)
			{
				pos = 0;
			}

			yield return new WaitForSeconds(time);
		}
	}
}
