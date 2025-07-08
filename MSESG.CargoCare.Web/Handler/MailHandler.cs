using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentEmail.Core;
using FluentEmail.Core.Models;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using MSESG.CargoCare.Core;
using MSESG.CargoCare.Core.EFServices.Dto;

namespace MSESG.CargoCare.Web.Handler
{
  public class MailHandler
  {
    protected EmpresaSetting _settings;

    public MailHandler(EmpresaSetting settings)
    {
      _settings = settings;
      Email.DefaultSender = new SmtpSender(settings);
      Email.DefaultRenderer = new RazorRenderer();
    }

    public Task<SendResponse> Test()
    {
      var email = new Email(_settings.FromEmail)
        .To(_settings.CcEmail)
        .Subject("Test email desde cargo care")
        .UsingTemplateFromFile($"{AppContext.BaseDirectory}/emailtpl/testMail.cshtml", new { Name = "Rad Dude" });

      return email.SendAsync();
    }

    public Task<SendResponse> SendPlanificacion(PlanificacionDto planificacion)
    {
      var email = new Email(_settings.FromEmail)
        .To(_settings.CcEmail)
        .Subject("Test email desde cargo care")
        .UsingTemplateFromFile($"{AppContext.BaseDirectory}/emailtpl/testMail.cshtml", new { Name = "Rad Dude" });

      return email.SendAsync();
    }



    public Task<SendResponse> SendPlanificacion(string? html)
    {
      var email = new Email(_settings.FromEmail)
        .To(_settings.CcEmail)
        .Subject("Test email desde cargo care")
        .Body(html, true);

      return email.SendAsync();
    }


  }
}
