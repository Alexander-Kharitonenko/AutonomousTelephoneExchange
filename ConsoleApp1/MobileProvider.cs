using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApp1
{

    

    public delegate void MessageHandler(object o, MobileProviderEventArgs e); //делегат обработки 


    public class MobileProvider  //предоставляет порты для подключения , выдаёт кажому клиенту номер , заключить договор
    {
       public MobileProvider(AutonomousTelephoneExchange Atc, TariffPlansOne tariffOne, TariffPlansTwo tariffTwo) 
       {
            this.Atc = Atc;
            this.tariffOne = tariffOne;
            this.tariffTwo = tariffTwo;
       }

        

        public event MessageHandler Notify;

        public AutonomousTelephoneExchange Atc;

        public TariffPlansOne tariffOne;

        public TariffPlansTwo tariffTwo;

        
        public ulong GetMyNumber(Client client) 
        {
                   
            return GetClient(client);
        }


        public ulong GetClient(Client person) // Ищим клиента в сети
        {
           
            if (Atc.Clients.Any( i => i.Key == person) && person != null)
            {
                return Atc.Clients[person];

            }
            else
            {

                Notify?.Invoke(this, new MobileProviderEventArgs($"No contract with this client"));
                return 0;
               

            }
          
        }


        public void Port(Client client1, Client client2) // порт соединения и разговора клиентов
        {

            

        }

        public void Message(object o, MobileProviderEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
