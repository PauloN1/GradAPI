namespace Auth0.NET.ViewModels
{
    public class LoginErrorViewModel
    {
        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public string Message { get; set; }
    }
}
