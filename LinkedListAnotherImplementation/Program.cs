using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedListAnotherImplementation
{
    class Program
    {
        static void Main(string[] args)
        {


            //IEnumerable<int> t = new List<int> { 12, 2, 3 };
            LinkedList_<int> temp = new LinkedList_<int>();

            for(int i = 0; i < 10; i++)
            {
                temp.addLast(i);
            }

            foreach (int i in temp)
                Console.WriteLine(i);

            temp.AddAfter(temp.head, 123);
            Console.ReadLine();
        }
    }
}
