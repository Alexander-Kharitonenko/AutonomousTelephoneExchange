using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ConsoleApp1
{
    public class Client 
    {

        public Thread Thread1 = new Thread(new ParameterizedThreadStart(CustomerVoice));
        public Thread Thread2 = new Thread(new ParameterizedThreadStart(CustomerVoice));


        public Client(string Name, string Surname, byte Age , Phone<MobileProvider> phone) 
        {
            this.Name = Name;
            this.Surname = Surname;
            this.Age = Age;
            this.Phone = phone;
            
        }

        public string Name;

        public string Surname;

        public byte Age;
   

        public Phone<MobileProvider> Phone;
        



        public static void CustomerVoice(object Vote) //голос клиента
        {
            try
            {
                string vote = (string)Vote;
                var reader = new Mp3FileReader(vote);
                var waveOut = new WaveOutEvent(); // or WaveOutEvent()
                waveOut.Init(reader);
                waveOut.Play();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            

        }


    }
}
