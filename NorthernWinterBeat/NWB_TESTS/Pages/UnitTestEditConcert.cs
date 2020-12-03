using Moq;
using NorthernWinterBeat.Models;
using NorthernWinterBeat.Pages;
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
    public class UnitTestEditConcert
    {

        [Fact]
        public void FindDate_ReturnsCorrectFormatWithSingleDigits()
        {
            //Arrange
            var mockDataAcces = new Mock<IDataAccess>();
            var mockFestivalManager = new Mock<IFestivalManager>();
            var mockBlobStorage = new Mock<IImageManager>();
            EditConcertModel Model = new EditConcertModel(mockDataAcces.Object, mockFestivalManager.Object, mockBlobStorage.Object)
            { 
                concert = new Concert(new DateTime(2020, 1, 1), new DateTime(2020, 01, 10), "5Artist", "2ArtistDescrition", mockDataAcces.Object) { ID = 1, Venue = new Venue("d", "address", 2, mockDataAcces.Object) },
            };
            string result = "2020-01-01";

            //Act
            string expected = Model.FindDate();

            //Assert
            Assert.Equal(expected, result);
        }
        [Fact]
        public void FindDate_ReturnsCorrectFormatWithDoubleDigit()
        {
            //Arrange
            var mockDataAcces = new Mock<IDataAccess>();
            var mockFestivalManager = new Mock<IFestivalManager>();
            var mockBlobStorage = new Mock<IImageManager>();
            EditConcertModel Model = new EditConcertModel(mockDataAcces.Object, mockFestivalManager.Object, mockBlobStorage.Object)
            {
                concert = new Concert(new DateTime(2022, 12, 21), new DateTime(2020, 01, 10), "5Artist", "2ArtistDescrition", mockDataAcces.Object) { ID = 1, Venue = new Venue("d", "address", 2, mockDataAcces.Object) },
            };
            string result = "2022-12-21";

            //Act
            string expected = Model.FindDate();

            //Assert
            Assert.Equal(expected, result);
        }
        [Fact]
        public void FindDate_ReturnsCorrectFormatWithSingleAndDoubleDigit()
        {
            //Arrange
            var mockDataAcces = new Mock<IDataAccess>();
            var mockFestivalManager = new Mock<IFestivalManager>();
            var mockBlobStorage = new Mock<IImageManager>();
            EditConcertModel Model = new EditConcertModel(mockDataAcces.Object, mockFestivalManager.Object, mockBlobStorage.Object)
            {
                concert = new Concert(new DateTime(2022, 1, 21), new DateTime(2020, 01, 10), "5Artist", "2ArtistDescrition", mockDataAcces.Object) { ID = 1, Venue = new Venue("d", "address", 2, mockDataAcces.Object) },
            };
            string result = "2022-01-21";

            //Act
            string expected = Model.FindDate();

            //Assert
            Assert.Equal(expected, result);
        }
    }
}
