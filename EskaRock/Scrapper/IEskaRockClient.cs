using System.Collections.Generic;
using System.Threading.Tasks;
using EskaRock.Scrapper.Models;
using RestEase;

namespace EskaRock.Scrapper;

public interface IEskaRockClient
{
    [Get("api/mobile/station/5380/was_played/?date={date}&hour={hour}")]
    Task<IEnumerable<Song>> GetWasPlayed([Path] string date, [Path] int hour);
}
