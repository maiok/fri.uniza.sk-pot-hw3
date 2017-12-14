using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSimpleService.ServiceReference1;

namespace TaskSimpleService
{
    class Program
    {
        static void Main(string[] args)
        {
            // Vytvorenie instancie proxy triedy, pomocou ktorej mozeme volat vzdialene metody
            var client = new SimpleServiceClient();

            // Zavolame vzdialenu metodu GetData - sluzba vrati retazec
            var resultString = client.GetData(123);
            Console.WriteLine(resultString);

            var compositeType = new CompositeType {StringValue = "Volam WCF sluzbu", BoolValue = true};
            var resultCompositeType = client.GetDataUsingDataContract(compositeType);
            Console.WriteLine(resultCompositeType.StringValue);

            Console.ReadLine();

            client.Close();
        }
    }
}
