using Xunit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BandTracker.Objects;

namespace BandTracker
{
  [Collection("band_tracker_test")]
 public class BandTest : IDisposable
  {
   public BandTest()
    {
      DBConfiguration.ConnectionString  = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      int result = Band.GetAll().Count;
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueIfBandsAreTheSame()
    {
      Band firstBand = new Band("Rage Against the Machine", "alternative");
      Band secondBand = new Band("Rage Against the Machine", "alternative");
      Assert.Equal(firstBand, secondBand);
    }

    [Fact]
    public void Test_Save_ToBandDatabase()
    {
      Band testBand = new Band("Nine Black Alps", "alt rock");
      testBand.Save();

      List<Band> result = Band.GetAll();
      List<Band> testList = new List<Band>{testBand};
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Save_AssignsIdToObject()
    {
      Band testBand = new Band("Nirvana", "Grunge");
      testBand.Save();
      int testId = testBand.GetId();
      int savedBandId = Band.GetAll()[0].GetId();
      Assert.Equal(testId, savedBandId);
    }

    [Fact]
    public void Test_Find_FindsBandsInDatabase()
    {
      Band testBand = new Band("Beatles", "Classic Rock");
      testBand.Save();
      Band foundBand = Band.Find(testBand.GetId());
      Assert.Equal(testBand, foundBand);
    }

    public void Dispose()
    {
      Band.DeleteAll();
      Venue.DeleteAll();
    }
  }
}
