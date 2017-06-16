using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BandTracker.Objects
{
  public class Venue
  {
    private string _name;
    private DateTime _concertDate;
    private int _id;

    public Venue(string Name, DateTime ConcertDate, int Id = 0)
    {
      _name = Name;
      _concertDate = ConcertDate;
      _id = Id;
    }

    public string GetName()
    {
      return _name;
    }
    public DateTime GetConcertDate()
    {
      return _concertDate;
    }
    public int GetId()
    {
      return _id;
    }

    public override bool Equals(System.Object otherVenue)
    {
      if (!(otherVenue is Venue))
      {
        return false;
      }
      else
      {
        Venue newVenue = (Venue) otherVenue;
        bool idEquality = (this.GetId() == newVenue.GetId());
        bool nameEquality = (this.GetName() == newVenue.GetName());
        bool concertDateEquality = (this.GetConcertDate() == newVenue.GetConcertDate());
        return (idEquality && nameEquality && concertDateEquality);
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM venues;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

    public static List<Venue> GetAll()
    {
      List<Venue> allVenues = new List<Venue>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM venues;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int venueId = rdr.GetInt32(0);
        string venueName = rdr.GetString(1);
        DateTime venueConcertDate = rdr.GetDateTime(2);
        Venue newVenue = new Venue(venueName, venueConcertDate, venueId);
        allVenues.Add(newVenue);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allVenues;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO venues (name, concert_date) OUTPUT INSERTED.id VALUES (@VenuesName, @VenuesConcertDate)", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@VenuesName";
      nameParameter.Value = this.GetName();

      SqlParameter concertDateParameter = new SqlParameter();
      concertDateParameter.ParameterName = "@VenuesConcertDate";
      concertDateParameter.Value = this.GetConcertDate();

      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(concertDateParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }

    public static Venue Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM venues WHERE id = @VenueId", conn);
      SqlParameter venueIdParameter = new SqlParameter();
      venueIdParameter.ParameterName = "@VenueId";
      venueIdParameter.Value = id.ToString();
      cmd.Parameters.Add(venueIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundVenueId = 0;
      string foundVenueName = null;
      DateTime foundVenueConcertDate = default(DateTime);

      while(rdr.Read())
      {
        foundVenueId = rdr.GetInt32(0);
        foundVenueName = rdr.GetString(1);
        foundVenueConcertDate = rdr.GetDateTime(2);
      }
      Venue foundVenue = new Venue(foundVenueName, foundVenueConcertDate, foundVenueId);

      if (rdr != null)
     {
       rdr.Close();
     }
     if (conn != null)
     {
       conn.Close();
     }

     return foundVenue;
    }

    public void AddBand(Band newBand)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO bands_venues (band_id, venue_id) VALUES (@BandId, @VenueId);", conn);
      SqlParameter venueIdParameter = new SqlParameter();
      venueIdParameter.ParameterName = "@VenueId";
      venueIdParameter.Value = this.GetId();
      cmd.Parameters.Add(venueIdParameter);

      SqlParameter bandIdParameter = new SqlParameter();
      bandIdParameter.ParameterName = "@BandId";
      bandIdParameter.Value = newBand.GetId();
      cmd.Parameters.Add(bandIdParameter);

      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

    public List<Band> GetBands()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT bands.* FROM venues JOIN bands_venues ON (venues.id = bands_venues.venue_id) JOIN bands ON (bands_venues.band_id = bands.id) WHERE venues.id = @VenueId;", conn);
      SqlParameter venuesIdParameter = new SqlParameter();
      venuesIdParameter.ParameterName = "@VenueId";
      venuesIdParameter.Value = this.GetId();

      cmd.Parameters.Add(venuesIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      List<Band> bands = new List<Band> {};

      while(rdr.Read())
        {
          int BandId = rdr.GetInt32(0);
          string bandName = rdr.GetString(1);
          string bandGenre = rdr.GetString(2);
          Band foundBand = new Band(bandName, bandGenre, BandId);
          bands.Add(foundBand);
        }
        if (rdr != null)
        {
          rdr.Close();
        }

      if (conn != null)
      {
        conn.Close();
      }
      return bands;
    }

  }
}
