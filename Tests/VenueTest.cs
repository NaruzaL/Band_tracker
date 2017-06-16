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
      Venue firstVenue = new Venue("Roseland");
      Venue secondVenue = new Venue("Roseland");
      Assert.Equal(firstVenue, secondVenue);
    }

    [Fact]
    public void Test_Save_ToVenueDatabase()
    {
      Venue testVenue = new Venue("Powell's books");
      testVenue.Save();

      List<Venue> result = Venue.GetAll();
      List<Venue> testList = new List<Venue>{testVenue};
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Save_AssignsIdToObject()
    {
      Venue testVenue = new Venue("Arlene Schnitzer Concert Hall");
      testVenue.Save();
      int testId = testVenue.GetId();
      int savedVenueId = Venue.GetAll()[0].GetId();
      Assert.Equal(testId, savedVenueId);
    }

    [Fact]
    public void Test_Find_FindsVenuesInDatabase()
    {
      Venue testVenue = new Venue("Columbia Gorge Ampatheater");
      testVenue.Save();
      Venue foundVenue = Venue.Find(testVenue.GetId());
      Assert.Equal(testVenue, foundVenue);
    }

    [Fact]
    public void Test_AddBand_AddsBandToVenue()
    {

      Venue testVenue = new Venue("Roseland",1);
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
      Venue testVenue = new Venue("Rose Gardenk");
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

    [Fact]
    public void Test_Update_UpdatesVenueInDatabase()
    {
      string name = "Rosland";
      int id = 1;
      Venue testVenue = new Venue(name, id);
      testVenue.Save();
      string newName = "Roseland";
      testVenue.Update(newName);
      string result = testVenue.GetName();

      Assert.Equal(newName, result);
    }

    [Fact]
    public void Test_Delete_DeletesVenueFromDatabase()
    {
      string name1 = "Rosland";
      int id = 1;
      Venue testVenue1 = new Venue(name1, id);
      testVenue1.Save();
      string name2 = "Roseland";
      int id2 = 2;
      Venue testVenue2 = new Venue(name2, id2);
      testVenue2.Save();

      testVenue1.Delete();
      List<Venue> resultVenue = Venue.GetAll();
      List<Venue> testVenueList = new List<Venue> {testVenue2};

      Assert.Equal(testVenueList, resultVenue);
    }

    public void Dispose()
    {
      Venue.DeleteAll();
      Band.DeleteAll();
    }
  }
}
