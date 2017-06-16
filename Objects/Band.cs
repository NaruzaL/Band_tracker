using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BandTracker.Objects
{
  public class BandTracker
  {
    private string name;
    private string genre;
    private string id;

    public Band(string Name, string Genre, int Id - 0)
    {
      _name = name;
      _genre = Genre;
      _id = Id;
    }

    public string GetName()
    {
      return _name;
    }
    public string GetGenre()
    {
      return _genre;
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
        bool genreEquality = (this.GetGenre() == newBand.GetGenre());
        return (idEquality && nameEquality && genreEquality);
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM bands;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

  }
}
