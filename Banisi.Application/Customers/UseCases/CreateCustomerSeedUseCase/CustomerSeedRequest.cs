using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Banisi.Application.Customers.UseCases.CreateCustomerSeedUseCase
{
    public class CustomerSeedRequest
    {
        public string Seed { get; set; }
    }
}