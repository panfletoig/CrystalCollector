using Classes;

namespace UnitTesting
{
	public class LevelUT
	{
		[Fact]
		public void TestGetActualLevel()
		{
			int actualLevel = 1;
			int[] posPlayer = [0, 0];
			string playerGender = "♀";
			Level level = new(actualLevel, posPlayer, playerGender);

			int expected = 1;

			Assert.Equal(expected, level.GetActualLevel());
		}
		[Fact]
		public void TestLevelSize()
		{
			int actualLevel = 5;
			int[] posPlayer = [0, 0];
			string playerGender = "♀";
			Level level = new(actualLevel, posPlayer, playerGender);

			int expected = 10;

			Assert.Equal(expected, level.GetLevelSize());
		}
		[Fact]
		public void TestMaxLevel()
		{
			int actualLevel = 5;
			int[] posPlayer = [0, 0];
			string playerGender = "♀";
			Level level = new(actualLevel, posPlayer, playerGender);

			int expected = 5;

			Assert.Equal(expected, level.GetMaxLevel());
		}
		[Fact]
		public void TestGenerateNewLevel()
		{
			int actualLevel = 5;
			int[] posPlayer = [0, 0];
			string playerGender = "♀";
			Level level = new(actualLevel, posPlayer, playerGender);
			level.SetLevelLose();

			Assert.True(level.GetNewLevel());
		}
	}
}