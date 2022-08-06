using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using DataTransferObject.User;
using DomainService.User;

namespace API.Controllers
{
    [Authorize("Bearer")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost("/GetAccountDetails")]
        public IActionResult GetAccountDetails(DTOUserIstek _userIstek)
        {
            try
            {
                DomainUser _account = new DomainUser();
                return Ok(_account.UserGetAccountDetails(_userIstek));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}