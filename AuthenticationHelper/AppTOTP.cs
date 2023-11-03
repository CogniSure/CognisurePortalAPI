using MsSqlAdapter.Interface;
using OtpNet;
using QRCoder;
using Services.MsSqlServices.Interface;
using System.Drawing.Imaging;

namespace AuthenticationHelper
{
    public class AppTOTP : BaseAuthentication
    {
        private readonly IMsSqlDatabase _iMsSqlDatabase;
        public AppTOTP(IMsSqlDatabase iMsSqlDatabase)
        {
            this._iMsSqlDatabase = iMsSqlDatabase;
        }
        public override bool ValidateOTP(string Email, string AuthOTP, string secretkey, out string generalMessage, out string technicalMessage)
        {

            var totp = new Totp(Base32Encoding.ToBytes(secretkey));
            if (totp.VerifyTotp(AuthOTP, out long timeStepMatched, new VerificationWindow(previous: 1, future: 1)))
            {
                generalMessage = string.Empty;
                technicalMessage = string.Empty;
                return true;
            }
            else
            {
                generalMessage = "Please enter correct OTP.";
                technicalMessage = string.Empty;
                return false;
            }
        }
        public override bool GenerateOTP(string Email, out string generalMessage, out string technicalMessage)
        {

            generalMessage = string.Empty;
            technicalMessage = "apptotp";
            return true;
        }
        public override string GenerateqrCodeUri(string email, string appName, string QRCodefilePath, string secret)
        {
            byte[] QRContent = default(byte[]);
            if (secret == null || secret == "" || secret == string.Empty)
            {
                secret = Base32Encoding.ToString(KeyGeneration.GenerateRandomKey(20));
            }
            string qrCodeUri = $"otpauth://totp/{email}?secret={secret}&issuer={appName}";

            var qrGenerator = new QRCodeGenerator();
            using (var qrCodeData = qrGenerator.CreateQrCode(qrCodeUri, QRCodeGenerator.ECCLevel.L))
            {
                using (var qrCode = new QRCode(qrCodeData))
                {
                    var qrCodeImage = qrCode.GetGraphic(10);
                    MemoryStream ms = new MemoryStream();
                    qrCodeImage.Save(ms, ImageFormat.Png);
                    QRContent = ms.ToArray();
                }
            }
            string QRcodebase64 = Convert.ToBase64String(QRContent);
            _iMsSqlDatabase.Enable2fa(email, secret, QRCodefilePath, QRcodebase64, "update", out string gmessage, out string tmessage);

            return qrCodeUri;
        }
    }
}
