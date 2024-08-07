﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using EskaRock.Scraper.Models;
using Newtonsoft.Json;

namespace EskaRock.Scraper;

public class ResultWriter
{
    public void WriteRawResults(string dir, DateOnly day, IEnumerable<Song> songs)
    {
        var path = Path.Combine(dir, Helper.FormatDate(day) + ".txt");

        File.WriteAllText(path, JsonConvert.SerializeObject(songs));
    }

    public void WriteSummary(string dir, DateOnly day, IDictionary<string, int> songs, IDictionary<string, int> artists)
    {
        var sb = new StringBuilder();

        sb.AppendLine(Helper.FormatDate(day));
        sb.AppendLine();

        foreach (var artist in artists)
        {
            sb.AppendLine($"{artist.Key} - {artist.Value}");
        }

        sb.AppendLine();

        foreach (var song in songs)
        {
            sb.AppendLine($"{song.Key} - {song.Value}");
        }

        File.WriteAllText($"{dir}/" + Helper.FormatDate(day) + "-result.txt", sb.ToString());
    }
}
