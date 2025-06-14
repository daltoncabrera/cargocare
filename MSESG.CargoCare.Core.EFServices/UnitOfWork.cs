using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MSESG.CargoCare.Core.EFServices
{
  public class UnitOfWork
  {
    public readonly UsuarioService UsuarioService;
    public readonly EmpresaRepository EmpresaRepository;
    public readonly ClienteRepository ClienteRepository;
    public readonly ProductoRepository ProductoRepository;
    public readonly TerminalRepository TerminalRepository;
    public readonly UpdaterTrackRepository TrackRepository;
    public readonly SelloRepository SelloRepository;
    public readonly CamionRepository CamionRepository;
    public readonly CisternaRepository CisternaRepository;
    public readonly ChoferRepository ChoferRepository;
    public readonly OrdenRepository OrdenRepository;
    public readonly PrecargaRepository PrecargaRepository;
    public readonly PlanificacionRepository PlanificacionRepository;
    public readonly InspeccionRepository InspeccionRepository;
    public readonly VerificacionRepository VerificacionRepository;
    public readonly UserManager<ApplicationUser> UserManager;
    public readonly CorreoRepository CorreoRepository;
    public readonly ActividadRepository ActividadRepository;
    public readonly SignInManager<ApplicationUser> SignInManager;
    public readonly IEmailSender _emailSender;
    public readonly ISmsSender _smsSender;
    public readonly ILogger _logger;
    public readonly string _externalCookieScheme;

    public ApplicationDbContext AppDbContext { get; }


    public UnitOfWork(
        ApplicationDbContext context,
        SignInManager<ApplicationUser> sinInManager,
        UserManager<ApplicationUser> userManager,
        UsuarioService usuaroiService,
        EmpresaRepository empresaRepository,
        ClienteRepository clienteRepository,
        ProductoRepository productoRepository,
        TerminalRepository terminalRepository,
        UpdaterTrackRepository trackRepository,
        SelloRepository selloRepository,
        CamionRepository camionRepository,
        CisternaRepository cisternaRepository,
        OrdenRepository ordenRepository,
        PrecargaRepository precargaRepository,
        PlanificacionRepository planificacionRepository,
        InspeccionRepository inspeccionRepository,
        CorreoRepository correoRepository,
        VerificacionRepository verificacionRepository,
        ActividadRepository actividadRepository,
        ChoferRepository choferRepository)
    {
      AppDbContext = context;
      UserManager = userManager;
      SignInManager = sinInManager;
      UsuarioService = usuaroiService;
      EmpresaRepository = empresaRepository;
      ClienteRepository = clienteRepository;
      ProductoRepository = productoRepository;
      TerminalRepository = terminalRepository;
      TrackRepository = trackRepository;
      CamionRepository = camionRepository;
      CisternaRepository = cisternaRepository;
      OrdenRepository = ordenRepository;
      PrecargaRepository = precargaRepository;
      PlanificacionRepository = planificacionRepository;
      InspeccionRepository = inspeccionRepository;
      ChoferRepository = choferRepository;
      SelloRepository = selloRepository;
      CorreoRepository = correoRepository;
      VerificacionRepository = verificacionRepository;
      ActividadRepository = actividadRepository;
    }

    public int Save()
    {
      return AppDbContext.SaveChanges();
    }

    public void Dispose()
    {
      if (AppDbContext != null)
      {
        AppDbContext.Dispose();
      }
    }
  }
}