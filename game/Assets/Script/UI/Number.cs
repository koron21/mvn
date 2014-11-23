using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Number : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		mKetaSprite = new List<SpriteRenderer>();
		foreach(SpriteRenderer obj in FindObjectsOfType<SpriteRenderer>()) {
			if( obj.transform.parent.gameObject == this.gameObject ) {
				mKetaSprite.Add( obj );
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		List<int> ketaList = new List<int>();
		int tempNumber = DispNumber;
		while(tempNumber >= 0) {
			int temp = tempNumber % 10;
			ketaList.Add(temp);

			tempNumber /= 10;
			if( tempNumber == 0 ) {
				break;
			}
		}

		if( ketaList.Count > mKetaSprite.Count ) {
			print (mKetaSprite.Count);
			throw new UnityException();
		}
		else {
			float width = 0.85f;
			float baseX = (float)(ketaList.Count - 1) * width * 0.5f;
			for(int i=0; i<mKetaSprite.Count; i++) {
				if( i >= ketaList.Count ) {
					mKetaSprite[i].color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
				}
				else {
					mKetaSprite[i].transform.localPosition = new Vector3( baseX - width * (float)i, 0.0f, 0.0f );
					mKetaSprite[i].sprite = NumberSprite[ketaList[i]];
					mKetaSprite[i].color = DispColor;
				}
			}
		}
	}

	public Sprite[] NumberSprite = new Sprite[10];
	public int DispNumber = 0;
	public Color DispColor;
	private List<SpriteRenderer> mKetaSprite;
}
