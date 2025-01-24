using DevFreela.Payments.API.Models;
using DevFreela.Payments.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.Payments.API.Controllers
{
    [Route("api/payments")]
    public class PaymentsControllers : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentsControllers(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost] //end point para receber os dados de pagamento, processar e retornar
        public async Task<IActionResult> Post([FromBody] PaymentInfoInputModel paymentInfoInputModel)
        {
            var result = await _paymentService.Process(paymentInfoInputModel);

            if (!result)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
