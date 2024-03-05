namespace PharmaceuticalSupplyChainSystem.Models
{
    public class MultiFactorAuthData
    {
        public string OTPSecretKey { get; set; }
        public DateTime LastOTPGeneration { get; set; }
        public string DeliveryEmail  { get; set; }
        public string[] RecoveryCodes { get; set; }
        public int AttemptCounter { get; set; }
    }
}
