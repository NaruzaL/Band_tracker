using System;
using System.Collections.Generic;
using BandTracker.Objects;
using System.Data;
using System.Data.SqlClient;
using Nancy;

namespace BandTracker
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        List<Band> allBands = Band.GetAll();
        List<Venue> allVenues = Venue.GetAll();
        model.Add("bands", allBands);
        model.Add("venues", allVenues);
        return View["index.cshtml"];
      };

      Get["/band/add"] = _ =>View["band_form"];
      Post["/band/new"] = _ => {
        Band newBand = new Band(Request.Form["name"], Request.Form["genre"]);
        return View["success.cshtml", newBand];
      };

      Get["/venue/add"] = _ =>View["venue_form"];
      Post["/venue/new"] = _ => {
        Venue newVenue = new Venue(Request.Form["name"]);
        return View["success.cshtml", newVenue];
      };

      Get["/band/{id}"]
    }
  }
}
