namespace API.ViewModel
{
    public class ResponseClient
    {
        public string message { get; set; }
        public int statusCode { get; set; }

        public ResponseLogin data { get; set; }
    }
}
