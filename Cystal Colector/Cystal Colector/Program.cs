﻿using static System.Console;

namespace Crystal_Colector
{
	internal class Program
	{
		static void Main()
		{
			//Cambia el titulo de la consola -> Console.Title
			Title = "Crystal Collector";
			//Cambiar la codificación de salida de la consola
			OutputEncoding = System.Text.Encoding.UTF8;

			OpModule.Start(true);

			//Usada en el desarollo para agregar preguntas a la trivia :3
			//True para solo entrar trivia module pero no al juego
			bool triviaModule = false;
			if(triviaModule) { TvModule.TriviaModule(); } //Si esta activo entra al modulo

			while (true && !triviaModule)
			{
				try
				{
					int op = 0; //Opcion seleccionada por el usuario
					do
					{
						//Permite ver el cursor
						CursorVisible = true;

						IOModules.DisplayMainMenu(); //Imprime el menu principal
						op = IOModules.GetIntUserInput(); //Espera obtener datos enteros del usuario
						
						//Limpia la pantalla
						Clear();

						switch(op)
						{
							case 1:
								OpModule.Start(); //Acepta un parametro booleano true para ver a los trolls o testear el juego
								break;
							case 2:
								IOModules.DisplayInstructions(); //Imprime las instucciones
								break;
							case 3:
								IOModules.DisplayInfoCrystalCollector(); //Imprime informacion de crystal collector
								break;
							case 4:
								WriteLine("\nSaliendo del juego :3");
								break;
							default:
								Clear();
								continue;
						}

						//Esperamos una confirmacion y borra la consola
						CursorVisible = false;
						if(op != 4)
						{
							WriteLine("\n(Presiona \"ENTER\" para continuar)\n");
							ReadLine();
						}
						Clear();
					}while (op != 4);
					break;
				}
				catch(Exception e)
				{
					IOModules.DisplayError(e); //Imprime el error
				}
			}
		}
	}
}
