using System;
using System.Threading.Tasks;
using BL.CustomExceptions;
using System.Collections.Generic;
using Command.Interfaces;
using Command.Commands;

namespace ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            bool showMenu = true;
            var exitCommand = new ExitCommand();
            var currentWeatherCommand = new GetCurrentWeatherCommand();
            var forecastCommand = new GetWeatherForecastCommand();

            var list = new List<ICommand>() 
            {
                exitCommand, currentWeatherCommand, forecastCommand
            };

            while (showMenu)
            {
                showMenu = await MainMenu(list);
            }
        }

        private static async Task<bool> MainMenu(List<ICommand> list)
        {
            try
            {
                Console.WriteLine("Chose an option:");

                for (int i = 0; i < list.Count; i++)
                {
                    Console.WriteLine(i + list[i].Text);
                }

                var selectedNumber = int.Parse(Console.ReadLine());

                if (selectedNumber > 2 || selectedNumber < 0)
                    return true;

                await list[selectedNumber].Execute();
            }

            catch (EmptyInputException ex)
            {
                Console.WriteLine(ex.Message);
            }

            catch (IncorrectDaysRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }

            catch (FormatException)
            {
                Console.WriteLine("Incorrect menu input");
            }

            catch (Exception)
            {
                Console.WriteLine("Server error");
            }

            return true;
        }
    }
}
