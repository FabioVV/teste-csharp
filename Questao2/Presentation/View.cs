using Questao2.Domain.Interface;


namespace Questao2.Presentation
{
    class View : IView
    {
        public IGoalService _goalService { get; set; }
        public View(IGoalService goalService)
        {
            _goalService = goalService;
        }

        public async Task Run()
        {
            try
            {
                string teamName = "Paris Saint-Germain";
                int year = 2013;
                int totalGoals = await _goalService.RequestScoredGoals(teamName, year);

                Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

                teamName = "Chelsea";
                year = 2014;
                totalGoals = await _goalService.RequestScoredGoals(teamName, year);

                Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

                Console.Write("Digite outro time: ");
                string AnotherteamName = Console.ReadLine() ?? "Time não reconhecido";

                Console.Write("Digite um ano: ");
                bool parseNum = int.TryParse(Console.ReadLine(), out int AnotherYear);
                if (!parseNum)
                {
                    Console.WriteLine("Por favor, digitar um número");
                    return;
                }

                totalGoals = await _goalService.RequestScoredGoals(AnotherteamName, AnotherYear);
                Console.WriteLine("Team " + AnotherteamName + " scored " + totalGoals.ToString() + " goals in " + AnotherYear);
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocorreu um erro: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Clique alguma tecla para finalizar o programa...");
                Console.ReadKey();
                Environment.Exit(1);
            }
        }

    }
}
