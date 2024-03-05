namespace PharmaceuticalSupplyChainSystem.Services.Interfaces
{
    public interface IEmailService
    {
        public Task SendMail(string from, string to, string subject, string body)
        {
           throw new NotImplementedException();
        }
    }
}
