using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InsuranceWebApp.Models;

namespace Insurance_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentDetailController : ControllerBase
    {
        private readonly InsuranceDbContext _context;

        public PaymentDetailController(InsuranceDbContext context)
        {
            _context = context;
        }

        // GET: api/PaymentDetail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDetail>>> GetPaymentDetails()
        {
            return await _context.PaymentDetails.ToListAsync();
        }

   
        // GET: api/PaymentDetail/by userid
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<PaymentDetail>>> GetPaymentDetailsByUserId(int userId)
        {
            var paymentDetails = await _context.PaymentDetails
                .Where(pd => pd.UserId == userId)
                .ToListAsync();

            if (paymentDetails == null || paymentDetails.Count == 0)
            {
                return NotFound();
            }

            return paymentDetails;
        }

        // GET: api/PaymentDetail/by payment id
        /*    [HttpGet("{id}")]
            public async Task<ActionResult<PaymentDetail>> GetPaymentDetail(int id)
            {
                var paymentDetail = await _context.PaymentDetails.FindAsync(id);

                if (paymentDetail == null)
                {
                    return NotFound();
                }

                return paymentDetail;
            }*/
        [HttpPost]
        public async Task<IActionResult> Post(PaymentDetail payment)
        {
            _context.Add(payment);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdatePayment(int userId, PaymentDetail payment)
        {
            if (payment == null || payment.PaymentId == 0) // Check if payment object is present and has ID
            {
                return BadRequest("Payment details missing in the request body.");
            }

            var paymentInfo = await _context.PaymentDetails.FirstOrDefaultAsync(p => p.UserId == userId && p.PaymentId == payment.PaymentId);
            if (paymentInfo == null)
            {
                return NotFound("Payment not found.");
            }

            // Update all properties based on the incoming payment object
            paymentInfo.CardOwnerName = payment.CardOwnerName;
            paymentInfo.CardNumber = payment.CardNumber;
            paymentInfo.SecurityCode = payment.SecurityCode;
            paymentInfo.ValidThrough = payment.ValidThrough;

            try
            {
                _context.Entry(paymentInfo).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentDetailExists(userId, payment.PaymentId)) // Check for existence using both userId and paymentId
                {
                    return NotFound("Payment not found.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // Helper method to check if a payment exists
        private bool CheckPaymentDetailExists(int id)
        {
            return _context.PaymentDetails.Any(e => e.PaymentId == id);
        }


        // DELETE: api/PaymentDetail/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            if (id < 1)
            {
                return BadRequest(); // Ensure ID is valid
            }

            var paymentInfo = await _context.PaymentDetails.FindAsync(id);
            if (paymentInfo == null)
            {
                return NotFound();
            }

            _context.PaymentDetails.Remove(paymentInfo);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool PaymentDetailExists(int userId, int paymentId)
{
  return _context.PaymentDetails.Any(e => e.UserId == userId && e.PaymentId == paymentId);
}

    }
}
