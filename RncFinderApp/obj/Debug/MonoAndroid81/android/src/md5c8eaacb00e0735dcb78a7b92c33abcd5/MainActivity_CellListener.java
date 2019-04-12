package md5c8eaacb00e0735dcb78a7b92c33abcd5;


public class MainActivity_CellListener
	extends android.telephony.PhoneStateListener
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCellLocationChanged:(Landroid/telephony/CellLocation;)V:GetOnCellLocationChanged_Landroid_telephony_CellLocation_Handler\n" +
			"";
		mono.android.Runtime.register ("RncFinderApp.MainActivity+CellListener, RncFinderApp", MainActivity_CellListener.class, __md_methods);
	}


	public MainActivity_CellListener ()
	{
		super ();
		if (getClass () == MainActivity_CellListener.class)
			mono.android.TypeManager.Activate ("RncFinderApp.MainActivity+CellListener, RncFinderApp", "", this, new java.lang.Object[] {  });
	}

	public MainActivity_CellListener (android.widget.TextView p0, android.telephony.TelephonyManager p1)
	{
		super ();
		if (getClass () == MainActivity_CellListener.class)
			mono.android.TypeManager.Activate ("RncFinderApp.MainActivity+CellListener, RncFinderApp", "Android.Widget.TextView&, Mono.Android:Android.Telephony.TelephonyManager, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public void onCellLocationChanged (android.telephony.CellLocation p0)
	{
		n_onCellLocationChanged (p0);
	}

	private native void n_onCellLocationChanged (android.telephony.CellLocation p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
