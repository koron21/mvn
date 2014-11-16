﻿using UnityEngine;
using System.Collections;

public class DropCoin : DropUnit 
{
	//==========================================================================
	// Unity Functions
	//==========================================================================
	// Use this for initialization
	protected override void Start()
	{
		base.Start();

		mRotForward = (Random.Range(0,2) == 0) ? 1 : -1;
	}
	
	// Update is called once per frame
	protected override void Update()
	{
		base.Update();

		// rotation
		addRotation(RotateSpeed * mRotForward, 0.0f, 0.0f);
		//addRotation(0.0f, RotateSpeed * mRotForward, 0.0f);
	}

	//==========================================================================
	// Public Member Variables
	//==========================================================================
	//! Rotation Speed
	public float RotateSpeed = 500.0f;

	//==========================================================================
	// Private Member Variables
	//==========================================================================
	//! Do Rotate Forward Direction
	private int mRotForward = 1;
}
