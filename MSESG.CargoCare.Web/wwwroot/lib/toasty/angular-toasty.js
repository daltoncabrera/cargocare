﻿/*!
 * angular-toasty
 */

'use strict';

/**
 *
 * The MIT License (MIT)
 *
 * Copyright (c) 2015 Invertase
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 *
 */

angular.module('angular-toasty', []);
angular.module('angular-toasty').directive('toasty', ['toasty', '$timeout', '$sce', function (toasty, $timeout, $sce) {
    return {
        replace: true,
        restrict: 'EA',
        scope: true,
        link: function (scope, element, attrs) {

            // Init the counter
            var uniqueCounter = 0;

            // Allowed themes
            var themes = ['default', 'material', 'bootstrap'];

            // Init the position
            scope.position = '';

            // Init the toasty store
            scope.toasty = [];

            // On new rootScope toasty-new broadcast
            scope.$on('toasty-new', function (event, data) {
                var config = data.config;
                var options = data.options;

                if (!scope.position)
                    scope.position = 'toasty-position-' + config.position;

                add(config, options);
            });

            // On new rootScope toasty-clear broadcast
            scope.$on('toasty-clear', function (event, data) {
                clear(data.id);
            });

            // On ng-click="close", remove the specific toast
            scope.close = function (id) {
                clear(id);
            };

            // On ng-click="close", remove the specific toast
            scope.clickToasty = function (toast) {
                scope.$broadcast('toasty-clicked', toast);
                if (toast.onClick && angular.isFunction(toast.onClick))
                    toast.onClick.call(toast);
                if (toast.clickToClose)
                    clear(toast.id);
            };

            // Clear all, or indivudally toast
            function clear(id) {
                if (!id) {
                    angular.forEach(scope.toasty, function (value, key) {
                        if (value.onRemove && angular.isFunction(value.onRemove))
                            value.onRemove.call(scope.toasty[key]);
                    });
                    scope.toasty = [];
                    scope.$broadcast('toasty-cleared');
                } else
                    angular.forEach(scope.toasty, function (value, key) {
                        if (value.id == id) {
                            scope.$broadcast('toasty-cleared', scope.toasty[key]);
                            if (value.onRemove && angular.isFunction(value.onRemove))
                                value.onRemove.call(scope.toasty[key]);
                            scope.toasty.splice(key, 1);
                            if (!scope.$$phase)
                                scope.$digest();
                        }
                    });
            }

            // Custom setTimeout function for specific
            // setTimeouts on individual toasts
            function setTimeout(toasty, time) {
                toasty.timeout = $timeout(function () {
                    clear(toasty.id);
                }, time);
            }

            // Checks whether the local option is set, if not,
            // checks the global config
            function checkConfigItem(config, options, property) {
                if (options[property] == false) return false;
                else if (!options[property]) return config[property];
                else return true;
            }

            // Add a new toast item
            function add(config, options) {
                // Set a unique counter for an id
                uniqueCounter++;

                // Set the local vs global config items
                var sound = checkConfigItem(config, options, 'sound');
                var showClose = checkConfigItem(config, options, 'showClose');
                var clickToClose = checkConfigItem(config, options, 'clickToClose');
                var html = checkConfigItem(config, options, 'html');
                var shake = checkConfigItem(config, options, 'shake');

                // If we have a theme set, make sure it's a valid one
                var theme;
                if (options.theme)
                    theme = themes.indexOf(options.theme) > -1 ? options.theme : config.theme;
                else
                    theme = config.theme;

                // If we've gone over our limit, remove the earliest 
                // one from the array
                if (scope.toasty.length >= config.limit)
                    scope.toasty.shift();

                // If sound is enabled, play the audio tag
                if (sound)
                    document.getElementById('toasty-sound').play();

                var toast = {
                    id: uniqueCounter,
                    title: html ? $sce.trustAsHtml(options.title) : options.title,
                    msg: html ? $sce.trustAsHtml(options.msg) : options.msg,
                    showClose: showClose,
                    clickToClose: clickToClose,
                    sound: sound,
                    shake: shake ? 'toasty-shake' : '',
                    html: html,
                    type: 'toasty-type-' + options.type,
                    theme: 'toasty-theme-' + theme,
                    onAdd: options.onAdd && angular.isFunction(options.onAdd) ? options.onAdd : null,
                    onRemove: options.onRemove && angular.isFunction(options.onRemove) ? options.onRemove : null,
                    onClick: options.onClick && angular.isFunction(options.onClick) ? options.onClick : null
                };

                // Push up a new toast item
                scope.toasty.push(toast);

                // If we have a onAdd function, call it here
                if (options.onAdd && angular.isFunction(options.onAdd))
                    options.onAdd.call(toast);

                // Broadcast that the toasty was added
                scope.$broadcast('toasty-added', toast);

                // If there's a timeout individually or globally,
                // set the toast to timeout
                if (options.timeout != false) {
                    if (options.timeout || config.timeout)
                        setTimeout(scope.toasty[scope.toasty.length - 1], options.timeout || config.timeout);
                }

            }
        },
        template: '<div id="toasty" ng-class="[position]">'
			        	+ '<audio id="toasty-sound" src="data:audio/wav;base64,UklGRiQ9AABXQVZFZm10IBAAAAABAAEAIlYAAESsAAACABAAZGF0YQA9AAA4AFAASABAADgAQAAwACgAGAAoADAAIAAoADgAKAAgACAAKAA4ADgAMABIADgAQABIAFAAWABQAHAASABYAFAAUAA4AEAAMAAwADgAQABQAFAASABYAFgAaABwAHgAeABoAGgAaABoAGgAUABQAFAAWABQAEgASAA4AEAAOAAgADAAMABIAEAAOABAADAAQAAwADgAKAAwACAAIAAwACAAIAAwACgAGAAQABAAAAD4//D/2P/o/+j/8P/o/+D/0P/Y/9D/2P/I/8D/yP/g/8j/0P/Y/+j/2P/w/9D/2P/A/8D/yP/A/9D/yP/A/9D/0P/I/8j/yP+4/9j/6P/g/+D/2P/Y/+D/4P/Y/9j/4P/o/+j/0P/g/+D/4P/o/+D/4P/g/9D/4P/g/+D/4P/w/wAAIAAwABgAMABAADAAOAAwADAAKAAwADgAIAAYACAAAAAAAAgAIAAIABAACAAgACAAIAAgAAgAEAAIAAAAAAAAAOj/8P8AAAAA+P/4//j/+P/o/+j/6P/g/+j/+P8AAPD/6P/o//D/2P/o/9D/4P/w/+j/4P8IABAAAAAQABgAAAAAAAgAAAAAAPj/8P/w//D/6P/4//D/+P8AAPj/AAAAAAgACAD4//j/CAAAAPj/CAAAAAAAAAAQABgAIAA4ADgAMABIAEgAOAAwAFAAOABAAEAASABIAEAAOAAgACgAGAAwACAAKAAwADgAMAA4ADgAQABAADAAKAAYAPj/6P/4/+j/4P/I/8D/uP+w/8D/uP+o/7j/qP/A/9j/qP/I/9j/wP/A/8D/4P/g/9j/2P+4/8D/wP+w/9D/wP/Q/9j/0P/Q/8D/2P/I/9j/6P/o/+j/+P/o//j/AAAAAAgAEAAIAAAAAAAIAAgAEAAYABAACAAgACAAAAAYADgAMABIAEAAKAA4ADAASABQAFAAUABYAEAAKAAQAAgAEAAQACgAGAAYABgAGAAgACAAMAAgADgAMAAwADgAMABIAEAASAAoAEgAQABYADAAUABQADgAMAAoACgAOABAAEAAMAAoACgAIAAgACAAOAAwACgAKAAoADAAKAAwACAAIAAYADAAKAAgACgAIAAQABgAGAAIAAgAAAAIAAAAAAD4//j/EAAYAAAAAAAQAPD/GAAgABAAEAAYACAAOABIAEAAMABAACgAKAAoAAgAEAAgABgACAAQAAgAEAAgAAgAAAAAABgACAAYABAACAAQAAAAAAAQAAgA+P/w/+j/4P/w/+D/6P/o//D/2P/g/9D/6P/o/8D/wP+w/7j/0P/I/9D/wP/g/9j/6P/g/+D/2P/Y/9D/2P/g/9j/0P+w/6j/4P/Y/9j/2P/Y/9D/0P/A/8D/8P+w/8D/uP/A/8j/0P/Q/9D/0P/w//D/2P/g/+D/6P8AAPD/6P/g/+j/6P/Q/+D/4P/4/+j/+P/o/+D/8P/4/wAAAAD4//j/6P/Y/9D/yP/g/+j/2P/Y/9j/yP/Q/9D/yP/Q/9D/yP+4/6D/qP+g/7j/sP+4/8D/yP/Y/8D/wP/Q//D/0P/g/8j/wP/A/7j/qP+w/7D/sP+w/6j/sP+w/7j/uP/A/8D/2P/A/+D/6P/o/+j/4P/Y/+j/2P/Q/9j/4P/o//j/EAAAAAgA+P/4//D/AAAAAAAAAAD4//j/8P/4/wAAIAAQACAAEAAIABgACAAQADAAKAAgACAAMABAAEgAMAAwAFgAWABgAFgAQABIAFgAYABYAGgAUABwAEgAQABAADgAOAA4ADgAMAAgACgAKAAoACgAMAAoACAAEAAQABAACAAIAAAAAAD4/wAAEAAIAPj/6P/Y//D/AAAAAOj/4P/w//j/8P8IAPj/EAAAAPj/6P/4/+D/4P/Y/9j/2P/g//D/8P/o/+D/6P8gABAACAD4/yAAIABAADgAMAA4ADgAIAAQACAAIAAgADAAKAAwAEgAOAA4AEAAQABIAEAAMABAAEAAMABQADAAYABAAEgAQABQADAAUABYABgAQABAAEAAKABIADAAIAAYABgAAAAgACAAGAAgAAgAGAAAAAgACAAQABAACAAQAPD/IAAoAPD/CAAgAPj/4P/w//D/+P/g/9j/2P/Y/9j/6P/I/6j/4P/Y/+D/0P/A/8D/yP/A/8D/2P8AAPD/CAAAAAAAGAAQAOj/GAAoAPD/GAAYAPD/IAAgAOj/GAAgAAAACABQACgAEABQADAAKADw//j/+P8IABAAGAAYADgAEAD4/8D/2P8AAFAAKAAoAPj/8P8QAOj/6P/Q/7j/iP+I/7j/8P8oAPj/aP9w/5j/+P8YAAAACAB4/5j/CAAQAOj/GADg/1D/uP8oAHAAKADw/4D/sAAwABAA4P9I/2j/AACAAXAAiABo/zAAMAAA/+D9EP7w/2AAKAGAAOAAiP8YAFD/YP5I/7j+mP2g/jAAIAPQAYAAMP/Y/8D/SP6g/ID9yP/QAYgCiAGQAMgAKAD4/0j+CP9g/ij9AP+QAAACQAAwADAAmP8gATD/cP6Y/rD/gADA/7j8iP04/WD/QAEYATD+yP9g/Lj8WP/I/tAAIP6I/wj+uP8IAagA8P9g/gj+cAOw/dD+QADYAYgDSP8QAhj8uP5wAXj+wAMIAfgBsP+w/7gBmAMwA4AAmAC4AAj/SP0IAegEoAWw/lD7iPrY/IACuP+QA4gEqAgAACj4CAFQ+9gB8P9wAjgDOP5QBbgCSAfgAfD/IP+Y/qgFmACIAsAEIP0wBTAAeAGo/OgFWP2oDrADoAZAAqgK+P5ACsgBCALwD0AL0AlwBhgCiP9o/dj7+AJg/6AEWAXICej7KADYAjgCEP94B4j4SAEY/yAAWP4Y/8D8QACIAYgDUP4w/fj98AEYANgDGAIoAJD90AcIBXgC4AeYB+D9OP0Q+IAAWAKQCPgFsAZI/AADmPxQBKD/kABIAcAFqP+IBnAH4AQoApgSYP4QDmjvwAGgBdgCiAJw+wgDAAE4B6gT8P6wB9D78AdQCbj/aACo/oAAWPtYAvj/IPug+MgDQAJQA7D7EPdI/yD2uPSg+dDyyPWw7pjysOvY9vjugO8Y6ZjfeOZw62jkgOXA4ijgsOSw4dDdKOBY1QjTMNUg1GDciNfY0ADbUNhA29DUqNQY3HDe4Ncg1QjVoND41aDUcNno2QDWYNdw2tDjUOEA5QDpoO3Y70D46PFY8vj3iP4QBRgBAAiADVAP6BDIFogfqCHQKTgzcDGgNfg48EPQRzBGCEvQUMhQ4FLgV2BZ8FpAUnBVUFhgUehRCFDYUShKeEMYQhg9uDCAMfgrACUwG8AUcBXYDBAGuALA/oD0mOt451jlyOAg3vDaQNhw1BDRgNBgzVDOIM+Y04jR0NJA0pDY0N0g3rjlUOow7tDt4PdA/agACASYCGAOSBHgEhAX8ByQH1AneCd4KbApACqALhAv+C4gMLgvuC/4MPgqyCmIJRgfAB2QGTgTyA6QCjgHcAK4+oj4MPHI7BDrYOgg4fjcSNrg1zDUINHAz2jNuMqAybjJaMewydjJYM7wz4DSgNRQ2XjciOP46ODrmPCg9UD5SP1YAcAE4AqYDDgSUBPAGKgb4BqYHiAg6CFoIFAj8CNgIvggoCBoHqAawBUwEygQMAtoB2gCYP6Y+Pj3APFA7UDqeOfw4qDeQN7I2nDYWNUQ1qjRCNJg0KjRANKQ1NDWGNfo2qDckN/44NjnyOuY8LD1OPvI/lgEAAegCqgOwBGoGFgZKB6IH3Ak8CHYJgAnqCh4KZgpACqQJ8gnICdQJUghuCDQHFAYMBPgD+gMaAfoBQgB2PwQ+LD1OPLo7kDsOOwo6ojn4OYw5vDj4OO45CDk2OSQ5MDnuOlI7SjukO9Q8yD2cPuY/LgBoAQAB0ALUA4wEVgTMBUQF9AYUBloG2ga+BlwGzgamBlIF3gWSBQoE7ARQBHADfAJaAdQA+AAaP14+iD2WPKQ8fjsCOuI6EDmqOM44ujhyOAQ30DgIOIg4tDiyOPA5ODlgOgw60DsIO+Y8hD22PmY+xD/cAB4AhgHoAgoDKAOOBIgFNAVOBcAGPAWgBewF/AWCBeQF7gUuBMAEuAPGA2ACfgJUAd4BWADKALg/pD6APo49sjz+PAg8PjtMO7Q7mjtEOpg6xjs2Ozg7MDtMO9A8HDySPQ49oD3iPpI/KD+QAEwBMgFaAjIC7gNAA4gDrAQCBGQEtATABVAFHgUgBR4E5gR4BB4DwgNAAzwCQAHUAQQA/gAeP5w/ID6IPcY9Yj0cPIY8RDu6O3g6jjrMOqY6SDpMOo462DsqO5g7uDvmO+A8nj0OPbo+GD7wP4QAbgDGAUYBkAJQAzIDbAQaBE4FMgTWBa4FogWABZAFiAXGBbIFfgUgBSoE7gRyA4IDXgKwAjYBUgEsAJo/xj9oPoA+Zj28PXg8xDycPEo8NDuIO1g7CjsqOo46/jrGO0w7WjvSPII80D0CPWo9hD5WPtA/Xj+uABYA+gFcAcQCmgLWAxYDSAQQBHYETgS8BKIEzATGBPwEWARmBGgEEgQUA+4DlANKAuYCegHcAWoA9AB4AA4/xj+aP3w+xD6IPlI95j26PV49qD1wPWA9RD1mPRw9aD10PUo9yD5kPp4+qD7SP1I/pj+eAA4APAAUAK4A3gECAWwBzAGuAfgBuAGUAeQBugGgAYgBqgEkAMIA2ACoAAYAIj/UP/Y/Wj98Psg+wj6CPhY9wD28PUo9cD1iPWY9VD14PT49MD1uPXQ9QD26Pao+Lj4OPqg+6D8oP2I/nAAUAKAA2AF0AbwB3AIMAgwCbAJcAsQCzAM8AzgDDgNSA04DVAMeAswC6AJsAjwBxgHAAY4BQgE8AHQAEgAyP8I/hj9OP3w+3j6EPqw+LD46Peo9tj32PfI+Gj4yPiA+dj5ePqo+kj8cP3I/Sj/WAAwAVgCYAKQBKgEwAUwBuAHuAhQCUgKqAkwCpAJWAmoCOAIaAiwCOAHEAgAB/AFUATQAtAB0P8A/9D9yP1I/MD6EPqI+Ej4KPco9nD20PUg9Sj0kPMA9GDzGPMQ8/DyiPPQ9MD1IPeg95j44PiA+RD6cPqo++D8aP2Y/qD/SABoAegBsAJ4A9ADUAT4BAgFcAVgBTAFuASQBHAE2AOgAzADaAMIAxgCoAEIAFj/wP4g/ij9uPxo/BD8ePvA+kD6uPm4+dD5aPlw+Yj5QPmY+WD50Pkw+jj6APtg+0j8QP0g/gD/OP/Y/xAAkP9oAFAA4AFIAvACCARwBIAEiASIBBgFgAXgBDgG2AUoBhgFSAVoBVgFKAVwBDAEgARYBAAE4ATQA3AEsANIA+ACqALwAhgDIAOwA6AD8AKAAzADMAOoAuACGAM4A3gDMANQA5gDCAQABLgEgAVoBQgGeAWgBfAEgARgBPADuANgA3gDgAP4A3ADUAMYA2gC8AFwAdgAWAAAAPj/oP8g/0j+yP2g/Xj9KP1Q/Bj9mPxA/Cj8WPzQ+6D7kPuo+1D7mPvg+1j8aP2A/Wj9UPwg/BD8gPwY/aj9SP6o/sj+gP6g/qj+gP5A/zj/mP+w/xD/QP9o/nj+IP7A/dD9yP3w/Rj+2P34/eD9SP3I/JD8GPz4+0D8EPwo/Cj8+PuQ+9D7cPsQ+8j7+Ptg/Aj8YPzI/PD82PyY/Aj9UP1g/lj+eP8IAEgAOADo/wAAUAAAAXABOAKAApgC6AL4AgADkAMwA/gDAATgA6ADiAOQA6ADSAMgA8gCyAGQAbAB2AGYASABiADwAAgAYP+o/iD+iP2Q/ZD9gP14/eD8iPyo+zj7APtQ+wD7EPsQ+zD7CPv4+nD72PvY+3D8qP2g/eD9cP6w/rj+6P4IAOj/OACYAHgBCALQAngDmAMIBAAE+APYA2AE4ATYBcAF4AWoBTAFUAXYBJAEqAQQBVgFYAXgBHAEiAO4AyADAAPwAhAD4AJYAoACGAKoARgBAAFYADAAmACAAKgAEADQ/yAA8P/A/wAAWACwAEgAuACoACAA4P9gAHAA0ACAAJAAsAAAATABAAF4ARABKAHgAJgAsABwAAABmABQAEAA2P+o/2j/eP+A/1j/WP8Y/4D/KP/Q/tj+uP7Q/pj+oP7A/rD+0P7Y/rD+gP5o/kj+OP64/mj+kP4I/yD/EP9I/4D/sP/Y/ygASAA4AHAAkADgAPAAmACIAKAAwAA4ATgB0AG4AZgBYAE4AcgAsACwAHgAsABYAIgAWABoAPD/sP+o/+j+0P7Y/gD/cP74/Qj+IP6A/Yj9QP0o/VD9cP1Y/ZD9CP3w/CD9+Pz4/PD84Px4/Vj9iP24/Yj9kP3Q/fD9aP7Y/lj/uP/Y/xgAeAB4ALgAMAFgAcgBYAJoApACaAIoAhgCiAJwAkACQAIwAlACQAIYAngCCALQAaAB8AC4AGgAAAD4/+j/UP/4/rj+4P2Q/cj9iP1A/Tj9QP1Y/cj8+Pzo/Mj8EP2I/XD9oP3w/Rj+KP5A/jj+aP7w/lD/yP+AAAgBmAHgAYACwALwAmgDoAMgBFAEgASYBDgEcAQIBfgEAAU4BUAFQAV4BUgFCAW4BGAECARwA+gCoAJgAigCkAEQAXAA6P9o//j+2P5w/hj+cP3A/Gj8KPwI/Nj7mPtA++D6kPp4+tD6uPq4+sD6APvQ+sD6yPpo+wj8CPwQ/Hj8UPzg/Ej9YP1o/dj9SP54/oj+2P5I/zj/eP+Y/5D/sP8QAFgAkACAAHgAWAAYALj/QP84/7j+2P7w/hD/8P6I/nj+gP54/kD+OP4w/kD+aP54/nD+0P6w/hD/GP8o/1D/gP8IAEgA+ABoAbABoAJYA4gD8ANwBNAEMAWIBegF8AX4BRAGeAagBrgGyAaoBoAGQAZABugFqAVgBRgFqARwBMADaAPwApgCwAHYACAAYP9Y/+D+mP4w/sD9KP34/ID8IPwQ/Pj7EPwA/OD7uPuw+4D7APwY/Ej8sPwY/RD9mP0Q/oD+2P7w/rj/0P+IABgBqAHYAUACqALYAvgCCAOIA8ADOATQA9gD+AMABJgDsANIAyADmAJIAgAC0AGgAVABEAFAAMj/OP/A/nD+6P2g/UD98PzA/HD8GPy4+5j7kPt4+zD7OPtI+2j7oPuY++D78Pvw+1j8uPzo/FD9oP0g/mj+uP4w/4j/yP9wABABUAGoAZgB8AEAAgAC6AEwAggCSAJIAiAC0AFYAfgA2ADQAIAAeAD4/6D/WP9A/4j+aP64/aD9cP0I/dj8yPyI/GD8iPww/Cj8EPxI/KD8uPz4/Kj9yP1A/sj+gP/Y/wgAiADQAIABEAKwAmgDIATIBAgFYAXIBSgGsAYoB7gH6AcYCCgIsAeIB1gHIAfIBqgGGAbgBWgF+ASIBNgDMAOIAugBmAGwAOj/KP9I/rD96PxA/JD72PqI+jD6wPmQ+Yj5QPnw+GD4gPiA+MD4MPlo+ej5+PlA+pj68Pqg+zD84PyY/Uj+2P6I/9j/SAD4AJgBAAKIAsACMAOIA9ADOAQYBCAEIAT4AxgEyAOAA3gDOAPAAnAC6AFoAeAAoABAAND/UP/I/ij+wP1o/fj8oPwY/Mj7YPuY+8D7sPug+6D7iPuo+9D76Ps4/LD8SP2o/Sj+cP44/3j/8P+AABABmAEQAsACKAO4A/ADWASIBOgESAVwBdgF6AXIBZgFkAVwBTgFAAUABaAEeAQIBJgDMAPQAmAC0AEwAeAAQAC4/xj/oP74/XD9yPxg/Mj7IPvw+qj6aPoY+uj54PnI+bD5mPl4+Xj5ePn4+Xj6IPsg+6j78PsY/KD8UP0w/rD+iP9AANAAWAEIAsACgAMgBHAE4AQYBdAFQAaoBvgG4AYoBygHSAc4B1AHYAdQB2AHEAfIBiAG2AWQBSAFsAQABHAD2AI4AtABEAGQADAAkP8Q/3D+GP6w/Uj9KP3Y/JD8EPzY+8j7ePu4+6D7uPvo+7D7qPvY++j7OPyY/BD9gP3g/Sj+WP6A/tj+EP+o/+j/OACAAIgA+ABoAcABsAHIAeABIAJQAmACcAJIAlACMAIoAtABeAEwAQgBKAHYAKgAWADY/2D/OP8Y/7D+oP5w/jj+2P3I/aj9gP2I/Vj9EP3Q/PD8+Pw4/XD9eP1Q/Uj9kP2I/fj9SP6Q/rD+uP4A/xj/CP9o/1j/cP9g/4j/eP9w/5j/gP+Y/5j/sP/Q/yAAGAAwAIAAQABQADAAMABgAFgAiACIAFgAYABwAHgAIADQ/9j/IAAgAPj/MAA4AAgA+P/I/6j/yP/I/7j/qP9o/0D/SP8Y/xj/AP/g/qj+cP54/oD+gP64/oj+YP5Q/uD90P0o/nD+qP6Y/uD+GP8g/yj/UP94/+j/EABQALgAAAEQAXgB6AHIAQACUAJwAqgC8AIwA1gDIAMQAygDSANgA5ADoANwA4gDSANQAygDyAKYAlAC6AG4AUAB+AC4AHAA4P+o/xj/yP6I/lj+MP4A/rD9SP3o/KD8iPxQ/DD8KPw4/ED8aPyQ/ID8wPwI/UD9cP24/VD+6P6A/+D/KABYAMgAWAHYATgCsAIQA5AD0AMoBHgE+AQQBWAFiAWwBcgF8AUABhAGoAVoBSgF2ASoBIAEAASoAygDoAIQAlgBsAAoAIj/AP94/hj+cP24/Dj8sPs4+7D6YPr4+fD52Pnw+cj5sPmY+cD56Pko+rD6APt4++j7gPzQ/HD98P2g/ij/2P9QAMAAkAFAAnAC6AKIA+ADaATIBCAFiAWgBfgF4AXwBegFuAWABXAFSAUgBeAEgAQQBHgDCAOQAvgBaAEAAYgAIACY/0j/sP5I/gj+cP34/Lj8aPwI/DD8KPwo/BD8KPwo/Fj8aPyg/Cj9qP1A/pj+8P4I/5D/EACYABABkAH4AWgC+AIoA5AD0AP4AyAEQARoBIgEeASYBFgE+AN4AxAD2AKQApgCCAJQAdgAUAC4/wD/eP7I/SD9aPwA/Ij7GPvY+mj6wPlQ+RD58PjA+LD4wPiQ+Lj46Pj4+Dj5cPng+Wj62PpA++j7aPwA/Yj90P1I/sj+UP/w/6AAMAGoATACwAIYAxgDMAN4A7gDAAQIBPgDwAOwA4ADIAPYArgCQALgAZgBcAEwAbAASACg/xD/uP5A/kD++P0I/uD9qP1w/VD9+PwY/UD9QP2Q/aj94P3w/RD+aP7Y/ij/kP/A/xAAeADQABABcAHAAdAB+AE4AmgCkAKwAtgC+AIIAwADsAKIAnACUAIgAugBiAEwAdgAeAAoAOj/aP/4/qj+eP4o/gD+yP1o/UD9CP3Q/MD8yPwI/XD9WP1Y/Tj9cP2Q/cj9GP5w/vD+QP+I//D/KACAANgAQAG4ASACoALoAkADmAPIA+ADEARYBJAE2AToBBgF+ATwBOAEqASYBJgEYAQYBOADsANYAxgD6AIoAuABmAFgASgB0ACQAFgA+P/Q/4j/QP8Y/wD/2P7g/sD+wP7o/rD+0P7Q/rj+4P4A/zj/UP9o/5D/uP/g//D/AAAQACgASABYAIgAkAB4AJAAoABgACgAGAAIAAAA6P/Y/7D/kP9w/1D/+P6o/lj+OP5I/gD+8P2w/Xj9QP0I/QD9GP0g/UD9eP2Q/Zj9wP3Q/QD+SP6Q/uj+IP+Y/8j/CABIAIgA2AAYAXABoAEQAkACmALYAhgDWAOAA5gDoAOwA7ADyAOwA4ADYAMwA8gCiAJQAhAC+AGYAUgB6AC4AGgA8P+w/2j/CP+4/pD+UP44/vj9gP1Q/Sj9AP3o/Mj88Pzw/AD9CP0g/WD9iP2Y/dj9EP5A/qj+AP8o/5D/8P8gAHgAoADIABgBUAHAAdgBCAIQAjACMAIgAggCEAIIAvAByAGoAWABGAHIAIgAMAD4/8D/eP8o/+j+aP4w/vD9wP1w/Tj98PzQ/MD8sPy4/ID8aPxg/Fj8YPyY/Lj8CP1Y/Yj9qP2w/eD9AP6A/tD+SP9w/9j/EABoAMAAAAEwAVABmAGgAdgB+AEYAgACKAI4AhAC6AHgAegBwAHIAcABmAFAASgB2AC4AJgAcABgAGgASAAIAOj/yP9o/0D/cP9A/zj/OP8Q//j+0P7A/rj+kP5w/nj+gP5g/nj+mP6I/qD+gP6Y/nD+eP6A/mD+cP6I/mj+YP6Q/oD+kP6o/qD+0P6o/tD+2P7g/vj+CP8g/xj/QP9o/8D/4P/w/wAAGABQAHgAsAC4ABABWAGgAdAB+AEIAigCUAJgApgCgAKgAtgC6ALoAgADCAP4AtgCoAJwAkACIALwAeABuAGAATgBKAHIAKAAeAAYAAgAsP9g/xD/oP5w/jD+6P3A/aj9iP1Y/Uj9MP0o/QD9EP1A/Xj9iP2g/bD96P1A/mD+gP6o/tj+CP9Q/6D/GAAwAIAAsADYAPAAMAFwAbgBAAIgAjgCQAIoAkgCWAJoAoACWAJIAmACYAJIAkgCGALwAagBaAFQAVgBEAEAAfgAyACwAIAAWABIAEgAMAAIAMj/uP+Q/5D/oP+o/8j/2P/Y/8D/0P/Q/9D/wP/Q/+D/+P8AAPj/CAAAABAAEAAoAEAAUABYABgACAAIAAgAEAA4ACgAWABoAFgAQAAwAEgAOABIADgAIAD4//j/AAD4/wAAMAAwACAACAAIAAAAAAAYABgACAAIACAAIAA4ADgAWABwAIAAgACIAHgAmACgALAAsACgALgAoACYAKAAmACgALAAmACgAJAAeABwAGgAgABoAEgAMAAgABAA4P/I/6j/kP9Y/0j/GP8Q/wD/0P7Q/pD+gP5o/lD+UP5Q/lj+eP5A/lj+SP44/jj+QP5g/lj+aP6Y/rj+wP74/iD/SP9g/2D/gP+I/7j/wP/Y/+j/CAAIAPD/8P/w/wgA+P/g/9j/2P/A/8D/eP+A/2D/QP9I/0D/CP/o/sj+qP6I/mj+YP44/jj+QP4I/vj9KP44/kj+UP54/oj+sP7Q/vD+MP9Q/4j/wP+g/zAAIACIAKgAAAEgAWABeAFwAbgB0AHgAfABCAIgAjgCQAJAAiACKAIIAugByAGoAbABuAGgAVgBIAEIAcgAkABgAEgAUABIACAA6P/Q/8j/mP94/3D/aP9g/0D/QP9I/yD/OP84/0j/IP9I/0j/aP94/3j/gP+Y/6D/sP+4/9j/6P/g/xAAGAA4ABAAAAAIAAAA+P8QACAAQAAwACgAOAAYADAAKAAIAPj/+P/o/9j/qP/Y/9j/sP+4/5D/kP+w/7D/yP+o/7j/qP+g/8D/4P/Q/8D/yP/w//j/CAAoADAAaAB4AIAAkACIALgAsACoAMAAsACoALAA8ADQAOAA2ADAAKgAmACYAKAAeAB4AGgASABQAGAAaABoAFAAWABgAHAAaABYAFgAeABwAHAAcACQAIAAeACQAJAAcACAAJgAiACwAMgAwADQALgAsADIAMgAyAC4ALgAmACIAIAAcABYAFAAOAAYAAgA8P/o/7j/uP+Q/4D/gP9I/yj/MP8w/wD/+P7Q/qj+kP54/pj+oP6o/qj+eP54/qj+oP64/tj+AP8I/xj/SP84/1j/gP+w/9D/6P8AABAAQABwAIgAsACwAPAAIAFAAVABSAFoAXgBcAGYAagBsAGoAYgBeAGgAYgBcAF4AVgBQAEIAfgAuACAAEAAQAAQAAAAuP+A/0j/OP8I/+D+qP6Y/nD+eP5g/lj+aP5Q/mD+YP5w/nj+mP6w/rj+4P4A/yj/OP9w/5j/mP+o/9j/AAAYAEgAiAB4AKAAuADAALgAAAEgAQABOAEIARAB+ADwAPAA6ADQAMAAqACIAEgAMAAIAPj/6P+w/3j/QP8Y/wj/6P7Y/pj+eP5Y/iD+AP4Y/iD+IP4g/jD+OP5A/kD+MP5A/oj+uP7Y/vD+IP9Q/4D/yP/o/xgAUAB4AKAA2AD4APgAKAFYAZABoAGwAbgBwAHYAdgB4AHwAegB0AGgAYgBeAFQATABGAH4AMgAuABwAGAAGAAAANj/sP+Y/3D/UP8Y//j++P7o/uD+yP7Q/tj+wP7A/sD+wP7w/gj/OP9A/0j/cP+I/7j/+P8QADAASABwAIgAuADYAOgA6ADwAOgA4AAAAfgAIAEwASgBMAE4AQgBEAHwAPAA4ADIAKAAqAB4AGgAGAAIAOj/0P+g/5D/cP9I/0j/QP8g//D+AP8A/+D+8P7Q/uj+8P74/hj/GP8Y/yj/GP8Y/1D/eP+4/7j/0P/A/9D/6P8AACAAKAAwADAAUABYADgAaAB4AFgAWABIAFgAQABQAGgAGAAAAAgAEAD4//j/AADo/9D/wP+o/5D/kP+Q/6D/mP+Q/4j/oP+Q/5D/kP+I/5D/qP+Y/7j/0P/o/wAA8P/o//D/6P/w/wAAEAAIACAACAAoAEgAQABYAFAAUABoAEgASABAACgAGAAoABgAKAA4ACgAIAAIAAgAAADw/+j/+P/w/+j/AAAAAPj/AAAAABgAIAAQACAAQAAoAGgAgACIALAAuADAAOAAAAEwAUgBcAFwAXgBiAGYAZABuAGwAbgBuAGoAaABkAGQAXABYAEoAQAB4ACwAJAAaABQABgA4P+w/3D/KP8Y/9D+oP5w/lD+OP4Q/uD9wP2w/aD9qP2I/Zj9oP2g/cD96P34/TD+UP54/qj++P4o/5D/uP8IAFgAkACwAAABUAGAAcgBEAI4AjgCUAJIAlgCcAJgAlgCOAJAAjACIALgAbABeAFQARAB2ACoAFgAAADQ/6j/WP8Y/7j+iP5Y/gD+wP2g/XD9UP0w/Qj96PzQ/Oj8+Pzw/Pj8CP1A/XD9mP3Q/Qj+QP6A/tD+IP9o/5j/2P8oAHAAoADoADgBSAGIAdAB0AHwARACIAIoAjACSAJAAjgCOAIIAuABqAF4ATgBKAHoALAAeAA4ABgA2P+w/3D/OP8g/+j+wP6Y/pD+cP5g/mj+WP5o/oj+kP7I/vj++P5I/2j/oP/A//D/SACgAMgAAAFAAWgBqAHYAfgBQAJIAlgCcAKAAogCiAKAAngCWAJQAkgCGALoAcABkAFIARAB0ACIAEAA8P+o/1j/CP+w/oD+aP5A/uj9sP2g/Wj9UP1I/Rj9EP0Q/RD9QP0w/WD9mP2Y/dj9KP5Y/pD+6P4w/3D/oP/w/xAAcADAAPgAOAF4AagByAHoARgCKAI4AkgCYAJQAkgCSAIgAgACuAF4AWABKAEIAdAAkABQACgA4P+4/1D/GP+4/nD+WP4Q/tj9mP2Q/XD9WP04/Sj9IP0I/Sj9MP1A/VD9aP2Y/bj9+P0A/lD+gP7Q/vj+QP9w/7j/8P8gAHgAuAAIAUABWAFoAZABmAG4AcgB0AHYAdgB2AG4AagBkAFwAXABUAE4AQgBwACoAHAASAAgAOj/uP+Y/4D/MP/g/uD+wP6Y/oj+aP5Q/kj+UP5Q/mD+cP6I/oj+iP6w/rj+0P7o/iD/KP9Q/4D/mP/Q/+j/EAAwAGgAmACwANgACAEwARgBMAE4AUgBKAFAATgBMAE4ASgBEAEYAfgA+ADoANAA0ADAAKAAkABwAGgAWAAwAPD/6P/Q/+D/4P/w/8j/0P/I/8j/0P/I/+j/8P/o/wAA6P8AAPj/AAAIADAAMAAwADAAQABAAFgAcACQAIAAsAC4ANgAwADgAOAA+ADoANgA2ACwALgAmACIAHgAcABwAGAAWABYAEgASABIAEAASABQAEAAQAAoABgAGAAIAAgAEAD4/wAAAAD4/wAA+P/w/+j/8P8AACAAKAA4AEAAKABYAEgAaABoAGAAeACQALAAkACIAJAAiAB4AIAAiACIAHgAgABwAGAAQAAwACAAIAAoABAAAAD4/wAA6P/Y/9j/wP+g/5j/kP+o/5j/kP+I/6j/mP+w/4j/mP+o/6j/oP+o/8D/qP+4/6j/sP/I/9j/2P/g//D/AAAIABAACAAAAPj/AADo//D/2P+4/6D/mP+o/6j/uP+Y/5D/kP94/3D/eP9w/2D/UP9o/0j/UP9Q/1j/YP94/3D/iP+Q/5j/mP/A/+D/+P8QACAAMAA4AFAAUABwAHgAsADYAOAA6ADwAAgBEAEQAQABOAFAAUABQAFIAVgBUAEoASABCAHgANgAuACgAHgAYAAwAAgA6P+w/3j/UP8w/xj/AP/w/sD+sP6Q/oj+YP5g/mD+SP5g/jj+OP4g/ij+OP44/jj+SP5Y/lj+cP6Q/rD+4P4I/0D/cP+Y/7D/4P8YADAAcACgANAA2ADwAAgBGAE4AWABYAFYAWgBcAFoAWgBSAFQATABIAHoANgAsAB4AJAAkABIAEgAKAAIAPj/wP+Y/5D/eP9o/1D/OP8w/yD/AP8A//D+4P7o/uD+6P74/gD/GP8g/yD/QP9I/2j/iP/A/9j/+P8QABAAIAAQADAAQABAAFgAgACAAJAAmACgAKgAoACoAJAAiACIAHgAaABYAFAAOAAYAAgA+P/Y/8j/uP+Q/3j/YP9I/1D/MP84/zj/KP8Y/xj/EP/o/uj+AP8Q/xD/KP9I/3j/mP+w/8D/0P/g/wAAIAA4AFgAkAC4AMgA4AAQARABGAFIAUgBWAFoAXgBmAGgAagBiAGQAXgBcAFwAVgBUAFIAUABKAEAAfgA0AC4AKgAmACQAHAAaABQAEgAKAAoABAAGAAAAAAACADo//D/0P/A/7j/uP/I/9j/6P/Y//D/+P8IABAAKAAIACgAEAAYABgAKAAYAAAACAAAAAAA+P/o/+D/yP/A/8j/mP+A/2j/cP9I/1D/SP9A/zj/KP8o/wD/+P7g/uj+8P7o/vD++P7o/tD+0P74/uD+8P4A//j+8P4Q/yD/SP9g/3D/cP94/3j/oP+w/8j/6P/4/wgAAAAYABgAGAAwADAAMAAoADAAEAAQAAAAAADo//j/+P/4/+j/8P/4/8j/4P/g/+D/4P/I/9D/yP/Q/9D/2P/Y/9D/uP/Q/8D/2P/A/+D/4P/w//j/8P/4/+j/EAA4ADgASABQAFAASABAADAAOAAYACAAKAAAAPj/CAD4//D/8P/Y/+j/0P/I/8j/oP+Q/5D/cP9g/1D/MP84/zj/OP8o/0j/KP8o/zj/KP9A/1D/SP9o/2j/eP+I/5D/mP+4/+j/4P8IABAAKAA4AFAAeACIAKAAwADIANAA0ADoAOgA+ADwAPgACAEAAfgA6ADYAOAA0AC4AKgAkAB4AGgAWABYAEgAOAAwABgA8P/g/+j/2P+4/8D/uP+4/7j/wP+w/8j/wP+4/8D/yP/g/9j/8P/g//D/+P8AAAAAEAAgADAAKABAAEgAWABgAGAAQABQAFgAWABwAGAAaAB4AFgAUAAwACgAEAAwAAgACAAAAPj/6P/Y/7j/wP+w/5j/kP+Q/5j/mP9w/3D/eP94/2j/gP+Q/6D/2P/Q/+D/8P8AAAAAEAAoAEgAYAB4AKAAqACwANAA4AAAAfgA8ADwAPAAEAEQAQgBEAEIASABAAEAAfgA8AD4AOgA2ADIALAAkACIAHAAYABAAFAAIAAoABAAAADo/9j/2P/I/7D/0P/A/7D/qP+g/6j/oP+w/9D/0P/Y/9j/+P/g/+D/UAB4AHAAeACIAJAAqACYAMAA4AAAATABMAFIATgBGAEoASgBMAEwAVABSAFIATABMAEgAQgBCAEAAdgAyACwAKgAsACQAJAAiABwAGAAOAAoABgAEAD4//j/4P/I/6D/eP9w/2D/UP9Q/0D/IP8w/yD/QP8o/xj/OP8o/xD/GP8g/wj/EP8Y/xj/CP8g/xj/IP8Y/yj/KP8g/xj/IP8Y/wj/GP8o/yD/OP9Q/1D/WP9w/1j/UP9g/2D/YP9Y/2D/aP9w/4j/kP+g/6j/uP+4/6j/oP+w/5j/sP+g/5j/kP+I/3j/cP9g/1j/WP9Y/1D/SP9A/zj/KP8w/zj/OP84/yD/KP8o/yj/KP9A/0j/OP8w/0D/OP9Q/1D/gP9w/4D/aP+A/4j/kP/A/7j/0P/I//D/+P8AABgAMABQAGgAiACQAKgAsACgALgAwADYAPAA6ADgAOgA4ADYAMgAyAC4AMAAwACoAJgAkABwAFgAUABQACgAMAAIABAA+P/g/8D/oP+I/3j/aP9o/2j/YP9A/zj/QP84/0D/UP9Y/1D/aP9o/2j/cP94/5D/kP+Y/7D/sP+4/7D/0P/4/wgAIABAAFgAWABoAIgAgACAAIgAkACQAJAAmACQAJAAiAB4AHAAcABoAFgAUAAwADgAMAAQAAgAAADg/9D/2P/Q/8D/yP+o/6D/iP+I/4j/gP+Q/3j/cP+A/5D/kP+I/5D/mP+o/6j/kP+w/9D/wP/o//D/+P8IABgAIAA4ADgAQABIAFAAUABQAGgAkABwAHAAcABoAHAAWABgAFAAOABAADAAIAAYADAAKAAgADAAIAAYAAAAAADo/wAA+P/4/+j/4P/A/7j/sP+Y/6D/sP+w/8D/wP/Q/9D/0P/Q/9D/2P/g//j/AAAgADAAQABAAGAAeACAAJgAwADAAOAAyADgAMAA6ADgANAA8ADoAAAB+AAIAdgAyADoAOgA2ADYAMgAwAC4AKgAqACQAHgAaABQAEAAKAAYAAgAAAAAAOD/uP+w/7D/oP+Y/5j/gP+A/3j/aP9w/3D/SP9g/2j/YP9g/2j/eP9w/3j/eP9w/3D/iP+Q/5D/mP+4/7j/wP/I/+j/6P/4/wAAAAAIABAAAAAIADAAOAAwAEgAQABIAFAASABQAGgAWABIAEgAUABYAFgAQAAoABgACAAQAPD/2P/Q/+D/yP/A/7j/uP/A/8D/sP+4/6j/oP+Y/4j/mP+Q/4D/gP+A/3j/iP+I/5j/oP+g/7D/yP/Y/+D/4P/g//D/+P8AAAAA6P8QAAAA+P8YABgACAAQACAAGAAYACAAKAA4AEgAUABYAGgAUAA4AEgASABAAEgAQAAoACgAMAAgABAAGAAwADAAIAAQACAAGAAwADgAKAAoADAAIAAYABgAIAAIABAACAAYAAAACAAoACgAMABAADgAUABIAFgAaABoAHgAeABwAGgAUABAADgAKAAwACAAGAAgABgACAAQACAAIAAgADAAQABAAFAAUABQAFAASABIADgAOAAwAEAAOABAACgAIAAgACgAKAAoADAAMAAYADAAMABAAEAAOABQAGgAaABwAIAAcABoAHAAYABIAFgAQABgAFAAcABwAHgAeABwAIAAiAB4AHAAaABoAEAAWABIACgAMAAgACAAAAAAAOj/6P/Q/+j/yP/A/7D/qP+g/4j/iP94/3D/aP9o/3D/OP8w/zj/GP8o/0D/MP9A/0D/OP8w/yj/OP9A/0j/UP9A/2D/WP9o/3j/gP9w/6D/mP+o/7j/qP+4/7D/2P/A/+D/yP/Q//D/6P/o/+D/AAD4/wgAKAAgADgAKAAgACgAMAAoACgACAAAABgAAAAIAAgACAAAAPD/AADw/9j/4P/Q/8j/0P+w/7j/sP+w/5j/iP9w/3D/YP9Y/1j/UP84/yj/IP9I/1j/WP9w/3D/cP+I/4j/eP+Q/4j/oP+w/6j/uP+4/7D/0P/Y//D/AAAIABAAIAAwAEAASABwAHgAcAB4AJAAmACIAIAAcABgAFAAYAAwADAAQAAgAAgACAD4//D/0P/Q/8D/qP+Y/4j/aP9w/2j/YP9A/yD/IP8I//D+GP8I/wj/AP8I/xD/KP9A/0j/UP9Y/3j/eP+Q/6j/yP/Q//D/AAAAADAAQABIAFAAaACIAJgAsAC4ANAA2ADgANAA2ADoAOgAyADQAMAAsACYAIgAgACIAIAAaABgAEgAKAAQAAAA+P/w/+D/wP+4/6D/mP+A/4D/UP9Q/0D/UP9A/xj/IP8g/yD/KP8g/zD/QP9A/0j/YP9w/4D/kP+Y/7D/wP/Q/+D/+P8AAAgAGAAoADgAUABQAGAAcACAAIgAiACIAJgAgACIAJAAiACYAJgAoACIAIgAkACIAHgAcABwAGAAWABAADgAKAAwACAAQAAoACAAKAAYACgAIAAwADAAKAAgADgAIAAgAEAAMABYAFgAaABoAGgAgACAAHAAaACAAHgAiABoAGgAeABgAGgAiABwAGgAQABAACAAIAAQAAAACAAIAAAA6P/Y/9D/4P/Y/8D/0P+4/7D/sP+o/5j/eP9w/0j/UP9I/0j/MP9Y/1j/aP+I/3j/kP+Y/5j/qP/A/8D/wP/Y//j/8P8QABAACAAgABAAEAAgADAAMABIAGAAYABYAFgAaABoAGgAaABoAFAAaABQAFgAUAAwADAAIAAgABAACAAYABAAAAAAAAgA+P8IAPj/CADw/+D/AAAAAAAAAAAYAAgAAAAAAAAAAAD4//j/AAAAAPj/8P/w//D/8P/w/wAACAAQAAAA8P/w//D/4P/w/+D/2P/o/8D/2P/g/7j/0P+w/8D/qP/A/7D/sP+o/7j/uP/I/8j/yP/Y/9j/0P/I/8j/4P/o//j/+P8QAAgAGAAYACAAEAAgAEAASABIAEgAUABgAGAAcABYAFgAaABQAEgAWABIAFgAWABgAFAASABQAEgAQABYAEgAUABYAEgAUAAwADAAOAAYABgAEAAAAPj/+P/4/+D/wP/A/7D/qP+w/6j/sP/Q/8j/2P/g/+D/AAAAAPj/+P8IAPD/AAAQAAgAAAAYABgACAAQABgAIAAgABgAGAAgACAAKAAwADAAKAAgABgAEAAYAAAA+P/o//D/2P/o/+D/0P/Q/9j/2P/I/+j/4P/w/9D/0P/A/7j/qP+w/5D/qP+Y/7D/sP+o/8D/yP/Y/+D/6P/4/wgAKAAwACgAOABAAEgASABAAEAAQAA4ACgAMABIAEgAUABwAGgAaABoAHgAaABoAGgAYABwAFAAWABQAEgASAA4ACgAOAAwABgAKAAAAAgAAAD4//j/6P/o//D/yP/I/9j/4P/g/9D/yP/I/8j/sP/I/8D/yP/Y/9D/4P/Y/+D/4P/Y//D/+P/g/wAACAAIAPD/EAAgADAAIAAQABAAGAAYABAAEAAwAEgAMAAwAEgAMAA4ACgAMAAgADAAKAAoABgACADw/+D/2P+4/8D/wP+g/5j/mP+o/6D/kP+Q/5j/qP+o/5j/qP+g/7D/qP+g/6D/qP+Y/6j/uP/A/8j/2P/Y/wAA8P/4/wAACAAYABgAIAAoABgAOAA4AEAAYAB4AHAAcABwAHgAeABQAGAAOAAgACgACAAQAAgACAAIAPj/4P/Y/+D/uP+w/7D/qP+Q/5j/kP94/2D/UP9A/1D/SP9I/2D/OP9A/0j/UP9Y/1D/UP9I/2D/aP94/4D/gP+Q/6D/oP+o/6D/qP/I/+D/4P/w/wAACAAQAAgAAAAgACAAMAAwAEAASABQAGgAYABoAHAAaACAAGAAaABQAEgASABAADAAMABAADgAMAAgABAAGAAQABgAIAAoABAAGAAoACAAGAAIAAAAAAAQAAAAAAAIAPD/AAAYAOj/AAAQABAAMAAoABgAMABAAEAAIAA4ADgAQABAACAAKAAYADgAKAA4AEgAOABIAFgAYABgAFgAcABwAIgAiACAAGAAaABwAHAAgABoAGAAeABIAGAAcABYAGgAUABIAFgAUABQAGAAWABgAGAAYABoAEgASABIADAAKAAwACgAIAAYABAACAAIAPj/6P/4/wAA+P/w/+j/AADo/+j/AADY//j/AADw//j/4P/Q/9j/2P/g/8D/uP/Q/8D/uP/I/8D/0P/Q/9D/wP+4/7D/mP+Y/5D/eP9w/2j/cP9g/2j/YP9Q/1j/WP9g/1j/cP94/4D/gP+A/4j/aP9g/2D/UP9o/2j/cP9Y/4D/gP94/4j/kP+Q/6D/sP+4/9j/4P8IAAAAAAAYABgAEAAQABgAKAAwACAAGAAgAEAAMAAwADgAKAAwADgAOAAYADAAGAAgABAAGAAAAAgAAADg/9D/yP/Y/9D/wP+4/8j/2P+4/7D/sP+4/8D/qP+Y/4j/cP9w/3D/cP9w/4j/kP+Y/6D/sP+w/7j/uP/Y/+D/4P8QAAgAIABAAEgAUABgAHAAeAB4AJAAmACoALAAuADAAMgAyADYAMAAuADAALAAoACYAJAAcABwAFAAQAAwABgAAAAQAOj/4P/Y/6D/oP+Y/4D/aP9g/1D/QP9I/1j/SP84/zD/SP9g/2j/cP+A/3j/iP+A/5j/kP+w/8D/4P/4//D/AAAQACAAUABAAFAAWACAAJAAiACIAIgAcAB4AGAAWABoAEgAUABIADAAMAAoACgAGAAQAPD/8P8AAPD/8P/Y/+D/+P/Q/8D/yP+w/7D/sP+w/6j/oP+g/5D/iP+I/5j/qP+o/6j/wP/Y/+D/8P8IACAAMAAoAEAAUABYAGAAcACIAKAAoACIAKgAsACwAKgAqACgALgAsACoAKgAsACgAJgAmACAAIgAYABQAFAASABgAEgASAAoADAAKAAYAAAAEAAYABAAAAAAAAAAAAAQAAgAAAAQAAgAAAAYACgAIAA4AEAAWABoAHAAeACIAIAAiACAAIAAcACAAIgAiACIAIgAeACAAHAAeABoAHgAeABIAEgAKAAoADgAMAAwADAAMAAYAAAAAADo/+j/+P/w//j/8P/g/+j/0P/I/7j/wP/A/7j/sP+w/7D/uP+w/5j/sP+o/7D/sP/A/8j/wP/I/+D/2P/g/+j/0P+4/9D/yP/Q/7j/uP/I/8D/wP+o/5j/gP94/2D/cP9w/4D/aP9w/1j/WP9g/0j/UP9Y/1D/QP9Q/zj/QP8o/zj/QP8w/0D/MP9I/1j/WP9o/2j/YP+A/2j/eP94/2D/aP94/3D/cP+Y/6D/sP+o/8D/uP+4/7j/sP/A/8D/uP+4/5j/uP+g/5j/mP+g/5j/kP+A/5D/iP+g/5j/sP+Y/4j/mP+g/5D/qP+Y/5D/mP+Y/4j/sP+o/7D/wP/Q/9D/0P8AAAgAAAAAACgAMAAwADgAWABYAFAAaABgAGAAUABgAFgA" preload="auto"></audio>'
						+ '<div class="toast" ng-repeat="toast in toasty" ng-class="[toast.type, toast.interact, toast.shake, toast.theme]" ng-click="clickToasty(toast)">'
					    	+ '<div ng-click="close(toast.id)" class="close-button" ng-if="toast.showClose"></div>'
							+ '<div ng-if="toast.title || toast.msg" class="toast-text">'
								+ '<span class="toast-title" ng-if="!toast.html && toast.title" ng-bind="toast.title"></span>'
								+ '<span class="toast-title" ng-if="toast.html && toast.title" ng-bind-html="toast.title"></span>'
								+ '<br ng-if="toast.title && toast.msg" />'
								+ '<span class="toast-msg" ng-if="!toast.html && toast.msg" ng-bind="toast.msg"></span>'
								+ '<span class="toast-msg" ng-if="toast.html && toast.msg" ng-bind-html="toast.msg"></span>'
							+ '</div>'
						+ '</div>'
				  + '</div>'
    }
}]);
angular.module('angular-toasty').provider('toastyConfig', function () {

    /**
	 * Default global config
	 * @type {Object}
	 */
    var object = {
        limit: 5,
        showClose: true,
        clickToClose: false,
        position: 'bottom-right',
        timeout: 5000,
        sound: true,
        html: false,
        shake: false,
        theme: 'default'
    };

    /**
	 * Over-ride config
	 * @type {Object}
	 */
    var updated = {};

    return {
        setConfig: function (override) {
            updated = override;
        },
        $get: function () {
            return {
                config: angular.extend(object, updated)
            }
        }
    }
});
angular.module('angular-toasty').factory('toasty', ['$rootScope', 'toastyConfig', function ($rootScope, toastyConfig) {

    // Get the global config
    var config = toastyConfig.config;

    /**
	 * Broadcast a new toasty item to the rootscope
	 * @param  {object} options Individual toasty config overrides
	 * @param  {string} type    Type of toasty; success, info, error etc.
	 */
    var toasty = function (options, type) {

        if (angular.isString(options) && options != '' || angular.isNumber(options)) {
            options = {
                title: options.toString()
            };
        }

        if (!options || !options.title && !options.msg) {
            console.error('angular-toasty: No toast title or message specified!');
        } else {
            options.type = type || 'default';
            $rootScope.$broadcast('toasty-new', { config: config, options: options });
        }
    };

    /**
	 * Toasty types
	 */

    toasty.default = function (options) {
        toasty(options);
    };

    toasty.info = function (options) {
        toasty(options, 'info');
    };

    toasty.wait = function (options) {
        toasty(options, 'wait');
    };

    toasty.success = function (options) {
        toasty(options, 'success');
    };

    toasty.error = function (options) {
        toasty(options, 'error');
    };

    toasty.warning = function (options) {
        toasty(options, 'warning');
    };

    /**
	 * Broadcast a clear event
	 * @param {int} Optional ID of the toasty to clear
	 */

    toasty.clear = function (id) {
        $rootScope.$broadcast('toasty-clear', { id: id });
    };

    /**
	 * Return the global config
	 */

    toasty.getGlobalConfig = function () {
        return config;
    };

    return toasty;

}]);