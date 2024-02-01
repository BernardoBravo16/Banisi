namespace Banisi.Application.Customers.UseCases.GetCustomerInformationUseCase
{
    public class CustomerInformationModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Identification { get; set; }
        public string IdentificationType { get; set; }
        public DateTime? Birthdate { get; set; }
        public string PlaceBirth { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
    }
}