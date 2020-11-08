using System;
using System.Linq;
using ClientApi.Data.Database;
using ClientApi.Data.Repository;
using ClientApi.Data.Tests.Infrastructure;
using ClientApi.Domain.Entities;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace ClientApi.Data.Tests.Repository
{
    public class RepositoryTests : DatabaseTestBase
    {
        private readonly ClientContext _ClientContext;
        private readonly Repository<Client> _testee;
        private readonly Repository<Client> _testeeFake;
        private readonly Client _newClient;

        public RepositoryTests()
        {
            _ClientContext = A.Fake<ClientContext>();
            _testeeFake = new Repository<Client>(_ClientContext);
            _testee = new Repository<Client>(Context);
            _newClient = new Client()
            {
                FirstName = "Son",
                LastName = "Goku",
                BirthDate = new DateTime(737, 04, 16),
                GenderId = 1,
                Address = "some address",
                PhoneNumber = "1234567890"
            };
        }

        [Theory]
        [InlineData("Changed")]
        public async void UpdateClientAsync_WhenClientIsNotNull_ShouldReturnClient(string firstName)
        {
            var client = Context.Clients.First();
            client.FirstName = firstName;

            var result = await _testee.UpdateAsync(client);

            result.Should().BeOfType<Client>();
            result.FirstName.Should().Be(firstName);
        }

        [Fact]
        public void AddAsync_WhenEntityIsNull_ThrowsException()
        {
            _testee.Invoking(x => x.AddAsync(null)).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void AddAsync_WhenExceptionOccurs_ThrowsException()
        {
            A.CallTo(() => _ClientContext.SaveChangesAsync(default)).Throws<Exception>();

            _testeeFake.Invoking(x => x.AddAsync(new Client())).Should().Throw<Exception>().WithMessage("entity could not be saved: Exception of type 'System.Exception' was thrown.");
        }

        [Fact]
        public async void CreateClientAsync_WhenCustomerIsNotNull_ShouldReturnCustomer()
        {
            var result = await _testee.AddAsync(_newClient);

            result.Should().BeOfType<Client>();
        }

        [Fact]
        public async void CreateClientAsync_WhenClientIsNotNull_ShouldAddClient()
        {
            var ClientCount = Context.Clients.Count();

            await _testee.AddAsync(_newClient);

            Context.Clients.Count().Should().Be(ClientCount + 1);
        }

        [Fact]
        public void GetAll_WhenExceptionOccurs_ThrowsException()
        {
            A.CallTo(() => _ClientContext.Set<Client>()).Throws<Exception>();

            _testeeFake.Invoking(x => x.GetAllAsync()).Should().Throw<Exception>().WithMessage("Couldn't retrieve entities: Exception of type 'System.Exception' was thrown.");
        }

        [Fact]
        public void UpdateAsync_WhenEntityIsNull_ThrowsException()
        {
            _testee.Invoking(x => x.UpdateAsync(null)).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void UpdateAsync_WhenExceptionOccurs_ThrowsException()
        {
            A.CallTo(() => _ClientContext.SaveChangesAsync(default)).Throws<Exception>();

            _testeeFake.Invoking(x => x.UpdateAsync(new Client())).Should().Throw<Exception>().WithMessage("entity could not be updated Exception of type 'System.Exception' was thrown.");
        }
    }
}
