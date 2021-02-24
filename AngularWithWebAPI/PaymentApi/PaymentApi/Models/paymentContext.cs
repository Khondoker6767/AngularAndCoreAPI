using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApi.Models
{
    public class paymentContext:DbContext
    {
        public paymentContext(DbContextOptions<paymentContext> options):base(options)
        {

        }
       public DbSet<PaymentDetail> PaymentDetails { get; set; }
    }
}
