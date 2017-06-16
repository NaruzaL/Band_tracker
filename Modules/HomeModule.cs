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
        return View["index.cshtml", model];
      };

      Get["/band/add"] = _ =>View["band_form"];
      Post["/band/new"] = _ => {
        Band newBand = new Band(Request.Form["name"], Request.Form["genre"]);
        newBand.Save();
        return View["success.cshtml", newBand];
      };

      Get["/venue/add"] = _ =>View["venue_form"];
      Post["/venue/new"] = _ => {
        Venue newVenue = new Venue(Request.Form["name"]);
        newVenue.Save();
        return View["success.cshtml", newVenue];
      };

      Get["/band/{id}"] = parameters => {
        Dictionary<string,object> model = new Dictionary<string,object>{};
        Band selectedBand = Band.Find(parameters.id);
        List<Venue> bandVenues = selectedBand.GetVenues();
        List<Venue> allVenues = Venue.GetAll();
        model.Add("bands", selectedBand);
        model.Add("bandVenues", bandVenues);
        model.Add("allVenues", allVenues);
        return View["bands.cshtml", model];
      };
      Post["/band/add_venue"] = _ => {
        Band selectedBand = Band.Find(Request.Form["band-id"]);
        Venue addedVenue = Venue.Find(Request.Form["venue-id"]);
        selectedBand.AddVenue(addedVenue);
        return View["success.cshtml"];
      };

      Get["/venue/{id}"] = parameters => {
        Dictionary<string,object> model = new Dictionary<string,object>{};
        Venue selectedVenue = Venue.Find(parameters.id);
        List<Band> venueBands = selectedVenue.GetBands();
        List<Band> allBands = Band.GetAll();
        model.Add("venues", selectedVenue);
        model.Add("venueBands", venueBands);
        model.Add("allBands", allBands);
        return View["venues.cshtml", model];
      };
      Post["/venue/add_band"] = _ => {
        Venue selectedVenue = Venue.Find(Request.Form["venue-id"]);
        Band addedBand = Band.Find(Request.Form["band-id"]);
        selectedVenue.AddBand(addedBand);
        return View["success.cshtml"];
      };

      Get["/venue/edit/{id}"] = parameters => {
        Venue selectedVenue = Venue.Find(parameters.id);
        return View["edit.cshtml", selectedVenue];
      };
      Patch["/venue/edit/{id}"] = parameters => {
        Venue selectedVenue = Venue.Find(parameters.id);
        selectedVenue.Update(Request.Form["venueName"]);
        return View["success.cshtml"];
      };

      Get["/venue/delete/{id}"] = parameters => {
        Venue selectedVenue = Venue.Find(parameters.id);
        return View["delete.cshtml", selectedVenue];
      };
      Delete["/venue/delete/{id}"] = parameters => {
        Venue selectedVenue = Venue.Find(parameters.id);
        selectedVenue.Delete();
        return View["success.cshtml"];
      };
    }
  }
}
