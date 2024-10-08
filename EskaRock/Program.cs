﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EskaRock.Scraper;

namespace EskaRock;

class Program
{
    private const int SongsThreshold = 2;
    private const int ArtistsThreshold = 4;

    private const bool FromFile = false;
    private static readonly ResultWriter _writer = new();

    static async Task Main(string[] args)
    {
        var startTime = Stopwatch.GetTimestamp();

        var day = new DateOnly(2024, 08, 18);

        var reader = new SongReader();
        var songs = FromFile
            ? reader.ReadSongsFromFile(day)
            : await reader.DownloadSongsAsync(day);

        if (!songs.Any())
        {
            return;
        }

        var outDir = Helper.GetOutDir();
        _writer.WriteRawResults(outDir, day, songs);

        var groupedBySong = songs
            .GroupBy(x => x.Name)
            .Where(x => x.Count() > SongsThreshold)
            .OrderByDescending(x => x.Count())
            .ToDictionary(x => x.Key, x => x.Count());

        var groupedByArtist = songs
            .GroupBy(x => x.Artists[0].Name)
            .Where(x => x.Count() > ArtistsThreshold)
            .OrderByDescending(x => x.Count())
            .ToDictionary(x => x.Key, x => x.Count());

        _writer.WriteSummary(outDir, day, groupedBySong, groupedByArtist);

        LogExecutionTime(startTime);

        Console.ReadKey();
    }

    private static void LogExecutionTime(long startTime) => Console.WriteLine($"Took {Stopwatch.GetElapsedTime(startTime)} to execute...");
}
