using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Utulek
{
    internal class KonzoleUI
    {
        private readonly Evidence evidence;

        public KonzoleUI(Evidence evidence)
        {
            this.evidence = evidence;
        }

        public void Spust()
        {
            bool konec = false;

            while (!konec)
            {
                Console.WriteLine("\n===== ÚTULEK PRO ZVÍŘATA =====");
                Console.WriteLine("1) Přidat zvíře");
                Console.WriteLine("2) Vypsat všechna zvířata");
                Console.WriteLine("3) Vyhledat / filtrovat");
                Console.WriteLine("4) Označit adopci");
                Console.WriteLine("5) Statistiky");
                Console.WriteLine("0) Konec");
                Console.Write("Volba: ");

                string volba = Console.ReadLine();

                switch (volba)
                {
                    case "1":
                        PridatZvire();
                        break;
                    case "2":
                        VypsatZvirata(evidence.VsechnaZvirata());
                        break;
                    case "3":
                        Vyhledat();
                        break;
                    case "4":
                        Adopce();
                        break;
                    case "5":
                        Statistiky();
                        break;
                    case "0":
                        konec = true;
                        Console.WriteLine("Program ukončen.");
                        break;
                    default:
                        Console.WriteLine("Neplatná volba.");
                        break;
                }
            }
        }

        private void PridatZvire()
        {
            Console.WriteLine("\n--- Přidání zvířete ---");

            string jmeno = CtiText("Jméno: ");
            string druh;

            while (true)
            {
                druh = CtiText("Druh (Pes/Kočka/Jiné): ");

                if (new[] { "pes", "kočka", "jiné"}
                    .Contains(druh.ToLower()))
                {
                    if (druh.Equals("pes", StringComparison.OrdinalIgnoreCase))
                        druh = "Pes";
                    else if (druh.Equals("kočka", StringComparison.OrdinalIgnoreCase))
                        druh = "Kočka";
                    else
                        druh = "Jiné";

                    break;
                }

                Console.WriteLine("Zadej Pes, Kočka nebo Jiné.");
            }

            int vek = CtiVek();
            string pohlavi = CtiNepovinnyText("Pohlaví: ");
            DateTime datumPrijmu = CtiDatum("Datum příjmu (dd.MM.rrrr): ");
            string zdravotniStav = CtiNepovinnyText("Zdravotní stav: ");
            string poznamka = CtiNepovinnyText("Poznámka: ");

            Zvire zvire = new Zvire
            {
                Jmeno = jmeno,
                Druh = druh,
                Vek = vek,
                Pohlavi = pohlavi,
                DatumPrijmu = datumPrijmu,
                ZdravotniStav = zdravotniStav,
                Poznamka = poznamka
            };

            evidence.PridejZvire(zvire);

            Console.WriteLine($"Zvíře bylo přidáno. Jeho ID je {zvire.ID}.");
        }

        private void VypsatZvirata(List<Zvire> zvirata)
        {
            Console.WriteLine("\n--- Seznam zvířat ---");

            if (zvirata.Count == 0)
            {
                Console.WriteLine("Nebyla nalezena žádná zvířata.");
                return;
            }

            Console.WriteLine(
                "{0,-4} {1,-12} {2,-10} {3,-5} {4,-10} {5,-12} {6,-12} {7,-10}",
                "ID", "Jméno", "Druh", "Věk", "Pohlaví",
                "Příjem", "Zdraví", "Adopce");

            Console.WriteLine(new string('-', 95));

            foreach (Zvire z in zvirata)
            {
                Console.WriteLine(
                    "{0,-4} {1,-12} {2,-10} {3,-5} {4,-10} {5,-12} {6,-12} {7,-10}",
                    z.ID,
                    Orez(z.Jmeno, 12),
                    Orez(z.Druh, 10),
                    z.Vek,
                    Orez(z.Pohlavi, 10),
                    z.DatumPrijmu.ToString("dd.MM.yyyy"),
                    Orez(z.ZdravotniStav, 12),
                    z.Adoptovano ? "Ano" : "Ne");
            }

            Console.WriteLine("\nPodrobnosti:");

            foreach (Zvire z in zvirata)
            {
                Console.WriteLine(
                    $"ID {z.ID}: {z.Jmeno}; poznámka: {Zobraz(z.Poznamka)}; " +
                    $"datum adopce: {(z.DatumAdopce.HasValue ? z.DatumAdopce.Value.ToString("dd.MM.yyyy") : "-")}");
            }
        }

        private void Vyhledat()
        {
            Console.WriteLine("\n--- Vyhledávání ---");
            Console.WriteLine("1) Podle druhu");
            Console.WriteLine("2) Podle jména");
            Console.WriteLine("3) Věk menší nebo roven (<=)");
            Console.WriteLine("4) Věk větší nebo roven (>=)");
            Console.Write("Volba: ");

            string volba = Console.ReadLine();
            List<Zvire> vysledek;

            switch (volba)
            {
                case "1":
                    string druh = CtiText("Zadej druh: ");
                    vysledek = evidence.VsechnaZvirata()
                        .Where(z => z.Druh.Equals(
                            druh, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                    break;

                case "2":
                    string jmeno = CtiText("Zadej jméno nebo jeho část: ");
                    vysledek = evidence.FiltrujPodleJmena(jmeno);
                    break;

                case "3":
                    int maxVek = CtiVek();
                    vysledek = evidence.FiltrujPodleVeku(maxVek, true);
                    break;

                case "4":
                    int minVek = CtiVek();
                    vysledek = evidence.FiltrujPodleVeku(minVek, false);
                    break;

                default:
                    Console.WriteLine("Neplatná volba.");
                    return;
            }

            VypsatZvirata(vysledek);
        }

        private void Adopce()
        {
            Console.Write("\nZadej ID zvířete k adopci: ");

            if (!int.TryParse(Console.ReadLine(), out int id) || id <= 0)
            {
                Console.WriteLine("ID musí být kladné celé číslo.");
                return;
            }

            if (evidence.OznacAdopci(id))
            {
                Console.WriteLine(
                    $"Zvíře ID {id} bylo označeno jako adoptované dne " +
                    $"{DateTime.Today:dd.MM.yyyy}.");
            }
            else
            {
                Console.WriteLine(
                    "Zvíře nebylo nalezeno nebo už bylo adoptováno.");
            }
        }

        private void Statistiky()
        {
            Console.WriteLine("\n--- Statistiky útulku ---");
            Console.WriteLine($"Celkem zvířat: {evidence.PocetZvirat()}");
            Console.WriteLine($"Adoptováno: {evidence.PocetAdoptovanych()}");
            Console.WriteLine(
                $"V útulku: {evidence.PocetZvirat() - evidence.PocetAdoptovanych()}");
        }

        private string CtiText(string zprava)
        {
            while (true)
            {
                Console.Write(zprava);
                string hodnota = (Console.ReadLine() ?? "").Trim();

                if (!string.IsNullOrWhiteSpace(hodnota))
                    return hodnota;

                Console.WriteLine("Tato položka je povinná.");
            }
        }

        private string CtiNepovinnyText(string zprava)
        {
            Console.Write(zprava);
            return (Console.ReadLine() ?? "").Trim();
        }

        private int CtiVek()
        {
            while (true)
            {
                Console.Write("Věk v letech: ");

                if (int.TryParse(Console.ReadLine(), out int vek) && vek >= 0)
                    return vek;

                Console.WriteLine("Věk musi byt celé číslo větší nebo rovno 0.");
            }
        }

        private DateTime CtiDatum(string zprava)
        {
            while (true)
            {
                Console.Write(zprava);
                string vstup = Console.ReadLine() ?? "";

                if (DateTime.TryParseExact(
                    vstup,
                    "dd.MM.yyyy",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None,
                    out DateTime datum)
                    && datum.Date <= DateTime.Today)
                {
                    return datum;
                }

                Console.WriteLine(
                    "Zadej platné datum ve formatu dd.MM.rrrr, které není v budoucnosti.");
            }
        }

        private string Orez(string text, int delka)
        {
            if (text.Length <= delka)
                return text;

            return text.Substring(0, delka - 3) + "...";
        }

        private string Zobraz(string text)
        {
            return string.IsNullOrWhiteSpace(text) ? "-" : text;
        }
    }
}
