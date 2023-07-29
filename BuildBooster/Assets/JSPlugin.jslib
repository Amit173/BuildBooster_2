mergeInto(LibraryManager.library, {  

  StringReturnValueFunction: function (str) {
    var returnStr = UTF8ToString(str);    
    OnSceneLoaded(returnStr);
  },
});