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
    public class ConsultantUnitTest
    {
        /// <summary>
        /// Test GetAllConsultants() method from ConsultantRepository
        /// </summary>
        [Fact]
        public async void TestGETallConsultantsFromDatabase()
        {
            //ARRANGE
            // Create a connection to the SQL SERVER database
            var connection = new ConnectionClass();
            using (var context = connection.CreateContextSQlDatabase())
            {
                // Create Consultant objects to populate the database
                var consultant1 = new Consultant
                {
                   FName = "John",
                   LName = "Carter",
                   Speciality = "Pediatrician"
                };
                var consultant2 = new Consultant
                {
                    FName = "Peter",
                    LName = "Benton",
                    Speciality = "Cardiologist"
                };
                var consultant3 = new Consultant
                {
                    FName = "Elizabeth",
                    LName = "Corday",
                    Speciality = "Oncologist"
                }; ;

                // ACT
                //Create the SQL SERVER database and save the Consultant objects inside of it
                var consultantRepository = new ConsultantRepository(context);
                await consultantRepository.CreateConsultant(consultant1);
                await consultantRepository.CreateConsultant(consultant2);
                await consultantRepository.CreateConsultant(consultant3);

                // Recover all consultants from the SQL SERVER database
                var results = consultantRepository.GetAllConsultants();

                // ASSERT
                // Test that (all) the consultants are persistently saved inside the SQL SERVER database
                Assert.NotEmpty(results.Result);
                Assert.Collection(results.Result, item => Assert.Contains("Carter", item.LName),
                                                  item => Assert.Contains("Benton", item.LName),
                                                  item => Assert.Contains("Corday", item.LName));
            }
        }

        /// <summary>
        /// Test GetSingleConsultant() method from ConsultantRepository
        /// </summary>
        [Fact]
        public async void TestGETSingleConsultantByIdFromDatabase()
        {
            //ARRANGE
            // Create a connection to the SQL SERVER database
            var connection = new ConnectionClass();
            using (var context = connection.CreateContextSQlDatabase())
            {
                // Create 1 Consultant object to populate the database
                var consultant1 = new Consultant
                {
                    FName = "Mark",
                    LName = "Greene",
                    Speciality = "Anesthesiologist"
                };

                // ACT
                //Create the SQL SERVER database and save the Consultant object inside of it
                var consultantRepository = new ConsultantRepository(context);
                await consultantRepository.CreateConsultant(consultant1);

                // Recover the specific Consultant from the SQL SERVER database
                var result = consultantRepository.GetSingleConsultant(1);

                // ASSERT
                // Test that the consultant is persistently saved inside the SQL SERVER database
                Assert.NotNull(result.Result);
            }
        }

        /// <summary>
        /// Test CreateConsultant() method from ConsultantRepository
        /// </summary>
        [Fact]
        public async void TestPOSTConsultantToDatabase()
        {
            //ARRANGE
            // Create a connection to the SQL SERVER database
            var connection = new ConnectionClass();
            using (var context = connection.CreateContextSQlDatabase())
            {
                // Create 1 Consultant object to populate the database
                var consultant1 = new Consultant
                {
                    FName = "Kerry",
                    LName = "Weaver",
                    Speciality = "Immunologist"
                };

                // ACT
                //Create the SQL SERVER database and save the Consultant object inside of it
                var consultantRepository = new ConsultantRepository(context);
                await consultantRepository.CreateConsultant(consultant1);

                var result = consultantRepository.GetSingleConsultant(1);

                // ASSERT
                // Test that the Consultant was created inside the SQL SERVER database
                Assert.NotNull(result.Result);
            }
        }

        /// <summary>
        /// Test UpdateConsultant() method from ConsultantRepository
        /// </summary>
        [Fact]
        public async void TestPUTConsultantToDatabase()
        {
            //ARRANGE
            // Create a connection to the SQL SERVER database
            var connection = new ConnectionClass();
            using (var context = connection.CreateContextSQlDatabase())
            {
                // Create 1 Consultant object to populate the database
                var consultant1 = new Consultant
                {
                    FName = "Doug",
                    LName = "Ross",
                    Speciality = "Emergency Medecine"
                };

                // ACT
                //Create the SQL SERVER database and save the Consultan object inside of it
                var consultantRepository = new ConsultantRepository(context);
                await consultantRepository.CreateConsultant(consultant1);

                // Recover the consultant from the SQL SERVER database, using its id and modify it
                var consultantToUpdate = consultantRepository.GetSingleConsultant(1);
                consultantToUpdate.Result.Speciality = "General Surgery";

                // Update the Consultant in the database
                await consultantRepository.UpdateConsultant(consultantToUpdate.Result);

                // Recover the updated Consultant from the database
                var result = await consultantRepository.GetSingleConsultant(1);

                // ASSERT
                // Test that the Consultant was created inside the SQL SERVER database
                Assert.Equal("General Surgery", result.Speciality);
            }
        }

        /// <summary>
        /// Test DeleteConsultant() method from ConsultantRepository
        /// </summary>
        [Fact]
        public async void TestDELETEConsultantFromDatabase()
        {
            //ARRANGE
            // Create a connection to the SQL SERVER database
            var connection = new ConnectionClass();
            using (var context = connection.CreateContextSQlDatabase())
            {
                // Create 1 Consultant object to populate the database
                var consultant1 = new Consultant
                {
                    FName = "Carol",
                    LName = "Hathaway",
                    Speciality = "Family Medecine"
                };

                // ACT
                //Create the SQL SERVER database and save the Consultant object inside of it
                var consultantRepository = new ConsultantRepository(context);
                await consultantRepository.CreateConsultant(consultant1);

                // Delete the Consultant from the database, so the Consultant table should be empty
                await consultantRepository.DeleteConsultant(1);

                // Get all Consultants from the database
                var result = await consultantRepository.GetAllConsultants();

                // ASSERT
                // Test that the Consultant was deleted from the SQL SERVER database
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
                builder.UseSqlServer($"Server=192.168.1.227,1433;Database=CHdbTest2;User=sa;Password=Str0ngPa$w0rd;").UseInternalServiceProvider(provider);

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
