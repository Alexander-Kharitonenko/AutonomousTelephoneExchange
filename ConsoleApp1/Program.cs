using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
       
        public static void Main()
        {
            AutonomousTelephoneExchange ATC = new AutonomousTelephoneExchange();
            
            MobileProvider mobileProvider = new MobileProvider(ATC, new TariffPlansOne(ATC), new TariffPlansTwo(ATC) ); // в симке два тарифных плана

            ConnectionPort connectionPort = new ConnectionPort(ATC);//создаём порт подключения для звонящих 

            Phone<MobileProvider> samsung = new Phone<MobileProvider> (mobileProvider, true);// можно включать выключать симку true/false
           
            Phone<MobileProvider> IPhone11 = new Phone<MobileProvider>(mobileProvider, true);// можно включать выключать симку true/false
         
            Phone<MobileProvider> Huawei = new Phone<MobileProvider>(mobileProvider, true);// можно включать выключать симку true/false
           
            //П О Д П И С Ы В А Е М С Я   Н А   В С Е   В О З М О Ж Н Ы Е  С О Б Ы Т И Я

            ATC.Notify += ATC.Message;
            mobileProvider.Notify += mobileProvider.Message;
            mobileProvider.tariffOne.Notify += mobileProvider.tariffOne.Message;
            mobileProvider.tariffTwo.Notify += mobileProvider.tariffTwo.Message;
            connectionPort.Notify += connectionPort.Message;
            samsung.Notify += samsung.Message;
            IPhone11.Notify += IPhone11.Message;
            Huawei.Notify += Huawei.Message;


            //С О З Д А Ё М   К Л И Е Н Т О В

            Client client1 = new Client("Вася", "Пупкин", 18, samsung); 
            Client client2 = new Client("Баба Валя", "Пупкина", 18, IPhone11);
            Client client3 = new Client("Иришка", "Пупкина", 18, Huawei);

            ATC.SignContract(client1); // заключаем контракты с Вася
            ATC.SignContract(client2); // заключаем контракты с Баобой валей
            ATC.SignContract(client3); // заключаем контракты с Иришкой

            Console.WriteLine();


            client1.Phone.AddContacts(client2);// Вася добавил контакт Баба Валя
            client2.Phone.AddContacts(client1);// Баба Валя добавил контакт Вася
            client3.Phone.AddContacts(client1);// Иришка добавил контакт Вася
            client3.Phone.AddContacts(client2);// Иришка добавил контакт Баба Валя

            Console.WriteLine();

            client1.Phone.SimCard.GetMyNumber(client1);// Получить личный номер клиента
            client2.Phone.SimCard.GetMyNumber(client2);
            client3.Phone.SimCard.GetMyNumber(client3);

            Console.WriteLine();

            // Получаем все звонки первого пользователя и оплачиваем их созласно тарифу
            client1.Phone.SimCard.tariffOne.GetAllCalls(client1);
            client1.Phone.SimCard.tariffOne.PaymentOfTariff1(client1);

            client2.Phone.SimCard.tariffTwo.GetAllCalls(client2);
            client2.Phone.SimCard.tariffTwo.PaymentOfTariff2(client2);



            connectionPort.Port(client1, client2, client3);



            Console.ReadKey();
        }
    }
}
