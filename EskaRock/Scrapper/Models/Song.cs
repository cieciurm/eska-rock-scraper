using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EskaRock.Scrapper.Models;

public class Song
{
    public List<Artist> Artists { get; set; }

    [JsonProperty("play_date")]
    public DateTime PlayDate { get; set; }
    public string Name { get; set; }

    [JsonProperty("media_id")]
    public int MediaId { get; set; }
}
