using Microsoft.Extensions.DependencyInjection;
using Questao2.Domain.Interface;
using Questao2.Infra.Helpers;
using Questao2.Presentation;

public class Program
{
    public static async Task Main()
    {
        ServiceCollection services = new ServiceCollection();

        services.AddHttpClient("goalClient", client =>
        {
            client.BaseAddress = new Uri("https://jsonmock.hackerrank.com/api/football_matches");
        });

        services.AddTransient<IGoalService, GoalService>();
        services.AddSingleton<IGoalHelper, GoalHelper>();
        services.AddSingleton<IHttpHelper, HttpHelper>();
        services.AddSingleton<IView, View>();

        ServiceProvider serviceProvider = services.BuildServiceProvider();
        IView view = serviceProvider.GetRequiredService<IView>();
        await view.Run();

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

}