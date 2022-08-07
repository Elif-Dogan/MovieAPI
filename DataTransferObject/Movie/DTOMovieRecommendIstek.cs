namespace DataTransferObject.Movie
{
    public class DTOMovieRecommendIstek
    {      
        public int MovieId { get; set; }  
        public string From { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string SenderMail { get; set; }
        public string Password { get; set; }
    }
}