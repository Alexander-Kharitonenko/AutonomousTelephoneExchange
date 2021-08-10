using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{

    public delegate void MessageHandlerTariffOne(object o, MobileProviderEventArgs e); //делегат обработки 

    public class TariffPlansOne
    {

        public TariffPlansOne(AutonomousTelephoneExchange ATC)
        {
            this.ATC = ATC;
        }

 
        public event MessageHandlerTariffOne Notify;

        public AutonomousTelephoneExchange ATC;

        public List<(uint, string)> InformationCall = new List<(uint, string)>(); // информация о звоках



        public uint СostMinutesTariff1(Client client) // стоимость тариф за месяц
        {
            uint result = 0;
            if (client != null)
            {
                for (int i = 0; i < 30; i++)
                {
                    InformationCall.Add(ATC.CallDateTime());

                }
                InformationCall.OrderBy(i => i.Item1);
            }
            else
            {

                Notify?.Invoke(this, new MobileProviderEventArgs("Specify a client"));

            }

            if (client != null)
            {
                for (int i = 0; i < 30; i++)
                {
                    result += InformationCall[i].Item1;
                }

                result *= 4;
            }
            else
            {

                Notify?.Invoke(this, new MobileProviderEventArgs("Specify a client"));

            }

            Console.WriteLine($"{client.Name} cost of calls per month - {result}P");
            return result;
        }

        public  void PaymentOfTariff1(Client client)//оплата тарифа
        {

            uint prise = СostMinutesTariff1(client);
            if (prise != 0)
            {
                Console.WriteLine($"cost of calls equally {prise} want to pay Y - Yes N - No ?");
                string Key = Console.ReadLine();
                try
                {
                    if (Key == "Y")
                    {
                        prise = 0;
                        Console.WriteLine("calls payment");

                    }
                    else if (Key == "N")
                    {
                        Notify?.Invoke(this, new MobileProviderEventArgs($"you refused to pay calls you owe a debt {prise}P"));
                        Console.WriteLine();
                        PaymentOfTariff1(client);
                    }
                    else
                    {
                        PaymentOfTariff1(client);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }

        }

        public void GetAllCalls(Client client) // выводт всех звонков 
        {

            СostMinutesTariff1(client);

            foreach (var i in InformationCall)
            {
                Console.WriteLine($"{client.Name} call date { i.Item2} - duration {i.Item1} minutes ");
            }
        }

        public void Message(object o, MobileProviderEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

    }
}
