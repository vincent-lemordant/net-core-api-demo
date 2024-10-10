using WebApplication1.Models;

namespace WebApplication1Test.Models;

public class PlayerServiceTest
{
    public class PlayerTests
    {
        // Test for WinningRatio property
        [Fact]
        public void WinningRatio_CalculatesCorrectly_WhenGamesExist()
        {
            // Arrange
            var player = new Player
            {
                Data = new Data
                {
                    Last = [1, 0, 1, 1] // 3 wins out of 4
                }
            };

            // Act
            var winningRatio = player.WinningRatio;

            // Assert
            Assert.NotNull(winningRatio);
            Assert.Equal(0.75, winningRatio); // 3/4 = 0.75
        }

        [Fact]
        public void WinningRatio_IsNull_WhenNoGamesExist()
        {
            // Arrange
            var player = new Player
            {
                Data = new Data
                {
                    Last = [] // No games played
                }
            };

            // Act
            var winningRatio = player.WinningRatio;

            // Assert
            Assert.Null(winningRatio); // No games should return null
        }

        // Test for IMC property
        [Fact]
        public void IMC_CalculatesCorrectly_WhenDataExists()
        {
            // Arrange
            var player = new Player
            {
                Data = new Data
                {
                    Height = 180, // Height in cm
                    Weight = 75000 // Weight in grams
                }
            };

            // Act
            var imc = player.IMC;

            // Assert
            Assert.NotNull(imc);
            Assert.Equal(23.15, Math.Round(imc.Value, 2)); // IMC = 75 / (1.80 * 1.80) = ~23.15
        }

        [Fact]
        public void IMC_IsNull_WhenDataIsMissing()
        {
            // Arrange
            var player = new Player
            {
                Data = null // No data available
            };

            // Act
            var imc = player.IMC;

            // Assert
            Assert.Null(imc); // No data should return null
        }

        [Fact]
        public void IMC_IsNull_WhenHeightOrWeightIsMissing()
        {
            // Arrange
            var playerWithNoHeight = new Player
            {
                Data = new Data
                {
                    Height = null,
                    Weight = 75000
                }
            };

            var playerWithNoWeight = new Player
            {
                Data = new Data
                {
                    Height = 180,
                    Weight = null
                }
            };

            // Act
            var imcNoHeight = playerWithNoHeight.IMC;
            var imcNoWeight = playerWithNoWeight.IMC;

            // Assert
            Assert.Null(imcNoHeight); // No height should return null
            Assert.Null(imcNoWeight); // No weight should return null
        }
    }
}