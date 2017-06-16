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

    public void Dispose()
    {
      Venue.DeleteAll();
      Band.DeleteAll();
    }
  }
}
