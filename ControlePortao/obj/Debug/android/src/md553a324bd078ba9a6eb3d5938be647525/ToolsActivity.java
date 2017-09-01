package md553a324bd078ba9a6eb3d5938be647525;


public class ToolsActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("ControlePortao.ToolsActivity, ControlePortao, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ToolsActivity.class, __md_methods);
	}


	public ToolsActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == ToolsActivity.class)
			mono.android.TypeManager.Activate ("ControlePortao.ToolsActivity, ControlePortao, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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
