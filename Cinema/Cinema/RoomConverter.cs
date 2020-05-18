namespace Cinema
{
    using System;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class RoomConverter : JsonConverter
    {
        public RoomConverter()
        {

        }

        /// <summary>
        /// No custom json (serializer) converter needed
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// ReadJson from the old json, to roomV2; creates the room (the list of seats and initializes the seats with props)
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            var obj = JObject.Load(reader);

            var mapLayout = (JArray)obj["layout"];
            var mapVacancy = (JArray)obj["vacancy"];

            //fixes edge case scenario where those 2 would differ in size!
            if (mapLayout.Count != mapVacancy.Count)
                throw new InvalidOperationException("data and columns must contain the same number of elements");


            RoomV2 room = new RoomV2();



            // mapVacancy[row] =>  "001111111100"
            // mapLayout[row]  =>  "001100011100"
            // 
            // mapLayout[row]  =>  "00[1]111111100" seat=2 ==> "1" 


            //"001111111100", [row]
            //"011111111110", [row+1]
            //"011111111110", [row+2]
            //"111112211111",
            //"111122221111",
            //"112223322211",
            //"112233332211",
            //"112223322211",
            //"111122221111",
            //"111112211111",
            //"011111111110",
            //"001111111100",
            //"001111111100",
            //"001111111100"


            for (int row = 0; row < mapLayout.Count; row++)
            {
                string rowValueLayout = mapLayout[row].ToString();
                string rowValueVacancy = mapVacancy[row].ToString();

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

                    switch (rowValueVacancy[seat])
                    {
                        case '0':
                            seatv2.vacant = false;
                            break;
                        case '1':
                            seatv2.vacant = true;
                            break;
                        default:
                            seatv2.vacant = true;
                            break;
                    }

                    room.seats.Add(seatv2);
                }
            }
            return room;
        }


        /// <summary>
        /// CanConvert returns a bool. Always returns true, as we want to convert old room to new room without original json type validation
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return true;
            // return objectType == typeof(RoomV2);
        }

    }
}