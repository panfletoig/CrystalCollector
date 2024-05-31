using Classes;

namespace UnitTesting
{
	public class PlayerUT
	{
		[Fact]
		public void TestGenderSetFemale()
		{
			int gender = 1;
			string nombre = "Pan";
			Player player = new(nombre, gender);

			string expected = "♀"; 

			Assert.Equal(expected, player.GetGender());
		}
		[Fact]
		public void TestGenderSetMale()
		{
			int gender = 2;
			string nombre = "Pan";
			Player player = new(nombre, gender);

			string expected = "♂";

			Assert.Equal(expected, player.GetGender());
		}
		[Fact]
		public void TestCrystals()
		{
			int gender = 2;
			string nombre = "Pan";
			string crystal = "♦";
			int actualLevel = 1;
			int gemsPerLevel = 6;
			Player player = new(nombre, gender);
			player.AddPoints(crystal, actualLevel, gemsPerLevel);
			player.AddPoints(crystal, actualLevel, gemsPerLevel);

			int expected = 2;

			Assert.Equal(expected, player.GetCrystals());
		}
		[Fact]
		public void TestCrystalsReset()
		{
			int gender = 2;
			string nombre = "Pan";
			string crystal = "♦";
			int actualLevel = 1;
			int levelSize = 3;
			int gemsPerLevel = 6;
			Player player = new(nombre, gender);
			player.AddPoints(crystal, actualLevel, gemsPerLevel);
			player.AddPoints(crystal, actualLevel, gemsPerLevel);
			player.ResetPlayer(actualLevel, levelSize);

			int expected = 0;

			Assert.Equal(expected, player.GetCrystals());
		}
		[Fact]
		public void TestRemove1LifeGem()
		{
			int gender = 1;
			string nombre = "Pan";
			Player player = new(nombre, gender);
			player.RemoveLifeGem();
			
			Assert.False(player.IsNotAlive());
		}
		[Fact]
		public void TestRemove3LifeGem() 
		{
			int gender = 1;
			string nombre = "Pan";
			Player player = new(nombre, gender);
			player.RemoveLifeGem();
			player.RemoveLifeGem();
			player.RemoveLifeGem();

			Assert.True(player.IsNotAlive());
		}
		[Fact]
		public void TestPos()
		{
			int gender = 1;
			string nombre = "Pan";
			Player player = new(nombre, gender);

			int[] expected = [0, 0];
			
			Assert.Equal(expected, player.GetPos());
		}
		[Fact]
		public void TestPosAfterMove()
		{
			int gender = 1;
			string nombre = "Pan";
			Player player = new(nombre, gender);
			int levelSize = 3;
			int[] playerPos = [0, 1];
			player.SetPosByMove(playerPos, levelSize);

			int[] expected = [0, 1];

			Assert.Equal(expected, player.GetPos());
		}
		[Fact]
		public void TestPosAfterMoveOutsideBoardOnY()
		{
			int gender = 1;
			string nombre = "Pan";
			Player player = new(nombre, gender);
			int levelSize = 3;
			int[] playerPos = [0, 150];
			player.SetPosByMove(playerPos, levelSize);

			int[] expected = [0, 2];

			Assert.Equal(expected, player.GetPos());
		}
		[Fact]
		public void TestPosAfterMoveOutsideBoardOnX()
		{
			int gender = 1;
			string nombre = "Pan";
			Player player = new(nombre, gender);
			int levelSize = 3;
			int[] playerPos = [150, 0];
			player.SetPosByMove(playerPos, levelSize);

			int[] expected = [2, 0];

			Assert.Equal(expected, player.GetPos());
		}
		[Fact]
		public void TestPosAfterMoveOutsideBoardOnMinusY()
		{
			int gender = 1;
			string nombre = "Pan";
			Player player = new(nombre, gender);
			int levelSize = 3;
			int[] playerPos = [0, -150];
			player.SetPosByMove(playerPos, levelSize);

			int[] expected = [0, 0];

			Assert.Equal(expected, player.GetPos());
		}
		[Fact]
		public void TestPosAfterMoveOutsideBoardOnMinusX()
		{
			int gender = 1;
			string nombre = "Pan";
			Player player = new(nombre, gender);
			int levelSize = 3;
			int[] playerPos = [-150, 0];
			player.SetPosByMove(playerPos, levelSize);

			int[] expected = [0, 0];

			Assert.Equal(expected, player.GetPos());
		}
	}
}