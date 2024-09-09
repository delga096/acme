using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACME.Domain.Models;

namespace ACME.Infrastructure
{
    public class MyPaymentGateway : IPaymentGateway
    {
        public bool ProcessPayment(Student student, Course course)
        {
            // Simulación del pago
            return true; // Simula que el pago fue exitoso
        }
    }
}
