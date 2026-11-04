using System;

namespace Utulek.Model
{
    public class Zvire
    {
        public int ID { get; set; }
        public string Jmeno { get; set; } = string.Empty;
        public string Druh { get; set; } = string.Empty;
        public int Vek { get; set; }
        public string Pohlavi { get; set; } = string.Empty;
        public DateTime DatumPrijmu { get; set; }
        public string ZdravotniStav { get; set; } = string.Empty;
        public string Poznamka { get; set; } = string.Empty;
        public bool Adoptovano { get; set; }
        public DateTime? DatumAdopce { get; set; }
    }
}