angular
  .module('storageService', [])
  .factory('StorageSvc', ['$window', function ($window) {
  	return {
  		setLocal: function (key, value) {
  			try {
  				if ($window.Storage) {
  					$window.localStorage.setItem(key, $window.JSON.stringify(value));
  					return true;
  				} else {
  					return false;
  				}
  			} catch (error) {
  				console.error(error, error.message);
  			}
  		},
  		getLocal: function (key) {
  			try {
  				if ($window.Storage) {
  					return $window.JSON.parse($window.localStorage.getItem(key));
  				} else {
  					return false;
  				}
  			} catch (error) {
  				console.error(error, error.message);
  			}
  		},
  		getLocalAll: function () {
  			try {
  				if ($window.Storage) {
  					var _json = '{"Data": [';
  					for (var i = 0; i < $window.localStorage.length; i++) {
  						_json += $window.localStorage.getItem($window.localStorage.key(i));
  						_json += ',';
  					}

  					if($window.localStorage.length > 0){ _json = _json.substring(0, _json.length - 1); }
  	
  					_json += ']}'

  					return $window.JSON.parse(_json);
  				} else {
  					return false;
  				}
  			} catch (error) {
  				console.error(error, error.message);
  			}
  		},
  		removeLocal: function (key) {
  			try {
  				if ($window.Storage) {
  					$window.localStorage.removeItem(key);
  					return true;
  				} else {
  					return false;
  				}
  			} catch (error) {
  				console.error(error, error.message);
  			}
  		},
  		clearLocal: function()
  		{
  			try {
  				if ($window.Storage) {
  					$window.localStorage.clear();
  				} else {
  					return false;
  				}
  			} catch (error) {
  				console.error(error, error.message);
  			}
  		},
  		setSession: function (key, value) {
  			try {
  				if ($window.Storage) {
  					$window.sessionStorage.setItem(key, $window.JSON.stringify(value));
  					return true;
  				} else {
  					return false;
  				}
  			} catch (error) {
  				console.error(error, error.message);
  			}
  		},
  		getSession: function (key) {
  			try {
  				if ($window.Storage) {
  					return $window.JSON.parse($window.sessionStorage.getItem(key));
  				} else {
  					return false;
  				}
  			} catch (error) {
  				console.error(error, error.message);
  			}
  		},
  		getSessionAll: function () {
  			try {
  				if ($window.Storage) {
  					var _json = '{"Data": [';
  					for (var i = 0; i < sessionStorage.length; i++) {
  						_json += $window.JSON.parse($window.sessionStorage.getItem(i));
  						_json += ',';
  					}

  					if (localStorage.length > 0) { _json.slice(0, -1); }

  					_json += ']}'

  					return _json;
  				} else {
  					return false;
  				}
  			} catch (error) {
  				console.error(error, error.message);
  			}
  		},
  		removeSession: function (key) {
  			try {
  				if ($window.Storage) {
  					$window.sessionStorage.removeItem(key);
  					return true;
  				} else {
  					return false;
  				}
  			} catch (error) {
  				console.error(error, error.message);
  			}
  		},
  		clearSession: function () {
			try{
  				if ($window.Storage) {
  					$window.sessionStorage.clear();
  				} else {
  					return false;
  				}
			} catch (error) {
		  		console.error(error, error.message);
			}
  		}
  	}
  }]);