using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RoundManager.Events
{
	/// <summary>
	/// Round events.
	/// </summary>
	public class RoundEvents
	{
		private static RoundEvents _instance;
		public static RoundEvents Instance {
			get {
				if (_instance == null) {
					_instance = new RoundEvents ();
				}

				return _instance;
			}
		}

		public delegate void EventDelegate<T> (T e) where T: RoundEvent;
		private delegate void _eventDelegate (RoundEvent e);

		private Dictionary<System.Type, _eventDelegate> delegates = new Dictionary<System.Type, _eventDelegate> ();
		private Dictionary<System.Delegate, _eventDelegate> delegateLookup = new Dictionary<System.Delegate, _eventDelegate> ();

		/// <summary>
		/// Adds listener to be called when event T is called.
		/// </summary>
		/// <param name="del">Del.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public void AddListener<T> (EventDelegate<T> del) where T : RoundEvent
		{
			_eventDelegate internalDelegate = (e) => del ((T)e);
			if (delegateLookup.ContainsKey (del) && delegateLookup [del] == internalDelegate) {
				return;
			}

			delegateLookup [del] = internalDelegate;

			_eventDelegate tempDel;
			if (delegates.TryGetValue (typeof(T), out tempDel)) {
				delegates [typeof(T)] = tempDel += internalDelegate; 
			} else {
				delegates [typeof(T)] = internalDelegate;
			}
		}

		/// <summary>
		/// Removes listener for event T.
		/// </summary>
		/// <param name="del">Del.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public void RemoveListener<T> (EventDelegate<T> del) where T : RoundEvent
		{
			_eventDelegate internalDelegate;
			if (delegateLookup.TryGetValue (del, out internalDelegate)) {
				_eventDelegate tempDel;
				if (delegates.TryGetValue (typeof(T), out tempDel)) {
					tempDel -= internalDelegate;
					if (tempDel == null) {
						delegates.Remove (typeof(T));
					} else {
						delegates [typeof(T)] = tempDel;
					}
				}
				
				delegateLookup.Remove (del);
			}
		}

		/// <summary>
		/// Raise the specified event e.
		/// </summary>
		/// <param name="e">E.</param>
		public void Raise (RoundEvent e)
		{
			_eventDelegate del;
			if (delegates.TryGetValue (e.GetType (), out del)) {
				del.Invoke (e);
			}
		}

	}
}
