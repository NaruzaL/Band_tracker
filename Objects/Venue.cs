using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BandTracker.Objects
{
  public class BandTracker
  {
    private string name;
    private DateTime _concertDate;
    private string id;

    public Band(string Name, DateTime ConcertDate, int Id - 0)
    {
      _name = name;
      _concertDate = ConcertDate;
      _id = Id;
    }

    public string GetName()
    {
      return _name;
    }
    public string GetConcertDate()
    {
      return _concertDate;
    }
    public int GetId()
    {
      return _id;
    }

    public override bool Equals(System.Object otherBand)
    {
      if (!(otherBand is Band))
      {
        return false;
      }
      else
      {
        Band newBand = (Band) otherBand;
        bool idEquality = (this.GetId() == newBand.GetId());
        bool nameEquality = (this.GetName() == newBand.GetName());
        bool concertDateEquality = (this.GetConcertDate() == newBand.GetConcertDate());
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

  }
}
