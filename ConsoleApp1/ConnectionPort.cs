using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    public delegate void MessageHandlerPort(object o, MobileProviderEventArgs e); //делегат обработки 

    public class ConnectionPort
    {
       
        public ConnectionPort(AutonomousTelephoneExchange ATC) 
        {
            this.ATC = ATC;
        }

        public event MessageHandlerPort Notify;

        AutonomousTelephoneExchange ATC;

        public void Port(Client client1, Client client2, Client client3 = null) // порт соединения и разговора клиентов
        {

            ulong Number1;
            ulong Number2;


            if (client1 == null && client1 == null)
            {
                Notify?.Invoke(this, new MobileProviderEventArgs($"There is nothing to connect to"));
            }

            else if (client1 != null && client2 != null && client2 == null)
            {
                Number1 = ATC.Clients[client1];//получаем номера клиентов
                Number2 = ATC.Clients[client2];

                if ((ATC.Clients.Any(i => i.Value == Number1) && client1.Phone.UseSim) && (ATC.Clients.Any(i => i.Value == Number2) && client2.Phone.UseSim))//проверяем есть ли в клиенской базе номера и включена ли симка
                {
                    client1.Phone.Call(client1); // даём голос нашим клиентам 
                    client2.Phone.Call2(client2);

                    Notify?.Invoke(this, new MobileProviderEventArgs($"{client1.Name} called in { client2.Name}"));

                }
                else
                {
                    Notify?.Invoke(this, new MobileProviderEventArgs($"The subscriber you are calling is temporarily unavailable"));
                }

            }

            else if (client1 != null && client2 == null)
            {


                Notify?.Invoke(this, new MobileProviderEventArgs($"The subscriber you are calling is temporarily unavailable"));

            }

            else if (client1 == null && client2 != null)
            {


                Notify?.Invoke(this, new MobileProviderEventArgs($"The subscriber you are calling is temporarily unavailable"));

            }

            else if (client1 != null && client2 != null && client3 != null)
            {
                Number1 = ATC.Clients[client1];//получаем номера клиентов
                Number2 = ATC.Clients[client2];

                if ((ATC.Clients.Any(i => i.Value == Number1) && client1.Phone.UseSim) && (ATC.Clients.Any(i => i.Value == Number2) && client2.Phone.UseSim))//проверяем есть ли в клиенской базе номера и включена ли симка
                {
                    client1.Phone.Call(client1); // даём голос нашим клиентам 
                    client2.Phone.Call2(client2);
                    Notify?.Invoke(this, new MobileProviderEventArgs($"{client3.Name} trying to call { client1.Name} but the line is busy"));

                }
                else
                {


                    Notify?.Invoke(this, new MobileProviderEventArgs($"The subscriber you are calling is temporarily unavailable"));

                }

                Console.WriteLine($"Call ended, call duration { ATC.CallDateTime().Item1}");//Евент

            }

        }

        public void Message(object o, MobileProviderEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
