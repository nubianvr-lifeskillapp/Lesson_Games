mergeInto(LibraryManager.library, {
  printPoints: function (score) {
    window.dispatchReactUnityEvent(
      "printPoints", score
    );
  },

   sendUsername: function(username)
   {
     window.dispatchReactUnityEvent(
      "sendUsername", username
    );
   }
});