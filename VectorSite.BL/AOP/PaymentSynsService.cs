using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VectorSite.BL.Interfaces.Services;

namespace VectorSite.BL.AOP
{
    public class PaymentSynsService : BackgroundService
    {
        private readonly IServiceProvider serviceProvider;

        private ILogger<PaymentSynsService> logger;

        public PaymentSynsService(IServiceProvider serviceProvider, ILogger<PaymentSynsService> logger)
        {
            this.serviceProvider = serviceProvider;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = serviceProvider.CreateScope())
                    {
                        var paymentService = scope.ServiceProvider.GetRequiredService<IPaymentService>();

                        await SyncPaymentAsync(paymentService);
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError($"Error during payment sychronization: {ex.Message}");
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }

        private async Task SyncPaymentAsync(IPaymentService paymentService)
        {
            throw new NotImplementedException();
        }
    }
}
