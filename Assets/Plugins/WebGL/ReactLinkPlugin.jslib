mergeInto(LibraryManager.library, {
   sendUsername: function(username)
   {
      window.dispatchReactUnityEvent(
      "sendUsername", Pointer_stringify(username)
    );
   },
   sendLevelComplete: function(sceneIndex)
   {
      window.dispatchReactUnityEvent("sendLevelComplete", sceneIndex);
   },
   loadLevel: function()
   {
      window.dispatchReactUnityEvent("loadLevel")
   }
});