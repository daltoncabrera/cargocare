using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MSESG.CargoCare.Core;
using MSESG.CargoCare.Web.Handler;
using MSESG.CargoCare.Core.EFServices;

namespace MSESG.CargoCare.Web
{
  public class Startup
  {
    public Startup(IHostingEnvironment env)
    {
      var builder = new ConfigurationBuilder()
          .SetBasePath(env.ContentRootPath)
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

      if (env.IsDevelopment())
      {
        // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
        builder.AddUserSecrets<Startup>();
      }

      builder.AddEnvironmentVariables();
      Configuration = builder.Build();
    }

    public IConfigurationRoot Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      // Add framework services.
      //services.AddDbContext<ApplicationDbContext>(options =>
      //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("MSESG.CargoCare.Web")));


      services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(Configuration.GetConnectionString("MyDefaultConnection"), b => b.MigrationsAssembly("MSESG.CargoCare.Web")));

      services.AddScoped<IViewRenderService, ViewRenderService>();
      //services.AddIdentity<ApplicationUser, ApplicationRole>()
      //    .AddEntityFrameworkStores<ApplicationDbContext,int>()
      //    .AddDefaultTokenProviders();

      services.AddIdentity<ApplicationUser, ApplicationRole>(o =>
                {
                  o.Password.RequireDigit = false;
                  o.Password.RequiredLength = 6;
                  o.Password.RequireNonAlphanumeric = false;
                  o.Password.RequireUppercase = false;
                  o.Password.RequireLowercase = false;
                  //o.Cookies.ApplicationCookie.ExpireTimeSpan = TimeSpan.FromMinutes(29);
                  //o.Cookies.ApplicationCookie.Events = new CookieAuthenticationEvents
                  //{

                  //    OnRedirectToLogin = ctx => HandleRedirect(ctx),
                  //    OnRedirectToAccessDenied = ctx => HandleRedirect(ctx)
                  //};
                })
                .AddUserStore<ApplicationUserStore>()
                .AddUserManager<ApplicationUserManager>()
                .AddRoleStore<ApplicationRoleStore>()
                .AddRoleManager<ApplicationRoleManager>()
                .AddSignInManager<ApplicationSignInManager>()
                .AddDefaultTokenProviders();

      // Add application services.
      services.AddScoped<UserManager<ApplicationUser>, ApplicationUserManager>();
      services.AddScoped<RoleManager<ApplicationRole>, ApplicationRoleManager>();
      services.AddScoped<SignInManager<ApplicationUser>, ApplicationSignInManager>();
      services.AddScoped<RoleStore<ApplicationRole, ApplicationDbContext, int, ApplicationUserRole, ApplicationRoleClaim>, ApplicationRoleStore>();
      services.AddTransient<IEmailSender, AuthMessageSender>();
      services.AddTransient<ISmsSender, AuthMessageSender>();

      services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
          .AddCookie(options =>
          {
            options.Events.OnRedirectToLogin = HandleRedirect;
            options.Events.OnRedirectToAccessDenied = HandleRedirect;

          });
      services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, AppClaimsPrincipalFactory>();
      services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


      services.AddMvc(o =>
      {
        o.Filters.Add(new ErrorHandlingFilter());
      }).AddJsonOptions(
          SetupAction
         );

      services.AddDistributedMemoryCache();
      services.AddSession(options =>
      {
        options.IdleTimeout = TimeSpan.FromMinutes(30);
      });

      //services.AddDataProtection()
      //    // This helps surviving a restart: a same app will find back its keys
      //    .PersistKeysToFileSystem(new DirectoryInfo(@"\store\keys\"))
      //    // This helps surviving a site update: each app has its own store, building the site creates a new app
      //    .SetApplicationName(this.GetType().Namespace)
      //    .SetDefaultKeyLifetime(TimeSpan.FromDays(90));



      //injecting things 
      services.AddOptions();

      ///my injects 
      services.AddSingleton<IConfiguration>(Configuration);
      services.AddScoped<UnitOfWork, UnitOfWork>();
      services.AddScoped<IMyContext, ApplicationDbContext>();
      services.AddScoped<EmpresaRepository, EmpresaRepository>();
      services.AddScoped<ClienteRepository, ClienteRepository>();
      services.AddScoped<SelloRepository, SelloRepository>();
      services.AddScoped<CamionRepository, CamionRepository>();
      services.AddScoped<CisternaRepository, CisternaRepository>();
      services.AddScoped<ChoferRepository, ChoferRepository>();
      services.AddScoped<PlanificacionRepository, PlanificacionRepository>();
      services.AddScoped<OrdenRepository, OrdenRepository>();
      services.AddScoped<PrecargaRepository, PrecargaRepository>();
      services.AddScoped<InspeccionRepository, InspeccionRepository>();
      services.AddScoped<ProductoRepository, ProductoRepository>();
      services.AddScoped<TerminalRepository, TerminalRepository>();
      services.AddScoped<CorreoRepository, CorreoRepository>();
      services.AddScoped<UpdaterTrackRepository, UpdaterTrackRepository>();
      services.AddScoped<UsuarioRepository, UsuarioRepository>();
      services.AddScoped<RoleRepository, RoleRepository>();
      services.AddScoped<UserRoleRepository, UserRoleRepository>();
      services.AddScoped<RolePermisoRepository, RolePermisoRepository>();
      services.AddScoped<UsuarioService, UsuarioService>();
      services.AddScoped<VerificacionRepository, VerificacionRepository>();
      services.AddScoped<ActividadRepository, ActividadRepository>();

      if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
      {
        
      }
      else
      {
        
      }

    }

    private void SetupAction(MvcJsonOptions options)
    {
      options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
      loggerFactory.AddConsole(Configuration.GetSection("Logging"));
      loggerFactory.AddDebug();

      //app.UseMiddleware(typeof(ErrorHandlingMiddleware));

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseDatabaseErrorPage();
        app.UseBrowserLink();
        try
        {
          app.SeedData().Wait();
        }
        catch (Exception e)
        {
          Console.WriteLine(e);
          throw;
        }
      }
      //else
      //{
      //    app.UseExceptionHandler("/Home/Error");
      //}

      app.UseSession();
      app.UseIdentity();
      app.UseStaticFiles();


      app.UseMvc(routes =>
      {

        routes.MapRoute(
                  name: "default",
                  template: "{controller=Home}/{action=Index}/{id?}");


      });
    }

    private static bool IsAjaxRequest(HttpRequest request)
    {
      var query = request.Query;
      if ((query != null) && (query["X-Requested-With"] == "XMLHttpRequest"))
      {
        return true;
      }
      IHeaderDictionary headers = request.Headers;
      return ((headers != null) && (headers["X-Requested-With"] == "XMLHttpRequest"));
    }

    private static Task HandleRedirect(RedirectContext<CookieAuthenticationOptions> ctx)
    {
      if (IsAjaxRequest(ctx.Request) &&
          ctx.Response.StatusCode == (int)HttpStatusCode.OK)
      {
        ctx.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
      }
      else
      {
        ctx.Response.Redirect(ctx.RedirectUri);
      }
      return Task.FromResult(0);
    }
  }
}
