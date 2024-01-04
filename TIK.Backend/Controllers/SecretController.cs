using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Mvc;
using TIK.Frontend.Server.Metrics;
using TIK.Shared;

namespace TIK.Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SecretController : ControllerBase
    {

        private readonly ILogger<SecretController> _logger;
        private readonly SecretClient _secret;

        public SecretController(ILogger<SecretController> logger, SecretClient secret)
        {
            _logger = logger;
            _secret = secret;
        }


        [HttpGet(Name = "GetSecretEnvironment")]
        public async Task<string> Get()
        {
            var response = await _secret.GetSecretAsync("Environment");
            var e = response.Value;
            string environment = response.Value.Value;
            return environment;
        }
    }
}
