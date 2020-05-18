namespace Cinema
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    //todo OPEN     Standard message class met enkel static void methodes.
    //todo OPEN     Het programma kan bij startup lezen of er een .txt-bestand in een
    //bepaalde map is, en de data in dit bestand omzetten naar een JSON-bestand;
    //zo kunnen nieuwe zalen worden toegevoegd
    //todo OPEN     De klant moet meerdere stoelen in een rij kunnen selecteren
    //todo DONE     Als je een kamer aan maak checkt hij niet meer of je input gelijk is
    //aan de de lengte van row. dus je kan een 5x5 zaal aanmaken met: 1 1 1 1 1 Wat niet hoort te kunnen.
    //todo TEST     als je de x,y coordinaat in vul dan crashed hij bij een lege input, ook bij
    //een input die te groot is.
    //Hier moet een try chatch of een handler om heen.
    //todo TEST     Onduidelijkheid over de x en y coordinaten
    //todo TEST     Foolproof maken van het bestellen van een kaartje
    //todo TEST     Coördinaat invoer bij het bestellen van een kaartje kan bugy zijn boven een
    //coördinaat dat 10 is
    //todo OPEN     Zalen worden opgeslagen in .jsons na het bestellen van een kaartje (willen
    //we een "weet je zeker dat je deze plekken wilt bestellen?" doen?)

    class RoomV2
    {
        public RoomV2()
        {
            seats = new List<SeatV2>();
        }

        public int NumberOfChairs
        {
            get => seats.Count;
        }

        public int roomtype { get; set; }

        public List<SeatV2> seats { get; set; }

        public int AmountOfRows
        {
            get => seats.Select(s => s.rowNumber).Distinct().Count();
        }

        public int Number { get; set; }

        public string Name { get; set; }

        public void CreateFromLayout(string[] mapLayout, string name = null)
        {
            this.Name = name;
            for (int row = 0; row < mapLayout.Length; row++)
            {
                string rowValueLayout = mapLayout[row].ToString();
                
                for (int seat = 0; seat < rowValueLayout.Length; seat++)
                {
                    SeatV2 seatv2 = new SeatV2()
                                    {
                                        seatNumber = seat,
                                        rowNumber = row,
                                    };


                    // switch 1 - get price category per chair in row
                    // switch 2 - get vacant per chair in row

                    switch (rowValueLayout[seat])
                    {
                        case '0':
                            seatv2.priceCategory = 0;
                            break;
                        case '1':
                            seatv2.priceCategory = 1;
                            break;
                        case '2':
                            seatv2.priceCategory = 2;
                            break;
                        case '3':
                            seatv2.priceCategory = 3;
                            break;
                        default:
                            seatv2.priceCategory = 0;
                            break;
                    }

                    seats.Add(seatv2);
                }
            }
        }

        public void SaveRoom(string fileName)
        {
            try
            {
                File.WriteAllText(@$"rooms\{fileName}.json", JsonConvert.SerializeObject(this, Formatting.Indented));
            }
            catch (Exception e)
            {
                StandardMessages.SomethingWW(e.Message);
            }
        }
        // rooms\test1\.json
        //public string SerializeJson(object objToSave)
        //{
        //    return JsonConvert.SerializeObject(objToSave);
        //    //System.IO.File.WriteAllText(Constants.bookbackup1, content);
        //}


        //public void Create(string backupName = Constants.bookbackup1)
        //{
        //    int timestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        //    DataOperator.Instance.WriteToFile(DataOperator.Instance.SerializeJson(Catalog.Instance.GetBookList()), backupName);
        //}

        //public void WriteToFile(string content, string filepath)
        //{
        //    try
        //    {
        //        Directory.CreateDirectory("Backups");

        //        System.IO.File.WriteAllText(filepath, content);
        //    }
        //    catch (Exception)
        //    {
        //        StandardMessages.FilePathError(filepath);
        //    }

        //}





        //public void PrintAvailableSeats()
        //{
        //    for (uint rowNumber = 1; rowNumber < AmountOfRows; rowNumber++)
        //    {
        //        Console.Write($"Row[{rowNumber:D2}| {seats.Count(s => s.rowNumber == rowNumber)}]:\t");

        //        foreach (SeatV2 seatV2 in seats.Where(s => s.rowNumber == rowNumber))
        //        {
        //            //Console.Write($"[{seatV2.priceCategory}]");
        //            Console.Write($"[{(seatV2.vacant ? "-" : "x")}]");
        //        }

        //        Console.WriteLine("");


        //    }
        //}

        //public void PrintRoom()
        //{
        //    //
        //    for (uint rowNumber = 1; rowNumber < AmountOfRows; rowNumber++)
        //    {
        //        Console.Write($"Row[{rowNumber:D2}| {seats.Count(s => s.rowNumber == rowNumber)}]:\t");

        //        foreach (SeatV2 seatV2 in seats.Where(s => s.rowNumber == rowNumber))
        //        {
        //            Console.Write($"[{seatV2.priceCategory}]");
        //           // Console.Write($"[{(seatV2.vacant ? "-" : "x")}]");
        //        }

        //        Console.WriteLine("");


        //    }
        //}

        //todo  switch true, 
    }
}