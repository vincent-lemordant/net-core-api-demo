using Moq;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.Services;

namespace WebApplication1Test.Services;

public class PlayerServiceTest
{
    public class PlayerServiceTests
    {
        private readonly Mock<IBaseRepository<Player>> _repositoryMock;
        private readonly PlayerService _playerService;

        public PlayerServiceTests()
        {
            _repositoryMock = new Mock<IBaseRepository<Player>>();
            _playerService = new PlayerService(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsAllPlayers()
        {
            // Arrange
            var mockPlayers = new List<Player>
            {
                new Player
                {
                    Firstname = "Rafael",
                    Lastname = "Nadal",
                },
                new Player
                {
                    Firstname = "Novak",
                    Lastname = "Djokovic",
                },
                new Player
                {
                    Firstname = "Serena",
                    Lastname = "Williams",
                },
            };

            _repositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(mockPlayers);

            // Act
            var players = await _playerService.GetAll();

            // Assert
            Assert.Equal(mockPlayers, players);
        }

        [Fact]
        public async Task GetById_ReturnsCorrectPlayer()
        {
            // Arrange
            var mockPlayer = new Player
            {
                Id = 7,
                Firstname = "Novak",
                Lastname = "Djokovic",
            };

            _repositoryMock.Setup(repo => repo.GetById(7)).ReturnsAsync(mockPlayer);

            // Act
            var player = await _playerService.GetById(7);

            // Assert
            Assert.Equal(mockPlayer, player);
        }

        [Fact]
        public async Task GetCountryWithBestWinningRatio_ReturnsCorrectCountry()
        {
            // Arrange
            var countryA = new Country { Code = "SRB" };
            var countryB = new Country { Code = "FRA" };
            var countryC = new Country { Code = "CAN" };
            var players = new List<Player>
            {
                new Player { Country = countryA, Data = new Data { Last = [ 1, 0, 1 ], Height = 180, Weight = 75000 } },
                new Player { Country = countryB, Data = new Data { Last = [ 1, 1 ], Height = 175, Weight = 68000 } },
                new Player { Country = countryC, Data = new Data { Last = [ 0, 0 ], Height = 190, Weight = 90000 } }
            };

            _repositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(players);

            // Act
            var bestCountry = await _playerService.GetCountryWithBestWinningRatio();

            // Assert
            Assert.Equal(countryB, bestCountry);
        }

        [Fact]
        public async Task GetAverageIMC_ReturnsCorrectAverageIMC()
        {
            // Arrange
            var players = new List<Player>
            {
                new Player { Data = new Data { Height = 180, Weight = 75000 } },
                new Player { Data = new Data { Height = 175, Weight = 68000 } },
                new Player { Data = new Data { Height = 190, Weight = 90000 } }
            };

            _repositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(players);

            // Act
            var averageIMC = await _playerService.GetAverageIMC();

            // Assert
            Assert.NotNull(averageIMC);
            Assert.Equal(23.43, Math.Round(averageIMC.Value, 2));
        }

        // Test for GetMedianHeight with odd players count
        [Fact]
        public async Task GetMedianHeight_ReturnsCorrectMedianHeight_WhenOddPlayersCount()
        {
            // Arrange
            var players = new List<Player>
            {
                new Player { Data = new Data { Height = 180 } },
                new Player { Data = new Data { Height = 175 } },
                new Player { Data = new Data { Height = 190 } }
            };

            _repositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(players);

            // Act
            var medianHeight = await _playerService.GetMedianHeight();

            // Assert
            Assert.Equal(180, medianHeight);
        }


        // Test for GetMedianHeight with even players count
        [Fact]
        public async Task GetMedianHeight_ReturnsCorrectMedianHeight_WhenEvenPlayersCount()
        {
            // Arrange
            var players = new List<Player>
            {
                new Player { Data = new Data { Height = 180 } },
                new Player { Data = new Data { Height = 175 } },
                new Player { Data = new Data { Height = 184 } },
                new Player { Data = new Data { Height = 190 } }
            };

            _repositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(players);

            // Act
            var medianHeight = await _playerService.GetMedianHeight();

            // Assert
            Assert.Equal(182, medianHeight);
        }

        // Test for GetMedianHeight when no players
        [Fact]
        public async Task GetMedianHeight_ThrowsInvalidOperationException_WhenNoPlayers()
        {
            // Arrange
            var players = new List<Player>();

            _repositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(players);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _playerService.GetMedianHeight());
        }
    }
}