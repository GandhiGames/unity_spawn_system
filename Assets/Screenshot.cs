using UnityEngine;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;

public class Screenshot : MonoBehaviour
{
	public int SuperSize = 1;

	private string ScreenShotName ()
	{
		return string.Format ("/screen_{0}.png",  
		                     System.DateTime.Now.ToString ("yyyy-MM-dd_HH-mm-ss"));
	}
	
	private string Directory ()
	{
		return string.Format ("/Users/robert/Dropbox/Work/Unity/screenshots/{0}", 
		                      Regex.Replace (Application.productName.ToString ().Trim (), "( )+", ""));
	}
	
	
	void LateUpdate ()
	{
		if (Input.GetKeyDown ("k")) {
			
			if (!System.IO.Directory.Exists (Directory ())) {
				System.IO.Directory.CreateDirectory (Directory ());
				Debug.Log ("Directory created: " + Directory ());
			}
			
			Application.CaptureScreenshot (Directory () + ScreenShotName (), SuperSize);
			Debug.Log ("Screenshot saved to: " + Directory () + ScreenShotName ());

		}
	}

}
