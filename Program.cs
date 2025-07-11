using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Eiger_filewriter_Console_app_01
{
    internal class Program
    {
        private static HttpClient eiger_client = new HttpClient();
        static async Task Main(string[] args)
        {
            //  query number of images, ni
            Console.WriteLine("Define number of images: ");
            // read input for number of images
            string ni_str = Console.ReadLine();
            // convert string to integer
            int ni = int.Parse(ni_str);
            Console.WriteLine(ni);
            Dictionary<string, int> ni_dict = new Dictionary<string, int>
            {
                { "value", ni }
            };
            // translate dictionary to json
            string json_ni = JsonConvert.SerializeObject(ni_dict, Formatting.Indented);
            var content_ni = new StringContent(json_ni, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await eiger_client.PutAsync("http://10.10.10.31/detector/api/1.8.0/config/nimages", content_ni);
            Console.WriteLine(response.StatusCode);

            //  query exposure time or count time, expt
            Console.WriteLine("Define count time or exposure time: ");
            // read input for count time
            string count_time_str = Console.ReadLine();
            // convert string to integer
            float count_time = float.Parse(count_time_str);
            Console.WriteLine(count_time);
            Dictionary<string, float> count_time_dict = new Dictionary<string, float>
            {
                { "value", count_time }
            };
            // translate dictionary to json
            string json_count_time = JsonConvert.SerializeObject(count_time_dict, Formatting.Indented);
            var content_count_time = new StringContent(json_count_time, Encoding.UTF8, "application/json");
            response = await eiger_client.PutAsync("http://10.10.10.31/detector/api/1.8.0/config/count_time", content_count_time);
            Console.WriteLine(response.StatusCode);

            //  query frame time or exposure period, expp
            Console.WriteLine("Define frame time or exposure period: ");
            // read input for frame time
            string frame_time_str = Console.ReadLine();
            // convert string to integer
            float frame_time = float.Parse(frame_time_str);
            Console.WriteLine(frame_time);
            Dictionary<string, float> frame_time_dict = new Dictionary<string, float>
            {
                { "value", frame_time }
            };
            // translate dictionary to json
            string json_frame_time = JsonConvert.SerializeObject(frame_time_dict, Formatting.Indented);
            var content_frame_time = new StringContent(json_frame_time, Encoding.UTF8, "application/json");
            response = await eiger_client.PutAsync("http://10.10.10.31/detector/api/1.8.0/config/frame_time", content_frame_time);
            Console.WriteLine(response.StatusCode);

            //  query energy threshold
            Console.WriteLine("Define energy threshold: ");
            // read input for energy threshold
            string energy_str = Console.ReadLine();
            // convert string to integer
            float energy = float.Parse(energy_str);
            Console.WriteLine(energy);
            Dictionary<string, float> energy_dict = new Dictionary<string, float>
            {
                { "value", energy }
            };
            // translate dictionary to json
            string json_energy = JsonConvert.SerializeObject(energy_dict, Formatting.Indented);
            var content_energy = new StringContent(json_energy, Encoding.UTF8, "application/json");
            response = await eiger_client.PutAsync("http://10.10.10.31/detector/api/1.8.0/config/photon_energy", content_energy);
            Console.WriteLine(response.StatusCode);

            Console.WriteLine("Configuration of acquition - passed");

            // adjust buffer size in configuration
            Dictionary<string, string> buffer_size_dict = new Dictionary<string, string>
            {
                { "value", "test" }
            };
            // translate dictionary to json
            string json_buffer_size = JsonConvert.SerializeObject(buffer_size_dict, Formatting.Indented);
            var content_buffer_size = new StringContent(json_buffer_size, Encoding.UTF8, "application/json");
            response = await eiger_client.PutAsync("http://10.10.10.31/filewriter/api/1.8.0/config/buffer_size", content_buffer_size);
            Console.WriteLine(response.StatusCode);

            // adjust nimages per file in configuration
            Dictionary<string, int> nimages_per_file_dict = new Dictionary<string, int>
            {
                { "value", 10000 }
            };
            // translate dictionary to json
            string json_nimages_per_file = JsonConvert.SerializeObject(nimages_per_file_dict, Formatting.Indented);
            var content_nimages_per_file = new StringContent(json_nimages_per_file, Encoding.UTF8, "application/json");
            response = await eiger_client.PutAsync("http://10.10.10.31/filewriter/api/1.8.0/config/nimages_per_file", content_nimages_per_file);
            Console.WriteLine(response.StatusCode);

            // adjust compression enabled in configuration
            Dictionary<string, bool> compression_enabled_dict = new Dictionary<string, bool>
            {
                { "value", true }
            };
            // translate dictionary to json
            string json_compression_enabled = JsonConvert.SerializeObject(compression_enabled_dict, Formatting.Indented);
            var content_compression_enabled = new StringContent(json_compression_enabled, Encoding.UTF8, "application/json");
            response = await eiger_client.PutAsync("http://10.10.10.31/filewriter/api/1.8.0/config/compression_enabled", content_compression_enabled);
            Console.WriteLine(response.StatusCode);

            // adjust mode in configuration
            Dictionary<string, string> mode_dict = new Dictionary<string, string>
            {
                { "value", "enabled" }
            };
            // translate dictionary to json
            string json_mode = JsonConvert.SerializeObject(mode_dict, Formatting.Indented);
            var content_mode = new StringContent(json_mode, Encoding.UTF8, "application/json");
            response = await eiger_client.PutAsync("http://10.10.10.31/filewriter/api/1.8.0/config/mode", content_mode);
            Console.WriteLine(response.StatusCode);

            Console.WriteLine("Configuration of filewriter - passed");

            // do arming
            Dictionary<string, string> arm_dict = new Dictionary<string, string>();
            string json_arm = JsonConvert.SerializeObject(arm_dict, Formatting.Indented);
            var arm_content = new StringContent(json_arm, Encoding.UTF8, "application/json");
            response = await eiger_client.PutAsync("http://10.10.10.31/detector/api/1.8.0/command/arm", arm_content);
            Console.WriteLine(response.StatusCode);
            Console.WriteLine("Arming.");
            // do triggering
            Dictionary<string, string> trigger_dict = new Dictionary<string, string>();
            string json_trigger = JsonConvert.SerializeObject(trigger_dict, Formatting.Indented);
            var trigger_content = new StringContent(json_trigger, Encoding.UTF8, "application/json");
            response = await eiger_client.PutAsync("http://10.10.10.31/detector/api/1.8.0/command/trigger", trigger_content);
            Console.WriteLine(response.StatusCode);
            Console.WriteLine("Acquition started");
            Console.WriteLine("Checking buffer?");

            float wait_delay = (float)ni * frame_time * 1000f + 1000f;
            int wait_time = (int)wait_delay;
            Thread.Sleep(wait_time);
        }
    }
}
