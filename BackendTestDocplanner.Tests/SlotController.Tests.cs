using BackendTestDocplanner.Controllers.Helpers;
using BackendTestDocplanner.Services.Slot;
using BackendTestDocplanner.Services.Slot.Schemas;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace BackendTestDocplanner.Tests
{
    public class Tests
    {
        [SetUpFixture]
        public class TestSetup
        {
            public static TestServer Server { get; private set; }
            public static HttpClient Client { get; private set; }

            [OneTimeSetUp]
            public void OneTimeSetup()
            {
                var builder = new WebHostBuilder()
                    .UseStartup<BackendTestDocplanner.Startup>(); // Ajusta según el Startup de tu aplicación

                Server = new TestServer(builder);

                Client = Server.CreateClient();
            }

            [OneTimeTearDown]
            public void OneTimeTearDown()
            {
                Client.Dispose();
                Server.Dispose();
            }
        }

        [Test]
        public void CalculateAvailableSlots_ShouldReturnCorrectAvailableSlots()
        {
            // Define work period
            var slotDurationMinutes = 30;
            var workPeriod = new WorkPeriod(9, 12, 13, 17); // 9-12 and 13-17

            // Define busy slots
            var busySlots = new Slot[]
            {
                new Slot(new DateTime(2023, 7, 10, 9, 0, 0), new DateTime(2023, 7, 10, 9, 30, 0)),
                new Slot(new DateTime(2023, 7, 10, 13, 0, 0), new DateTime(2023, 7, 10, 13, 30, 0)),
                new Slot(new DateTime(2023, 7, 10, 15, 0, 0), new DateTime(2023, 7, 10, 15, 30, 0)),
            };

            // Create availability for monday
            var mondayAvailability = new DailyAvailability(workPeriod, busySlots);

            // Create GetAvailabilityResponse
            var availabilityResponse = new FacilityWeeklyAvailability(
                new Facility("1234", "Test Facility", "Test Address"),
                slotDurationMinutes,
                mondayAvailability,
                null,
                null,
                null,
                null,
                null,
                null
            );

            // Define expected available slots for monday
            var expectedAvailableSlots = new List<Slot>
            {
                // From 9 to 12
                // new SlotModel(new DateTime(2023, 7, 10, 9, 0, 0), new DateTime(2023, 7, 10, 9, 30, 0)),
                new Slot(new DateTime(2023, 7, 10, 9, 30, 0), new DateTime(2023, 7, 10, 10, 0, 0)),
                new Slot(new DateTime(2023, 7, 10, 10, 0, 0), new DateTime(2023, 7, 10, 10, 30, 0)),
                new Slot(new DateTime(2023, 7, 10, 10, 30, 0), new DateTime(2023, 7, 10, 11, 0, 0)),
                new Slot(new DateTime(2023, 7, 10, 11, 0, 0), new DateTime(2023, 7, 10, 11, 30, 0)),
                new Slot(new DateTime(2023, 7, 10, 11, 30, 0), new DateTime(2023, 7, 10, 12, 0, 0)),
                
                // From 13 to 17
                // new SlotModel(new DateTime(2023, 7, 10, 13, 0, 0), new DateTime(2023, 7, 10, 13, 30, 0))
                new Slot(new DateTime(2023, 7, 10, 13, 30, 0), new DateTime(2023, 7, 10, 14, 0, 0)),
                new Slot(new DateTime(2023, 7, 10, 14, 0, 0), new DateTime(2023, 7, 10, 14, 30, 0)),
                new Slot(new DateTime(2023, 7, 10, 14, 30, 0), new DateTime(2023, 7, 10, 15, 0, 0)),
                // new SlotModel(new DateTime(2023, 7, 10, 15, 0, 0), new DateTime(2023, 7, 10, 15, 30, 0)),
                new Slot(new DateTime(2023, 7, 10, 15, 30, 0), new DateTime(2023, 7, 10, 16, 0, 0)),
                new Slot(new DateTime(2023, 7, 10, 16, 0, 0), new DateTime(2023, 7, 10, 16, 30, 0)),
                new Slot(new DateTime(2023, 7, 10, 16, 30, 0), new DateTime(2023, 7, 10, 17, 0, 0))
            };

            // Calculate available slots for monday
            DateTime baseDate = new DateTime(2023, 7, 10);

            var availableSlots = SlotHelper.GetAvailableSlots(
                SlotHelper.GenerateSlots(baseDate, workPeriod, slotDurationMinutes),
                availabilityResponse.Monday!.BusySlots
            );

            // Assert
            Assert.That(availableSlots, Has.Count.EqualTo(expectedAvailableSlots.Count), "The number of available slots does not match the expected count.");
            for (int i = 0; i < expectedAvailableSlots.Count; i++)
            {
                Assert.That(availableSlots[i].Start, Is.EqualTo(expectedAvailableSlots[i].Start), $"Start time of slot {i} does not match.");
                Assert.That(availableSlots[i].End, Is.EqualTo(expectedAvailableSlots[i].End), $"End time of slot {i} does not match.");
            }
        }
    }
}