using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACME.Domain.Models;

namespace ACME.Infrastructure
{
    public interface IPaymentGateway
    {
        bool ProcessPayment(Student student, Course course);
    }
}
