Hierarchie
Utulek/
├── Program.cs           // Vstupní bod aplikace (Main) – propojení Evidence a UI
├── Model/
│   └── Zvire.cs         // Datový model zvířete (vlastnosti zvířete)
├── Services/
│   └── Evidence.cs      // Aplikační logika, správa List<Zvire>, filtrování a adopce
└── UI/
    └── KonzoleUI.cs     // Uživatelské rozhraní v konzoli, menu, čtení a validace vstupů

===== ÚTULEK PRO ZVÍŘATA =====
1) Přidat zvíře
2) Vypsat všechna zvířata
3) Vyhledat/ filtrovat
4) Označit adopci
5) Statistiky
0) Konec
Volba: _    (uživatel vždy píše čísla 1-5, pro exit 0)

## Hlavní funkce programu

### 1. Přidání nového zvířete
- **Automatické ID:** Každé zvíře dostane unikátní pořadové číslo.
- **Povinné údaje (s validací):** Jméno, Druh (`Pes`/`Kočka`/`Jiné`), Věk (`>= 0`) a Datum příjmu (formát `dd.MM.rrrr`, nesmí být v budoucnosti).
- **Nepovinné údaje (lze vynechat):** Pohlaví, Zdravotní stav a Poznámka.

### 2. Výpis všech zvířat
- **Přehledná tabulka:** Zarovnané sloupce (`ID`, `Jméno`, `Druh`, `Věk`, `Pohlaví`, `Příjem`, `Zdraví`, `Adopce`). Dlouhé texty se zkracují (`...`), prázdné nahrazuje `-`.
- **Podrobnosti:** Pod tabulkou se vypisuje celá poznámka a přesné datum adopce.

### 3. Vyhledávání a filtrování
Umožňuje filtrovat zvířata (nerozlišuje velikost písmen) a vypsat je do tabulky:
1. **Podle druhu** (`Pes`, `Kočka`, `Jiné`)
2. **Podle jména** (nebo jeho části)
3. **Podle věku** (`<=` zadaný věk nebo `>=` zadaný věk)

### 4. Označení adopce
- Podle zadaného `ID` nastaví stav `Adoptovano` na `Ano` a uloží dnešní datum jako `DatumAdopce`.
- Kontroluje, zda `ID` existuje a zda už zvíře nebylo adoptováno.

### 5. Statistiky útulku
- Zobrazí celkový počet zvířat, počet adoptovaných a kolik jich v útulku aktuálně zbývá.


Jak spusit? 
    1. otevřít projekt v visual studio 2022
    2. spustit Utulek.sln


Rozdělení rolí
-  Martin Bucňák        -  Branch merging a oprava projektu a sepsání README
-  Lubomír Karpíšek     -  kód Evidence.cs a Program.cs
-  Alexandra Černovská  -  kód Zvire.cs
-  Terka Dufková        -  kód KonzoleUI.cs


