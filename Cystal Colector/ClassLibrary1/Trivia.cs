using static System.Console;
using System.Text.Json;

namespace Classes
{
	public class Trivia
	{
		private List<string> Questions;
		private List<string[]> Answers;
		private List<int> Correct;

		private readonly string[] FileNames = ["Questions.json", "Answers.json", "Correct.json"];
		private readonly string folder = @"JsonFiles\";
		private readonly string[] Paths;
		private readonly JsonSerializerOptions options = new() { WriteIndented = true }; //Opciones de serializacion

		public Trivia()
        {
			Paths = new string[FileNames.Length];
			string domain = AppDomain.CurrentDomain.BaseDirectory + folder;
			
			//Si la carpeta no existe la crea
			if (!Directory.Exists(domain)) { Directory.CreateDirectory(domain);}

			Paths[0] = Path.Combine(domain, FileNames[0]);
			Paths[1] = Path.Combine(domain, FileNames[1]);
			Paths[2] = Path.Combine(domain, FileNames[2]);

			this.Questions = Deserializer<string>(0);
			this.Answers = Deserializer<string[]>(1);
			this.Correct = Deserializer<int>(2);
		}	
		//Obtiene la cantidad total del array de correctas que es igual al total de posibilidades
		public int GetLen()
		{
			return Correct.Count;
		}
		//Obtiene una pregunta
		public string GetQuestion(int Index)
		{
			return this.Questions[Index];
		}
		//Obtiene un set de respuestas
		public string[] GetAnswers(int Index)
		{
			return this.Answers[Index];
		}
		//Obtiene el indice de la respuesta correcta
		public int GetCorrect(int Index)
		{
			return this.Correct[Index];
		}
		//Agrega una nueva pregunta, respuesta e indice de correcta
		public void AddNewEntry(string newQuestion, string[] attachAnswers, int correctIndex)
		{
			try
			{
				AddQuestion(newQuestion); //Agrega la pregunta
				AddAnwers(attachAnswers); //Agrega las respuestas
				AddCorrect(correctIndex); //Agrega la respuesta correcta
			}
			catch (Exception e)
			{
				WriteLine($"{e.Message}");
			}
		}
		private void AddQuestion(string newQuestion)
		{
			this.Questions = Deserializer<string>(0); //Obtenemos las preguntas
			Questions.Add(newQuestion); //Agregamos la nueva
			Serializer<string>(Questions, 0); //Serializa
		}
		private void AddAnwers(string[] newAnswers)
		{
			Answers = Deserializer<string[]>(1); //Obtenemos
			Answers.Add(newAnswers); //Agregamos
			Serializer<string[]>(Answers, 1); //Serializamos
		}
		private void AddCorrect(int CorrectIndex)
		{
			Correct = Deserializer<int>(2); //Obtenemos
			Correct.Add(CorrectIndex); //Agrega
			Serializer<int>(Correct, 2); //Serializa
		}
		//Serializa el json
		private void Serializer<T>(List<T> list, int pathIndex)
		{
			//Obtiene el formato json y crea el archivo
			string json = JsonSerializer.Serialize(list, options);
			File.WriteAllText(Paths[pathIndex], json);
		}
		//Deserializa el json
		public List<T> Deserializer<T>(int PathIndex)
		{

			//Obtiene todos los datos del archivo y retorna la lista
			string json = File.ReadAllText(Paths[PathIndex]);
			return JsonSerializer.Deserialize<List<T>>(json)!;
		}
	}
}
