//    [DllImport("__Internal")]
//    private static extern void WindowAlert(string message);




var HandleIO = {
    WindowAlert : function(message)
    {
        window.alert(Pointer_stringify(message));
    },
    SyncFiles : function()
	{
		FS.syncfs(false, function(err) {
			// handle callback
		});
	}
	};

mergeInto(LibraryManager.library, HandleIO);
