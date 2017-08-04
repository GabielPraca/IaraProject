package md57cbc051877f5a4faa21469a5d8b0b917;


public class MyScrollListener
	extends android.support.v7.widget.RecyclerView.OnScrollListener
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Iara.MyScrollListener, Iara, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MyScrollListener.class, __md_methods);
	}


	public MyScrollListener () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MyScrollListener.class)
			mono.android.TypeManager.Activate ("Iara.MyScrollListener, Iara, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
