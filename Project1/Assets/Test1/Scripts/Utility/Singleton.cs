﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SN.Base
{
	public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static T mInstance = null;

		public bool _PersistentOnSceneChange = false;

		protected Singleton() { }

		public static T pInstance
		{
			get
			{
				if (mInstance == null)
					mInstance = FindObjectOfType<T>();

				return mInstance;
			}
		}

		protected virtual void Awake()
		{
			if (_PersistentOnSceneChange)
				DontDestroyOnLoad(gameObject);
		}

		protected virtual void Start()
		{
			T[] instance = FindObjectsOfType<T>();
			if (instance.Length > 1)
			{
				Debug.Log(gameObject.name + " has been destroyed because another object already has the same component.");
				Destroy(gameObject);
			}
		}

		protected virtual void OnDestroy()
		{
			if (this == mInstance)
				mInstance = null;
		}
	}
}