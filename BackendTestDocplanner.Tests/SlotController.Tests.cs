using BackendTestDocplanner.Controllers;
using BackendTestDocplanner.Controllers.Helpers;
using BackendTestDocplanner.Controllers.Schemas;
using BackendTestDocplanner.Services.Slot;
using BackendTestDocplanner.Services.Slot.Schemas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;

namespace BackendTestDocplanner.Tests
{
    public class Tests
    {
        private SlotService _slotService;
        private SlotController _slotController;

        [SetUp]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Get username and password from configuration file
            string username = configuration["SlotService:Username"] ?? throw new InvalidOperationException("Please provide a username for the Slot Service.");
            string password = configuration["SlotService:Password"] ?? throw new InvalidOperationException("Please provide a password for the Slot Service.");

            var authHeaderValue = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{username}:{password}"));

            // HttpClient basic configuration for the slot service
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://draliatest.azurewebsites.net/"),
            };
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);

            _slotService = new SlotService(httpClient);
            _slotController = new SlotController(_slotService);
        }

        [Test, Order(1)]
        public async Task SlotService_GetWeeklyAvailability_ShouldReturnWeeklyAvailabilty_WhenDateIsValid()
        {
            // Check that the connection to the slot service is correct when using next week's monday
            var nextWeekDate = SlotHelper.GetWeekStartDate(DateTime.Today.AddDays(7)).ToString("yyyyMMdd");

            var response = await _slotService.GetWeeklyAvailabilityAsync(nextWeekDate);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            // Additional assertion to check deserialization to FacilityWeeklyAvailability
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.Write(responseBody);
                var availability = JsonConvert.DeserializeObject<FacilityWeeklyAvailability>(responseBody);

                Assert.That(availability, Is.Not.Null);
                Assert.That(availability.Facility, Is.Not.Null);
                Assert.That(availability.Monday, Is.Not.Null);
            }

            // Check that the connection to the slot service is NOT correct when using next week's sunday
            nextWeekDate = SlotHelper.GetWeekEndDate(DateTime.Today.AddDays(7)).ToString("yyyyMMdd");

            response = await _slotService.GetWeeklyAvailabilityAsync(nextWeekDate);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }


        [Test, Order(2)]
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

        [Test, Order(3)]
        public async Task SlotController_GetAvailableSlots_ShouldReturnWeeklyAvailableSlots()
        {
            // Check that the connection to the slot service is correct when using next week's monday
            var nextWeekDate = SlotHelper.GetWeekStartDate(DateTime.Today.AddDays(7)).ToString("yyyyMMdd");
            var response = await _slotController.GetAvailableSlotsAsync(nextWeekDate);

            Assert.That(response, Is.Not.Null);
            Assert.That(response, Is.InstanceOf<OkObjectResult>());
            var okResult = response as OkObjectResult;

            // Additional assertion to check that data received is WeeklyAvailableSlots
            Assert.That(okResult!.Value, Is.Not.Null);
            Assert.That(okResult.Value, Is.InstanceOf<WeeklyAvailableSlots>());
            var weeklyAvailableSlots = okResult.Value as WeeklyAvailableSlots;
            Assert.That(weeklyAvailableSlots!.FacilityId, Is.Not.Null);
            Assert.That(weeklyAvailableSlots.Monday, Is.Not.Null);


            // Check that the connection to the slot service is also correct when using next week's sunday
            nextWeekDate = SlotHelper.GetWeekEndDate(DateTime.Today.AddDays(7)).ToString("yyyyMMdd");
            response = await _slotController.GetAvailableSlotsAsync(nextWeekDate);

            Assert.That(response, Is.Not.Null);
            Assert.That(response, Is.InstanceOf<OkObjectResult>());
            okResult = response as OkObjectResult;

            // Additional assertion to check that data received is WeeklyAvailableSlots
            Assert.That(okResult!.Value, Is.Not.Null);
            Assert.That(okResult.Value, Is.InstanceOf<WeeklyAvailableSlots>());
            weeklyAvailableSlots = okResult.Value as WeeklyAvailableSlots;
            Assert.That(weeklyAvailableSlots!.FacilityId, Is.Not.Null);
            Assert.That(weeklyAvailableSlots.Monday, Is.Not.Null);
        }

        [Test, Order(4)]
        public async Task SlotService_TakeSlot_ShouldReturnOK_WhenSlotIsAvailable()
        {
            // Check that the connection to the slot service is correct when using next week's monday
            var nextWeekDate = SlotHelper.GetWeekStartDate(DateTime.Today.AddDays(7)).ToString("yyyyMMdd");

            var availabilityResponse = await _slotController.GetAvailableSlotsAsync(nextWeekDate);
            var weeklyAvailableSlots = (availabilityResponse as OkObjectResult)!.Value as WeeklyAvailableSlots;

            var daysOfWeek = new Dictionary<string, List<Slot>>
                {
                    { "Monday", weeklyAvailableSlots!.Monday },
                    { "Tuesday", weeklyAvailableSlots.Tuesday },
                    { "Wednesday", weeklyAvailableSlots.Wednesday },
                    { "Thursday", weeklyAvailableSlots.Thursday },
                    { "Friday", weeklyAvailableSlots.Friday },
                    { "Saturday", weeklyAvailableSlots.Saturday },
                    { "Sunday", weeklyAvailableSlots.Sunday }
                };

            // Take the first available slot
            Slot? selectedSlot = null;
            foreach (var kvp in daysOfWeek)
            {
                var dayOfWeek = kvp.Key;
                var slots = kvp.Value;

                if (slots.Count > 0)
                {
                    selectedSlot = slots[0];
                    break;
                }
                else
                {
                    Console.WriteLine($"No slots available for {dayOfWeek}.");
                }
            }
            Assert.That(selectedSlot, Is.Not.Null);

            // Use first available slot for taking a slot
            var takeSlotRequest = new TakeSlotRequest(
                facilityId: weeklyAvailableSlots.FacilityId,
                start: selectedSlot.Start.ToString("yyyy-MM-dd HH:mm:ss"),
                end: selectedSlot.End.ToString("yyyy-MM-dd HH:mm:ss"),
                comments: "My back hurts.",
                patient: new Patient
                (
                    name: "Elena",
                    secondName: "Nito",
                    email: "elena.nito@example.com",
                    phone: "555 44 33 22"
                ));

            // Check that the connection to the slot service is correct with the available slot
            var response = await _slotService.TakeSlotAsync(takeSlotRequest);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}