using static System.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cystal_Colector
{
	internal static class IOModules
	{
		public static void DisplayMainMenu()
		{
			string text = ""; 
			text += "Seleccione una opcion\n";
			text += "1. Iniciar nueva partida\n";
			text += "2. Instrucciones\n";
			text += "3. Informacion sobre Crystal Collector\n";
			text += "4. Salir del Juego\n";
			text += "(Opcion)> ";
			Write(text);
		}
		public static int GetIntUserInput()
		{
			int op = 0;
			if(int.TryParse(ReadLine(), out int result))
			{
				op = result;
			}
			return op;
		}

		public static void DisplayInstructions()
		{
			string text = "INSTRUCCIONES\n";
			text += "\n";
			text += "1. Selecciona tu personaje\n";
			text += "2. Asignale un nombre\n";
			text += "3. Muevete por el tablero\n";
			text += "\t-Reune todas las gemas ♦\n";
			text += "\t-Responde las preguntas de los trolls\n";
			text += "\t\t-Mala respuesta te regresara un nivel y te quitara una joya de vida\n";
			text += "\t\t-Responde correctamente y te dara una joya de vida\n";
			text += "4. Ve hacia el portal\n";
			text += "5. Termina los siguientes niveles\n";

			WriteLine(text);
		}

		public static void DisplayInfoCrystalCollector()
		{
			string text = "INFORMACION\n";
			text += "\n";
			text += "Tabla de Punteo";
			WriteLine(text);

			string[,] punteos = new string[2, 6];
			punteos[0, 0] = "Cristal";
			punteos[0, 1] = "Trivia Correcta";
			punteos[0, 2] = "Nivel Completo";
			punteos[0, 3] = "Nivel Final";
			punteos[0, 4] = "Trivia Incorrecta";
			punteos[0, 5] = "Portal sin obtener los 6 cristales";

			punteos[1, 0] = "+15";
			punteos[1, 1] = "+10";
			punteos[1, 2] = "+50";
			punteos[1, 3] = "+1000";
			punteos[1, 4] = "-20";
			punteos[1, 5] = "-5";

			//formato
			string formats = "|{0, -35}  ||  {1, 15}|";

			//Imprime lineas superiores a la tabla
			const int len = 58;
			DisplayTableDashLines('-', len); ;
			
			//Imprime en formato tabla
			WriteLine(format: formats,"Descripcion", "Puntos");
			for(int i = 0; i < 6; i++)
			{
				WriteLine(format: formats, $"{punteos[0, i]}", $"{punteos[1, i]}");
			}

			DisplayTableDashLines('-', len);

		}
		public static void DisplayError(Exception e)
		{
			WriteLine("\nOcurrio un error :c");
			WriteLine(e.Message);
			ReadLine();
			Clear();
		}

		public static void DisplayTableDashLines(char dashType, int len)
		{
			//Imprime lineas
			string dash = "";
			for (int i = 0; i < len; i++)
			{
				dash += dashType;
			}
			WriteLine(dash);
		}
	}
}
