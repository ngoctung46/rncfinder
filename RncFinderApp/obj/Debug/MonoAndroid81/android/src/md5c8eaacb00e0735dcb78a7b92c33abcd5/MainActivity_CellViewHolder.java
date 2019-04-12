package md5c8eaacb00e0735dcb78a7b92c33abcd5;


public class MainActivity_CellViewHolder
	extends android.support.v7.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("RncFinderApp.MainActivity+CellViewHolder, RncFinderApp", MainActivity_CellViewHolder.class, __md_methods);
	}


	public MainActivity_CellViewHolder (android.view.View p0)
	{
		super (p0);
		if (getClass () == MainActivity_CellViewHolder.class)
			mono.android.TypeManager.Activate ("RncFinderApp.MainActivity+CellViewHolder, RncFinderApp", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
	}

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
