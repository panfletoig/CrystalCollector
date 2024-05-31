using static System.Console;

namespace Classes
{
    public class Level
    {
        private readonly int LevelSize;      //Tamaño del tablero 3 = 3x3
        private readonly int GemsPerLevel = 6; //Cristales que se pueden generar por nivel
        private readonly int[,] PosEnemys;   //Enemy Position
        private readonly string PlayerGender;  //Caracter que representa al player
        private readonly string EnemyRepresent; //Solo para visualizarlo en el testeo
        private readonly string[,] Board;    //tablero :3
        private int ActualLevel;         //Numero de nivel actual
        private int[] LastPlayerPos;  //Ultima posicion del player

		private bool NewLevel = false;

		private readonly int[] LevelSizes = [3, 4, 5, 6, 10];
		private readonly int[] EnemysPerLevel = [0, 1, 4, 7, 12];


		public Level(int ActualLevel, int[] PosPlayer, string PlayerGender, bool debug = false)
		{
			//Asignamos Variables
			this.ActualLevel = ActualLevel;
			this.LevelSize = LevelSizes[ActualLevel - 1];
			this.PlayerGender = PlayerGender;
			this.LastPlayerPos = [PosPlayer[0], PosPlayer[1]];
            this.Board = new string[LevelSize, LevelSize];
            this.PosEnemys = new int[LevelSize, LevelSize];
			
            this.EnemyRepresent = (debug) ? "X" : " ";

            /*
             * Orden de generacion
             * Tablero Vacio -> Player -> Portal -> Gemas -> Enemigos
             */
            GenerateEmptyLevel();
            SetPlayerPos(PosPlayer);
            GeneratePortal();
            GenerateGems();
            for (int i = 0; i < EnemysPerLevel[this.ActualLevel - 1]; i++)
            {
                GenerateEnemy();
            }
        }
		//Retorna el nivel actual
		public int GetActualLevel()
		{
			return this.ActualLevel;
		}
		//Retorna el tamaño del nivel
		public int GetLevelSize()
		{
			return this.LevelSizes[ActualLevel - 1];
		}
		//retorna las gemas maximas por nivel
		public int GetGemsPerLevel()
		{
			return this.GemsPerLevel;
		}
		//Retorna el numero maximo de niveles
		public int GetMaxLevel()
		{
			return LevelSizes.Length;
		}
		//Retorna si deberia crear un nuevo nivel
		public bool GetNewLevel()
		{
			return this.NewLevel;
		}
		//Cambia el valor de new level
		private void GenerateNewLevel()
		{
			this.NewLevel = true;
		}
		//Ve que hay en la siguiente coordenada a la que se movera el jugador
		public string GetNextBox(int[] CoordXY)
		{
			//Siguiente Coordenada :3
			int x = LastPlayerPos[0] + CoordXY[0];
			int y = LastPlayerPos[1] + CoordXY[1];

			//Operadores ternarios
			//(si x es menor a 0) ? dale el valor de 0 : (si x es mayor al tamano del tablero) ? dale el valor maximo : sino dale el valor original
			x = (x < 0) ? 0 : (x >= LevelSize) ? LevelSize - 1 : x;
			y = (y < 0) ? 0 : (y >= LevelSize) ? LevelSize - 1 : y;

			//Retorna el siguiente caracter por si hay un enemigo sino retorna el caracter de el tablero
			if (this.Board[y, x] == EnemyRepresent || this.Board[y, x] == " ")
			{
				return this.PosEnemys[y, x].ToString();
			}
			return this.Board[y, x];
		}
		//Llena la matriz con caracteres vacios y posiciones de enemigos en 0
		private void GenerateEmptyLevel()
        {
            for(int x = 0; x < LevelSize; x++)
            {
                for(int y = 0; y < LevelSize; y++)
                {
                    this.Board[x, y] = " ";
                    this.PosEnemys[x, y] = 0;
                }
            }
        }
		//Reduce el nivel actual en caso de perder
		public void SetLevelLose()
		{
			this.ActualLevel--;
			GenerateNewLevel();
		}
		public void SetLevelWin()
		{
			this.ActualLevel++;
			GenerateNewLevel();
		}
		//Coloca al player en el tablero
		public void SetPlayerPos(int[] PosPlayer)
		{
			//X y Y invertidos porque el array pos player esta invertido en el board
			//Borra la anterior posicion
			this.Board[this.LastPlayerPos[1], this.LastPlayerPos[0]] = " ";

			//Obtiene las nuevas coordenadas y reasigna este a lastPosition
			int x = PosPlayer[0];
			int y = PosPlayer[1];
			this.LastPlayerPos = [x, y];

			//Le asigna el carracter al tablero
			this.Board[y, x] = PlayerGender; //Posiciona al player
		}
		//Agrega un portal en coordenadas aleatorias exeptuando el nivel 1 a el tablero
		public void GeneratePortal()
		{
			if (ActualLevel != 1)
			{
				DrawInEmptyCoords("▒");
			}
			else
			{
				this.Board[2, 2] = "▒";
			}
		}
		//Agrega los enemigos al tablero y a la matriz de posicion
		private void GenerateEnemy()
        {
            DrawInEmptyCoords(EnemyRepresent, false);
		}
		//Agrega las gemas al tablero
		private void GenerateGems(bool debug = false)
		{
			//Par para que solo aparescan en casillas pares o impares
			int oddEven = 2;
			for (int i = 0; i < GemsPerLevel; i++)
			{
				if(ActualLevel == 1)
				{
					DrawInEmptyCoords("♦");
				}
				else
				{
					DrawAndDontTouch("♦", ref oddEven);
				}

				//Si se activa se puede ver como se va generando las gemas
				if (debug)
				{
					DisplayLevel();
				}
			}
		}
		//Si el player se topa con un enemigo se elimina y se crea uno nuevo
		public void ResetEnemy(int[] PosPlayer, int[] CoordXY)
        {
            this.PosEnemys[PosPlayer[1] + CoordXY[1], PosPlayer[0] + CoordXY[0]] = 0; //Elimina al enemigo
            GenerateEnemy(); //Genera uno nuevo
		}
		//Dibuja los diamantes para que no se toquen
		public void DrawAndDontTouch(string character, ref int oddEven)
		{
			Random rnd = new();
			bool isEmptySpace; //Almacenara si esta ocupado el espacio esta vacio
			while (true)
			{
				int x = rnd.Next(0, LevelSize); //Coordenada x aleatoria
				int y = rnd.Next(0, LevelSize); //Coordenada y aleatoria

				if(oddEven == 2)
				{
					oddEven = (x + y) % 2;
				}

				//comprueba si esta vacia la casilla de troll o enemigo
				isEmptySpace = (this.Board[x, y] == " ") && (this.PosEnemys[x, y] == 0);

				if (!isEmptySpace)
				{
					continue;
				}
				bool canDraw = true;

				if((x+y)%2 != oddEven)
				{
					canDraw = false;
				}

				if (canDraw)
				{
					Board[x, y] = character;
					break;
				}
			}
		}

