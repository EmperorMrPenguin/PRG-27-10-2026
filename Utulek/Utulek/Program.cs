using Utulek.Services;
using Utulek.UI;

namespace Utulek
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Evidence evidence = new Evidence();
            KonzoleUI menu = new KonzoleUI(evidence);
            menu.SpustMenu();
        }
    }
}