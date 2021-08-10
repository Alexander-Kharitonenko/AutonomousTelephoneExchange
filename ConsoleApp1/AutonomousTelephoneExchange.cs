using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    public delegate ulong AssignsNumberClient();//делегат генератора номеров

    public delegate void MessageHandlerAutonomousTelephoneExchange(object o, MobileProviderEventArgs e); //делегат обработки 


    public class AutonomousTelephoneExchange
    {

        AssignsNumberClient numberClient;

        public event MessageHandler Notify;

        private ulong Numbet { get; set; } // номер 

        public byte Minutes { get; set; }
        public string DateCall { get; set; }


        public Dictionary<Client, ulong> Clients = new Dictionary<Client, ulong>(); // список клиенто



        private ulong GetNumber() // генератор номеров
        {

            ulong RegionТumber = 37529;
            Random NumberGeneration = new Random();
            Numbet = ulong.Parse(RegionТumber.ToString() + NumberGeneration.Next(1000000, 8000000).ToString());
            return Numbet;

        }

        public void SignContract(Client person) // Заключаем контракт
        {


            if (Clients.All(i => i.Key.Name != person.Name & person.Age >= 18 & person != null))
            {

                numberClient += GetNumber;
                Clients.Add(person, numberClient());
                Notify?.Invoke(this, new MobileProviderEventArgs($"The contract with the client has been successfully concluded"));


            }
            else
            {

                Notify?.Invoke(this, new MobileProviderEventArgs($"The contract with the client has been successfully concluded"));

            }


        }

        public (uint, string) CallDateTime()//длительность звонка и дата
        {

            (uint, string) Result = (0, null);


            Random GetNumber = new Random();
            Minutes = (byte)GetNumber.Next(1, 61);

            DateTime GetDateCall = new DateTime(2020, new Random().Next(1, 13), new Random().Next(1, 28) + 1, new Random().Next(1, 12), new Random().Next(1, 55), new Random().Next(1, 55));
            DateCall = GetDateCall.ToString();

            Result = (Minutes, DateCall);



            return Result;
        }

        public void Message(object o, MobileProviderEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
