using static System.Console;
using System.Diagnostics;

namespace Classes
{
	public class Player
	{
		private readonly string Name;//Nombre del personaje
        private int lives;          //Vidas del personaje
        private int Crystals;       //Cristales obtenidos
        private int Points;         //Puntos obtenidos
        private int[] PosPlayer;    //Posicion del Player
		private string Gender = "";     //Genero ♀ = Mujer, ♂ = Hombre 
		private readonly Dictionary<string, int> PointValues = new()
        {
            { "♦", 15 },  //Diamante
            { "+", 10 },  //Trivia bien
            { "+▒", 50 }, //Portal nivel completo
            { "▒", 1000 }, //Portal nivel final
            { "-", -20 }, //Trivia mal
            { "-▒", -5 }, //Portal sin completar
        };

		public Player(string Name, int Gender)
        {
            //Asignacion de variables
            this.Name = Name;
            this.lives = 3;
            this.Points = 0;
            this.Crystals = 0;
            this.PosPlayer = [0, 0];

			//Seteo de genero
			SetGender(Gender);
        }
        //Retorna vidas restantes
		public bool IsNotAlive()
		{
			return this.lives <= 0;
		}
        //Retorna el genero del player
		public string GetGender()
		{
			return this.Gender;
		}
        //Retorna la posicion del player
		public int[] GetPos()
		{
			return this.PosPlayer;
		}
        //Retorna los cristales obtenidas
		public int GetCrystals()
		{
			return this.Crystals;
		}
		//Agrega vida
		public void AddLifeGem()
		{
			//Operador Ternario
			//si las vidas del player superan 3 las setea a 3 maximo
			this.lives = (this.lives + 1 > 3) ? 3 : this.lives + 1;
		}
		//Le quita una vida
		public void RemoveLifeGem()
		{
			this.lives--;
		}
		//Resetea los cristales y posicion
		public void ResetPlayer(int actualLevel, int levelSize)
		{
			ResetCrystals();
			ResetPos(actualLevel, levelSize);
		}
		//Resetea la posicion del player a una aleatoria exeptuando nivel 1
		public void ResetPos(int actualLevel, int levelSize)
		{
			Random rnd = new();

			this.PosPlayer[0] = rnd.Next(0, levelSize);
			this.PosPlayer[1] = rnd.Next(0, levelSize);

			if (actualLevel == 1)
			{
				this.PosPlayer = [0, 0];
			}
		}
		//Setea a 0 los cristales
		private void ResetCrystals()
		{
			Crystals = 0;
		}
		//Coloca el genero
		private void SetGender(int Gender)
        {
            //Si el usuario eligio 1 = mujer de otra forma eligio Hombre :3
            if(Gender == 1)
            {
                this.Gender = "♀";
            }
            else if(Gender == 2)
            {
                this.Gender = "♂";
			}
        }
        //Actualiza las coordenadas del player
        public void SetPosByMove(int[] CoordXY, int levelSize)
        {
            //Mueve a la siguiente coordenada
            this.PosPlayer[0] += CoordXY[0];
			this.PosPlayer[1] += CoordXY[1];

            //Operador ternario
            //Si la posicion no esta entre el rango le asigna los valores minimos y maximos
            this.PosPlayer[0] = (PosPlayer[0] < 0) ? 0 : (PosPlayer[0] >= levelSize) ? levelSize - 1 : PosPlayer[0];
            this.PosPlayer[1] = (PosPlayer[1] < 0) ? 0 : (PosPlayer[1] >= levelSize) ? levelSize - 1 : PosPlayer[1];
        }
		//Agrega los puntos correspondientes
		public void AddPoints(string character, int actualLevel, int gemsPerLevel)
        {
            string prefix = ""; //Prefijo para determinar el punteo
            bool portal = character.Equals("▒"); //Si el caracter es un portal
            bool crystals = this.Crystals >= gemsPerLevel; //Si obtubo todos los cristales

            //Asignamos el prefijo dependiento de el portal
            //Si es nivel final prefijo = "", si es nivel completo prefijo = "+", si es nivel sin completar prefijo = "-"
            //Si gano la trivia character = "+", si perdio trivia character = "-"
			if (portal && crystals && actualLevel == 5)
            {
				prefix = "";
			}
            else if(portal && crystals)
            {
                prefix = "+";
            }
            else if(portal && !crystals)
            {
                prefix = "-";
            }
            else if(character.Equals("2"))
            {
                character = "+";
            }
            else if(character.Equals("1"))
            {
                character = "-";
            }

            //Se agrega para luego realizar busqueda en el diccionario
            character = prefix + character;

            //Buscamos si esta en el diccionario
			if (PointValues.TryGetValue(character, out int value))
            {
                //Si es un dimante se añade un cristal al inventario
                if(character.Equals(PointValues.Keys.ElementAt(0)))
                {
                    this.Crystals++;
                }
                //Se le agregan los puntos
                this.Points += value;
            }
        }
		//Imprime la caja de cristales
		public void DisplayBox(int gemsPerLevel)
		{
			int margin = 2;
			WriteLine("Caja Recolectora");
			Write(" ");
			for(int i = 0; i < gemsPerLevel * 3 + margin; i++) {
				Write("_");
			}
			WriteLine();

			Write("|");
			for(int i = 0; i < gemsPerLevel; i++) {
				if(this.Crystals == gemsPerLevel)
				{
					Write("    Ve al portal  ");
					break;
				}
				else if (i < this.Crystals)
				{
					Write("  ♦");
				}
				else
				{
					Write("   ");
				}
			}
			WriteLine("  |");

			Write(" ");
			for (int i = 0; i < gemsPerLevel * 3 + margin; i++)
			{
				Write("¯");
			}
			WriteLine();
		}
        //Imprime una tabla con los datos del Player :3
		public void DisplayState(int gemsPerLevel)
		{
			int[] lens = [15, 20, 20, 10, 10];

			//Crea un formato para la tabla con los tamaños de celda de lens
			string formats = "";
			for (int i = 0; i < lens.Length; i++)
			{
				formats += "|{" + $"{i}, {lens[i]}" + "}";
			}
			formats += "|";

			//Coloca una tapa superior a la tabla
			Write(" ");
			for (int i = 0; i < (lens.Length + lens.Sum()) - 1; i++)
			{
				Write("_");
			}
			WriteLine();

			WriteLine(format: formats, "Nombre", "Cristales Restantes", "Cristales Obtenidos", "Puntos", "Coords");
			WriteLine(format: formats, this.Name, gemsPerLevel - this.Crystals, this.Crystals, this.Points, $"{this.PosPlayer[0]},{this.PosPlayer[1]}");

			//Coloca una tapa al final de la tabla
			Write(" ");
			for (int i = 0; i < (lens.Length + lens.Sum()) - 1; i++)
			{
				Write("¯");
			}

			WriteLine();
		}
	}
}
