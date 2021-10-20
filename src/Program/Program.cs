using System;
using CompAndDel.Pipes;
using CompAndDel.Filters;
using TwitterUCU;

namespace CompAndDel
{
    class Program
    {
        static void Main(string[] args)
        {
            PictureProvider provider = new PictureProvider();
            IPicture picture = provider.GetPicture(@"beer.jpg");

            IPipe nulo = new PipeNull();

            IFilter negativo = new FilterNegative();

            IPipe serial = new PipeSerial(negativo, nulo);

            IFilter grises = new FilterGreyscale();

            IPipe serial2 = new PipeSerial(grises, serial);

            IPicture fotoNegativa = serial.Send(picture);
            provider.SavePicture(fotoNegativa, @"Cerveza-Negativa.jpg");

            IPicture fotoGris = serial2.Send(picture);
            provider.SavePicture(fotoGris, @"Foto-Gris.jpg");

            TwitterImage twitter = new TwitterImage();
            Console.WriteLine(twitter.PublishToTwitter("Prueba ",@"Cerveza-Negativa.jpg"));

            TwitterImage twitter2 = new TwitterImage();
            Console.WriteLine(twitter2.PublishToTwitter("Prueba 2",@"Foto-Gris.jpg"));


        }
    }
}
