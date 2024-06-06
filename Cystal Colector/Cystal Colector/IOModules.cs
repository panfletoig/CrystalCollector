using static System.Console;
using Classes;

namespace Cystal_Colector
{
	internal static class IOModules
	{
		//InputData

		//Obtiene la direccion de los ejes X y Y
		public static int[] AxisInput()
		{
			ConsoleKey keyPress = ReadKey().Key;
			WriteLine();
			int x = 0, y = 0;

			bool[] direction = [
					keyPress == ConsoleKey.A || keyPress == ConsoleKey.LeftArrow,
					keyPress == ConsoleKey.S || keyPress == ConsoleKey.DownArrow,
					keyPress == ConsoleKey.D || keyPress == ConsoleKey.RightArrow,
					keyPress == ConsoleKey.W || keyPress == ConsoleKey.UpArrow,
					keyPress == ConsoleKey.M,
				];

			//Izquierda
			if (direction[0]) { x = -1; }
			//Abajo
			else if (direction[1]) { y = 1; }
			//Derecha
			else if (direction[2]) { x = 1; }
			//Arriba
			else if (direction[3]) { y = -1; }
			else if (direction[4]) { x = 3; y = 3; }
			return [x, y];
		}
		//Obtiene una valor entero
		public static int GetIntUserInput(string text = "", string indicator = "")
		{
			while (true) 
			{
				Write(text + indicator);
				if(int.TryParse(ReadLine(), out int result))
				{
					return result;
				}
				else
				{
					WriteLine("¡¡Debe ser un valor entero valido!!");
				}
			}
		}
		//Obtiene una cadena
		public static string GetStringUserInput(string text, string indicator = "Opcion") 
		{
			WriteLine(text);
			Write($"({indicator})> ");
			return ReadLine()!;
		}

		//Output
		//Usado en los menus para imprimir las opciones
		private static void DisplayOptions(string title, string[] options, string indicator = "")
		{
			string text = $"";
			ForegroundColor = ConsoleColor.Yellow;
			WriteLine(title);

			ForegroundColor = ConsoleColor.Cyan;
			for (int i = 0; i < options.Length; i++)
			{
				//[..1] al parecer abrebia .substring(0,1) y si es un guion imprime una tabulacion
				if (options[i][..1].Equals("-"))
				{
					text += "\t";
					if (options[i][..2].Equals("--"))
					{
						text += $"\t";
						options[i] = options[i][1..]; //[1..] = SubString(1, options.length - 1)
					}

					text += $"{options[i]} \n";
					continue;
				}
				text += $" {i + 1}. {options[i]} \n";
			}
			Write(text);

			ResetColor();
			Write(indicator);
		}

		//Imprime el menu Principal
		public static void DisplayMainMenu()
		{
			string title = "Seleccione una opcion (Ej. 3)";
			string[] options = [
				"Iniciar nueva partida",
				"Instrucciones",
				"Informacion sobre Crystal Collector",
				"Salir del Juego"
				];
			string indicator = "(Options)> ";
			
			DisplayOptions(title, options, indicator);
		}

		//Imprime Las Instucciones
		public static void DisplayInstructions()
		{
			string title = "INSTRUCCIONES\n";
			string[] options = [
				"Asignale un nombre",
				"Selecciona tu personaje",
				"Muevete por el tablero",
				"-Reune todas las gemas ♦",
				"-Responde las preguntas de los trolls",
				"--Mala respuesta te regresara un nivel y te quitara una joya de vida",
				"--Responde correctamente y te dara una joya de vida",
				"Ve hacia el portal",
				"Termina los siguientes niveles"
				];
			DisplayOptions(title, options);
		}
		//Imprime La informacion de Cristal Collector
		public static void DisplayInfoCrystalCollector()
		{
			ForegroundColor = ConsoleColor.Yellow;
			WriteLine("INFORMACION\n");
			
			ForegroundColor = ConsoleColor.DarkGray;
			WriteLine("Tabla de Punteo");

			string[,] points = new string[2, 6];
			points[0, 0] = "Cristal";
			points[0, 1] = "Trivia Correcta";
			points[0, 2] = "Nivel Completo";
			points[0, 3] = "Nivel Final";
			points[0, 4] = "Trivia Incorrecta";
			points[0, 5] = "Portal sin obtener los 6 cristales";

			points[1, 0] = "+15";
			points[1, 1] = "+10";
			points[1, 2] = "+50";
			points[1, 3] = "+1000";
			points[1, 4] = "-20";
			points[1, 5] = "-5";

			//formato
			string formats = "|{0, -35}  ||  {1, 15}|";

			//Imprime lineas superiores a la tabla
			const int len = 58;

			DisplayTableDashLines('-', len); ;
			
			//Imprime en formato tabla
			WriteLine(format: formats,"Descripcion", "Puntos");
			for(int i = 0; i < 6; i++)
			{
				WriteLine(format: formats, $"{points[0, i]}", $"{points[1, i]}");
			}

			DisplayTableDashLines('-', len);

			ResetColor();
		} 
		//Imprime lineas para dar formato a la tabla de informacion
		public static void DisplayTableDashLines(char dashType, int len)
		{
			string dash = "";
			for (int i = 0; i < len; i++)
			{
				dash += dashType;
			}
			WriteLine(dash);
		}
		//Imprime el menu de selecion de genero
		public static void DisplayGenderSelection()
		{
			string title = "Eres chico o chica? (ej. 1)";
			string[] options = [
				"Chica (♀)",
				"Chico (♂)"
				];
			string indicator = "(Genero) > ";

			DisplayOptions(title, options, indicator);
		}
		//Imprime la trivia
		public static bool StartTrivia() 
		{
			DisplayBull();
			CursorVisible = true;

			Random rnd = new();
			Trivia trivia = new();

			int index = rnd.Next(0, trivia.GetLen());
			string question = trivia.GetQuestion(index);
			string[] answers = trivia.GetAnswers(index);
			int correct = trivia.GetCorrect(index);
			
			ForegroundColor = ConsoleColor.Yellow;
			WriteLine($"¿{question}?");
			ForegroundColor = ConsoleColor.Cyan;
			for(int i = 0; i < answers.Length; i++)
			{
				WriteLine($"{i+1}. {answers[i]}");
			}
			ResetColor();

			//Cuando la opcion seleccionada se encuentre en el rango sigue
			int option;
			do
			{
				option = GetIntUserInput("", "(Opcion)> ");
			} while (option <= 0 || option > 4);

			CursorVisible = false;

			//Si la opcion es igual a la correcta devuelbe true
			return option == correct;
		}

