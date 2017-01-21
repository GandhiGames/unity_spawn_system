using UnityEngine;
using System.Collections;

namespace RoundManager
{
	/// <summary>
	/// Lerp from one float to another over time.
	/// </summary>
	public class LerpOverTime
	{
		private float startTime;
	
		private float t;
		private float f;
		private float d;
	
		private bool started = false;
	
		public float ElapsedTime {
			get {
				if (started) 
					return Time.time - startTime;
				else
					return 0;
			}
		}
	
		/// <summary>
		/// Current lerped value.
		/// </summary>
		/// <value>The value.</value>
		public float Value {
			get {
				if (started) 
					return Mathf.SmoothStep (f, t, (ElapsedTime) / d);
				else 
					return f;
			}
		}
	
		public LerpOverTime (float from, float to, float duration)
		{
			t = to;
			f = from;
			d = duration;
		}
	
		public void Start ()
		{
			startTime = Time.time;
			started = true;
		}
	}
}
