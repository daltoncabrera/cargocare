using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MSESG.CargoCare.Core;
using MSESG.CargoCare.Core.EFServices;
using MSESG.CargoCare.Web.Models;
using MSESG.CargoCare.Web.Models.ManageViewModels;

namespace MSESG.CargoCare.Web.Controllers
{

    [Route("api/[controller]/[action]")]
    public class PermisosController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly string? _externalCookieScheme;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;

        public PermisosController(UnitOfWork uow, UserManager<ApplicationUser> usermanager) : base(uow)
        {
          _userManager = usermanager;
        }

        #region  Usuarios

        [HttpGet]
        [ActionName("Usuario")]
        public async Task<IActionResult> GetUsuarios()
        {
            var result = _unitOfWork.UsuarioService.GetUsuarios();
            return Ok(result);
        }

        [HttpGet]
        [ActionName("Usuario/{id}")]
        public async Task<IActionResult> GetUsuarioById(int id)
        {
            var result = _unitOfWork.UsuarioService.GetUsuarioById(id, false);
            return Ok(result);
        }
        [HttpPost]
        [ActionName("Usuario")]
        public async Task<IActionResult> PostUsuario([FromBody] UsuarioModel vm)
        {

            var user = new ApplicationUser { UserName = vm.Email, Email = vm.Email, FullName = vm.FullName, PhoneNumber = vm.Phone };
            var pass = CreatePassword(6);
            var result = await _userManager.CreateAsync(user, pass);
            if (result.Succeeded)
            {

               var curUser = _unitOfWork.UsuarioService.SaveUsuario(vm);
                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=532713
                // Send an email with this link
                //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                //await _emailSender.SendEmailAsync(vm.Usuario.Email, "Confirm your account",
                //   $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>");
                
                return Ok(new {User = curUser, Password = pass});
            }
            else
            {
                var err = string.Join("; ", result.Errors.Select(s => s.Description));
                return BadRequest(err);
            
            }

        }

        public string? CreatePassword(int length)
        {
            //const string? valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            const string? valid = "1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        [HttpPut]
        [ActionName("Usuario/{id}")]
        public IActionResult PutUsuario(int id, [FromBody] UsuarioModel usuario)
        {
            _unitOfWork.UsuarioService.UpdateUsuario(id, usuario);
            return Ok();
        }

        [HttpDelete]
        [ActionName("Usuario/{id}")]
        public IActionResult DeleteUsuario(int id)
        {

            _unitOfWork.UsuarioService.EliminarUsuario(id);
            return Ok(id);

        }
        #endregion

        #region Roles
        [HttpGet]
        [ActionName("Rol")]
        public async Task<IActionResult> GetRoles()
        {
            var result = _unitOfWork.UsuarioService.GetRoles();
            return Ok(result);
        }

        [HttpGet]
        [ActionName("Rol/{id}")]
        public async Task<IActionResult> GetRoles(int id)
        {
            var result = _unitOfWork.UsuarioService.GetRolById(id);
            return Ok(result);
        }
        [HttpPost]
        [ActionName("Rol")]
        public IActionResult PostRoles([FromBody] ApplicationRole role)
        {
            try
            {
                role.NormalizedName = CleanInput(role.Name, true);
                _unitOfWork.UsuarioService.InsertRol(role);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
            return Ok();
        }

        [HttpPut]
        [ActionName("Rol/{id}")]
        public IActionResult PutRoles(int id, [FromBody] ApplicationRole role)
        {
            try
            {
                role.NormalizedName = CleanInput(role.Name, true);
                _unitOfWork.UsuarioService.UpdateRole(id, role);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return HandleError(e);
            }
            return Ok();
        }

        #endregion

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            AddLoginSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

        #endregion


    }
}