		//Dibuja el caracter que pasamos como parametro y si va a ser visible
		private void DrawInEmptyCoords(string character, bool visible = true)
		{
			Random rnd = new();
			bool isEmptySpace; //Almacenara si esta ocupado el espacio esta vacio

			do
			{
				int x = rnd.Next(0, LevelSize); //Coordenada x aleatoria
				int y = rnd.Next(0, LevelSize); //Coordenada y aleatoria

				//comprueba si esta vacia la casilla de troll o enemigo
				isEmptySpace = (this.Board[x, y] == " ") && (this.PosEnemys[x, y] == 0);

				//Agrega el caracter si el espacio esta vacio
				if (isEmptySpace && visible)
				{
					this.Board[x, y] = character;
				}
                else if(isEmptySpace && !visible)
                {
                    this.PosEnemys[x, y] = 1;
					this.Board[x, y] = character;
				}
			} while (!isEmptySpace); //repite hasta que encuentre un lugar vacio
		}
		//Imprime el tablero
		public void DisplayLevel()
		{
			ForegroundColor = ConsoleColor.Blue;
			WriteLine($"Crystal Collector\n");

			string initSpace = "  "; //formato predeterminado



			ForegroundColor = ConsoleColor.DarkGray;
			Write($" {initSpace}");
			//Imprime cordenada X formato plano cartesiano 
			for (int i = 0; i < LevelSize; i++)
			{
				Write($"  {i} ");
			}
			WriteLine();

			//Imprime coordenada Y e Imprime el tablero con instruccuines
			string[] text = [
				"- ASDW o ←↑→↓ para moverse", 
				"- Recolecta todos los Cristales ♦", 
				"- Ve al portal ▒", 
				"- Es posible encontrar a trolls invisibles" 
				];


			for (int x = 0; x < LevelSize; x++)
			{
				//Imprime coordenada en el eje Y
				ForegroundColor = ConsoleColor.DarkGray;
				Write($"{x}{initSpace}");
				ResetColor();

				//Imprime el tablero
				for (int y = 0; y < LevelSize; y++)
				{
					Write($"| ");
					if (Board[x, y] == "♦")
					{
						ForegroundColor = ConsoleColor.Red;
						Write(Board[x, y]);
					}
					else if (Board[x, y] == "▒")
					{
						ForegroundColor = ConsoleColor.DarkMagenta;
						Write(Board[x, y]);
					}
					else if (Board[x, y] == "♂")
					{
						ForegroundColor = ConsoleColor.DarkCyan;
						Write(Board[x, y]);
					}
					else if (Board[x, y] == "♀")
					{
						ForegroundColor = ConsoleColor.DarkYellow;
						Write(Board[x, y]);
					}
					else
					{
						Write(Board[x, y]);
					}
					Write(" ");
					ResetColor();
				}
				Write("| ");

				//Imprime texto a la izquierda
				ForegroundColor = ConsoleColor.DarkGray;
				if (x < text.Length)
				{
					Write(text[x]);
				}
				WriteLine();
				ResetColor();
			}
			WriteLine("");
		}
	}
}



