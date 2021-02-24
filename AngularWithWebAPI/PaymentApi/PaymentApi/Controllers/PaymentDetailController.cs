﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentDetailController : ControllerBase
    {
        private readonly paymentContext _context;

        public PaymentDetailController(paymentContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDetail>>> GetPaymentDetails()
        {
            return await _context.PaymentDetails.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDetail>> GetPaymentDetailById(int id)
        {
            var paymentDetail = await _context.PaymentDetails.FindAsync(id);
            if (paymentDetail==null)
            {
                return NotFound();
            }
            return paymentDetail;
        }
        [HttpPost]
        public async Task<ActionResult<PaymentDetail>> PostPaymentDetail(PaymentDetail paymentDetail)
        {
            _context.PaymentDetails.Add(paymentDetail);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetPaymentDetails", new { id=paymentDetail.PaymentDetailId}, paymentDetail);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePaymentDetail(int id)
        {
            var paymentDetail = await _context.PaymentDetails.FindAsync(id);
            if (paymentDetail == null)
            {
                return NotFound();
            }
            _context.PaymentDetails.Remove(paymentDetail);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> PutPaymentDetail(int id, PaymentDetail obj)
        {
            if (id!=obj.PaymentDetailId)
            {
                return BadRequest();
            }
            _context.Entry(obj).State=EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!payDetailsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }                
            }
            return NoContent();
        }

        private bool payDetailsExists(int id)
        {
            return _context.PaymentDetails.Any(p=>p.PaymentDetailId==id);
        }
    }
}
