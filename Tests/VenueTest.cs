using Xunit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BandTracker.Objects;

namespace BandTracker
{
  [Collection("band_tracker_test")]
 public class VenueTest : IDisposable
  {
   public VenueTest()
    {
      DBConfiguration.ConnectionString  = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      int result = Venue.GetAll().Count;
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueIfVenuesAreTheSame()
    {
      Venue firstVenue = new Venue("Roseland", new DateTime(2017,6,17));
      Venue secondVenue = new Venue("Roseland", new DateTime(2017,6,17));
      Assert.Equal(firstVenue, secondVenue);
    }

    [Fact]
    public void Test_Save_ToVenueDatabase()
    {
      Venue testVenue = new Venue("Powell's books", new DateTime(2011,4,17));
      testVenue.Save();

      List<Venue> result = Venue.GetAll();
      List<Venue> testList = new List<Venue>{testVenue};
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Save_AssignsIdToObject()
    {
      Venue testVenue = new Venue("Arlene Schnitzer Concert Hall", new DateTime(2017, 1, 1));
      testVenue.Save();
      int testId = testVenue.GetId();
      int savedVenueId = Venue.GetAll()[0].GetId();
      Assert.Equal(testId, savedVenueId);
    }

    [Fact]
    public void Test_Find_FindsVenuesInDatabase()
    {
      Venue testVenue = new Venue("Columbia Gorge Ampatheater", new DateTime(2017, 1, 1));
      testVenue.Save();
      Venue foundVenue = Venue.Find(testVenue.GetId());
      Assert.Equal(testVenue, foundVenue);
    }

    [Fact]
    public void Test_AddBand_AddsBandToVenue()
    {

      Venue testVenue = new Venue("Roseland", new DateTime(2011, 1 , 1), 1);
      testVenue.Save();

      Band testBand = new Band("Sevendust", "Heavy Metal");
      testBand.Save();
      Band testBand2 = new Band("Them Crooked Vultures", "Hard Rock");
      testBand2.Save();

      testVenue.AddBand(testBand);
      testVenue.AddBand(testBand2);
      List<Band> result = testVenue.GetBands();
      List<Band> testList = new List<Band>{testBand, testBand2};
      Assert.Equal(testList, result);
    }
      //As Written these are the same thing with different test names
    [Fact]
    public void Test_GetBand_RetrievesAllBandWithVenue()
    {
      Venue testVenue = new Venue("Rose Gardenk", new DateTime(2008, 12, 4));
      testVenue.Save();

      Band firstBand = new Band("Evercleaer", "Rock");
      firstBand.Save();
      Band secondBand = new Band("Queens of the Stone Age", "Rock");
      secondBand.Save();

      testVenue.AddBand(firstBand);
      testVenue.AddBand(secondBand);

      List<Band> testBandList = new List<Band> {firstBand, secondBand};
      List<Band> resultBandList = testVenue.GetBands();
      Assert.Equal(testBandList, resultBandList);
    }

    public void Dispose()
    {
      Venue.DeleteAll();
      Band.DeleteAll();
    }
  }
}
