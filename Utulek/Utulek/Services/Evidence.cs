
using System;
using System.Collections.Generic;
using System.Linq;
using Utulek.Model;

namespace Utulek.Services
{
    public class Evidence
    {
        private List<Zvire> seznamZvirat = new List<Zvire>();
        private int dalsiID = 1;

        public Evidence()
        {
        }

        // Pridani noveho zvirete do evidence
        public void PridejZvire(Zvire zvire)
        {
            zvire.ID = dalsiID;
            dalsiID++;

            seznamZvirat.Add(zvire);
        }

        // Vrati seznam vsech zvirat
        public List<Zvire> VsechnaZvirata()
        {
            return seznamZvirat;
        }

        // Vyhledani zvirat podle druhu
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

        // Vyhledani zvirat podle jmena
        public List<Zvire> FiltrujPodleJmena(string jmeno)
        {
            List<Zvire> vysledek = new List<Zvire>();

            foreach (Zvire zvire in seznamZvirat)
            {
                if (zvire.Jmeno.Contains(jmeno, StringComparison.OrdinalIgnoreCase))
                {
                    vysledek.Add(zvire);
                }
            }

            return vysledek;
        }

        // Filtrovani podle veku
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

        // Oznaceni zvirete jako adoptovane
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

        // Uprava udaju zvirete
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

        // Smazani zvirete z evidence
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

        // Pocet vsech zvirat
        public int PocetZvirat()
        {
            return seznamZvirat.Count;
        }

        // Pocet adoptovanych zvirat
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