/*
 * Funcion desechada
 * Puede llegar a darse el caso que en el nivel 2
 * el tablero 4x4 se generen de tal manera que no permita la creacion 
 * del sexto diamante y se pete
 * 
 * Solucion
 * Una nueva funcion que si empieza generando en casillas par
 * solo salgan en casillas par y si empiezan en casillas impar
 * solo se generen en casillas impar
 * 
 * 
 * Ejemplo del error
 *      0   1   2   3
 *	0  | ♂ | ♦ |   | ♦ | - ASDW o ←↑→↓ para moverse
 *	1  |   |   | ♦ |   | - Recolecta todos los Cristales ♦
 *	2  | ♦ |   |   | ▒ | - Ve al portal ▒
 *	3  |   |   | ♦ |   | - Es posible encontrar a trolls invisibles
 *	
 *	No hay espacio para la 6ta gema
 *	Podria cambiar el orden de generacion y podria seguir pasando
 *	seria mas raro pero no imposible
 *	
 *	pero la nueva solucion es mas facil, rapida y funca :3
 *	
 *
public void DrawAndDontTouch(string character, ref int oddEven)
{
	Random rnd = new();
	bool isEmptySpace; //Almacenara si esta ocupado el espacio esta vacio
	while(true)  
	{
		int x = rnd.Next(0, LevelSize); //Coordenada x aleatoria
		int y = rnd.Next(0, LevelSize); //Coordenada y aleatoria

		//comprueba si esta vacia la casilla de troll o enemigo
		isEmptySpace = (this.Board[x, y] == " ") && (this.PosEnemys[x, y] == 0);

		if (!isEmptySpace)
		{
			continue;
		}

		bool[] direction = [
		true, //Izquierda
				true, //Abajo
				true, //Derecha
				true, //Arriba
				];

		bool canDraw = true;

		//Comprueba si esta en esquinas
		//X y Y invertidas
		//Izquierda
		if (y == 0)
		{
			direction[0] = false;
		}
		//Abajo
		if (x == LevelSize - 1)
		{
			direction[1] = false;
		}
		//Derecha
		if (y == LevelSize - 1)
		{
			direction[2] = false;
		}
		//Arriba
		if (x == 0)
		{
			direction[3] = false;
		}

		if (direction[0] && this.Board[x, y - 1] == character)
		{
			canDraw = false;
		}
		if (direction[1] && this.Board[x + 1, y] == character)
		{
			canDraw = false;
		}
		if (direction[2] && this.Board[x, y + 1] == character)
		{
			canDraw = false;
		}
		if (direction[3] && this.Board[x - 1, y] == character)
		{
			canDraw = false;
		}

		if (canDraw)
		{
			Board[x, y] = character;
			break;
		}
	}
}*/