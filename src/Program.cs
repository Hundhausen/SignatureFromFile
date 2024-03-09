#region Header
//
// Copyright © 2024 - Hundhausen/SignatureFromFile (Jean-Pierre Hundhausen)
//
// This file is part of SignatureFromFile.
// This software is a free product and is hereby released under the MIT License.
//
// You should have received a copy of the MIT License
// along with this program.  If not, see <https://opensource.org/license/MIT>.
//
#endregion

using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Formatting = Newtonsoft.Json.Formatting;

namespace SignatureFromFile
{

   public class OutMessage
   {
      [JsonProperty("message")]
      public String message { get; set; }

      public OutMessage(String sMessageIn)
      {
         message = sMessageIn;
      }
   }

   internal static class Program
   {
      private static JObject Read()
      {
         Stream stdin = Console.OpenStandardInput();
         Int32 length;

         Byte[] lengthBytes = new Byte[4];
         stdin.Read(lengthBytes, 0, 4);
         length = BitConverter.ToInt32(lengthBytes, 0);

         Char[] buffer = new Char[length];
         using (StreamReader reader = new StreamReader(stdin))
         {
            while (reader.Peek() >= 0)
            {
               reader.Read(buffer, 0, buffer.Length);
            }
         }
         return JsonConvert.DeserializeObject<JObject>(new String(buffer));
      }

      static void SendMessage(String json)
      {
         Byte[] bytes = System.Text.Encoding.UTF8.GetBytes(json);

         Stream stdout = Console.OpenStandardOutput();
         stdout.WriteByte((Byte)((bytes.Length >> 0) & 0xFF));
         stdout.WriteByte((Byte)((bytes.Length >> 8) & 0xFF));
         stdout.WriteByte((Byte)((bytes.Length >> 16) & 0xFF));
         stdout.WriteByte((Byte)((bytes.Length >> 24) & 0xFF));
         stdout.Write(bytes, 0, bytes.Length);
         stdout.Flush();
      }

      static void SendNullMessage()
      {
         SendMessage(JsonConvert.SerializeObject(new OutMessage("err"), Formatting.None));
      }

      static void Main(String[] args)
      {
         JObject inMessage = Read();
         JToken token = inMessage["tag"];

         if (token is null)
         {
            SendNullMessage();
            return;
         }

         String fileContent;
         try
         {
            FileInfo file = new FileInfo(token.ToString());
            if (!file.Exists)
            {
               SendNullMessage();
               return;
            }

            fileContent = File.ReadAllText(file.FullName);
         }
         catch (Exception)
         {
            SendNullMessage();
            return;
         }

         SendMessage(JsonConvert.SerializeObject(new OutMessage(fileContent), Formatting.None));
      }
   }
}

//
// Copyright © 2024 - Hundhausen/SignatureFromFile (Jean-Pierre Hundhausen)
//