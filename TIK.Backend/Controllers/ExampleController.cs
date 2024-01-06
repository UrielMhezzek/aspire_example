using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace TIK.Backend.Controllers
{
    [ApiController]
    [Route("api/examples")]
    public class ExampleController : ControllerBase
    {

        private readonly ILogger<ExampleController> _logger;

        public ExampleController(ILogger<ExampleController> logger)
        {
            _logger = logger;
        }


        [HttpGet("getExample1")]
        public async Task<IActionResult> GetExample1()
        {
            List<ExampleClass> classes = await CreateExampleListAsync();

            return Ok(classes);
        }

        [HttpGet("getExample2")]
        public async Task<ActionResult<List<ExampleClass>>> GetExample2()
        {
            List<ExampleClass> classes = await CreateExampleListAsync();

            return Ok(classes);
        }

        private async Task<List<ExampleClass>> CreateExampleListAsync()
        {
            return await Task.Run(() =>
            {
                List<ExampleClass> classes = new List<ExampleClass>();
                Random rnd = new Random();

                for (int i = 0; i < 10; i++)
                {
                    ExampleClass example = new ExampleClass()
                    {
                        MyProperty = rnd.Next(0, 9),
                        MyProperty1 = rnd.Next(100, 801),
                        MyProperty2 = rnd.Next(1300, 9000),
                    };

                    classes.Add(example);
                }
                return classes;
            });
        }
    }

    public class ExampleClass
    {
        public int MyProperty { get; set; }
        public int MyProperty1 { get; set; }
        public int MyProperty2 { get; set; }
    }
}
