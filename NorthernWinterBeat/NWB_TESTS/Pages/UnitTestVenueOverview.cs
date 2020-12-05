using Moq;
using NorthernWinterBeat.Models;
using NorthernWinterBeat.Pages.Admin;
using NorthernWinterBeat.Pages.Admin.ParticipantAdmin;
using NorthernWinterBeatLibrary.DataAccess;
using NorthernWinterBeatLibrary.Managers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
namespace NWB_TESTS.Pages
{
    public class UnitTestVenueOverview
    {
        public List<Venue> GenerateVenues()
        {
            var mockDataAcces = new Mock<IDataAccess>();
            return new List<Venue>()
            {
                new Venue("bName", "1Address", 2, mockDataAcces.Object),
                new Venue("cName", "3Address", 3, mockDataAcces.Object),
                new Venue("aName", "2Address", 1, mockDataAcces.Object),
            };
        }


        [Fact]
        public void SortVenues_SortByVenue()
        {
            var mockDataAcces = new Mock<IDataAccess>(); 
            var mockFestivalManager = new Mock<IFestivalManager>();
            mockFestivalManager.Setup(f => f.Calendar).Returns(new Calendar(mockDataAcces.Object));
            VenuesOverviewModel Model = new VenuesOverviewModel(mockFestivalManager.Object)
            {
                Venues = GenerateVenues(),
                SortBy = "Venue"
            };
            List<string> Expected = new List<string>()
            {
                "aName", "bName", "cName"
            };

            //Act
            var resultVenues = Model.SortVenues(Model.Venues);


            Assert.Equal(Expected[0], resultVenues[0].Name);
            Assert.Equal(Expected[1], resultVenues[1].Name);
            Assert.Equal(Expected[2], resultVenues[2].Name);
        }
        [Fact]
        public void SortVenues_SortByCapacity()
        {
            var mockDataAcces = new Mock<IDataAccess>();
            var mockFestivalManager = new Mock<IFestivalManager>();
            mockFestivalManager.Setup(f => f.Calendar).Returns(new Calendar(mockDataAcces.Object));
            VenuesOverviewModel Model = new VenuesOverviewModel(mockFestivalManager.Object)
            {
                Venues = GenerateVenues(),
                SortBy = "Capacity"
            };
            List<int> Expected = new List<int>()
            {
                1,2,3
            };

            //Act
            var resultVenues = Model.SortVenues(Model.Venues);


            Assert.Equal(Expected[0], resultVenues[0].Capacity);
            Assert.Equal(Expected[1], resultVenues[1].Capacity);
            Assert.Equal(Expected[2], resultVenues[2].Capacity);
        }
        [Fact]
        public void SortVenues_SortByAddress()
        {
            var mockDataAcces = new Mock<IDataAccess>();
            var mockFestivalManager = new Mock<IFestivalManager>();
            mockFestivalManager.Setup(f => f.Calendar).Returns(new Calendar(mockDataAcces.Object));
            VenuesOverviewModel Model = new VenuesOverviewModel(mockFestivalManager.Object)
            {
                Venues = GenerateVenues(),
                SortBy = "Address"
            };
            List<string> Expected = new List<string>()
            {
                "1Address", "2Address", "3Address"
            };

            //Act
            var resultVenues = Model.SortVenues(Model.Venues);


            Assert.Equal(Expected[0], resultVenues[0].Address);
            Assert.Equal(Expected[1], resultVenues[1].Address);
            Assert.Equal(Expected[2], resultVenues[2].Address);
        }
    }
}
