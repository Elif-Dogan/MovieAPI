using DomainService;

namespace API.Service
{
    public class MovieService:BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    MovieServis servis = new MovieServis();
                    servis.GetAllMovies();
                }
                catch (Exception ex) { }
                await Task.Delay(TimeSpan.FromSeconds(3600));
            }
        }
    }
}