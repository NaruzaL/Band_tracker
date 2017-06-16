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

  }
}
