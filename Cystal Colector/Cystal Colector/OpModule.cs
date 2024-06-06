using static System.Console;
using Classes;

namespace Crystal_Colector
{
	internal static class OpModule
	{
		//Se los copie a Unity :3
		public static void Start(bool debug = false)
		{
			string name = "";
			int gender = 2;

			if (!debug)
			{
				GetUserDataPreferences(ref name, ref gender); //Obtiene datos del usuario
			}

			Player player = new(name, gender); //Crea un objeto de la clase Player
			//Crea el primer nivel
			Level level = new(1, player.GetPos(), player.GetGender(), debug);
			
			//Ocultamos el cursor _
			CursorVisible = false;
			//Llamamos al update
			Update(player, level, debug);
		}

		//Ciclo principal
		public static void Update(Player player, Level level, bool debug = false)
		{
			while (true)
			{
				level.DisplayLevel(); //Nivel Actual
				player.DisplayState(level.GetGemsPerLevel()); //Imprime el estado del jugador
				player.DisplayBox(level.GetGemsPerLevel());

				int[] CoordXY = IOModules.AxisInput(); //Coordenada que se suma a donde busca moverse el jugador
				string character = level.GetNextBox(CoordXY); //retorna que hay en la siguente casilla

				AddPoints(CoordXY, character, ref player, ref level, debug); //Añade los puntos
				Movement(CoordXY, character, ref player, ref level); //Mueve al personaje

				//Rompe el ciclo si el player termino los 5 niveles o perdio todas las vidas
				if(level.GetActualLevel() > level.GetMaxLevel() || player.IsNotAlive())
				{
					Clear();
					if (player.IsNotAlive())
					{
						IOModules.DisplayLoseMessage();
					}
					else
					{
						IOModules.DisplayWinMessage();
					}
					player.DisplayState(level.GetGemsPerLevel());
					break;
				}

				GenerateNewLevel(ref player, ref level, debug); //Genera un nuevo nivel
				if(!debug) { Clear(); }
			}	
		}
		//Si es una trivia ejecuta el evento
		private static void AddPoints(int[] CoordXY, string character, ref Player player, ref Level level, bool debug = false)
		{
			//Si es una trivia la realiza
			if (character == "1")
			{
				if (!debug)
				{
					Clear();
				}
				if (IOModules.StartTrivia())
				{
					player.AddLifeGem();
					character = "2";
				}
				else
				{
					player.RemoveLifeGem();
					level.SetLevelLose();
				}
				level.ResetEnemy(player.GetPos(), CoordXY);
			}
			//Añade los puntos al player
			player.AddPoints(character, level.GetActualLevel(), level.GetGemsPerLevel()); //Suma los puntos
		}
		//Permite el movimiento
		private static void Movement(int[] CoordXY, string character, ref Player player, ref Level level)
		{
			bool canMove = true;
			bool allCrystals = player.GetCrystals() == level.GetGemsPerLevel();
			bool isPortal = character == "▒"; //Si el caracter es un portal = true

			//Si no es un portal sin todas las gemas genera un nuevo portal exepto nivel 1
			if (isPortal && !allCrystals && level.GetActualLevel() != 1)
			{
				level.GeneratePortal();
			}

			//Si es un portal y no tiene los cristales y es el nivel 1 detiene el movimiento
			if (isPortal && player.GetCrystals() != level.GetGemsPerLevel() && level.GetActualLevel() == 1)
			{
				canMove = false;
			}
			if (canMove)
			{
				player.SetPosByMove(CoordXY, level.GetLevelSize());
				level.SetPlayerPos(player.GetPos());
			}

			//Suma al nivel actual
			if (isPortal && allCrystals)
			{
				level.SetLevelWin();
			}
		}
		//Genera un nuevo nivel y resetea al player si se nesesita generar
		private static void GenerateNewLevel(ref Player player, ref Level level, bool debug)
		{
			if (level.GetNewLevel())
			{
				player.ResetPlayer(level.GetActualLevel(), level.GetLevelSize());
				level = new Level(level.GetActualLevel(), player.GetPos(), player.GetGender(), debug);
			}
		}
		//Obtiene el nombre y genero del usuario
		private static void GetUserDataPreferences(ref string name, ref int gender)
		{
			IOModules.DisplayColorMessage("Bienvenido a Crystal Collector!!!\n", ConsoleColor.Blue);

			name = IOModules.GetStringUserInput("¿Cómo deberìa llamarte?", "Nombre"); //Obtiene input de texto para nombrar a el player :3

			if(name.Length >= 15)
			{
				name = name[..12] + "...";
			}
			Clear();


			IOModules.DisplayColorMessage($"Bienvenido {name} a Crystal Collector!!!\n", ConsoleColor.Blue);//Imprime el mensaje con ese color
			
			IOModules.DisplayGenderSelection();
			gender = IOModules.GetIntUserInput(); //Obtiene input entero para seleccionar genero del jugador
			Clear();
		}
	}
}
