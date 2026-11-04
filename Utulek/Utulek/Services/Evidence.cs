using System;
using System.Collections.Generic;
using Utulek.Model;

namespace Utulek.Services
{
    public class Evidence
    {
        private List<Zvire> seznamZvirat = new List<Zvire>();
        private int dalsiID = 1;

        public Evidence()
        {
            NactiVychoziZvirata();
        }

        private void NactiVychoziZvirata()
        {
            PridejZvire(new Zvire
            {
                Jmeno = "Alík",
                Druh = "Pes",
                Vek = 3,
                Pohlavi = "Samec",
                DatumPrijmu = new DateTime(2024, 11, 15),
                ZdravotniStav = "Zdravý",
                Poznamka = "Přátelský k dětem",
                Adoptovano = false
            });

            PridejZvire(new Zvire
            {
                Jmeno = "Micka",
                Druh = "Kočka",
                Vek = 2,
                Pohlavi = "Samice",
                DatumPrijmu = new DateTime(2025, 1, 10),
                ZdravotniStav = "Očkovaná",
                Poznamka = "Klidná povaha",
                Adoptovano = true,
                DatumAdopce = new DateTime(2025, 2, 20)
            });

            PridejZvire(new Zvire
            {
                Jmeno = "Ferda",
                Druh = "Jiné",
                Vek = 1,
                Pohlavi = "Samec",
                DatumPrijmu = new DateTime(2025, 2, 1),
                ZdravotniStav = "Zdravý",
                Poznamka = "Zakrslý králík",
                Adoptovano = false
            });
        }

        public void PridejZvire(Zvire zvire)
        {
            zvire.ID = dalsiID;
            dalsiID++;

            seznamZvirat.Add(zvire);
        }

        public List<Zvire> VsechnaZvirata()
        {
            return seznamZvirat;
        }

        public List<Zvire> FiltrujPodleDruhu(string druh)
        {
            List<Zvire> vysledek = new List<Zvire>();

            foreach (Zvire zvire in seznamZvirat)
            {
                if (zvire.Druh.Equals(druh, StringComparison.OrdinalIgnoreCase))
                {
                    vysledek.Add(zvire);
                }
            }

            return vysledek;
        }

        public List<Zvire> FiltrujPodleJmena(string jmeno)
        {
            List<Zvire> vysledek = new List<Zvire>();

            foreach (Zvire zvire in seznamZvirat)
            {
                if (zvire.Jmeno.IndexOf(jmeno, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    vysledek.Add(zvire);
                }
            }

            return vysledek;
        }

        public List<Zvire> FiltrujPodleVeku(int vek, bool mensiNeboRovno)
        {
            List<Zvire> vysledek = new List<Zvire>();

            foreach (Zvire zvire in seznamZvirat)
            {
                if (mensiNeboRovno && zvire.Vek <= vek)
                {
                    vysledek.Add(zvire);
                }
                else if (!mensiNeboRovno && zvire.Vek >= vek)
                {
                    vysledek.Add(zvire);
                }
            }

            return vysledek;
        }

        public bool OznacAdopci(int id)
        {
            foreach (Zvire zvire in seznamZvirat)
            {
                if (zvire.ID == id)
                {
                    if (zvire.Adoptovano)
                    {
                        return false;
                    }

                    zvire.Adoptovano = true;
                    zvire.DatumAdopce = DateTime.Today;

                    return true;
                }
            }

            return false;
        }

        public bool UpravZvire(int id, Action<Zvire> uprava)
        {
            foreach (Zvire zvire in seznamZvirat)
            {
                if (zvire.ID == id)
                {
                    uprava(zvire);
                    return true;
                }
            }

            return false;
        }

        public bool SmazZvire(int id)
        {
            foreach (Zvire zvire in seznamZvirat)
            {
                if (zvire.ID == id)
                {
                    seznamZvirat.Remove(zvire);
                    return true;
                }
            }

            return false;
        }

        public int PocetZvirat()
        {
            return seznamZvirat.Count;
        }

        public int PocetAdoptovanych()
        {
            int pocet = 0;

            foreach (Zvire zvire in seznamZvirat)
            {
                if (zvire.Adoptovano)
                {
                    pocet++;
                }
            }

            return pocet;
        }
    }
}