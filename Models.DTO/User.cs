namespace Models.DTO
{
    public class User
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int ClientID { get; set; }
        public string ClientName { get; set; }
        public string UserTypeName { get; set; }
        public int UserTypeID { get; set; }
        public string ClientCode { get; set; }
        public bool IsActive { get; set; }
        public bool IsVerified { get; set; }
        public string AddedBy { get; set; }
        public string UploadedFilePath { get; set; }
        public byte[] UserImage { get; set; }

        public int AddedByID { get; set; }
        public DateTime? AddedOn { get; set; }
        public string AddedOnText
        {
            get
            {
                if (AddedOn != null && AddedOn.Value.Year > 1900)
                {
                    return AddedOn.Value.ToString();
                }
                else
                {
                    return "";
                }
            }
        }
        public string ModifiedBy { get; set; }
        public int ModifiedByID { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedOnText
        {
            get
            {
                if (ModifiedOn != null && ModifiedOn.Value.Year > 1900)
                {
                    return ModifiedOn.Value.ToString();
                }
                else
                {
                    return "";
                }
            }
        }
        public string Departments { get; set; }
        public string CompanyName { get; set; }
        public string Designation { get; set; }
        public bool IsTermsAndConditionsAccepted { get; set; }
        public string SAMLMetadata { get; set; }
        public string AccountIDs { get; set; }
        public string AccountNames { get; set; }
        public string RegistrationCode { get; set; }
        public string Address { get; set; }
        public DateTime? RegistrationExpiryDate { get; set; }
        public string AllowedDomainNames { get; set; }
        public int PrimaryContactUserId { get; set; }
        public string TwoFactorAuthenticationSecretKey { get; set; }
        public string TwoFactorAuthenticationQRCodeFilePath { get; set; }
    }
}