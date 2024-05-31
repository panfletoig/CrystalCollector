using static System.Console;

namespace Cystal_Colector
{
	internal class Program
	{
		static void Main()
		{
			//Cambia el titulo de la consola -> Console.Title
			Title = "Crystal Collector";

			while (true)
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
						Clear();

						switch(op)
						{
							case 1:
								OpModule.Start();
								break;
							case 2:
								IOModules.DisplayInstructions(); //Imprime las instucciones
								break;
							case 3:
								IOModules.DisplayInfoCrystalCollector(); //Imprime informacion de crystal collector
								break;
							case 4:
								WriteLine("\nSaliendo :3");
								break;
							default:
								WriteLine("\n> El dato ingresado debe ser de tipo entero entre el rango (1 - 4) <");
								break;
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
