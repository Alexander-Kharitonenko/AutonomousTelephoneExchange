using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApp1
{
    
     public delegate void MessageHandlerPhone(object o , MobileProviderEventArgs e); //делегат обработки событий
 


   public class Phone<T> where T : MobileProvider  
    {

       
           
        public Phone(T SimCard, bool UseSim) 
        {
            this.SimCard = SimCard;
            this.UseSim = UseSim;
           
        }

        private readonly string Vote1 = @"C:\Users\Александр\Desktop\С#\Проект26 - ДЗ Делегаты\ConsoleApp1\ConsoleApp1\Data\Голос парня1.mp3";
        private readonly string Vote2 = @"C:\Users\Александр\Desktop\С#\Проект26 - ДЗ Делегаты\ConsoleApp1\ConsoleApp1\Data\Голос бабушки1.mp3";

       

        public bool UseSim;

        public T SimCard;

        public Dictionary<Client, ulong> ContactsInMobile = new Dictionary<Client, ulong>();

        public event MessageHandlerPhone Notify;

        




        public void AddContacts(Client client) // добавичь человека в список контактов каждый человек соответствует определённому номеру
        {


                ulong Resuly = SimCard.GetClient(client);

                if (ContactsInMobile.All(i => i.Value != Resuly))
                {

                    ContactsInMobile.Add(client, Resuly);
                    Notify?.Invoke(this, new MobileProviderEventArgs($"{client.Name} added to your contact list ")); //Евент
                }
                else
                {
                    Notify?.Invoke(this, new MobileProviderEventArgs($"Contact name { client.Name} not added to contact list ")); //Евент
                }
            
           
        }

        public void RemoveContacts(Client person) // удалить человека из списков контактов 
        {
  
            if (ContactsInMobile.Any(i => i.Key.Name == person.Name & i.Key.Surname == person.Surname))
            {
                ContactsInMobile.Remove(person);
               
            }          
            else
            {
                Notify?.Invoke(this, new MobileProviderEventArgs("The client does not exist"));
            }
            ContactsInMobile.OrderBy(i => i.Key.Name);
        }

        public ulong GetNumberContacts(Client client) //получить одного клиента из списка контактов или все записанные в телефоне номера
        {
            ulong Number;
            if (client != null & ContactsInMobile.Any(i => i.Key.Name == client.Name))
            {
                Number = ContactsInMobile[client];
                Notify?.Invoke(this, new MobileProviderEventArgs($"Name - {client.Name} Phome - {Number}"));
                ContactsInMobile.OrderBy(i => i.Key.Name);
                return Number;
            }
            else 
            {
                 Number = 0;
                Console.WriteLine($"Contact not found, here is a list of all contacts");
                foreach (var i in ContactsInMobile) 
                {
                    Notify?.Invoke(this, new MobileProviderEventArgs($"Name - {i.Key.Name} Phone - {i.Value}"));
                }
                ContactsInMobile.OrderBy(i => i.Key.Name);
                return Number;
            }
            
        }


        public void Call (Client Contact) 
        {

            ulong Resuly = SimCard.GetClient(Contact);

            if (UseSim)
            { 
                Contact.Thread1.Start(Vote1);
                Thread.Sleep(2000);
                SimCard.Atc.CallDateTime();// длительность разговора 
                Notify?.Invoke(this, new MobileProviderEventArgs($"Call in progress { SimCard.Atc.CallDateTime().Item2} subscriber {Contact.Name}"));

            }
            else
            {
                Notify?.Invoke(this, new MobileProviderEventArgs($"Connection with {Contact.Name} failed, maybe the SIM card is not included or the contract is not concluded")); //Евент
            }

            
        }

        public void Call2(Client Contact)
        {

            ulong Resuly = SimCard.GetClient(Contact);

            if (UseSim)
            {
                Contact.Thread1.Start(Vote2);
                Thread.Sleep(2000);
                SimCard.Atc.CallDateTime();// длительность разговора 
                

            }
            else
            {
                Notify?.Invoke(this, new MobileProviderEventArgs($"Connection with {Contact.Name} failed, maybe the SIM card is not included or the contract is not concluded")); //Евент
            }


        }


        public void Message(object o, MobileProviderEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }

    
}
