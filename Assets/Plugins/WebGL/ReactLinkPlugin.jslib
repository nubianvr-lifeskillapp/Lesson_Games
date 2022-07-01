mergeInto(LibraryManager.library, {
   sendUsername: function(username)
   {
     window.dispatchReactUnityEvent(
      "sendUsername", Pointer_stringify(username)
    );
   },
});