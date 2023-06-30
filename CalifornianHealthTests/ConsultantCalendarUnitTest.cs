using AppointmentService;
using AppointmentService.Models;
using AppointmentService.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalifornianHealthTests
{
    public class ConsultantCalendarUnitTest
    {
        /// <summary>
        /// Test GetAllConsultantCalendars() method from ConsultantCalendarRepository
        /// </summary>
        [Fact]
        public async void TestGETallConsultantCalendarsFromDatabase()
        {
            //ARRANGE
            // Create a connection to the SQL SERVER database
            var connection = new ConnectionClass();
            using (var context = connection.CreateContextSQlDatabase())
            {
                // Create ConsultantCalendars objects to populate the database
                var consultantCalendar1 = new ConsultantCalendar
                {
                    ConsultantId = 1,
                    Date = new DateTime(2023,07,03,09,00,00),
                    Available = true,
                };
                var consultantCalendar2 = new ConsultantCalendar
                {
                    ConsultantId = 2,
                    Date = new DateTime(2023, 07, 03,10, 00, 00),
                    Available = true,
                };
                var consultantCalendar3 = new ConsultantCalendar
                {
                    ConsultantId = 4,
                    Date = new DateTime(2023, 07, 03, 14, 30, 00),
                    Available = true,
                };

                // ACT
                //Create the SQL SERVER database and save the ConsultantCalendar objects inside of it
                var consultantCalendarRepository = new ConsultantCalendarRepository(context);
                await consultantCalendarRepository.CreateConsultantCalendar(consultantCalendar1);
                await consultantCalendarRepository.CreateConsultantCalendar(consultantCalendar2);
                await consultantCalendarRepository.CreateConsultantCalendar(consultantCalendar3);

                // Recover all consultantCalendars from the SQL SERVER database
                var results = consultantCalendarRepository.GetAllConsultantCalendars();

                // ASSERT
                // Test that (all) the consultantCalendars are persistently saved inside the SQL SERVER database
                Assert.NotEmpty(results.Result);
            }
        }

        /// <summary>
        /// Test GetConsultantCalendarById() method from ConsultantCalendarRepository
        /// </summary>
        [Fact]
        public async void TestGETSingleConsultantCalendarByIdFromDatabase()
        {
            //ARRANGE
            // Create a connection to the SQL SERVER database
            var connection = new ConnectionClass();
            using (var context = connection.CreateContextSQlDatabase())
            {
                // Create 1 ConsultantCalendar object to populate the database
                var consultantCalendar1 = new ConsultantCalendar
                {
                    ConsultantId = 1,
                    Date = new DateTime(2023, 07, 03, 09, 00, 00),
                    Available = true,
                };

                // ACT
                //Create the SQL SERVER database and save the ConsultantCalendar object inside of it
                var consultantCalendarRepository = new ConsultantCalendarRepository(context);
                await consultantCalendarRepository.CreateConsultantCalendar(consultantCalendar1);

                 // Recover the specific ConsultantCalendar, with its id, from the SQL SERVER database
                var result = consultantCalendarRepository.GetConsultantCalendarById(1);

                // ASSERT
                // Test that the ConsultantCalendar is persistently saved inside the SQL SERVER database
                Assert.NotNull(result.Result);
            }
        }

        /// <summary>
        /// Test GetConsultantCalendarsByConsultantId() method from ConsultantCalendarRepository
        /// </summary>
        [Fact]
        public async void TestGETConsultantCalendarsByConsultantIdFromDatabase()
        {
            //ARRANGE
            // Create a connection to the SQL SERVER database
            var connection = new ConnectionClass();
            using (var context = connection.CreateContextSQlDatabase())
            {
                // Create ConsultantCalendars objects to populate the database
                var consultantCalendar1 = new ConsultantCalendar
                {
                    ConsultantId = 1,
                    Date = new DateTime(2023, 07, 03, 09, 00, 00),
                    Available = true,
                };
                var consultantCalendar2 = new ConsultantCalendar
                {
                    ConsultantId = 2,
                    Date = new DateTime(2023, 07, 03, 10, 00, 00),
                    Available = true,
                };
                var consultantCalendar3 = new ConsultantCalendar
                {
                    ConsultantId = 4,
                    Date = new DateTime(2023, 07, 03, 14, 30, 00),
                    Available = true,
                };
                var consultantCalendar4 = new ConsultantCalendar
                {
                    ConsultantId = 1,
                    Date = new DateTime(2023, 07, 03, 11, 00, 00),
                    Available = true,
                };

                // ACT
                //Create the SQL SERVER database and save the ConsultantCalendar object inside of it
                var consultantCalendarRepository = new ConsultantCalendarRepository(context);
                await consultantCalendarRepository.CreateConsultantCalendar(consultantCalendar1);
                await consultantCalendarRepository.CreateConsultantCalendar(consultantCalendar2);
                await consultantCalendarRepository.CreateConsultantCalendar(consultantCalendar3);
                await consultantCalendarRepository.CreateConsultantCalendar(consultantCalendar4);

                // Recover all ConsultantCalendar for a given consultant, with their ConsultantId, from the SQL SERVER database
                var result = consultantCalendarRepository.GetConsultantCalendarsByConsultantId(1);

                // ASSERT
                // Test that the ConsultantCalendars are persistently saved inside the SQL SERVER database, and can be filtered by their ConsultantId
                Assert.NotNull(result.Result);
                Assert.Equal(2, result.Result.Count);
            }
        }

        /// <summary>
        /// Test CreateConsultantCalendar() method from ConsultantCalendarRepository
        /// </summary>
        [Fact]
        public async void TestPOSTConsultantCalendarToDatabase()
        {
            //ARRANGE
            // Create a connection to the SQL SERVER database
            var connection = new ConnectionClass();
            using (var context = connection.CreateContextSQlDatabase())
            {
                // Create 1 ConsultantCalendar object to populate the database
                var consultantCalendar1 = new ConsultantCalendar
                {
                    ConsultantId = 1,
                    Date = new DateTime(2023, 07, 03, 09, 00, 00),
                    Available = true,
                };

                // ACT
                //Create the SQL SERVER database and save the ConsultantCalendar object inside of it
                var consultantCalendarrepository = new ConsultantCalendarRepository(context);
                await consultantCalendarrepository.CreateConsultantCalendar(consultantCalendar1);

                //Recover the created ConsultantCalendar , by its ConsultantId, from the SQL SERVER database
                var result = consultantCalendarrepository.GetConsultantCalendarsByConsultantId(1);

                // ASSERT
                // Test that the ConsultantCalendar was created inside the SQL SERVER database
                Assert.NotNull(result.Result);
                Assert.Single(result.Result);
            }
        }

        /// <summary>
        /// Test UpdateConsultantCalendar() method from ConsultantCalendarRepository
        /// </summary>
        [Fact]
        public async void TestPUTAppointmentToDatabase()
        {
            //ARRANGE
            // Create a connection to the SQL SERVER database
            var connection = new ConnectionClass();
            using (var context = connection.CreateContextSQlDatabase())
            {
                // Create 1 ConsultantCalendar object to populate the database
                var consultantCalendar1 = new ConsultantCalendar
                {
                    ConsultantId = 1,
                    Date = new DateTime(2023, 07, 03, 09, 00, 00),
                    Available = true,
                };

                // ACT
                //Create the SQL SERVER database and save the ConsultantCalendar object inside of it
                var consultantCalendarRepository = new ConsultantCalendarRepository(context);
                await consultantCalendarRepository.CreateConsultantCalendar(consultantCalendar1);

                // Recover the ConsultantCalendar from the SQL SERVER database, using its id and modify it
                var consultantCalendarToUpdate = await consultantCalendarRepository.GetConsultantCalendarById(1);
                consultantCalendarToUpdate.Available = false;

                // Update the ConsultantCalendar in the database
                await consultantCalendarRepository.UpdateConsultantCalendar(consultantCalendarToUpdate);

                // Recover the updated ConsultantCalendar from the database
                var result = await consultantCalendarRepository.GetConsultantCalendarById(1);

                // ASSERT
                // Test that the ConsultantCalendar was updated inside the SQL SERVER database
                Assert.False(result.Available);
            }
        }

        /// <summary>
        /// Test DeleteConsultantCalendar() method from ConsultantCalendarRepository
        /// </summary>
        [Fact]
        public async void TestDELETEConsultantCalendarFromDatabase()
        {
            //ARRANGE
            // Create a connection to the SQL SERVER database
            var connection = new ConnectionClass();
            using (var context = connection.CreateContextSQlDatabase())
            {
                // Create 1 ConsultantCalendar object to populate the database
                var consultantCalendar1 = new ConsultantCalendar
                {
                    ConsultantId = 1,
                    Date = new DateTime(2023, 07, 03, 09, 00, 00),
                    Available = true,
                };

                // ACT
                //Create the SQL SERVER database and save the ConsultantCalendar object inside of it
                var consultantCalendarRepository = new ConsultantCalendarRepository(context);
                await consultantCalendarRepository.CreateConsultantCalendar(consultantCalendar1);

                // Delete the ConsultantCalendar from the database, so the ConsultantCalendar table should be empty
                await consultantCalendarRepository.DeleteConsultantCalendar(1);

                // Get all ConsultantCalendars from the database
                var result = await consultantCalendarRepository.GetAllConsultantCalendars();

                // ASSERT
                // Test that the ConsultantCalendar was deleted from the SQL SERVER database
                Assert.Empty(result);
            }
        }

        //Add a class containing the methods that create the connections
        //this class implement the IDisposable interface to free unmanaged resources (= close the connection) at the end of each test
        public class ConnectionClass : IDisposable
        {
            //indicate that the Dispose() method has already been run to prevent it from running while we create the connections
            private bool disposedValue;


            // create sql server connection context 
            public CHDBContext CreateContextSQlDatabase()
            {
                var provider = new ServiceCollection().AddEntityFrameworkSqlServer().BuildServiceProvider();
                var builder = new DbContextOptionsBuilder<CHDBContext>();
                builder.UseSqlServer($"Server=192.168.1.227,1433;Database=CHdbTest3;User=sa;Password=Str0ngPa$w0rd;").UseInternalServiceProvider(provider);

                var context = new CHDBContext(builder.Options);
                if (context != null)
                {
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();
                }
                return context;
            }

            //Add a Dispose() method to close the connection when the test is over and free resources
            protected virtual void Dispose(bool disposing)
            {
                if (!disposedValue)
                {
                    if (disposing)
                    {
                        // TODO: dispose managed state (managed objects)
                    }

                    // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                    // TODO: set large fields to null
                    disposedValue = true;
                }
            }

            public void Dispose()
            {
                // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
                Dispose(disposing: true);
                GC.SuppressFinalize(this);
            }
        }
    }
}
