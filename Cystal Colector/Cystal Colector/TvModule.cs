using static System.Console;
using Classes;

namespace Crystal_Colector
{
	internal static class TvModule
	{
		public static void TriviaModule()
		{
			Trivia trivia = new(); //Objeto de la clase trivia

			//Ciclo sin salida exepto el cerrar la consola
			while (true)
			{
				try
				{
					string newQuestion = "";
					string[] attachAnswers = new string[4];
					int correctIndex = 0;

					WriteLine("Trivia (de uso personal si quieres salir cierra la consola :3)\n");
					newQuestion = IOModules.GetStringUserInput("Agrega la pregunta ", "Pregunta"); //Obtiene la pregunta

					for (int i = 0; i < attachAnswers.Length; i++) 
					{
						attachAnswers[i] = IOModules.GetStringUserInput($"Agrega la repuesta No.{i+1} ", "Respuesta"); //Obtiene las respuestas
					}

					correctIndex = IOModules.GetIntUserInput("Selecciona la respuesta correcta\n", "(Opción)> "); //Obtiene el indice de la correcta
					Clear();

					//Imprime un resumen
					DisplaySummary(newQuestion, attachAnswers, correctIndex);

					//Si es correcto todo lo agrega
					if(IOModules.GetIntUserInput("Esta seguro de su respuesta\n1. Si\n2. No\n", "(Opción)> ") == 1)
					{
						trivia.AddNewEntry(newQuestion, attachAnswers, correctIndex);
					}
					Clear();
				}
				catch (Exception e) 
				{
					WriteLine(e.Message);
				}
			}
		}

		private static void DisplaySummary(string newQuestion, string[] attachAnswers, int correctIndex)
		{
			WriteLine("Resumen");
			WriteLine(newQuestion);
			for (int i = 0; i < attachAnswers.Length; i++)
			{
				WriteLine($"{i + 1}. {attachAnswers[i]}");
			}
			WriteLine($"Correcta: {correctIndex}\n");
		}
	}
}
