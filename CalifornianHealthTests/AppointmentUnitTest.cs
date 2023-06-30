using AppointmentService;
using AppointmentService.DTOs;
using AppointmentService.Models;
using AppointmentService.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CalifornianHealthTests
{
    public class AppointmentUnitTest
    {
        /// <summary>
        /// Test GetAllAppointments() method from AppointmentRepository
        /// </summary>
        [Fact]
        public async void TestGETallAppointmentsFromDatabase()
        {
            //ARRANGE
            // Create a connection to the SQL SERVER database
            var connection = new ConnectionClass();
            using (var context = connection.CreateContextSQlDatabase())
            {
                // Create Appointment objects to populate the database
                var appointment1 = new Appointment
                 {
                     StartDateTime = new DateTime(2023, 07, 10, 09, 00, 00),
                     EndDateTime = new DateTime(2023, 07, 10, 09, 30, 00),
                     ConsultantId = 1,
                     PatientId = "1"
                 };
                var appointment2 = new Appointment
                {
                    StartDateTime = new DateTime(2023, 07, 10, 10, 00, 00),
                    EndDateTime = new DateTime(2023, 07, 10, 10, 30, 00),
                    ConsultantId = 2,
                    PatientId = "5"
                };
                var appointment3 = new Appointment
                {
                    StartDateTime = new DateTime(2023, 07, 10, 11, 30, 00),
                    EndDateTime = new DateTime(2023, 07, 10, 12, 00, 00),
                    ConsultantId = 4,
                    PatientId = "8"
                };

                // We also need to create the ConsultantCalendars that are use to book these appointemnts,
                // as the CreateAppointment() method checks for the ConsultantCalendar's availability
                var consultantCalendar1 = new ConsultantCalendar
                {
                    ConsultantId = 1,
                    Date = new DateTime(2023, 07, 10, 09, 00, 00),
                    Available = true,
                };
                var consultantCalendar2 = new ConsultantCalendar
                {
                    ConsultantId = 2,
                    Date = new DateTime(2023, 07, 10, 10, 00, 00),
                    Available = true,
                };
                var consultantCalendar3 = new ConsultantCalendar
                {
                    ConsultantId = 4,
                    Date = new DateTime(2023, 07, 10, 11, 30, 00),
                    Available = true,
                };

                // ACT
                //Create the SQL SERVER database and save the ConsultantCalendar and appointment objects inside of it
                var consultantCalendarrepository = new ConsultantCalendarRepository(context);
                await consultantCalendarrepository.CreateConsultantCalendar(consultantCalendar1);
                await consultantCalendarrepository.CreateConsultantCalendar(consultantCalendar2);
                await consultantCalendarrepository.CreateConsultantCalendar(consultantCalendar3);

                var appointmentRepository = new AppointmentRepository(context);
                await appointmentRepository.CreateAppointment(appointment1);
                await appointmentRepository.CreateAppointment(appointment2);
                await appointmentRepository.CreateAppointment(appointment3);

                // Recover all appointments from the SQL SERVER database
                var results = appointmentRepository.GetAllAppointments();

                // ASSERT
                // Test that (all) the appointments are persistently saved inside the SQL SERVER database
                Assert.NotEmpty(results.Result);
                Assert.Collection(results.Result, item => Assert.Contains("1", item.PatientId),
                                                  item => Assert.Contains("5", item.PatientId),
                                                  item => Assert.Contains("8", item.PatientId));
            }

            

        }

        /// <summary>
        /// Test GetSingleAppointment() method from AppointmentRepository
        /// </summary>
        [Fact]
        public async void TestGETSingleAppointmentByIdFromDatabase()
        {
            //ARRANGE
            // Create a connection to the SQL SERVER database
            var connection = new ConnectionClass();
            using (var context = connection.CreateContextSQlDatabase())
            {
                // Create 1 Appointment object to populate the database
                var appointment1 = new Appointment
                {
                    StartDateTime = new DateTime(2023, 07, 10, 09, 00, 00),
                    EndDateTime = new DateTime(2023, 07, 10, 09, 30, 00),
                    ConsultantId = 1,
                    PatientId = "1"
                };             

                // We also need to create the ConsultantCalendar that is use to book this appointemnt,
                // as the CreateAppointment() method checks for the ConsultantCalendar's availability
                var consultantCalendar1 = new ConsultantCalendar
                {
                    ConsultantId = 1,
                    Date = new DateTime(2023, 07, 10, 09, 00, 00),
                    Available = true,
                };

                // ACT
                //Create the SQL SERVER database and save the ConsultantCalendar and appointment objects inside of it
                var consultantCalendarrepository = new ConsultantCalendarRepository(context);
                await consultantCalendarrepository.CreateConsultantCalendar(consultantCalendar1);

                var appointmentRepository = new AppointmentRepository(context);
                await appointmentRepository.CreateAppointment(appointment1);


                // Recover the specific appointment from the SQL SERVER database
                var result = appointmentRepository.GetSingleAppointment(1);

                // ASSERT
                // Test that the appointment is persistently saved inside the SQL SERVER database
                Assert.NotNull(result.Result);
            }
        }

        /// <summary>
        /// Test CreateAppointment() method from AppointmentRepository
        /// </summary>
        [Fact]
        public async void TestPOSTAppointmentToDatabase()
        {
            //ARRANGE
            // Create a connection to the SQL SERVER database
            var connection = new ConnectionClass();
            using (var context = connection.CreateContextSQlDatabase())
            {
                // Create 1 Appointment object to populate the database
                var appointment1 = new Appointment
                {
                    StartDateTime = new DateTime(2023, 07, 10, 09, 00, 00),
                    EndDateTime = new DateTime(2023, 07, 10, 09, 30, 00),
                    ConsultantId = 1,
                    PatientId = "1"
                };

                // We also need to create the ConsultantCalendar that is use to book this appointemnt,
                // as the CreateAppointment() method checks for the ConsultantCalendar's availability
                var consultantCalendar1 = new ConsultantCalendar
                {
                    ConsultantId = 1,
                    Date = new DateTime(2023, 07, 10, 09, 00, 00),
                    Available = true,
                };

                // ACT
                //Create the SQL SERVER database and save the ConsultantCalendar and appointment objects inside of it
                var consultantCalendarrepository = new ConsultantCalendarRepository(context);
                await consultantCalendarrepository.CreateConsultantCalendar(consultantCalendar1);

                var appointmentRepository = new AppointmentRepository(context);
                await appointmentRepository.CreateAppointment(appointment1);

                var result = appointmentRepository.GetSingleAppointment(1);

                // ASSERT
                // Test that the appointment was created inside the SQL SERVER database
                Assert.NotNull(result.Result);
            }
        }

        /// <summary>
        /// Test UpdateAppointment() method from AppointmentRepository
        /// </summary>
        [Fact]
        public async void TestPUTAppointmentToDatabase()
        {
            //ARRANGE
            // Create a connection to the SQL SERVER database
            var connection = new ConnectionClass();
            using (var context = connection.CreateContextSQlDatabase())
            {
                // Create 1 Appointment object to populate the database
                var appointment1 = new Appointment
                {
                    StartDateTime = new DateTime(2023, 07, 10, 09, 00, 00),
                    EndDateTime = new DateTime(2023, 07, 10, 09, 30, 00),
                    ConsultantId = 1,
                    PatientId = "1"
                };

                // We also need to create the ConsultantCalendar that is use to book this appointemnt,
                // as well as the ConsultantCalendar that the appointment will be updated to
                var consultantCalendar1 = new ConsultantCalendar
                {
                    ConsultantId = 1,
                    Date = new DateTime(2023, 07, 10, 09, 00, 00),
                    Available = true,
                };
                var consultantCalendar2 = new ConsultantCalendar
                {
                    ConsultantId = 2,
                    Date = new DateTime(2023, 07, 10, 09, 00, 00),
                    Available = true,
                };

                // ACT
                //Create the SQL SERVER database and save the ConsultantCalendar and appointment objects inside of it
                var consultantCalendarrepository = new ConsultantCalendarRepository(context);
                await consultantCalendarrepository.CreateConsultantCalendar(consultantCalendar1);
                await consultantCalendarrepository.CreateConsultantCalendar(consultantCalendar2);

                var appointmentRepository = new AppointmentRepository(context);
                await appointmentRepository.CreateAppointment(appointment1);

                // Recover the appointment from the SQL SERVER database, using its id and modify it
                var appointmentToUpdate = appointmentRepository.GetSingleAppointment(1);
                appointmentToUpdate.Result.ConsultantId = 2;

                // Update the appointment in the database
                await appointmentRepository.UpdateAppointment(appointmentToUpdate.Result);

                // Recover the updated appointment from the database
                var result = await appointmentRepository.GetSingleAppointment(1);

                // ASSERT
                // Test that the appointment was created inside the SQL SERVER database
                 Assert.Equal(2, result.ConsultantId);
            }
        }

        /// <summary>
        /// Test DeleteAppointment() method from AppointmentRepository
        /// </summary>
        [Fact]
        public async void TestDELETEAppointmentFromDatabase()
        {
            //ARRANGE
            // Create a connection to the SQL SERVER database
            var connection = new ConnectionClass();
            using (var context = connection.CreateContextSQlDatabase())
            {
                // Create 1 Appointment object to populate the database
                var appointment1 = new Appointment
                {
                    StartDateTime = new DateTime(2023, 07, 10, 09, 00, 00),
                    EndDateTime = new DateTime(2023, 07, 10, 09, 30, 00),
                    ConsultantId = 1,
                    PatientId = "1"
                };

                // We also need to create the ConsultantCalendar that is use to book this appointemnt,
                // as the CreateAppointment() method checks for the ConsultantCalendar's availability
                var consultantCalendar1 = new ConsultantCalendar
                {
                    ConsultantId = 1,
                    Date = new DateTime(2023, 07, 10, 09, 00, 00),
                    Available = true,
                };

                // ACT
                //Create the SQL SERVER database and save the ConsultantCalendar and appointment objects inside of it
                var consultantCalendarrepository = new ConsultantCalendarRepository(context);
                await consultantCalendarrepository.CreateConsultantCalendar(consultantCalendar1);

                var appointmentRepository = new AppointmentRepository(context);
                await appointmentRepository.CreateAppointment(appointment1);

                // Delete the appointment from the database, so the Appointments table should be empty
                await appointmentRepository.DeleteAppointment(1);

                // Get all appointments from the database
                var result = await appointmentRepository.GetAllAppointments();

                // ASSERT
                // Test that the appointment was deleted from the SQL SERVER database
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
                builder.UseSqlServer($"Server=192.168.1.227,1433;Database=CHdbTest;User=sa;Password=Str0ngPa$w0rd;").UseInternalServiceProvider(provider);

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