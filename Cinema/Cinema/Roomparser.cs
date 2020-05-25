namespace Cinema
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Newtonsoft.Json;

    
    
        class RoomParser
        {
        /// <summary>
        /// LoadRooms loads all Room rooms. This is a helper function for the LoadRooms
        /// </summary>
        /// <param name="roomJson"></param>
        /// <returns></returns>
        public static RoomV2 LoadFromLegacy(string file)
            {
                string FileContentString = System.IO.File.ReadAllText(file);
                RoomV2 DeserializedS = JsonConvert.DeserializeObject<RoomV2>(FileContentString, new RoomConverter()); // here I made instances of classes from a JSON string in a list of class movie
                return DeserializedS;
            }

        /// <summary>
        /// LoadRooms loads all RoomV2 rooms. This is a helper function LoadRooms
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static RoomV2 LoadFromJson(string filename)
            {
                string FileContentString = "";

                RoomV2 DeserializedS;
                try
                {
                    FileContentString = System.IO.File.ReadAllText(filename);
                    DeserializedS = JsonConvert.DeserializeObject<RoomV2>(FileContentString); // here I made instances of classes from a JSON string in a list of class movie
                    return DeserializedS;
                }
                catch (Exception e)
                {
                    StandardMessages.SomethingWW(e.Message);
                    return null;
                }
            }

        /// <summary>
        /// LoadRooms loads all the rooms in the directory .\rooms ending with .json
        /// </summary>
        /// <returns></returns>
        public static List<RoomV2> LoadRooms()
            {

                List<RoomV2> rooms = new List<RoomV2>();

                try
                {
                    string[] files = Directory.GetFiles(@".\rooms", "*.json");
                    foreach (string file in files)
                    {
                        // old files => room1.json
                        // Roomv2 => room6262_v2.json

                        // if conditions that chooses the suited method based on the file name 
                        RoomV2 r = (file.EndsWith(@"*_v2.json")) ? LoadFromJson(file) : LoadFromLegacy(file);

                        if (r != null)
                        {
                            rooms.Add(r);
                        }
                    }
                    return rooms;
                }
                catch (Exception e)
                {
                    StandardMessages.SomethingWW(e.Message);
                    return null;
                }
            }
        }
    }
