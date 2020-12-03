using Moq;
using NorthernWinterBeat.Models;
using NorthernWinterBeat.Pages;
using NorthernWinterBeat.Pages.Admin.ParticipantAdmin;
using NorthernWinterBeatLibrary.DataAccess;
using NorthernWinterBeatLibrary.Managers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NWB_TESTS.Pages
{
    public class UnitTestParticipantConcertOverview
    {
        public List<Concert> GenerateConcerts()
        {
            var mock = new Mock<IDataAccess>(); 
            return new List<Concert>()
            {
                new Concert(new DateTime(2020, 01, 10), new DateTime(2020, 01, 10), "4Artist", "1ArtistDescrition", mock.Object){ID = 0, Venue = new Venue("a", "address", 2, mock.Object) },
                new Concert(new DateTime(2020, 01, 10), new DateTime(2020, 01, 10), "5Artist", "2ArtistDescrition", mock.Object){ID = 1, Venue = new Venue("d", "address", 2, mock.Object) },
                new Concert(new DateTime(2020, 01, 12), new DateTime(2020, 01, 10), "1Artist", "3ArtistDescrition", mock.Object){ID = 2, Venue = new Venue("c", "address", 2, mock.Object) },
                new Concert(new DateTime(2020, 01, 12), new DateTime(2020, 01, 10), "3Artist", "4ArtistDescrition", mock.Object){ID = 3, Venue = new Venue("b", "address", 2, mock.Object) },
                new Concert(new DateTime(2020, 01, 14), new DateTime(2020, 01, 10), "2Artist", "5ArtistDescrition", mock.Object){ID = 4, Venue = new Venue("e", "address", 2, mock.Object) }
            };
        }
        [Fact]
        public void ConcertDayChooser_CheckCorrectListReturn()
        {
            //Arrange
            var mockFestivalManager = new Mock<IFestivalManager>();
            ParticipantConcertOverviewModel p1 = new ParticipantConcertOverviewModel(mockFestivalManager.Object)
            {
                Concerts = GenerateConcerts()
            };
            string result = "Sunday";
            //Act
            var resultConcerts = p1.ConcertDayChooser(new DateTime(2020, 01, 12));

            //Assert
            Assert.Equal(result, resultConcerts[0].Start.DayOfWeek.ToString());
            Assert.Equal(result, resultConcerts[1].Start.DayOfWeek.ToString());
        }
        [Fact]
        public void ConcertDayChooser_ReturnCorrectlyIfNoConcerts()
        {
            //Arrange
            var mockFestivalManager = new Mock<IFestivalManager>();
            ParticipantConcertOverviewModel p1 = new ParticipantConcertOverviewModel(mockFestivalManager.Object)
            {
                Concerts = GenerateConcerts()
            };
            int result = 0;
            //Act
            var resultConcerts = p1.ConcertDayChooser(new DateTime(2020, 01, 13));

            //Assert
            Assert.Equal(result, resultConcerts.Count);
        }

        [Fact]
        public void SortConcertsBy_CorrectlySortByArtist()
        {
            //Arrange
            var mockFestivalManager = new Mock<IFestivalManager>();
            ParticipantConcertOverviewModel p1 = new ParticipantConcertOverviewModel(mockFestivalManager.Object)
            {
                Concerts = GenerateConcerts(),
                SortBy = "Artist" 
            };

            //Act
            var resultConcerts = p1.SortConcertBy(p1.Concerts);

            Assert.Equal("1Artist", resultConcerts[0].Artist);
            Assert.Equal("2Artist", resultConcerts[1].Artist);
            Assert.Equal("3Artist", resultConcerts[2].Artist);
            Assert.Equal("4Artist", resultConcerts[3].Artist);
            Assert.Equal("5Artist", resultConcerts[4].Artist);
        }
        [Fact]
        public void SortConcertsBy_CorrectlySortByTime()
        {
            //Arrange
            var mockFestivalManager = new Mock<IFestivalManager>();
            ParticipantConcertOverviewModel p1 = new ParticipantConcertOverviewModel(mockFestivalManager.Object)
            {
                Concerts = GenerateConcerts(),
                SortBy = "Time"
            };
            p1.Concerts.Reverse(); 

            //Act
            var resultConcerts = p1.SortConcertBy(p1.Concerts);

            Assert.Equal(0, resultConcerts[0].ID);
            Assert.Equal(1, resultConcerts[1].ID);
            Assert.Equal(2, resultConcerts[2].ID);
            Assert.Equal(3, resultConcerts[3].ID);
            Assert.Equal(4, resultConcerts[4].ID);
        }

        [Fact]
        public void SortConcertsBy_CorrectlySortByVenue()
        {
            //Arrange
            var mockFestivalManager = new Mock<IFestivalManager>();
            ParticipantConcertOverviewModel p1 = new ParticipantConcertOverviewModel(mockFestivalManager.Object)
            {
                Concerts = GenerateConcerts(),
                SortBy = "Venue"
            };
            p1.Concerts.Reverse();

            //Act
            var resultConcerts = p1.SortConcertBy(p1.Concerts);


            //Assert
            Assert.Equal(0, resultConcerts[0].ID);
            Assert.Equal(3, resultConcerts[1].ID);
            Assert.Equal(2, resultConcerts[2].ID);
            Assert.Equal(1, resultConcerts[3].ID);
            Assert.Equal(4, resultConcerts[4].ID);
        }
    }
}
