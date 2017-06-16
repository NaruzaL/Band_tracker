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

    public void Dispose()
    {
      Venue.DeleteAll();
      Band.DeleteAll();
    }
  }
}
