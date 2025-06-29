mergeInto(LibraryManager.library, {
  GetPropertyAsString: function(propertyNamePtr) {
    const propertyName = UTF8ToString(propertyNamePtr);
    let value = "";
    if (window.Telegram && window.Telegram.WebApp && window.Telegram.WebApp[propertyName]) {
      value = window.Telegram.WebApp[propertyName];
    }
    let bufferSize = lengthBytesUTF8(value) + 1;
    let buffer = _malloc(bufferSize);
    stringToUTF8(value, buffer, bufferSize);
    return buffer;
  },
  GetPropertyAsJsonString: function(propertyNamePtr) {
    const propertyName = UTF8ToString(propertyNamePtr);
    let value = "";
    if (window.Telegram && window.Telegram.WebApp && window.Telegram.WebApp[propertyName]) {
      value = JSON.stringify(window.Telegram.WebApp[propertyName]);
    }
    let bufferSize = lengthBytesUTF8(value) + 1;
    let buffer = _malloc(bufferSize);
    stringToUTF8(value, buffer, bufferSize);
    return buffer;
  },
  GetPropertyAsInt: function(propertyNamePtr) {
    const propertyName = UTF8ToString(propertyNamePtr);
    let value = 0;
    if (window.Telegram && window.Telegram.WebApp && window.Telegram.WebApp[propertyName]) {
      value = window.Telegram.WebApp[propertyName];
    }
    return value;
  },
  GetPropertyAsFloat: function(propertyNamePtr) {
    const propertyName = UTF8ToString(propertyNamePtr);
    let value = 0.0;
    if (window.Telegram && window.Telegram.WebApp && window.Telegram.WebApp[propertyName]) {
      value = window.Telegram.WebApp[propertyName];
    }
    return value;
  },
  GetPropertyAsBool: function(propertyNamePtr) {
    const propertyName = UTF8ToString(propertyNamePtr);
    let value = false;
    if (window.Telegram && window.Telegram.WebApp && window.Telegram.WebApp[propertyName]) {
      value = window.Telegram.WebApp[propertyName];
    }
    return value;
  },
  InvokeMethod: function(methodNamePtr) {
    const methodName = UTF8ToString(methodNamePtr);
    if (window.Telegram && window.Telegram.WebApp) {
      const parts = methodName.split('.');
      if (parts.length > 1) {
        // Handle nested methods like 'MainButton.show'
        window.Telegram.WebApp[parts[0]][parts[1]]();
      } else {
        window.Telegram.WebApp[methodName]();
      }
    }
  },
  InvokeMethodWithParam: function(methodNamePtr, paramPtr) {
    const methodName = UTF8ToString(methodNamePtr);
    const param = UTF8ToString(paramPtr);
    if (window.Telegram && window.Telegram.WebApp) {
      const parts = methodName.split('.');
      if (parts.length > 1) {
        // Handle nested methods like 'MainButton.setText'
        window.Telegram.WebApp[parts[0]][parts[1]](param);
      } else {
        window.Telegram.WebApp[methodName](param);
      }
    }
  },
  InvokeMethodWithJsonStringParam: function(methodNamePtr, paramPtr) {
    const methodName = UTF8ToString(methodNamePtr);
    const param = UTF8ToString(paramPtr);
    const paramObject = JSON.parse(param);
    if (window.Telegram && window.Telegram.WebApp) {
      const parts = methodName.split('.');
      if (parts.length > 1) {
        // Handle nested methods like 'MainButton.show'
        window.Telegram.WebApp[parts[0]][parts[1]](paramObject);
      } else {
        window.Telegram.WebApp[methodName](paramObject);
      }
    }
  },
  InvokeMethodWithParamReturnBoolean: function(methodNamePtr, paramPtr) {
    const methodName = UTF8ToString(methodNamePtr);
    const param = UTF8ToString(paramPtr);
    let result = false;
    if (window.Telegram && window.Telegram.WebApp) {
      const parts = methodName.split('.');
      if (parts.length > 1) {
        // Handle nested methods like 'MainButton.setText'
        result = window.Telegram.WebApp[parts[0]][parts[1]](param);
      } else {
        result = window.Telegram.WebApp[methodName](param);
      }
    }
    return result;
  },
  RegisterEventCallback: function(eventNamePtr, callback) {
    const eventName = UTF8ToString(eventNamePtr);
    if (window.Telegram && window.Telegram.WebApp) {
      const eventCallback = () => {
        dynCall_v(callback);
      };
      if(!window.Telegram.eventCallbacks) {
        window.Telegram.eventCallbacks = {};
      }
      window.Telegram.eventCallbacks[eventName] = eventCallback;
      window.Telegram.WebApp.onEvent(eventName, eventCallback);
    }
  },
  UnregisterEventCallback: function(eventNamePtr) {
    const eventName = UTF8ToString(eventNamePtr);
    if (window.Telegram && window.Telegram.WebApp) {
      const eventCallback = window.Telegram.eventCallbacks ? window.Telegram.eventCallbacks[eventName]: undefined;
      if (eventCallback) {
        window.Telegram.WebApp.offEvent(eventName, eventCallback);
        delete window.Telegram.eventCallbacks[eventName];
      }
    }
  },
  RegisterEventCallbackWithJsonString: function(eventNamePtr, callback) {
    const eventName = UTF8ToString(eventNamePtr);
    if (window.Telegram && window.Telegram.WebApp) {
      const eventCallback = (eventData) => {
        const jsonString = JSON.stringify(eventData);
        const bufferSize = lengthBytesUTF8(jsonString) + 1;
        const buffer = _malloc(bufferSize);
        stringToUTF8(jsonString, buffer, bufferSize);
        dynCall_vi(callback, buffer);
        _free(buffer);
      };
      if(!window.Telegram.eventCallbacks) {
        window.Telegram.eventCallbacks = {};
      }
      window.Telegram.eventCallbacks[eventName] = eventCallback;
      window.Telegram.WebApp.onEvent(eventName, eventCallback);
      console.log("event " + eventName + "Registered");
    }
  },
  InvokeMethodWithBoolParam: function(methodNamePtr, param) {
    const methodName = UTF8ToString(methodNamePtr);
    const paramBool = param != 0;
    if (window.Telegram && window.Telegram.WebApp) {
      const parts = methodName.split('.');
      if (parts.length > 1) {
        window.Telegram.WebApp[parts[0]][parts[1]](paramBool);
      } else {
        window.Telegram.WebApp[methodName](paramBool);
      }
    }
  }
});
