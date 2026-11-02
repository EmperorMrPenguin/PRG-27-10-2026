using System.Security.Policy;

namespace Utulek
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Vytvoreni evidence zvirat
            Evidence evidence = new Evidence();

            // Vytvoreni uzivatelskeho rozhrani
            KonzoleUI menu = new KonzoleUI(evidence);

            // Spusteni hlavniho menu
            menu.SpustMenu();
        }
    }
}
