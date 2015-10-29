using System;

public class JavaHolder : Java.Lang.Object
{
	public object Instance;

	public JavaHolder (object instance)
	{
		Instance = instance;
	}
}

