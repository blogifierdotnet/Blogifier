using Blogifier.Core.Data.Models;
using Blogifier.Core.Helpers;
using Blogifier.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Blogifier.Core.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailsController : ControllerBase
    {
        IDataService _dataService;

        public EmailsController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet("emailmodel")]
        [Administrator]
        public async Task<EmailModel> GetEmailModel()
        {
            return await _dataService.CustomFields.GetEmailModel();
        }

        [HttpGet("sendgrid")]
        [Administrator]
        public async Task<SendGridModel> GetSendGridModel()
        {
            return await _dataService.CustomFields.GetSendGridModel();
        }

        [HttpGet("mailkit")]
        [Administrator]
        public async Task<MailKitModel> GetMailKitModel()
        {
            return await _dataService.CustomFields.GetMailKitModel();
        }

        [HttpPost("emailmodel")]
        [Administrator]
        public async Task<ActionResult<EmailModel>> PostEmailModel(EmailModel model)
        {
            try
            {
                var created = await _dataService.CustomFields.SaveEmailModel(model);
                return Created($"/api/emails/emailmodel", created);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("sendgrid")]
        [Administrator]
        public async Task<ActionResult<SendGridModel>> PostSendGrid(SendGridModel model)
        {
            try
            {
                var created = await _dataService.CustomFields.SaveSendGridModel(model);
                return Created($"/api/emails/sendgrid", created);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("mailkit")]
        [Administrator]
        public async Task<ActionResult<MailKitModel>> PostMailKit(MailKitModel model)
        {
            try
            {
                var created = await _dataService.CustomFields.SaveMailKitModel(model);
                return Created($"/api/emails/mailkit", created);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("testemail")]
        [Administrator]
        public async Task<ActionResult<bool>> SendTestEmail([FromBody]EmailModel model)
        {
            try
            {
                EmailFactory factory = new EmailService(_dataService);
                var emailService = factory.GetEmailService();
                string msg = await emailService.SendEmail(model.FromName, model.FromEmail, model.SendTo, "test subject", "test content");

                if (string.IsNullOrEmpty(msg))
                    return Ok();
                else
                    return StatusCode(StatusCodes.Status400BadRequest, msg);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
