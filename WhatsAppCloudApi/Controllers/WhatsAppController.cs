using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WhatsAppCloudApi.Data;
using WhatsAppCloudApi.Services;

namespace WhatsAppCloudApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WhatsAppController : ControllerBase
    {
        private readonly WhatsAppCloudService _whatsAppService;
        private readonly WhatsAppContext _context;

        public WhatsAppController(WhatsAppCloudService whatsAppService, WhatsAppContext context)
        {
            _whatsAppService = whatsAppService;
            _context = context;
        }


        [HttpPost("send")]
        public async Task<IActionResult> SendWhatsAppMessage([FromBody] string phoneNumber)
        {
            string message = "Hello! We have 3 questions for you:\n1. What is your name?\n2. How old are you?\n3. Where are you from?";
            await _whatsAppService.SendMessageAsync(phoneNumber, message);

            return Ok("Message Sent");
        }


        [HttpPost("webhook")]
        public async Task<IActionResult> ReceiveMessage([FromBody] dynamic body)
        {
            var from = body.entry[0].changes[0].value.messages[0].from;
            var message = body.entry[0].changes[0].value.messages[0].text.body;

            
            var userResponse = new UserResponse
            {
                PhoneNumber = from,
                Question1 = message
            };

            _context.UserResponses.Add(userResponse);
            await _context.SaveChangesAsync();

            return Ok();
        }


    }
}
