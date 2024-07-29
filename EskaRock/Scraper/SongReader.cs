using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using EskaRock.Scraper.Models;
using Newtonsoft.Json;

namespace EskaRock.Scraper;

public class SongReader
{
    public async Task<IEnumerable<Song>> DownloadSongsAsync(DateOnly day)
    {
        var client = RestEase.RestClient.For<IEskaRockClient>("https://www.eskarock.pl");

        var allSongs = new List<Song>();

        var date = Helper.FormatDate(day);

        for (var hour = 0; hour < 24; hour++)
        {
            var songs = await client.GetWasPlayed(date, hour);
            allSongs.AddRange(songs);
        }

        return allSongs;
    }

    public IEnumerable<Song> ReadSongsFromFile(DateOnly day)
    {
        var str = File.ReadAllText(Helper.FormatDate(day) + ".txt");

        return JsonConvert.DeserializeObject<List<Song>>(str);
    }
}
