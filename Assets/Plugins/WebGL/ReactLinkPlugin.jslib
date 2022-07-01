mergeInto(LibraryManager.library, {
   sendUsername: function(username)
   {
      console.log(username);
      window.dispatchReactUnityEvent(
      "sendUsername", Pointer_stringify(username)
    );
   },
});