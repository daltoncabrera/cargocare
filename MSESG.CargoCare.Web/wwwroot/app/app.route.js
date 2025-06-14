angular
    .module('app')
    .config(['$routeProvider', '$httpProvider', '$locationProvider', function ($routeProvider, $httpProvider, $locationProvider) {

        //initialize get if not there
        if (!$httpProvider.defaults.headers.get) {
            $httpProvider.defaults.headers.get = {};
        }

        //disable IE ajax request caching
        $httpProvider.defaults.headers.get['If-Modified-Since'] = 'Mon, 26 Jul 1997 05:00:00 GMT';
        // extra
        $httpProvider.defaults.headers.get['Cache-Control'] = 'no-cache';
        $httpProvider.defaults.headers.get['Pragma'] = 'no-cache';
        $httpProvider.defaults.headers.get["X-Requested-With"] = "XMLHttpRequest"

        $routeProvider
            // home
            .when("/", { templateUrl: "dashboard/dashboard.html", controller: "DashboardCtrl", title: 'Dashboard', navName: 'dashboard', parentName: '' })
            .when("/empresas", { templateUrl: "empresa/empresa.ctrl.html", controller: "empresaCtrl", parentTitle:'Configuracion',  title: 'Empresas', navName: 'empresas', parentName: 'configuracion' })
            .when("/empresa/:id", { templateUrl: "empresa/empresa.edit.ctrl.html", controller: "empresaEditCtrl", parentTitle: 'Configuracion', title: 'Empresas', navName: 'empresa', parentName: 'configuracion' })
            .when("/clientes", { templateUrl: "cliente/cliente.ctrl.html", controller: "clienteCtrl", parentTitle: 'Configuracion', title: 'Clientes', navName: 'clientes', parentName: 'configuracion' })
            .when("/cliente/:id", { templateUrl: "cliente/cliente.edit.ctrl.html", controller: "clienteEditCtrl", parentTitle: 'Configuracion', title: 'Creacion/Edicion Cliente', navName: 'cliente', parentName: 'configuracion' })
            .when("/terminales", { templateUrl: "terminal/terminal.ctrl.html", controller: "terminalCtrl", parentTitle: 'Configuracion',  title: 'Terminales', navName: 'terminal', parentName: 'configuracion' })
            .when("/productos", { templateUrl: "producto/producto.ctrl.html", controller: "productoCtrl", parentTitle: 'Configuracion',  title: 'Productos', navName: 'producto', parentName: 'configuracion' })
            .when("/sellos", { templateUrl: "sello/sello.ctrl.html", controller: "selloCtrl", parentTitle: 'Clientes', title: 'Sellos', navName: 'sello', parentName: 'sello' })
            .when("/camiones", { templateUrl: "camion/camion.ctrl.html", controller: "camionCtrl", parentTitle: 'Clientes',  title: 'Camiones', navName: 'camion', parentName: 'camion' })
            .when("/cisternas", { templateUrl: "cisterna/cisterna.ctrl.html", controller: "cisternaCtrl", parentTitle: 'Clientes',  title: 'Cisternas', navName: 'cisterna', parentName: 'cisterna' })
            .when("/choferes", { templateUrl: "chofer/chofer.ctrl.html", controller: "choferCtrl", parentTitle: 'Clientes', title: 'Choferes', navName: 'chofer', parentName: 'chofer' })
            //.when("/ordenes", { templateUrl: "orden/orden.ctrl.html", controller: "ordenCtrl", parentTitle: 'Clientes',  title: 'Ordenes', navName: 'orden', parentName: 'orden' })
            //.when("/orden/:id/:planificacionId?", { templateUrl: "orden/orden.edit.ctrl.html", controller: "ordenEditCtrl", parentTitle: 'Clientes', title: 'Creacion/Edicion Orden', navName: 'editorden', parentName: 'configuracion' })
            .when("/precargas", { templateUrl: "precarga/precarga.ctrl.html", controller: "precargaCtrl", parentTitle: 'Operaciones', title: 'Precargas', navName: 'precarga', parentName: 'operaciones' })
            .when("/precarga/:id/:inspeccionId?", { templateUrl: "precarga/precarga.edit.ctrl.html", controller: "precargaEditCtrl", parentTitle: 'Operaciones', title: 'Creacion/Edicion precarga', navName: 'editprecarga', parentName: 'operaciones' })
            .when("/inspecciones", { templateUrl: "inspeccion/inspeccion.ctrl.html", controller: "inspeccionCtrl", parentTitle: 'Operaciones',  title: 'Inspecciones', navName: 'inspeccion', parentName: 'inspeccion' })
            .when("/inspeccion/:id/:precargaId?", { templateUrl: "inspeccion/inspeccion.edit.ctrl.html", controller: "inspeccionEditCtrl", parentTitle: 'Operaciones',  title: 'Creacion/Edicion Inspeccion', navName: 'editinspeccion', parentName: 'configuracion' })
            .when("/planificaciones", { templateUrl: "planificacion/planificacion.ctrl.html", controller: "planificacionCtrl", parentTitle: 'Clientes',  title: 'Planificaciones', navName: 'planificacion', parentName: 'planificacion' })
            .when("/planificacion/:id", { templateUrl: "planificacion/planificacion.edit.ctrl.html", controller: "planificacionEditCtrl", parentTitle: 'Clientes', title: 'Creacion/Edicion Planificacion', navName: 'editplanificacion', parentName: 'configuracion' })
            .when("/usuarios", { templateUrl: "usuario/usuario.ctrl.html", controller: "usuarioCtrl", parentTitle: 'Configuracion',  title: 'Usuarios', navName: 'usuario', parentName: 'configuracion' })
            .when("/usuario/:id", { templateUrl: "usuario/usuario.edit.ctrl.html", controller: "usuarioEditCtrl", parentTitle: 'Configuracion', title: 'Creacion/Edicion Usuario', navName: 'editusuario', parentName: 'configuracion' })
            .when("/actividades", { templateUrl: "reportes/actividades.ctrl.html", controller: "actividadesCtrl", parentTitle: 'Reportes', title: 'Reporte actvidades', navName: 'actvidades', parentName: 'reportes' })
            .when("/verificaciones", { templateUrl: "verificacion/verificacion.ctrl.html", controller: "verificacionCtrl", parentTitle: 'Verficiacion', title: 'Listado Verificacion', navName: 'verificaciones', parentName: 'Operaciones' })
            .when("/verificacion/:id", { templateUrl: "verificacion/verificacion.edit.ctrl.html", controller: "verificacionEditCtrl", parentTitle: 'Verificacion', title: 'Creacion/Edicion Verificacion', navName: 'verificacionedit', parentName: 'reportes' })
            .when("/reporteAct", { templateUrl: "reportes/reporteAct.ctrl.html", controller: "reporteActCtrl", parentTitle: 'Reportes', title: 'Reporte Actividades', navName: 'reporteAct', parentName: 'clientes' })
            .when("/reporteGeneral", { templateUrl: "reportes/reporteGeneral.ctrl.html", controller: "reporteGeneralCtrl", parentTitle: 'Reportes', title: 'Reporte General', navName: 'reporteGeneral', parentName: 'reportes' })
            //.when("/permisos", { templateUrl: "rol/rol.ctrl.html", controller: "rolCtrl", title: 'Roles/Permisos', navName: 'permiso', parentName: 'configuracion' })
            .when("/401", { templateUrl: "error/noaccess.html", controller: "NoAccessCtrl", title: '401 - Access Denied' })
            // 404
            .when("/404", { templateUrl: "error/notfound.html", controller: "NotFoundCtrl", title: '404 - Not Found' })
            // otherwise
            .otherwise({ redirectTo: "404" });           


    }]); 