		//Espera confirmacion del usuario
		//Imprime el error y borra la consola
		public static void DisplayError(Exception e)
		{
			//Imprime el error y tambien limpia la consola
			WriteLine("\nOcurrio un error :c");
			WriteLine(e.Message);
			ReadLine();
			Clear();
		}
		//Imprime
		public static void DisplayLoseMessage()
		{
			string text = "";
			text += "   :::   :::  ::::::::  :::    :::          :::        ::::::::   ::::::::  :::::::::: \n";
			text += "  :+:   :+: :+:    :+: :+:    :+:          :+:       :+:    :+: :+:    :+: :+:        \n";
			text += "  +:+ +:+  +:+    +:+ +:+    +:+          +:+       +:+    +:+ +:+        +:+          \n";
			text += "  +#++:   +#+    +:+ +#+    +:+          +#+       +#+    +:+ +#++:++#++ +#++:++#      \n";
			text += "  +#+    +#+    +#+ +#+    +#+          +#+       +#+    +#+        +#+ +#+           \n";
			text += " #+#    #+#    #+# #+#    #+#          #+#       #+#    #+# #+#    #+# #+#     \n";
			text += "###     ########   ########           ########## ########   ########  ########## \n";

			ForegroundColor = ConsoleColor.DarkGray;
			WriteLine(text);
			ResetColor();
		}
		//Imprime un You Win
		public static void DisplayWinMessage()
		{
			string text = "";
			text += "   :::   ::: :::::::: :::    :::      :::       ::::::::::::::::::    :::\n";
			text += "  :+:   :+::+:    :+::+:    :+:      :+:       :+:    :+:    :+:+:   :+:\n";
			text += "  +:+ +:+ +:+    +:++:+    +:+      +:+       +:+    +:+    :+:+:+  +:+\n";
			text += "  +#++:  +#+    +:++#+    +:+      +#+  +:+  +#+    +#+    +#+ +:+ +#+\n";
			text += "  +#+   +#+    +#++#+    +#+      +#+ +#+#+ +#+    +#+    +#+  +#+#+#\n";
			text += " +#+   +#+    +#++#+    +#+      +#+ +#+#+ +#+    +#+    +#+  +#+#+#\n";
			text += "###    ########  ########         ###   ###  ##############    ####\n";

			ForegroundColor = ConsoleColor.DarkGray;
			WriteLine(text);
			ResetColor();
		}
		//Imprime un toro :3
		private static void DisplayBull()
		{
			string asciiArt = "";
			asciiArt += "  i    -\"       |l                ].    /      i\n";
			asciiArt += " ,\" .:j         `8o  _,,+.,.--,   d|   `:::;    b\n";
			asciiArt += " i  :'|          \"88p;.  (-.\"_\"-.oP        \\.   :\n";
			asciiArt += " ; .  (            >,%%%   f),):8\"          \\:'  i\n";
			asciiArt += "i  :: j          ,;%%%:; ; ; i:%%%.,        i.   `.\n";
			asciiArt += "i  `: ( ____  ,-::::::' ::j  [:```          [8:   )\n";
			asciiArt += "<  ..``'::::8888oooooo.  :(jj(,;,,,         [8::  <\n";
			asciiArt += "`. ``:.      oo.8888888888:;%%%8o.::.+888+o.:`:'  |\n";
			asciiArt += " `.   `        `o`88888888b`%%%%%88< Y888P\"\"'-    ;\n";
			asciiArt += "   \"`---`.       Y`888888888;;.,\"888b.\"\"\"..::::'-'\n";
			asciiArt += "           \"-....  b`8888888:::::.`8888._::-\"\n";
			asciiArt += "             `:::. `:::::O:::::::.`%%'|\n";
			asciiArt += "              `.      \"``::::::''    .'\n";
			asciiArt += "                `.                   <\n";
			asciiArt += "                  +:         `:   -';\n";
			asciiArt += "                   `:         : .::/\n";
			asciiArt += "                    ;+_  :::. :..;;;\n";
			asciiArt += "                    ;;;;,;;;;;;;;,;;\n";


			ForegroundColor = ConsoleColor.DarkGray;
			WriteLine(asciiArt);
			WriteLine("¡Oh no! Un enemigo");
			WriteLine("Si respondes correctamente te dejara pasar\n");

			ResetColor();
		}
	}
}
