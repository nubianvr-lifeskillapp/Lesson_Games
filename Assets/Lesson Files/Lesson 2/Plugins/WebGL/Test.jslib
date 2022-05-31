mergeInto(LibraryManager.library, {
  printPoints: function (score) {
    window.dispatchReactUnityEvent(
      "printPoints", score
    );
  },
});