using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PharmaceuticalSupplyChainSystem.Models;
using PharmaceuticalSupplyChainSystem.Models.RequestModels;
using PharmaceuticalSupplyChainSystem.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PharmaceuticalSupplyChainSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly AuthService _authService;
        private readonly EmailService _emailService;
        public AuthController(IConfiguration configuration, AuthService authService, EmailService emailService)
        {
            _configuration = configuration;
            _authService = authService;
            _emailService = emailService;
        }

        private string generateToken(User user)
        {
            //Claims here
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserID),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"], 
                _configuration["Jwt:Audience"], 
                claims,
                expires: DateTime.Now.AddDays(7), 
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            try
            {
                var newUser = await _authService.Register(user);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest req)
        {
            try
            {
                //Authenticate User
                var user = await _authService.Login(req);

                if (user != null)
                {
                    string to = user.Email;
                    string subject = "Login OTP";
                    string message = user.MFAData.OTPSecretKey;
                    await _emailService.SendMail(to, subject, message);
                    return Ok(user);
                }
                return Unauthorized();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOTP([FromBody] OTPVerificationRequest req)
        {
            try
            {
                var user = await _authService.VerifyOTP(req);
                Console.WriteLine(user);

                //otp verified then generate jwt
                if (user != null)
                {
                    var token = generateToken(user);
                    return Ok(new { user, token });
                }
                return Unauthorized();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("user")]
        public async Task<IActionResult> getUserById([FromQuery]string userId)
        {
            try
            {
                var user = await _authService.getUserById(userId);

                if(user == null)
                    return NotFound("User not found");
 
                return Ok(user);
            }
            catch(Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
