using MapDataReader;

namespace Happy_Devs_BE.Services
{
    [GenerateDataReaderMapper]
    public class IdAndName
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
